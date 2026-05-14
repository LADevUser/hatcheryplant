# Azure IaC (Bicep) - Dev Environment

This folder contains Bicep templates for provisioning the Cloud Solution Factory dev baseline.

## Provisioned resources
- App Service Plan (Linux)
- Backend App Service
- Azure Static Web App (Free)
- Storage Account (minimal baseline)
- Key Vault (placeholder for later secret integration)

## Files
- `main.bicep`: root template
- `main.dev.parameters.json`: dev parameter values
- `modules/`: resource modules

## Validate
```bash
az deployment group validate \
  --resource-group <rg-name> \
  --template-file solution-factory/infra/bicep/main.bicep \
  --parameters @solution-factory/infra/bicep/main.dev.parameters.json
```

## Deploy
```bash
az deployment group create \
  --resource-group <rg-name> \
  --template-file solution-factory/infra/bicep/main.bicep \
  --parameters @solution-factory/infra/bicep/main.dev.parameters.json
```

## Outputs
- `backendUrl`
- `frontendUrl`
- `storageAccountNameOutput`
- `keyVaultNameOutput`
