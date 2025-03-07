using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker;
using AzureTestFunctionKatas.Services;
using AzureTestFunctionKatas.DataAccessLayer;

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
        services.AddLogging();
    })
    .Build();

await host.RunAsync();