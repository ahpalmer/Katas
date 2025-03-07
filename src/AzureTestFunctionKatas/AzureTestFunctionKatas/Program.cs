using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker;
using AzureTestFunctionKatas.Services;
using AzureTestFunctionKatas.DataAccessLayer;
using AzureTestFunctionKatas.Entities;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Register HttpClient
        services.AddHttpClient();
        // Add the services to the DI container
        services.AddSingleton<IPythagoreanTheoremService, PythagoreanTheoremService>();
        services.AddSingleton<CosmosClient>(sp =>
        {
            // Emulator connection string (default values)
            string connectionString = "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";

            // Or get from configuration
            // string connectionString = context.Configuration["CosmosDb:ConnectionString"];

            CosmosClientOptions options = new CosmosClientOptions
            {
                ConnectionMode = ConnectionMode.Gateway,
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                },
                ServerCertificateCustomValidationCallback = (cert, chain, sslPolicyErrors) => true // Trust emulator SSL cert

            };

            return new CosmosClient(connectionString, options);
        });

        services.AddSingleton<ICosmosDbAccessor<Product>>(sp =>
        {
            CosmosClient cosmosClient = sp.GetRequiredService<CosmosClient>();
            string databaseId = "TestDb";
            string containerId = "TestContainer";
            return new CosmosDbAccessor<Product>(cosmosClient, databaseId, containerId);
        });
        services.AddLogging();
    })
    .Build();

await host.RunAsync();