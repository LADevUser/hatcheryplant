# Backend - Cloud Solution Factory API

ASP.NET Core Minimal API (.NET 10 target architecture) for demo auth flow.

## Endpoints
- `GET /health`
- `POST /auth/register/email`
- `POST /auth/login/email`
- `GET /auth/verify-email?token=...`
- `POST /auth/resend-verification`
- `GET /me`

## Run locally
```bash
cd solution-factory/backend/src/CloudSolutionFactory.Api
dotnet restore
dotnet run
```

## Run tests
```bash
cd solution-factory/backend
dotnet test tests/CloudSolutionFactory.Api.Tests/CloudSolutionFactory.Api.Tests.csproj
```
