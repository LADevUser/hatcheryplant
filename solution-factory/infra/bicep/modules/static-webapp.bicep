param name string
param location string = 'West Europe'
param tags object = {}

resource staticSite 'Microsoft.Web/staticSites@2023-12-01' = {
  name: name
  location: location
  sku: {
    name: 'Free'
    tier: 'Free'
  }
  properties: {
    stagingEnvironmentPolicy: 'Enabled'
  }
  tags: tags
}

output defaultHostname string = 'https://${staticSite.properties.defaultHostname}'
