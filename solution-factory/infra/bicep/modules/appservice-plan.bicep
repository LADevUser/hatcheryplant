param name string
param location string
param skuName string = 'B1'
param tags object = {}

resource plan 'Microsoft.Web/serverfarms@2023-12-01' = {
  name: name
  location: location
  kind: 'linux'
  sku: {
    name: skuName
    tier: startsWith(skuName, 'B') ? 'Basic' : 'Standard'
  }
  properties: {
    reserved: true
  }
  tags: tags
}

output id string = plan.id
