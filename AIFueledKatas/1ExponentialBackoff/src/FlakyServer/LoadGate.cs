namespace FlakyServer;

public sealed class LoadGate(int capacity, TimeSpan window, TimeSpan recoveryPeriod)
{
    private readonly Lock sync = new();
    private readonly Queue<DateTimeOffset> admittedRequests = new();
    private DateTimeOffset overloadedUntil = DateTimeOffset.MinValue;

    public int Capacity { get; } = capacity > 0
        ? capacity
        : throw new ArgumentOutOfRangeException(nameof(capacity));

    public TimeSpan Window { get; } = window > TimeSpan.Zero
        ? window
        : throw new ArgumentOutOfRangeException(nameof(window));

    public TimeSpan RecoveryPeriod { get; } = recoveryPeriod > TimeSpan.Zero
        ? recoveryPeriod
        : throw new ArgumentOutOfRangeException(nameof(recoveryPeriod));

    public bool TryEnter(DateTimeOffset now)
    {
        lock (sync)
        {
            if (now < overloadedUntil)
            {
                return false;
            }

            while (admittedRequests.TryPeek(out var oldest) && now - oldest >= Window)
            {
                admittedRequests.Dequeue();
            }

            if (admittedRequests.Count >= Capacity)
            {
                overloadedUntil = now + RecoveryPeriod;
                admittedRequests.Clear();
                return false;
            }

            admittedRequests.Enqueue(now);
            return true;
        }
    }

    public bool IsOverloaded(DateTimeOffset now)
    {
        lock (sync)
        {
            return now < overloadedUntil;
        }
    }

    public void Reset()
    {
        lock (sync)
        {
            admittedRequests.Clear();
            overloadedUntil = DateTimeOffset.MinValue;
        }
    }
}