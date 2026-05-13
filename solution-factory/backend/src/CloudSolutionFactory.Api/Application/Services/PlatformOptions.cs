namespace CloudSolutionFactory.Api.Application.Services;

public sealed class PlatformOptions
{
    public const string SectionName = "CloudSolutionFactory";
    public int EmailVerificationTokenHours { get; init; } = 24;
    public bool AllowMockSocialAuth { get; init; } = true;
}
