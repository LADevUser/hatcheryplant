namespace CloudSolutionFactory.Api.Domain;

public sealed class TenantWorkspace
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
