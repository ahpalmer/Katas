// BaseCosmosDBTest.cs
using AzureTestFunctionKatas.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureTestFunctionKatas.Tests
{
    [TestClass]
    public abstract class BaseCosmosDbTest
    {
        protected static CosmosClient _cosmosClient;
        protected static Database _database;
        protected static Container _container;
        protected static readonly string _databaseId = "TestDb";
        protected static readonly string _containerId = "TestContainer";

        [ClassInitialize]
        public static async Task ClassInitialize(TestContext context)
        {
            // Connect to local emulator - only once per test class
            _cosmosClient = new CosmosClient("https://localhost:8081",
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway,
                    ServerCertificateCustomValidationCallback = (cert, chain, sslPolicyErrors) => true
                });

            // Ensure database exists
            DatabaseResponse databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
            _database = databaseResponse.Database;

            // Ensure container exists with partition key path
            ContainerResponse containerResponse = await _database.CreateContainerIfNotExistsAsync(
                id: _containerId,
                partitionKeyPath: "/categoryId",
                throughput: 400);
            _container = containerResponse.Container;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _cosmosClient.Dispose();
        }

        protected async Task CleanupTestDataAsync()
        {
            // Query for all test products
            string query = "SELECT * FROM c WHERE c.name LIKE 'Test%'";
            var iterator = _container.GetItemQueryIterator<Product>(query);

            // Delete all test products
            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                foreach (var product in response)
                {
                    await _container.DeleteItemAsync<Product>(product.id, new PartitionKey(product.categoryId));
                }
            }
        }
    }
}