param name string
param location string
param appServicePlanId string
param linuxFxVersion string = 'DOTNETCORE|10.0'
param tags object = {}

resource webApp 'Microsoft.Web/sites@2023-12-01' = {
  name: name
  location: location
  kind: 'app,linux'
  properties: {
    httpsOnly: true
    serverFarmId: appServicePlanId
    siteConfig: {
      linuxFxVersion: linuxFxVersion
      alwaysOn: false
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
    }
  }
  tags: tags
}

output defaultHostname string = 'https://${webApp.properties.defaultHostName}'
