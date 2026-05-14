using Xunit;
using System.Net;
using System.Net.Http.Json;
using CloudSolutionFactory.Api.Application.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CloudSolutionFactory.Api.Tests;

public class ApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Me_ReturnsUnauthorized_WithoutToken()
    {
        var response = await _client.GetAsync("/me");
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task RegisterVerifyLoginThenMe_ReturnsUser()
    {
        var tenantId = "00000000-0000-0000-0000-000000000002";
        var registerResponse = await _client.PostAsJsonAsync("/auth/register/email", new RegisterEmailRequest(tenantId, "api@example.com", "pass123"));
        registerResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        var registerBody = await registerResponse.Content.ReadFromJsonAsync<RegisterEmailResponse>();

        var token = registerBody!.VerificationLink.Split("token=")[1];
        var verifyResponse = await _client.GetAsync($"/auth/verify-email?token={token}");
        verifyResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var loginResponse = await _client.PostAsJsonAsync("/auth/login/email", new LoginEmailRequest(tenantId, "api@example.com", "pass123"));
        var loginBody = await loginResponse.Content.ReadFromJsonAsync<AuthResponse>();
        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginBody!.AccessToken);

        var meResponse = await _client.GetAsync("/me");
        meResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
