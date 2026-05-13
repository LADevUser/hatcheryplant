# Backend

Minimal API with in-memory repositories and auth skeleton.

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
dotnet run
```
