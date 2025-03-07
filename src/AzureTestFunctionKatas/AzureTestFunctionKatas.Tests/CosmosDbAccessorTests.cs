using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AzureTestFunctionKatas.DataAccessLayer;
using AzureTestFunctionKatas.Entities;

namespace AzureTestFunctionKatas.Tests
{
    [TestClass]
    public class CosmosDBAccessorTests : BaseCosmosDbTest
    {
        private CosmosDbAccessor<Product> _productAccessor;

        [ClassInitialize]
        public static async Task ClassInit(TestContext testContext)
        {
            // Call the base class initialization
            await ClassInitialize(testContext);
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            // Initialize the accessor we're testing using the shared client
            _productAccessor = new CosmosDbAccessor<Product>(_cosmosClient, _databaseId, _containerId);

            // Clean up any existing test data
            await CleanupTestDataAsync();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await CleanupTestDataAsync();
        }
            //private CosmosClient _cosmosClient;
            //private CosmosDbAccessor<Product> _productAccessor;
            //private readonly string _databaseId = "TestDb";
            //private readonly string _containerId = "TestContainer";
            //private Database _database;
            //private Container _container;

            //[TestInitialize]
            //public async Task TestInitialize()
            //{
            //    // Connect to local emulator
            //    // Note: The emulator must be running for these tests to work
            //    _cosmosClient = new CosmosClient("https://localhost:8081",
            //        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            //        new CosmosClientOptions
            //        {
            //            ConnectionMode = ConnectionMode.Gateway,
            //            ServerCertificateCustomValidationCallback = (cert, chain, sslPolicyErrors) => true // Trust emulator SSL cert
            //        });

            //    // Ensure database exists
            //    DatabaseResponse databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
            //    _database = databaseResponse.Database;

            //    // Ensure container exists with partition key path
            //    ContainerResponse containerResponse = await _database.CreateContainerIfNotExistsAsync(
            //        id: _containerId,
            //        partitionKeyPath: "/categoryId",
            //        throughput: 400); // Minimum throughput
            //    _container = containerResponse.Container;

            //    // Initialize the accessor we're testing
            //    _productAccessor = new CosmosDbAccessor<Product>(_cosmosClient, _databaseId, _containerId);

            //    // Clean up any existing test data
            //    await CleanupTestDataAsync();
            //}

            //[TestCleanup]
            //public async Task TestCleanup()
            //{
            //    await CleanupTestDataAsync();
            //    _cosmosClient.Dispose();
            //}

            //private async Task CleanupTestDataAsync()
            //{
            //    // Query for all test products
            //    string query = "SELECT * FROM c WHERE c.name LIKE 'Test%'";
            //    var iterator = _container.GetItemQueryIterator<Product>(query);

            //    // Delete all test products
            //    while (iterator.HasMoreResults)
            //    {
            //        var response = await iterator.ReadNextAsync();
            //        foreach (var product in response)
            //        {
            //            await _container.DeleteItemAsync<Product>(product.id, new PartitionKey(product.categoryId));
            //        }
            //    }
            //}



            [TestMethod]
        public async Task AddItemAsync_ShouldAddProductToContainer()
        {
            // Arrange
            var product = new Product
            {
                id = Guid.NewGuid().ToString(),
                name = "Test Product",
                categoryId = "electronics",
                price = 99.99,
                tags = new[] { "test", "new" }
            };

            // Act
            await _productAccessor.AddItemAsync(product, product.categoryId);

            // Assert
            var retrievedProduct = await _productAccessor.GetItemAsync(product.id, product.categoryId);
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(product.name, retrievedProduct.name);
            Assert.AreEqual(product.price, retrievedProduct.price);
        }

        [TestMethod]
        public async Task GetItemAsync_WithExistingId_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product
            {
                id = Guid.NewGuid().ToString(),
                name = "Test Product GetItem",
                categoryId = "books",
                price = 29.99,
                tags = new[] { "test", "book" }
            };
            await _container.CreateItemAsync(product, new PartitionKey(product.categoryId));

            // Act
            var retrievedProduct = await _productAccessor.GetItemAsync(product.id, product.categoryId);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(product.id, retrievedProduct.id);
            Assert.AreEqual(product.name, retrievedProduct.name);
        }

        [TestMethod]
        public async Task GetItemAsync_WithNonExistingId_ShouldReturnNull()
        {
            // Arrange
            string nonExistingId = Guid.NewGuid().ToString();
            string partitionKey = "electronics";

            // Act
            var retrievedProduct = await _productAccessor.GetItemAsync(nonExistingId, partitionKey);

            // Assert
            Assert.IsNull(retrievedProduct);
        }

        [TestMethod]
        public async Task GetItemsAsync_WithQuery_ShouldReturnMatchingProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Test Product Query 1",
                    categoryId = "electronics",
                    price = 99.99,
                    tags = new[] { "test", "electronics" }
                },
                new Product
                {
                    id = Guid.NewGuid().ToString(),
                    name = "Test Product Query 2",
                    categoryId = "electronics",
                    price = 149.99,
                    tags = new[] { "test", "electronics" }
                }
            };

            foreach (var product in products)
            {
                await _container.CreateItemAsync(product, new PartitionKey(product.categoryId));
            }

            // Act
            string query = "SELECT * FROM c WHERE c.categoryId = 'electronics' AND c.name LIKE 'Test Product Query%'";
            var retrievedProducts = await _productAccessor.GetItemsAsync(query);

            // Assert
            Assert.AreEqual(2, retrievedProducts.Count());
            Assert.IsTrue(retrievedProducts.Any(p => p.name == "Test Product Query 1"));
            Assert.IsTrue(retrievedProducts.Any(p => p.name == "Test Product Query 2"));
        }

        [TestMethod]
        public async Task UpdateItemAsync_ShouldUpdateExistingProduct()
        {
            // Arrange
            var product = new Product
            {
                id = Guid.NewGuid().ToString(),
                name = "Test Product Before Update",
                categoryId = "clothing",
                price = 49.99,
                tags = new[] { "test", "clothing" }
            };
            await _container.CreateItemAsync(product, new PartitionKey(product.categoryId));

            // Act
            product.name = "Test Product After Update";
            product.price = 39.99;
            await _productAccessor.UpdateItemAsync(product.id, product, product.categoryId);

            // Assert
            var updatedProduct = await _productAccessor.GetItemAsync(product.id, product.categoryId);
            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual("Test Product After Update", updatedProduct.name);
            Assert.AreEqual(39.99, updatedProduct.price);
        }

        [TestMethod]
        public async Task DeleteItemAsync_ShouldRemoveProductFromContainer()
        {
            // Arrange
            var product = new Product
            {
                id = Guid.NewGuid().ToString(),
                name = "Test Product To Delete",
                categoryId = "toys",
                price = 19.99,
                tags = new[] { "test", "toys" }
            };
            await _container.CreateItemAsync(product, new PartitionKey(product.categoryId));

            // Verify product exists
            var retrievedBeforeDelete = await _productAccessor.GetItemAsync(product.id, product.categoryId);
            Assert.IsNotNull(retrievedBeforeDelete);

            // Act
            await _productAccessor.DeleteItemAsync(product.id, product.categoryId);

            // Assert
            var retrievedAfterDelete = await _productAccessor.GetItemAsync(product.id, product.categoryId);
            Assert.IsNull(retrievedAfterDelete);
        }
    }
}