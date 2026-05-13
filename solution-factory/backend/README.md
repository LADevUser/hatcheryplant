# Backend

Minimal API with in-memory repositories and auth skeleton.

## Runtime target
- .NET SDK: `10.0.300` (validated in your local Debian 13 / Trixie environment)
- Target Framework Moniker: `net10.0`

## Endpoints
- POST `/auth/register/email`
- POST `/auth/login/email`
- GET `/auth/verify-email?token=...`
- POST `/auth/resend-verification`
- GET `/me`
- GET `/health`

## Run
```bash
cd src/CloudSolutionFactory.Api
dotnet restore
dotnet build
dotnet run
```

## Local verification
```bash
cd src/CloudSolutionFactory.Api
dotnet --version
dotnet restore
dotnet build
dotnet test
```
