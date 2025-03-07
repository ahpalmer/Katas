using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using AzureTestFunctionKatas.Entities;
using System.Net;

namespace AzureTestFunctionKatas.DataAccessLayer
{
    public class TestDatabaseFunctions
    {
        private readonly ICosmosDbAccessor<Product> _cosmosDbAccessor;
        private readonly ILogger _logger;

        public TestDatabaseFunctions(
            ICosmosDbAccessor<Product> cosmosDbAccessor,
            ILogger<TestDatabaseFunctions> logger)
        {
            _cosmosDbAccessor = cosmosDbAccessor;
            _logger = logger;
        }

        [Function("TestDataGenerator")]
        public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "TestDataGenerator")] HttpRequestData req)
        {
            _logger.LogInformation("Running TestDataGenerator");

            List<Product> randomlyCreatedData = CreateTestData();
            List<Task> productSendToDB = new List<Task>();
            List<string> productIdsSent = new List<string>();
            foreach (var product in randomlyCreatedData)
            {
                _logger.LogInformation($"Product ID: {product.id}, Name: {product.name}, Category: {product.categoryId}, Price: {product.price}");
                productSendToDB.Add(_cosmosDbAccessor.AddItemAsync(product, product.categoryId));
                productIdsSent.Add(product.id);
            }

            await Task.WhenAll(productSendToDB);
            _logger.LogInformation("All products added to the database.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(productIdsSent.Aggregate((workingSentence, next) => workingSentence + ", " + next).ToString());

            _logger.LogInformation($"Product IDs sent to the database: {response.Body.ToString()}");
            return response;
        }

        [Function("UpdateProductData")]
        public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "UpdateProductData")] 
        HttpRequestData req,
        string productId,
        string partitionKey)
        {
            _logger.LogInformation("Running UpdateProductData {productId}", productId);

            try
            {
                // Get the current product with its ETag
                var product = await _cosmosDbAccessor.GetItemAsync(productId, partitionKey);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product {productId} not found");
                }

                Random random = new Random();
                double newPrice = random.Next(1, 11);
                // Update the price
                product.price= newPrice;

                // Try to save with ETag check
                await _cosmosDbAccessor.UpdateItemAsync(productId, product, partitionKey);
                _logger.LogInformation("Product {productId} updated successfully", productId);

                var response = req.CreateResponse(HttpStatusCode.OK);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString(productId);

                return response;
            }
            catch (ConcurrencyException)
            {
                throw new Exception($"The product {productId} was modified by another process");
            }
        }

        public List<Product> CreateTestData()
        {
            var products = new List<Product>();

            for (int i = 1; i < 11; i++)
            {
                products.Add(CreateTestProduct(i));
            }

            return products;
        }
        public Product CreateTestProduct(int count)
        {
            Random random = new Random();
            double randomNumber = random.Next(1, 11);
            string category;
            if (randomNumber % 2 == 0)
            {
                category = "Even";
            }
            else
            {
                category = "Odd";
            }

            return new Product
            {
                id = Guid.NewGuid().ToString(),
                name = $"Test Product {count}",
                categoryId = category,
                price = randomNumber,
                tags = new[] { "tag1", "tag2" }
            };
        }
    }
}
