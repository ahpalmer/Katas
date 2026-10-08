Backoff and jitter look weak because the server model accidentally favors fixed retries.

Your server has highly predictable behavior:

It accepts exactly 300 requests.
It rejects everything for exactly one second.
It then immediately returns to full capacity.
Successful work finishes essentially instantly.
The fixed retry delay is two seconds, perfectly longer than recovery.
That makes synchronized retries unusually effective. Every two seconds, another batch gets exactly 300 successes:

Initial: 300 succeed
Retry 1: 300 succeed
Retry 2: 300 succeed
Retry 3: 100 succeed

Fixed retries therefore achieve a perfect result in only 2,200 attempts. Exponential backoff sends the same synchronized batches, merely later.

Jitter initially performs worse because some clients randomly retry during the server’s one-second rejection period. Those attempts are guaranteed to fail and consume their retry budget.

What Backoff And Jitter Solve

They are most valuable when:

Recovery time is unknown or variable.
Processing occupies resources for meaningful time.
Continued retries prolong the outage.
Many callers repeatedly hit a partially recovered service.
Capacity returns gradually rather than instantaneously.
Request arrivals and failures are not perfectly deterministic.
Multiple independent client populations are retrying.
The system has downstream dependencies that are also saturated.
Backoff reduces sustained retry pressure. Jitter prevents callers from synchronizing into recurring spikes.

Your conclusion that fixed retries are “probably fine” is broader than the experiment supports. They are fine here because the fixed two-second delay happens to exceed the known one-second recovery period. In production, that relationship is usually unknown and unstable.