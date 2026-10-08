Here are 20, roughly ordered from a morning to a weekend. Each one has the thing to build and the way to break it.
Resilience patterns
1. Retry with exponential backoff and jitter. Run 1,000 simulated clients against a flaky server, with and without jitter, and watch the retry storm form.
2. Circuit breaker. Break it with concurrent callers during the half-open state. How many probe requests slip through?
3. Bulkhead / concurrency limiter. Starve one downstream dependency and see whether it takes down calls to healthy ones.
4. Timeout and deadline propagation. Chain three services, give each its own timeout, and find out why the sum of timeouts is a bug.

Rate limiting and caching
5. Token-bucket rate limiter. Test burst behavior, thread-safety, and what happens when the clock jumps.
6. Sliding-window rate limiter. Compare its accuracy and memory cost against the token bucket under the same load.
7. LRU cache with TTL. Trigger a cache stampede on a hot key, then fix it with single-flight request coalescing.
8. Bloom filter. Predict the false-positive rate from the math, then measure it and see how far off you are.

Queues and messaging
9. Job queue with at-least-once delivery and visibility timeouts. Kill workers mid-job, watch duplicates appear, then make the handler idempotent.
10. In-memory pub/sub broker. Add a slow consumer and see what happens without backpressure.
11. Transactional outbox. Crash between the database write and the publish, and check that nothing is lost or double-sent.
12. Idempotency-key middleware. Fire two identical requests at the same instant and find the race.

Storage
13. Write-ahead log. Simulate torn writes by truncating the file mid-record, then make recovery handle it.
14. Mini LSM-tree key-value store. Crash during a memtable flush and verify replay from the WAL.
15. Optimistic concurrency with ETags. Build a versioned store and reproduce the lost-update problem before you fix it. This is directly relevant to how Cosmos DB behaves.
16. Cursor-based pagination. Mutate the data while someone pages through it and see what gets skipped or duplicated.
Distributed-systems building blocks
17. Consistent hashing ring. Add and remove nodes and measure how many keys move, with and without virtual nodes.
18. Distributed lock with leases and fencing tokens. Pause the lock holder (simulating a GC pause), let the lease expire, and watch two holders believe they own the lock.
19. Lease-based leader election. Use a single blob or document with ETag-based renewal, then partition the leader away.
20. Raft-lite (leader election only, no log). Simulate message loss and delay, and look for split votes and flapping leaders. This is the weekend-sized one.

