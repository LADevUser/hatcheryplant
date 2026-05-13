using CloudSolutionFactory.Api.Domain;

namespace CloudSolutionFactory.Api.Application.Repositories;

public interface IEmailVerificationTokenRepository
{
    Task AddAsync(EmailVerificationToken token, CancellationToken ct);
    Task<EmailVerificationToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task InvalidateForUserAsync(Guid userId, CancellationToken ct);
    Task UpdateAsync(EmailVerificationToken token, CancellationToken ct);
}
