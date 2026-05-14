# Cloud Solution Factory Platform

Monorepo skeleton for use case 001: registration, email verification, login, and welcome dashboard.

## Structure
- `frontend/` React app
- `backend/` ASP.NET Core Minimal API (.NET 10)
- `docs/` architecture and product docs
- `infra/` future IaC
- `templates/` future template libraries
- `.devcontainer/` dev environment
- `.github/workflows/` CI

## Run backend
```bash
cd solution-factory/backend/src/CloudSolutionFactory.Api
dotnet restore
dotnet run
```

## Run frontend
```bash
cd solution-factory/frontend
npm install
npm run dev
```

## Devcontainer
Open repository in VS Code and “Reopen in Container”. Container includes .NET 10 + Node LTS.

## CI
- Backend workflow restores, builds (warnings as errors), format-checks, and runs tests with coverage artifact upload.
- Frontend workflow installs, lints, format-checks, builds, and runs tests with coverage artifact upload.

## Known limitations
- In-memory repositories only.
- OAuth providers are mocked/documented only.
- Email sending is not implemented (verification link returned in API response).
- Demo session token uses in-memory local storage.

## Next steps
- Add test projects.
- Integrate real OAuth/OIDC and email provider.
- Add persistent storage + Key Vault + Azure deployment pipeline.


## Backend tests
```bash
cd solution-factory/backend
dotnet test tests/CloudSolutionFactory.Api.Tests/CloudSolutionFactory.Api.Tests.csproj
```


## Frontend tests
```bash
cd solution-factory/frontend
npm install
npm run test:run
```


## Frontend quality commands
```bash
cd solution-factory/frontend
npm run lint
npm run format:check
npm run test:coverage
```

## Backend quality commands
```bash
cd solution-factory/backend
dotnet format src/CloudSolutionFactory.Api/CloudSolutionFactory.Api.csproj --verify-no-changes
dotnet test tests/CloudSolutionFactory.Api.Tests/CloudSolutionFactory.Api.Tests.csproj --configuration Release --collect:"XPlat Code Coverage"
```
