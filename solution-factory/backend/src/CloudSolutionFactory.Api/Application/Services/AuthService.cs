using System.Security.Cryptography;
using System.Text;
using CloudSolutionFactory.Api.Application.DTOs;
using CloudSolutionFactory.Api.Application.Repositories;
using CloudSolutionFactory.Api.Domain;

namespace CloudSolutionFactory.Api.Application.Services;

public sealed class AuthService
{
    private readonly IUserRepository _users;
    private readonly IEmailVerificationTokenRepository _tokens;
    private readonly SessionService _sessions;
    private readonly IEmailDispatcher _emailDispatcher;
    private readonly PlatformOptions _options;

    public AuthService(IUserRepository users, IEmailVerificationTokenRepository tokens, SessionService sessions, IEmailDispatcher emailDispatcher, PlatformOptions options)
    {
        _users = users;
        _tokens = tokens;
        _sessions = sessions;
        _emailDispatcher = emailDispatcher;
        _options = options;
    }

    public async Task<RegisterEmailResponse> RegisterEmailAsync(RegisterEmailRequest request, CancellationToken ct)
    {
        var existing = await _users.GetByEmailAsync(request.TenantId, request.Email, ct);
        if (existing is not null) throw new InvalidOperationException("User already exists.");

        var user = new User
        {
            TenantId = request.TenantId,
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = Hash(request.Password),
            Status = UserStatus.PendingEmailVerification,
            AuthProvider = AuthProvider.EmailPassword,
            Role = PlatformRole.Member
        };

        await _users.AddAsync(user, ct);
        var token = await IssueVerificationTokenAsync(user.Id, ct);
        await _emailDispatcher.QueueVerificationEmailAsync(user.Email, token.Token, token.ExpiresAtUtc, ct);
        return new RegisterEmailResponse("Registration successful. Please verify your email.", token.Token[..12]);
    }

    public async Task<AuthResponse> LoginEmailAsync(LoginEmailRequest request, CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(request.TenantId, request.Email, ct)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (user.PasswordHash != Hash(request.Password)) throw new UnauthorizedAccessException("Invalid credentials.");
        if (user.Status != UserStatus.Active) throw new InvalidOperationException("Account not active.");

        var accessToken = _sessions.CreateSession(user.Id, user.TenantId);
        return new AuthResponse("Login successful.", accessToken);
    }

    public async Task<VerifyEmailResponse> VerifyEmailAsync(string token, CancellationToken ct)
    {
        var item = await _tokens.GetByTokenAsync(token, ct);
        if (item is null || item.IsConsumed || item.ExpiresAtUtc < DateTimeOffset.UtcNow)
            return new VerifyEmailResponse("Token is invalid or expired.", false);

        var user = await _users.GetByIdAsync(item.UserId, ct);
        if (user is null) return new VerifyEmailResponse("User not found.", false);

        user.Status = UserStatus.Active;
        item.IsConsumed = true;

        await _users.UpdateAsync(user, ct);
        await _tokens.UpdateAsync(item, ct);

        return new VerifyEmailResponse("Email verification successful.", true);
    }

    public async Task<AuthResponse> ResendVerificationAsync(ResendVerificationRequest request, CancellationToken ct)
    {
        var user = await _users.GetByEmailAsync(request.TenantId, request.Email, ct)
            ?? throw new KeyNotFoundException("User not found.");

        if (user.Status == UserStatus.Active) return new AuthResponse("Account already active.");
        var token = await IssueVerificationTokenAsync(user.Id, ct);
        await _emailDispatcher.QueueVerificationEmailAsync(user.Email, token.Token, token.ExpiresAtUtc, ct);
        return new AuthResponse($"Verification email queued. Token preview: {token.Token[..12]}");
    }

    private async Task<EmailVerificationToken> IssueVerificationTokenAsync(Guid userId, CancellationToken ct)
    {
        await _tokens.InvalidateForUserAsync(userId, ct);
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(24));
        var model = new EmailVerificationToken
        {
            Token = token,
            UserId = userId,
            ExpiresAtUtc = DateTimeOffset.UtcNow.AddHours(_options.EmailVerificationTokenHours)
        };
        await _tokens.AddAsync(model, ct);
        return model;
    }

    private static string Hash(string input)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash);
    }
}
