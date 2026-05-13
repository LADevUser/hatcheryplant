using CloudSolutionFactory.Api.Application.DTOs;
using CloudSolutionFactory.Api.Application.Repositories;
using CloudSolutionFactory.Api.Application.Services;
using CloudSolutionFactory.Api.Domain;
using Microsoft.Extensions.Options;
using CloudSolutionFactory.Api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<PlatformOptions>(builder.Configuration.GetSection(PlatformOptions.SectionName));
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IEmailVerificationTokenRepository, InMemoryEmailVerificationTokenRepository>();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddSingleton<IEmailDispatcher, ConsoleEmailDispatcher>();
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<PlatformOptions>>().Value);
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapPost("/auth/register/email", async (RegisterEmailRequest request, AuthService auth, CancellationToken ct) =>
{
    if (string.IsNullOrWhiteSpace(request.TenantId) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
        return Results.BadRequest(new AuthResponse("TenantId, email and password are required."));

    try { return Results.Ok(await auth.RegisterEmailAsync(request, ct)); }
    catch (InvalidOperationException ex) { return Results.Conflict(new AuthResponse(ex.Message)); }
});

app.MapPost("/auth/login/email", async (LoginEmailRequest request, AuthService auth, CancellationToken ct) =>
{
    try { return Results.Ok(await auth.LoginEmailAsync(request, ct)); }
    catch (UnauthorizedAccessException ex) { return Results.Unauthorized(); }
    catch (InvalidOperationException ex) { return Results.BadRequest(new AuthResponse(ex.Message)); }
});

app.MapGet("/auth/verify-email", async (string token, AuthService auth, CancellationToken ct)
    => Results.Ok(await auth.VerifyEmailAsync(token, ct)));

app.MapPost("/auth/resend-verification", async (ResendVerificationRequest request, AuthService auth, CancellationToken ct) =>
{
    try { return Results.Ok(await auth.ResendVerificationAsync(request, ct)); }
    catch (KeyNotFoundException ex) { return Results.NotFound(new AuthResponse(ex.Message)); }
});

app.MapGet("/me", async (HttpRequest request, SessionService sessions, IUserRepository users, CancellationToken ct) =>
{
    if (!request.Headers.TryGetValue("Authorization", out var authHeader)) return Results.Unauthorized();
    var token = authHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase).Trim();
    var session = sessions.GetSession(token);
    if (session is null) return Results.Unauthorized();

    var user = await users.GetByIdAsync(session.Value.UserId, ct);
    return user is null
        ? Results.NotFound()
        : Results.Ok(new MeResponse(user.Id, user.TenantId, user.Email, user.Status.ToString(), user.Role.ToString()));
});

app.Run();
