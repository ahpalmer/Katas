Database practice:
1. MongoDB locally. Stand up MongoDB in Docker, create a collection, insert/query/update/delete data, kill the container, and discover whether your data survives. Then add a volume and repeat. MongoDB currently provides an official Atlas Local Docker image for exactly this kind of local workflow.
2. PostgreSQL locally. Start Postgres in Docker. Create users and orders, add a foreign key, seed ~1,000 rows, then intentionally violate referential integrity.
3. Database indexing. Take that Postgres DB to 100K+ rows. Query an unindexed column, inspect the query plan, add an index, inspect again. Then create an index that doesn't help and figure out why.
4. Transactions and isolation. Open two DB sessions against the same rows. Reproduce a lost update or other concurrency anomaly, then change the transaction/isolation strategy and see what changes.
5. Connection-pool exhaustion. Put a tiny API in front of Postgres. Give it a connection pool of 5 and hammer it with concurrent requests. Hold connections open intentionally and watch requests pile up.
6. Database migrations. Start with schema V1, insert real data, then migrate to V2 without dropping anything. Make the migration fail halfway through and determine what state you've created.
7. Stand up a cloud database. Provision PostgreSQL, MongoDB Atlas, Cosmos DB, etc. Connect from your laptop, insert/query data, then deliberately break network access or credentials and diagnose the failure.
8. Local → cloud migration. Seed a local database, export it, import it into your cloud instance, and verify record counts/checksums. Then make a schema mismatch and see how the migration fails.
9. Database backup + restore. Populate a database, take a backup, delete/corrupt some data, and restore it. The kata isn't complete until you prove the restored data is correct.
10. Primary/replica behavior. Run a replicated DB locally if practical. Write to the primary, read from a replica, kill the primary, and observe what happens during failover.

Kubernetes:
11. Your first Pod. Run nginx as a Pod. exec into it, inspect its filesystem/processes/networking, then delete it. Notice that Kubernetes does not bring a naked Pod back.
12. Pod → Deployment. Put nginx behind a Deployment with 3 replicas. Delete one Pod manually and watch Kubernetes replace it.
The question to answer:
Who noticed the Pod disappeared, and why did it come back?
13. Deployment → Service. Put those 3 Pods behind a Service. Repeatedly hit the Service and prove that Pod IP addresses can change without your client caring.
14. Rolling deployment. Deploy v1 of a tiny web API, then roll out v2 while continuously sending requests. Watch Pods transition between versions. Then roll back.
15. Break a readiness probe. Create working readiness/liveness probes. Intentionally make readiness fail and observe the difference between running, ready, and receiving traffic.
16. CrashLoopBackOff. Deploy an application that exits immediately. Diagnose it using only:
Fix it without deleting/recreating the Deployment.
17. ConfigMap vs Secret. Give an application configuration through environment variables, then through a ConfigMap, then a Secret. Change the configuration and determine when the running container actually sees the change.
18. Resource limits. Give a container an absurdly small memory limit and force it past the limit. Diagnose why it died. Then do the same experiment with CPU limits and observe the different behavior.
19. Persistent storage. Run Postgres in Kubernetes, write data, delete the Pod, and make sure the data survives. Then deliberately misunderstand the relationship between Pod, PVC, and PV and diagnose why your data disappeared.
20. Horizontal scaling. Put your tiny API under load and manually scale:
Watch Pods appear, then scale back down.
21. Kubernetes networking detective. Deploy client → API → database as three workloads. Break one network path and debug it from inside the cluster using DNS lookup, curl, and connection tests.
22. Build your own tiny production-ish stack.
Then sequentially:
kill an API Pod
kill Postgres
break readiness
deploy a bad image
remove a Secret
exhaust API memory
scale API to 0
For each failure, predict Kubernetes's behavior before causing it.

Graph Theory
23. Build a graph from scratch. Implement an adjacency-list graph with:
Do both directed and undirected versions.
24. BFS. Implement breadth-first search without looking up the algorithm. Use it to find the shortest path through an unweighted graph. Then create a disconnected graph and make sure you handle unreachable nodes.
25. DFS. Implement DFS recursively and iteratively. Create a graph with cycles and break your implementation by forgetting the visited set.
26. Cycle detection. Given a directed graph representing dependencies, detect whether adding a dependency creates a cycle.
Use something relatable:
Then accidentally add:
and detect it.
27. Topological sort. Model a build pipeline or package dependency tree as a DAG. Determine the valid execution order. Then introduce a cycle.
This one is particularly relevant to the build/pipeline systems you've been working with.
28. Dijkstra's algorithm. Model cities/servers/datacenters as nodes and latency as edge weights. Find the lowest-latency path. Increase the latency of one edge and watch the optimal path change.
29. A vs Dijkstra.* Make a 2D grid with obstacles. Implement both algorithms and compare how many nodes they explore to reach the same destination.
30. Connected components. Generate a graph containing several isolated clusters. Determine how many independent components exist and which nodes belong to each.
More interesting graph problems
31. Social-network degree analysis. Generate a fake social network and calculate degree distribution. Add one "celebrity" node and watch how the topology changes.
32. Detect bridges. Build a network with redundant paths. Find the edges whose removal splits the graph into disconnected components.
Think:
Which link is your single point of failure?
33. Strongly connected components. Create a directed service-dependency graph and determine which services can mutually reach one another.
34. Minimum spanning tree. Give every network connection a cost. Connect all datacenters while minimizing total cost.
Then remove one edge and recompute it.
35. Graph coloring. Model jobs as vertices where an edge means "these cannot run on the same worker." Assign workers while minimizing the number needed.
36. Max flow. Model services/network links with capacity limits.
Calculate maximum throughput.
Then reduce one capacity and find your bottleneck.
37. PageRank. Implement simplified PageRank on a tiny directed graph. Add one highly connected node and see what happens to ranking.
38. Six degrees experiment. Generate a random graph with thousands of vertices. Sample random pairs and calculate shortest-path lengths. See how surprisingly small the average becomes as edge density increases.
Graph database crossover
Then I'd combine your database and graph theory tracks.
39. Stand up Neo4j locally.
Model something you actually understand:
Write queries like:
"Which services indirectly depend on Service X?"
40. SQL vs graph database.
Put the same dataset in Postgres and Neo4j:
Ask:
Find every transitive dependency of Service A.
Implement it both ways and compare how you have to think about the problem.