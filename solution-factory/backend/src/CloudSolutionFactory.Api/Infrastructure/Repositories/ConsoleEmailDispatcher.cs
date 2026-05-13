using CloudSolutionFactory.Api.Application.Services;

namespace CloudSolutionFactory.Api.Infrastructure.Repositories;

public sealed class ConsoleEmailDispatcher : IEmailDispatcher
{
    private readonly ILogger<ConsoleEmailDispatcher> _logger;

    public ConsoleEmailDispatcher(ILogger<ConsoleEmailDispatcher> logger)
    {
        _logger = logger;
    }

    public Task QueueVerificationEmailAsync(string email, string verificationToken, DateTimeOffset expiresAtUtc, CancellationToken ct)
    {
        _logger.LogInformation(
            "[MockEmail] Verification email queued for {Email}. Token={Token}, Expires={ExpiresAtUtc}",
            email,
            verificationToken,
            expiresAtUtc);
        return Task.CompletedTask;
    }
}
