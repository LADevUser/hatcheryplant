namespace CloudSolutionFactory.Api.Application.Services;

public sealed class SessionService
{
    private readonly Dictionary<string, (Guid UserId, Guid TenantId)> _sessions = new();

    public string CreateSession(Guid userId, Guid tenantId)
    {
        var token = Guid.NewGuid().ToString("N");
        _sessions[token] = (userId, tenantId);
        return token;
    }

    public (Guid UserId, Guid TenantId)? GetSession(string token)
        => _sessions.TryGetValue(token, out var value) ? value : null;
}
