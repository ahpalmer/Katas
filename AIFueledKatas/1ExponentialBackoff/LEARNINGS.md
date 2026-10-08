Conclusion:
Brainless retries are better than I originally expected them to be. Unless you are dealing with massive throughput problems, brainless retries are probably fine. If you are dealing with massive throughput problems, backoff with jitter (0-2, 2-4, 4-8) is the best combination of dependability, reducing the number of retries, and speed. It is a little slower than brainless retrying, but it also runs fewer retries (20 fewer, so not THAT good) and is just as dependable. There are some options for changing the server functionality that I didn't explore.  For example, it fails after 500 attempts instead of 300, or if 300 attempts in 0.2 seconds fails the server instead of 1 second. I'm suprised at how little benefit there seemed to be for implementing BackOff and jitter mechanics. I thought the numbers would be way better

Process:
Created the 'flaky server' and made the server 'fail' after 300 concurrent operations within 1 second.
RetryStorm.Client runs with 3 brainless retries. It is very consistent but it takes a while
RetryStormBackoff.Client is equally consistent with RetryStorm.Client but ironically takes longer. It implements backoff which may be helpful for more randomized requests, but for this "1000 requests in the same second" all that the backoff accomplishes is to make the clients wait for a really long time before doing retries. It has the same dependability but takes a lot longer
RetryStormJitter.Client is much faster than the previous runs, but less dependable. Due to random chance, there are a handful of requests that run through their 3 retries and do not ever successfully get a response. The failure rate is about 1 / 10. 
I tried RetryStormBackOffJitterV1.Client with 0-2, 0-4, and 0-8. This was faster than RetryStormBackoff, slower than RetryStormJitter, but way more dependable than just Jitter. 
Last try: RetryStormBackOffJitter.Client with 0-2, 2-4, and 4-8. That should make it so that there is actual backoff and jitter. This is the best option but only has marginal improvements over brainless retries.


DATA:
\Katas\AIFueledKatas\1ExponentialBackoff\src> dotnet run --project .\RetryStorm.Client\ -- --clients 1000
Sending 1000 requests to http://localhost:5000/work, with up to 3 retries after 503 responses or transport failures.
Finished in 6,659 ms.
200: 1000
503: 1200
\Katas\AIFueledKatas\1ExponentialBackoff\src> dotnet run --project .\RetryStormBackoff.Client\ -- --clients 1000
Sending 1000 requests to http://localhost:5000/work, with exponential retry delays of 2, 4, and 8 seconds.
Finished in 14,636 ms.
200: 1000
503: 1200
\Katas\AIFueledKatas\1ExponentialBackoff\src> dotnet run --project .\RetryStormJitter.Client\ -- --clients 1000
Sending 1000 requests to http://localhost:5000/work, with up to 3 retries using random delays from 0 to 2 seconds.
Finished in 4,731 ms.
200: 910
503: 1575
\Katas\AIFueledKatas\1ExponentialBackoff\src> dotnet run --project .\RetryStormBackOffJitterV1.Client\ -- --clients 1000
Sending 1000 requests to http://localhost:5000/work, with jittered exponential retry windows of 0-2, 0-4, and 0-8 seconds.
Finished in 10,895 ms.
200: 982
503: 1312
\Katas\AIFueledKatas\1ExponentialBackoff\src> dotnet run --project .\RetryStormBackOffJitter.Client\ -- --clients 1000
Sending 1000 requests to http://localhost:5000/work, with jittered exponential retry windows of 0-2, 2-4, and 4-8 seconds.
Finished in 10,985 ms.
200: 1000
503: 1181