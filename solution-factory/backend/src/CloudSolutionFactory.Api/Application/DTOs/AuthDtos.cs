namespace CloudSolutionFactory.Api.Application.DTOs;

public sealed record RegisterEmailRequest(string TenantId, string Email, string Password);
public sealed record LoginEmailRequest(string TenantId, string Email, string Password);
public sealed record ResendVerificationRequest(string TenantId, string Email);

public sealed record AuthResponse(string Message, string? AccessToken = null, string? VerificationLink = null);
public sealed record RegisterEmailResponse(string Message, string VerificationLink);
public sealed record VerifyEmailResponse(string Message, bool Success);

public sealed record MeResponse(Guid UserId, Guid TenantId, string Email, string Status, string Role);
