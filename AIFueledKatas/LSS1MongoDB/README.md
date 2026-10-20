# MongoDB + Python kata

Run the database in Docker and the Python program on your Mac. No Atlas
account, cloud database, or manually entered database queries are needed.

## 1. Install and start Docker

Install [Docker Desktop for Mac](https://docs.docker.com/desktop/setup/install/mac-install/)
using the **Apple silicon** download for this Mac. Open Docker from Applications,
complete its setup prompts, and wait until the engine is running. Open a new
terminal if necessary, then check:

```bash
docker version
docker compose version
```

`docker version` must show both a Client and a Server. A missing command means
the CLI is not installed/on PATH; a connection error usually means Docker
Desktop is not running.

## 2. Prepare the code

Python 3.10 or newer is required (this Mac has Python 3.14). Poetry 2 or newer
manages the dependencies and creates an isolated environment in `.venv`.
If Poetry is not installed, install it with Homebrew:

```bash
brew install poetry
```

Then install this project's dependencies:

```bash
cd /Users/andrewpalmer/PersonalRepos/Katas/AIFueledKatas/LSS1MongoDB
poetry env use python3
poetry install
poetry run python main.py --help
```

In VS Code, run **Python: Select Interpreter** and select this folder's
`.venv/bin/python`. There is no need to manually activate the environment:
`poetry run` uses it automatically. Run all commands below from this folder.

[pyproject.toml](./pyproject.toml) declares the dependencies;
`poetry.lock` records the resolved versions for repeatable installs.
To add another library later, use `poetry add <package>`.

This project uses Microsoft's approved Python package source,
`https://packagefeedproxy.microsoft.io/pypi/simple`, configured as the primary
source in [pyproject.toml](./pyproject.toml). Poetry's implicit public PyPI source
is disabled; dependencies are resolved through the Microsoft feed.

Read [main.py](./main.py). The PyMongo driver connects to port 27017
on your Mac, selects the `learning_mongodb` database, then selects a collection
(roughly comparable to a SQL table). In Python, documents are dictionaries;
MongoDB generates an `_id` for each inserted task.

## 3. Start MongoDB without named volumes

```bash
docker compose -f compose.yaml up -d --wait --wait-timeout 180
poetry run python main.py crud
```

The first startup downloads the official Atlas Local image and can take a few
minutes. Docker runs it in the background (`-d`); `--wait` waits for the image's
built-in health check before returning.

The program inserts a task, queries it by `_id`, marks it completed with `$set`,
and deletes it. Assertions verify each result, including that the deleted task
is no longer queryable. It deliberately leaves no task behind.

Create a separate marker document to use for the persistence experiment:

```bash
poetry run python main.py seed
poetry run python main.py list
```

`seed` uses an upsert: insert the marker if absent, otherwise keep it unchanged.
Its original timestamp helps show whether you are reading the same document.
**Do not run `seed` between a restart/recreation and a verification**, since
that would recreate a missing marker and hide data loss.

## 4. Kill and restart the same container

Predict the result, then run:

```bash
docker compose -f compose.yaml kill mongodb
docker compose -f compose.yaml up -d --wait --wait-timeout 180
poetry run python main.py verify-present
```

The marker should survive. Killing a container stops its process; it does not
remove its filesystem or detach its existing storage.

Now remove the container **and its attached anonymous volumes**, then create
a new one:

```bash
docker compose -f compose.yaml down -v
docker compose -f compose.yaml up -d --wait --wait-timeout 180
poetry run python main.py verify-empty
```

This time the marker should be absent. MongoDB images can create anonymous
volumes even when you did not declare named ones. The `-v` makes removal of
those attached volumes explicit, rather than leaving orphaned data behind.
These commands target only this kata's Compose project.

## 5. Repeat with named volumes

Remove the empty experimental container first:

```bash
docker compose -f compose.yaml down -v
docker compose -f compose.yaml -f compose.persist.yaml up -d --wait --wait-timeout 180
poetry run python main.py crud
poetry run python main.py seed
```

The second Compose file adds named storage for database data, configuration,
and Atlas Search. Docker manages these volumes separately from the container.

Test killing and restarting again:

```bash
docker compose -f compose.yaml -f compose.persist.yaml kill mongodb
docker compose -f compose.yaml -f compose.persist.yaml up -d --wait --wait-timeout 180
poetry run python main.py verify-present
```

Then remove and recreate the container, **keeping the named volumes**:

```bash
docker compose -f compose.yaml -f compose.persist.yaml down
docker compose -f compose.yaml -f compose.persist.yaml up -d --wait --wait-timeout 180
poetry run python main.py verify-present
```

The marker should survive both experiments. Recreating the container attaches
the same named volumes. Use both `-f` arguments for all persistent-mode commands.

## 6. Stop or reset

Stop and remove the persistent container, but keep your data:

```bash
docker compose -f compose.yaml -f compose.persist.yaml down
```

**Destructive reset, only when you want to delete this kata's database:**

```bash
docker compose -f compose.yaml -f compose.persist.yaml down -v
```

## Troubleshooting and learning notes

- Inspect startup failures with `docker compose -f compose.yaml logs --tail 100 mongodb`.
  In persistent mode, include `-f compose.persist.yaml` as well.
- If port 27017 is already in use, stop the conflicting database or change both
  the host port in [compose.yaml](./compose.yaml) and the connection URI in
  [main.py](./main.py).
- A connection failure exits the program with an error after about 10 seconds.
  Start the database and wait for healthy status before running code.
- This database has **no authentication** and is bound to `127.0.0.1` for a
  local-only exercise. Do not expose it to a network or store sensitive data.
- The image uses `latest` for this learning exercise. Production environments
  should pin a tested image version or digest.
- A volume is not a backup: deleting a document also deletes it from the
  database stored in that volume.
- Try changing the task fields, querying by `completed`, and inserting several
  tasks with `insert_many` once the basic workflow works.
- Run Python normally, without `-O`, so the exercise's verification assertions
  remain enabled.

Reference: [MongoDB's official Atlas Local Docker guide](https://www.mongodb.com/docs/atlas/cli/current/atlas-cli-deploy-docker/).
