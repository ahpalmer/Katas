using Microsoft.Azure.Cosmos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AzureTestFunctionKatas.DataAccessLayer;
using AzureTestFunctionKatas.Entities;

namespace AzureTestFunctionKatas.Tests
{
    [TestClass]
    public class CosmosDbConcurrencyTests : BaseCosmosDbTest
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

        [TestMethod]
        public async Task UpdateItemAsync_ConcurrentUpdates_ThrowsConcurrencyException()
        {
            // Arrange
            // Create a mock CosmosClient and Container
            var mockContainer = new Mock<Container>();
            var mockCosmosClient = new Mock<CosmosClient>();

            // Set up the GetContainer method to return our mock container
            mockCosmosClient.Setup(client =>
                client.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockContainer.Object);

            // Create a test product with an ETag
            var product = new Product
            {
                id = "test-product-1",
                name = "Test Product",
                categoryId = "electronics",
                price = 9.99,
                tags = new[] { "test", "sample" },
                ETag = "original-etag-value"
            };

            // Set up the mock to throw a PreconditionFailed exception when UpsertItemAsync is called
            mockContainer.Setup(c =>
                c.UpsertItemAsync(
                    It.IsAny<Product>(),
                    It.IsAny<PartitionKey>(),
                    It.Is<ItemRequestOptions>(o => o.IfMatchEtag == "original-etag-value"),
                    It.IsAny<CancellationToken>()))
                .ThrowsAsync(new CosmosException(
                    "Precondition failed",
                    HttpStatusCode.PreconditionFailed,
                    412, // Substatus code
                    "Etag doesn't match",
                    1.0 // Request charge in RUs
                ));

            // Create the accessor with the mock client
            var accessor = new CosmosDbAccessor<Product>(
                mockCosmosClient.Object,
                "TestDb",
                "TestContainer"
            );

            // Act & Assert
            // Verify that a concurrency exception is thrown
            try
            {
                await accessor.UpdateItemAsync(product.id, product, product.categoryId);
                Assert.Fail("Expected ConcurrencyException was not thrown");
            }
            catch (ConcurrencyException ex)
            {
                // Verify the exception message contains the ID
                StringAssert.Contains(ex.Message, product.id);
            }
        }

        [TestMethod]
        public async Task UpdateItemAsync_NoETagConflict_CompletesSuccessfully()
        {
            // Arrange
            var mockContainer = new Mock<Container>();
            var mockCosmosClient = new Mock<CosmosClient>();

            mockCosmosClient.Setup(client =>
                client.GetContainer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockContainer.Object);

            var product = new Product
            {
                id = "test-product-1",
                name = "Test Product",
                categoryId = "electronics",
                price = 9.99,
                tags = new[] { "test", "sample" },
                ETag = "valid-etag-value"
            };

            // Set up the mock to successfully complete the update
            var mockResponse = new Mock<ItemResponse<Product>>();
            mockResponse.Setup(r => r.Resource).Returns(product);

            mockContainer.Setup(c =>
                c.UpsertItemAsync(
                    It.IsAny<Product>(),
                    It.IsAny<PartitionKey>(),
                    It.Is<ItemRequestOptions>(o => o.IfMatchEtag == "valid-etag-value"),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse.Object);

            var accessor = new CosmosDbAccessor<Product>(
                mockCosmosClient.Object,
                "testdb",
                "testcontainer"
            );

            // Act
            // No exception should be thrown
            await accessor.UpdateItemAsync(product.id, product, product.categoryId);

            // Assert
            // Verify that UpsertItemAsync was called with the correct parameters
            mockContainer.Verify(c =>
                c.UpsertItemAsync(
                    It.Is<Product>(p => p.id == product.id),
                    It.Is<PartitionKey>(pk => pk.ToString().Contains(product.categoryId)),
                    It.Is<ItemRequestOptions>(o => o.IfMatchEtag == "valid-etag-value"),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [TestMethod]
        public async Task IntegrationTest_ConcurrentUpdates_ShouldCauseETagConflict()
        {
            // Arrange
            // The _productAccessor is already initialized in the TestInitialize method

            // Create a test product
            var productId = Guid.NewGuid().ToString();
            var product = new Product
            {
                id = productId,
                name = "Concurrency Test Product",
                categoryId = "test",
                price = 10.0,
                tags = new[] { "test" }
            };

            // Add the product
            await _productAccessor.AddItemAsync(product, product.categoryId);

            // Act
            // Get the product with its ETag
            var originalProduct = await _productAccessor.GetItemAsync(productId, product.categoryId);

            // Get it again (simulating a second user/process)
            var concurrentProduct = await _productAccessor.GetItemAsync(productId, product.categoryId);

            // First update
            originalProduct.price = 15.0;
            await _productAccessor.UpdateItemAsync(productId, originalProduct, product.categoryId);

            // Assert
            // Second update should fail due to ETag mismatch
            concurrentProduct.price = 20.0;

            try
            {
                await _productAccessor.UpdateItemAsync(productId, concurrentProduct, product.categoryId);
                Assert.Fail("Expected ConcurrencyException was not thrown");
            }
            catch (ConcurrencyException)
            {
                // Test passed if exception is thrown
            }
            finally
            {
                // Clean up
                await _container.DeleteItemAsync<Product>(productId, new PartitionKey(product.categoryId));
            }
        }
    }
}