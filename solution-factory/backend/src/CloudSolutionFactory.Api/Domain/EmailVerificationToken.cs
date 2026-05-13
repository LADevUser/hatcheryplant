namespace CloudSolutionFactory.Api.Domain;

public sealed class EmailVerificationToken
{
    public required string Token { get; init; }
    public required Guid UserId { get; init; }
    public DateTimeOffset ExpiresAtUtc { get; init; }
    public bool IsConsumed { get; set; }
}
