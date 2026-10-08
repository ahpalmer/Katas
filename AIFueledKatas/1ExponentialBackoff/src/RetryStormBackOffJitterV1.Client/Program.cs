using RetryStormBackOffJitterV1.Client;

var options = ClientOptions.Parse(args);
using var httpClient = new HttpClient(new SocketsHttpHandler
{
    MaxConnectionsPerServer = options.RequestCount
});

Console.WriteLine($"Sending {options.RequestCount} requests to {options.Endpoint}, with jittered exponential retry windows of 0-2, 0-4, and 0-8 seconds.");
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