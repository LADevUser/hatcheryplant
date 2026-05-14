targetScope = 'resourceGroup'

@description('Environment short name, e.g. dev/test/prod')
param environment string = 'dev'

@description('Azure location for all resources')
param location string = resourceGroup().location

@description('Project workload prefix')
param workloadName string = 'csf'

@description('SKU for App Service plan')
param appServicePlanSkuName string = 'B1'

@description('Linux runtime stack for backend app service')
param backendLinuxFxVersion string = 'DOTNETCORE|10.0'

@description('Region for Static Web App')
param staticWebAppLocation string = 'West Europe'

@description('Optional tags to apply to resources')
param tags object = {}

var suffix = toLower('${workloadName}-${environment}')
var appServicePlanName = 'asp-${suffix}'
var backendWebAppName = 'app-${suffix}-api'
var staticWebAppName = 'swa-${suffix}'
var storageAccountName = take(replace('st${workloadName}${environment}${uniqueString(resourceGroup().id)}', '-', ''), 24)
var keyVaultName = take(replace('kv-${workloadName}-${environment}-${uniqueString(resourceGroup().id)}', '-', ''), 24)

module appServicePlan 'modules/appservice-plan.bicep' = {
  name: 'appServicePlan'
  params: {
    name: appServicePlanName
    location: location
    skuName: appServicePlanSkuName
    tags: tags
  }
}

module backendWebApp 'modules/backend-webapp.bicep' = {
  name: 'backendWebApp'
  params: {
    name: backendWebAppName
    location: location
    appServicePlanId: appServicePlan.outputs.id
    linuxFxVersion: backendLinuxFxVersion
    tags: tags
  }
}

module staticWebApp 'modules/static-webapp.bicep' = {
  name: 'staticWebApp'
  params: {
    name: staticWebAppName
    location: staticWebAppLocation
    tags: tags
  }
}

module storageAccount 'modules/storage-account.bicep' = {
  name: 'storageAccount'
  params: {
    name: storageAccountName
    location: location
    tags: tags
  }
}

module keyVault 'modules/key-vault-placeholder.bicep' = {
  name: 'keyVaultPlaceholder'
  params: {
    name: keyVaultName
    location: location
    tags: tags
  }
}

output backendUrl string = backendWebApp.outputs.defaultHostname
output frontendUrl string = staticWebApp.outputs.defaultHostname
output storageAccountNameOutput string = storageAccount.outputs.name
output keyVaultNameOutput string = keyVault.outputs.name
