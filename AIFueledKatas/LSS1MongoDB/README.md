# Local MongoDB kata

Python runs on Windows; MongoDB runs in Docker. Dependencies are managed
with uv, using this project's local `.venv` directory.

## Start MongoDB

Run these commands in PowerShell from this directory:

```powershell
uv sync --locked
docker version
docker compose -f compose.local.yaml up -d --wait --wait-timeout 240
```

The first run downloads the official `mongodb/mongodb-atlas-local:8.0` image.
Compose waits for its built-in health check. No Atlas account is required.
MongoDB listens only on `127.0.0.1:27017`, with no authentication. This is for
local learning only: do not expose it to a network or store sensitive data.
The `8.0` tag receives updates; pin a tested digest for reproducible image builds.

If Docker is unavailable, install
[Docker Desktop for Windows](https://docs.docker.com/desktop/setup/install/windows-install/),
enable its WSL 2 backend and Linux containers, then start Docker Desktop.
`docker version` must show both Client and Server before running Compose.
If startup fails, use `docker compose -f compose.local.yaml logs --tail 100 mongodb`.
If port 27017 is occupied, change the host port in `compose.local.yaml` and pass
the matching `--uri` to Python.

## Insert, query, update, and delete

```powershell
uv run main.py insert '{"_id":"task-1","title":"Learn MongoDB","completed":false}'
uv run main.py query
uv run main.py query --filter '{"completed":false}'
uv run main.py update --filter '{"_id":"task-1"}' --set '{"completed":true}'
uv run main.py query --filter '{"_id":"task-1"}'
uv run main.py delete --filter '{"_id":"task-1"}'
uv run main.py query --filter '{"_id":"task-1"}'
```

The final query returns `[]`. The first insert creates the database and collection
automatically. Omitting `_id` lets MongoDB generate an ObjectId. Output and filters
support MongoDB Extended JSON, including `{"_id":{"$oid":"..."}}`.
Update uses `$set`, preserving fields you did not specify. Update and delete
affect **one** matching document; their counts report when nothing matched.
Database errors are printed to stderr and exit nonzero.

`database.py` exposes `insert_document`, `query_documents`, `update_document`,
and `delete_document`. Each takes a PyMongo collection; write methods return
PyMongo result objects, and queries return a list of dictionaries. Driver errors
propagate to the caller. For example:

```python
from pymongo import MongoClient
from database import Document, insert_document, query_documents

with MongoClient[Document](
    "mongodb://127.0.0.1:27017/?directConnection=true",
    serverSelectionTimeoutMS=5_000,
    timeoutMS=10_000,
) as client:
    tasks = client["learning_mongodb"]["tasks"]
    inserted = insert_document(tasks, {"title": "Practice CRUD", "completed": False})
    print(query_documents(tasks, {"_id": inserted.inserted_id}))
```

The CLI defaults to database `learning_mongodb` and collection `tasks`.
Global options go before the command:

```powershell
uv run main.py --database sandbox --collection notes query
uv run main.py --uri 'mongodb://127.0.0.1:27017/?directConnection=true' query
```

`MONGODB_URI` also overrides the default URI. `uv run main.py --help` lists options.
uv selects the local environment without manual activation.
Commit `uv.lock`, but do not commit `.venv` or `__pycache__`.

Optional PowerShell activation: `.\.venv\Scripts\Activate.ps1`.
If script execution is restricted, use `uv run` instead of changing the policy.

## Experiment 1: without named volumes

The base Compose file does not declare volumes. The image may still create
anonymous volumes; inspect the actual mounts:

```powershell
docker inspect lss1mongodb-mongodb-1 --format '{{json .Mounts}}'
uv run main.py insert '{"_id":"survival-test","title":"Persistence marker"}'
docker compose -f compose.local.yaml kill mongodb
docker compose -f compose.local.yaml up -d --wait --wait-timeout 240
uv run main.py query --filter '{"_id":"survival-test"}'
```

The marker survives: killing a process does not remove the container or its
storage. Do not reinsert the marker before checking, since that would hide loss.

Now remove this kata's container **and attached anonymous volumes**, then start
a fresh container. This intentionally deletes this experiment's data:

```powershell
docker compose -f compose.local.yaml down -v
docker compose -f compose.local.yaml up -d --wait --wait-timeout 240
uv run main.py query --filter '{"_id":"survival-test"}'
```

The query should return `[]`.

## Experiment 2: with named volumes

Remove the empty experimental container, then add the volume override:

```powershell
docker compose -f compose.local.yaml down -v
docker compose -f compose.local.yaml -f compose.volumes.yaml up -d --wait --wait-timeout 240
uv run main.py insert '{"_id":"survival-test","title":"Persistent marker"}'
docker compose -f compose.local.yaml -f compose.volumes.yaml kill mongodb
docker compose -f compose.local.yaml -f compose.volumes.yaml up -d --wait --wait-timeout 240
uv run main.py query --filter '{"_id":"survival-test"}'
docker compose -f compose.local.yaml -f compose.volumes.yaml down
docker compose -f compose.local.yaml -f compose.volumes.yaml up -d --wait --wait-timeout 240
uv run main.py query --filter '{"_id":"survival-test"}'
```

The marker survives both killing and replacing the container. Named volumes keep
database files, configuration, and Search data at `/data/db`, `/data/configdb`,
and `/data/mongot`. Always include both `-f` arguments in this mode.

Stop and remove the container while retaining these volumes:

```powershell
docker compose -f compose.local.yaml -f compose.volumes.yaml down
```

**Destructive reset:** add `-v` to that command only when you want to delete this
kata's named volumes and all their data. A volume is not a backup: deleting a
document also removes it from the database stored in the volume.

## Tests

```powershell
uv run --locked python -m unittest -v
$env:MONGODB_INTEGRATION = "1"
uv run --locked python -m unittest -v
Remove-Item Env:\MONGODB_INTEGRATION
```

Unit tests need no database. The opt-in integration test exercises real CRUD,
duplicate keys, unchanged and unmatched updates, and missing-document deletion.
It creates a uniquely named test collection and drops only that collection.

Reference: [MongoDB's official Atlas Local Compose guide](https://www.mongodb.com/docs/atlas/cli/current/atlas-cli-docker-compose/).