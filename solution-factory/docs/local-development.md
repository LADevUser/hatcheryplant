# Local Development

Environment note: automation/sandbox environments may miss dotnet/node. In that case, create files and run commands locally or in devcontainer/GitHub Actions.

## Debian 13
- .NET SDK 10.0.300
- Node.js LTS

## Commands
Backend:
`dotnet restore && dotnet build && dotnet run`

Frontend:
`npm install && npm run dev`
