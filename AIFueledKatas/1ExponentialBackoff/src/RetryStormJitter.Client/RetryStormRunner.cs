using System.Collections.Concurrent;
using System.Diagnostics;

namespace RetryStormJitter.Client;

public static class RetryStormRunner
{
    private const int MaximumRetries = 3;
    private static readonly TimeSpan MaximumJitterDelay = TimeSpan.FromSeconds(2);

    public static async Task<StormResult> SendBurstAsync(
        HttpClient httpClient,
        Uri endpoint,
        int requestCount,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(requestCount);

        var startGate = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var statusCounts = new ConcurrentDictionary<int, int>();
        var transportFailures = 0;
        var stopwatch = Stopwatch.StartNew();

        var requests = Enumerable.Range(0, requestCount)
            .Select(async _ =>
            {
                await startGate.Task.WaitAsync(cancellationToken);

                for (var attempt = 0; attempt <= MaximumRetries; attempt++)
                {
                    var shouldRetry = false;

                    try
                    {
                        using var response = await httpClient.GetAsync(endpoint, cancellationToken);
                        statusCounts.AddOrUpdate((int)response.StatusCode, 1, (_, count) => count + 1);
                        shouldRetry = response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable;
                    }
                    catch (HttpRequestException)
                    {
                        Interlocked.Increment(ref transportFailures);
                        shouldRetry = true;
                    }

                    if (!shouldRetry || attempt == MaximumRetries)
                    {
                        break;
                    }

                    await Task.Delay(CalculateJitterDelay(), cancellationToken);
                }
            })
            .ToArray();

        startGate.SetResult();
        await Task.WhenAll(requests);
        stopwatch.Stop();

        return new StormResult(stopwatch.Elapsed, statusCounts, transportFailures);
    }

    private static TimeSpan CalculateJitterDelay() =>
        TimeSpan.FromMilliseconds(Random.Shared.NextDouble() * MaximumJitterDelay.TotalMilliseconds);
}

public sealed record StormResult(
    TimeSpan Elapsed,
    IReadOnlyDictionary<int, int> StatusCounts,
    int TransportFailures);

public sealed record ClientOptions(Uri Endpoint, int RequestCount)
{
    public static ClientOptions Parse(string[] args)
    {
        var endpoint = new Uri("http://localhost:5000/work");
        var requestCount = 1_000;

        for (var index = 0; index < args.Length; index++)
        {
            switch (args[index])
            {
                case "--server" when index + 1 < args.Length:
                    endpoint = new Uri(new Uri(args[++index].TrimEnd('/') + "/"), "work");
                    break;
                case "--clients" when index + 1 < args.Length && int.TryParse(args[++index], out var parsedCount):
                    requestCount = parsedCount;
                    break;
                default:
                    throw new ArgumentException($"Unknown or incomplete argument: {args[index]}");
            }
        }

        return new ClientOptions(endpoint, requestCount);
    }
}