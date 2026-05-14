namespace CloudSolutionFactory.Api.Domain;

public sealed class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required Guid TenantId { get; init; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public UserStatus Status { get; set; } = UserStatus.PendingEmailVerification;
    public AuthProvider AuthProvider { get; set; } = AuthProvider.EmailPassword;
    public Role Role { get; set; } = Role.Member;
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
