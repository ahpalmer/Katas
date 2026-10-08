using RetryStorm.Client;

var options = ClientOptions.Parse(args);
using var httpClient = new HttpClient(new SocketsHttpHandler
{
    MaxConnectionsPerServer = options.RequestCount
});

Console.WriteLine($"Sending {options.RequestCount} requests to {options.Endpoint}, with up to 3 retries after 503 responses or transport failures.");
var result = await RetryStormRunner.SendBurstAsync(httpClient, options.Endpoint, options.RequestCount);

Console.WriteLine($"Finished in {result.Elapsed.TotalMilliseconds:N0} ms.");
foreach (var status in result.StatusCounts.OrderBy(item => item.Key))
{
    Console.WriteLine($"{status.Key}: {status.Value}");
}

if (result.TransportFailures > 0)
{
    Console.WriteLine($"Transport failures: {result.TransportFailures}");
}