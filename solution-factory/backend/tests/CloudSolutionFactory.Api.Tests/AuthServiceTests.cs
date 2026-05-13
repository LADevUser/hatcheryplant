using CloudSolutionFactory.Api.Application.DTOs;
using CloudSolutionFactory.Api.Application.Services;
using CloudSolutionFactory.Api.Domain;
using CloudSolutionFactory.Api.Infrastructure.Repositories;
using FluentAssertions;

namespace CloudSolutionFactory.Api.Tests;

public class AuthServiceTests
{
    private const string TenantId = "00000000-0000-0000-0000-000000000001";

    private static (AuthService auth, InMemoryUserRepository users, InMemoryEmailVerificationTokenRepository tokens) BuildSut()
    {
        var users = new InMemoryUserRepository();
        var tokens = new InMemoryEmailVerificationTokenRepository();
        var session = new SessionService();
        return (new AuthService(users, tokens, session), users, tokens);
    }

    [Fact]
    public async Task RegisterEmail_CreatesPendingUser_And24hToken()
    {
        var (auth, users, tokens) = BuildSut();
        var before = DateTimeOffset.UtcNow;

        var result = await auth.RegisterEmailAsync(new RegisterEmailRequest(TenantId, "test@example.com", "pass123"), CancellationToken.None);

        result.VerificationLink.Should().Contain("/auth/verify-email?token=");
        var user = await users.GetByEmailAsync(Guid.Parse(TenantId), "test@example.com", CancellationToken.None);
        user.Should().NotBeNull();
        user!.Status.Should().Be(UserStatus.PendingEmailVerification);

        var tokenValue = result.VerificationLink.Split("token=")[1];
        var token = await tokens.GetByTokenAsync(tokenValue, CancellationToken.None);
        token.Should().NotBeNull();
        token!.ExpiresAtUtc.Should().BeAfter(before.AddHours(23.9));
        token.ExpiresAtUtc.Should().BeBefore(before.AddHours(24.1));
    }

    [Fact]
    public async Task VerifyEmail_ActivatesUser_AndLoginRulesApply()
    {
        var (auth, _, _) = BuildSut();
        var register = await auth.RegisterEmailAsync(new RegisterEmailRequest(TenantId, "demo@example.com", "pass123"), CancellationToken.None);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            auth.LoginEmailAsync(new LoginEmailRequest(TenantId, "demo@example.com", "pass123"), CancellationToken.None));
        ex.Message.Should().Contain("not active");

        var token = register.VerificationLink.Split("token=")[1];
        var verified = await auth.VerifyEmailAsync(token, CancellationToken.None);
        verified.Success.Should().BeTrue();

        var login = await auth.LoginEmailAsync(new LoginEmailRequest(TenantId, "demo@example.com", "pass123"), CancellationToken.None);
        login.AccessToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ResendVerification_InvalidatesOldToken_AndCreatesNewToken()
    {
        var (auth, _, tokens) = BuildSut();
        var register = await auth.RegisterEmailAsync(new RegisterEmailRequest(TenantId, "resend@example.com", "pass123"), CancellationToken.None);
        var oldToken = register.VerificationLink.Split("token=")[1];

        var resend = await auth.ResendVerificationAsync(new ResendVerificationRequest(TenantId, "resend@example.com"), CancellationToken.None);
        var newToken = resend.VerificationLink!.Split("token=")[1];

        newToken.Should().NotBe(oldToken);
        var oldTokenEntity = await tokens.GetByTokenAsync(oldToken, CancellationToken.None);
        oldTokenEntity!.IsConsumed.Should().BeTrue();
    }
}
