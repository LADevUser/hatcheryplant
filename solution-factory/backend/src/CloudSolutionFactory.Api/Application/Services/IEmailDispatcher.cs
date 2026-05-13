namespace CloudSolutionFactory.Api.Application.Services;

public interface IEmailDispatcher
{
    Task QueueVerificationEmailAsync(string email, string verificationToken, DateTimeOffset expiresAtUtc, CancellationToken ct);
}
