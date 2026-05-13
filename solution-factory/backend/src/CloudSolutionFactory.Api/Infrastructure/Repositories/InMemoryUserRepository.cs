using CloudSolutionFactory.Api.Application.Repositories;
using CloudSolutionFactory.Api.Domain;

namespace CloudSolutionFactory.Api.Infrastructure.Repositories;

public sealed class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task<User?> GetByEmailAsync(string tenantId, string email, CancellationToken ct)
        => Task.FromResult(_users.FirstOrDefault(u => u.TenantId == tenantId && u.Email == email.Trim().ToLowerInvariant()));

    public Task<User?> GetByIdAsync(Guid userId, CancellationToken ct)
        => Task.FromResult(_users.FirstOrDefault(u => u.Id == userId));

    public Task AddAsync(User user, CancellationToken ct)
    {
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken ct) => Task.CompletedTask;
}
