namespace CloudSolutionFactory.Api.Application.DTOs;

public sealed record RegisterEmailRequest(string TenantId, string Email, string Password);
public sealed record LoginEmailRequest(string TenantId, string Email, string Password);
public sealed record ResendVerificationRequest(string TenantId, string Email);

public sealed record AuthResponse(string Message, string? AccessToken = null);
public sealed record RegisterEmailResponse(string Message, string VerificationTokenPreview);
public sealed record VerifyEmailResponse(string Message, bool Success);

public sealed record MeResponse(Guid UserId, string TenantId, string Email, string Status, string Role);
