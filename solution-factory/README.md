# Cloud Solution Factory Platform (Monorepo)

Initial skeleton for use case 001: registration, email verification and login to welcome dashboard.

## Structure
- `frontend/` React app
- `backend/` ASP.NET Core Minimal API
- `docs/` architecture and decisions
- `infra/` infra placeholders
- `templates/` template placeholders
- `.github/workflows/` CI placeholders

## Run locally
1. Backend
   - `cd backend/src/CloudSolutionFactory.Api`
   - `dotnet restore`
   - `dotnet build`
   - `dotnet run`
2. Frontend
   - `cd frontend`
   - `npm install`
   - `npm run dev`

## Local verification commands
Run from `solution-factory/`:
- Backend quick checks:
  - `cd backend/src/CloudSolutionFactory.Api && dotnet --version`
  - `cd backend/src/CloudSolutionFactory.Api && dotnet restore`
  - `cd backend/src/CloudSolutionFactory.Api && dotnet build`
  - `cd backend/src/CloudSolutionFactory.Api && dotnet test` *(no tests yet, should report 0 test projects)*
- Frontend quick checks:
  - `cd frontend && npm install`
  - `cd frontend && npm run build`
