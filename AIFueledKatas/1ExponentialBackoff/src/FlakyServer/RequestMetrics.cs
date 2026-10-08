namespace FlakyServer;

public sealed class RequestMetrics
{
    private readonly Lock sync = new();
    private readonly SortedDictionary<long, RequestBucket> buckets = [];
    private long totalRequests;
    private long acceptedRequests;
    private long rejectedRequests;

    public void Record(DateTimeOffset timestamp, bool accepted)
    {
        var bucketStartMilliseconds = timestamp.ToUnixTimeMilliseconds() / 100 * 100;

        lock (sync)
        {
            totalRequests++;

            if (!buckets.TryGetValue(bucketStartMilliseconds, out var bucket))
            {
                bucket = new RequestBucket(bucketStartMilliseconds);
                buckets.Add(bucketStartMilliseconds, bucket);
            }

            bucket.Received++;
            if (accepted)
            {
                acceptedRequests++;
                bucket.Accepted++;
            }
            else
            {
                rejectedRequests++;
                bucket.Rejected++;
            }
        }
    }

    public MetricsSnapshot Snapshot()
    {
        lock (sync)
        {
            return new MetricsSnapshot(
                totalRequests,
                acceptedRequests,
                rejectedRequests,
                buckets.Values
                    .Select(bucket => new RequestBucketSnapshot(
                        DateTimeOffset.FromUnixTimeMilliseconds(bucket.StartMilliseconds),
                        bucket.Received,
                        bucket.Accepted,
                        bucket.Rejected))
                    .ToArray());
        }
    }

    public void Reset()
    {
        lock (sync)
        {
            totalRequests = 0;
            acceptedRequests = 0;
            rejectedRequests = 0;
            buckets.Clear();
        }
    }

    private sealed class RequestBucket(long startMilliseconds)
    {
        public long StartMilliseconds { get; } = startMilliseconds;
        public int Received { get; set; }
        public int Accepted { get; set; }
        public int Rejected { get; set; }
    }
}

public sealed record MetricsSnapshot(
    long TotalRequests,
    long AcceptedRequests,
    long RejectedRequests,
    IReadOnlyList<RequestBucketSnapshot> Buckets);

public sealed record RequestBucketSnapshot(
    DateTimeOffset Start,
    int Received,
    int Accepted,
    int Rejected);