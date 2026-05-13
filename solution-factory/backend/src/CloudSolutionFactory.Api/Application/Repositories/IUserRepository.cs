using CloudSolutionFactory.Api.Domain;

namespace CloudSolutionFactory.Api.Application.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(Guid tenantId, string email, CancellationToken ct);
    Task<User?> GetByIdAsync(Guid userId, CancellationToken ct);
    Task AddAsync(User user, CancellationToken ct);
    Task UpdateAsync(User user, CancellationToken ct);
}
