resource storageAccount 'Microsoft.Storage/storageAccounts@2022-09-01' = {
  name: 'biceplearnstorage09876'
  location: 'eastus'
  sku: {
    name: 'Standard_LRS'
  }
  kind: 'StorageV2'
  properties: {
    accessTier: 'Hot'
  }
}

resource appServicePlan 'Microsoft.Web/serverFarms@2022-03-01' = {
  name: 'toy-product-launch-bicep-learn-09876'
  location: 'eastus'
  sku: {
    name: 'F1'
  }
  properties: {}
}

resource appServiceApp 'Microsoft.Web/sites@2022-03-01' = {
  name: 'toy-product-bicep-learn-app-09876'
  location: 'eastus'
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
  }
}
