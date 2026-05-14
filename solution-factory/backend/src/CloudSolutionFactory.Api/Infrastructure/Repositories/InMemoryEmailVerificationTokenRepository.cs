using CloudSolutionFactory.Api.Application.Repositories;
using CloudSolutionFactory.Api.Domain;

namespace CloudSolutionFactory.Api.Infrastructure.Repositories;

public sealed class InMemoryEmailVerificationTokenRepository : IEmailVerificationTokenRepository
{
    private readonly List<EmailVerificationToken> _tokens = new();

    public Task AddAsync(EmailVerificationToken token, CancellationToken ct)
    {
        _tokens.Add(token);
        return Task.CompletedTask;
    }

    public Task<EmailVerificationToken?> GetByTokenAsync(string token, CancellationToken ct)
    => Task.FromResult(_tokens.FirstOrDefault(t => t.Token == token));

    public Task InvalidateForUserAsync(Guid userId, CancellationToken ct)
    {
        foreach (var token in _tokens.Where(t => t.UserId == userId && !t.IsConsumed)) token.IsConsumed = true;
        return Task.CompletedTask;
    }

    public Task UpdateAsync(EmailVerificationToken token, CancellationToken ct) => Task.CompletedTask;
}
