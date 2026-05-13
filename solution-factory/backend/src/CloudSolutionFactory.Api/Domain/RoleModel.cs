namespace CloudSolutionFactory.Api.Domain;

public sealed class RoleModel
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Name { get; init; }
}
