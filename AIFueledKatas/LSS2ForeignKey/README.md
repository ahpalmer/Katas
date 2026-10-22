# Local PostgreSQL foreign-key kata

Entry 2 of `LEARNINGSPECIFICSKILLS.md`: run PostgreSQL in Docker, create
users and orders, seed about 1,000 rows, and deliberately violate referential
integrity. No PostgreSQL installation or SQL client on Windows is needed.
Here, users are application records, not database login accounts.

## Python project setup

Python runs on Windows and connects to PostgreSQL in Docker using Psycopg 3.
Dependencies are managed with uv in this project's local `.venv` directory.
Run all Python commands from this directory:

```powershell
uv sync --locked
```

uv creates `.venv` automatically; `uv run` uses it without manual activation.
Optional PowerShell activation: `.\.venv\Scripts\Activate.ps1`.
Commit `uv.lock`, but not `.venv` or `__pycache__`.

## Start Docker Desktop and PostgreSQL

Run these commands in PowerShell from this directory:

```powershell
Start-Process -FilePath 'C:\Program Files\Docker\Docker\Docker Desktop.exe'
docker version
```

Wait until `docker version` shows both Client and Server. Docker Desktop must
use Linux containers; a missing `dockerDesktopLinuxEngine` pipe usually means
the engine has not started.

Generate a local password and start the database:

```powershell
$env:POSTGRES_PASSWORD = [guid]::NewGuid().ToString('N')
docker compose -f compose.local.yaml up -d --wait --wait-timeout 180
```

The first run downloads the official `postgres:17` image. PostgreSQL listens
only on `127.0.0.1:5432`, and a named volume preserves its data. This is a local
learning environment using the default administrator account; do not expose it
to a network or store sensitive data. The `17` tag receives updates.

If startup fails:

```powershell
docker compose -f compose.local.yaml logs --tail 50 postgres
```

If port 5432 is occupied, change the mapping to `127.0.0.1:5433:5432`.
The commands below connect inside the container and need no port changes.

The password initializes a **new** volume only. If you open another PowerShell
session, set `POSTGRES_PASSWORD` again so Compose can read the configuration,
but generating a new value does not change an existing database's password.
The commands below use local socket access inside the container, which the
official image allows without a password by default.

Python connects over TCP instead, so it needs the database's actual password.
For the Docker container created by this Compose project, load that password
into this PowerShell session without printing it or saving it in source code:

```powershell
$container = docker inspect lss2foreignkey-postgres-1 | ConvertFrom-Json
$passwordEntry = $container[0].Config.Env | Where-Object { $_ -like 'POSTGRES_PASSWORD=*' }
if (-not $passwordEntry) { throw 'Container has no POSTGRES_PASSWORD setting.' }
$env:PGPASSWORD = $passwordEntry.Substring('POSTGRES_PASSWORD='.Length)
```

Repeat this in new PowerShell sessions before running Python. For another
PostgreSQL server, supply its password through `PGPASSWORD` or a PostgreSQL
password file instead. Do not commit credentials.

The Python defaults are host `127.0.0.1`, port `5432`, database
`learning_postgres`, and user `postgres`. Override them with `PGHOST`, `PGPORT`,
`PGDATABASE`, and `PGUSER`. If you changed the Docker host port to 5433, set
`$env:PGPORT = "5433"`.

## Create the schema

Read `schema.sql` before running it. Identify the primary keys, the foreign
key, and which table references the other.

```powershell
Get-Content -Raw .\schema.sql | docker compose -f compose.local.yaml exec -T postgres psql -X -U postgres -d learning_postgres -f -
```

Run this once against a fresh database. It does not drop or replace existing
tables. The SQL script stops on errors and creates the schema in a transaction.

## Seed 100 users and 1,000 orders

```powershell
Get-Content -Raw .\seed.sql | docker compose -f compose.local.yaml exec -T postgres psql -X -U postgres -d learning_postgres -f -
```

Run the seed once, immediately after creating the schema. Expected counts:
**100 users and 1,000 orders**, with ten orders per user. Re-running it fails
on duplicate emails rather than silently replacing your data.

## Explore the relationship

Open the interactive SQL client:

```powershell
docker compose -f compose.local.yaml exec postgres psql -X -U postgres -d learning_postgres
```

The following commands run in **psql**, not PowerShell. SQL statements end
with `;`. Inspect the tables with these psql commands:

```text
\d users
\d orders
```

Query the relationship:

```sql
SELECT u.id, u.name, count(o.id) AS order_count,
       sum(o.total) AS total_spent
FROM users AS u
JOIN orders AS o ON o.user_id = u.id
GROUP BY u.id, u.name
ORDER BY u.id
LIMIT 10;
```

Each displayed user should have ten orders.

## Intentionally break referential integrity

Predict the result before each command. Run these **separately**, without
wrapping them in `BEGIN`. Each should fail with a foreign-key violation
(SQLSTATE `23503`).

Create an order for a nonexistent user:

```sql
INSERT INTO orders (user_id, total) VALUES (999999, 25.00);
```

Change an existing order to reference a nonexistent user:

```sql
UPDATE orders SET user_id = 999999 WHERE id = 1;
```

Delete a user who still has orders:

```sql
DELETE FROM users WHERE id = 1;
```

The database rejects all three statements. We deliberately did not use
`ON DELETE CASCADE`: deleting a user must not automatically delete their orders.
If you instead try the failures inside an explicit transaction, run `ROLLBACK;`
after the first error before continuing.

Check that the failed statements left the data intact:

```sql
SELECT count(*) AS user_count FROM users;
SELECT count(*) AS order_count FROM orders;
SELECT user_id FROM orders WHERE id = 1;
SELECT count(*) AS orphan_count
FROM orders AS o
LEFT JOIN users AS u ON u.id = o.user_id
WHERE u.id IS NULL;
```

Expected: **100**, **1,000**, **1**, and **0**, respectively, on the fresh seed.

Now demonstrate a valid deletion without keeping its effects:

```sql
BEGIN;
DELETE FROM orders WHERE user_id = 1;
DELETE FROM users WHERE id = 1;
SELECT count(*) FROM users;
SELECT count(*) FROM orders;
ROLLBACK;
```

The counts inside the transaction are **99** and **990**. After `ROLLBACK`,
the original rows are restored. Explain why deleting the child rows first
worked, but deleting their parent first failed.

## Insert, query, update, and delete with Python

Create the schema and seed only if this is a fresh database. If they already
exist, skip those steps. The Python CLI does not recreate or reseed tables.
Exit psql with `\q` before running these commands in PowerShell:

```powershell
uv run main.py users query --limit 5
uv run main.py users query --id 1
uv run main.py orders query --user-id 1 --limit 10
uv run main.py users insert --name "Practice User" --email "practice@example.com"
```

The insert prints the new user's `id`. Capture that ID, rather than assuming
the next sequence value:

```powershell
$userId = [long](Read-Host 'User ID returned by the insert')
uv run main.py users update $userId --name "Updated User" --email "updated-practice@example.com"
uv run main.py orders insert --user-id $userId --total 12.50
$orderId = [long](Read-Host 'Order ID returned by the insert')
uv run main.py orders query --id $orderId
uv run main.py orders update $orderId --user-id $userId --total 20.00
```

Now attempt to delete the user while their order exists:

```powershell
uv run main.py users delete $userId
```

Expected: an error containing SQLSTATE `23503` and a nonzero exit status.
Delete the child first, then its parent:

```powershell
uv run main.py orders delete $orderId
uv run main.py users delete $userId
```

Inserts return the complete new row. Queries return JSON arrays, ordered by ID,
with a default limit of 100; use `--limit` to request more. Updates require all
editable fields and return `updated` plus the resulting row. A nonexistent
update returns `updated: false`; a nonexistent delete returns `deleted_count: 0`.
Deletes affect only the specified ID and never cascade. Money is output as a
decimal string to preserve precision, and timestamps are ISO 8601 strings.

CLI writes commit only after the operation succeeds; database errors roll back,
print to stderr, and exit nonzero. Inputs use SQL parameters rather than string
interpolation. Run `uv run main.py --help` or, for example,
`uv run main.py orders insert --help` for command options.

`database.py` also exposes `insert_user`, `query_users`, `update_user`,
`delete_user`, and the corresponding four order functions. They accept a
Psycopg connection and leave transaction ownership to the caller:

```python
from decimal import Decimal

import psycopg

from database import insert_order, insert_user

with psycopg.connect(
    host="127.0.0.1",
    dbname="learning_postgres",
    user="postgres",
    connect_timeout=5,
) as connection:
    user = insert_user(connection, "Python User", "python@example.com")
    order = insert_order(connection, user.id, Decimal("12.50"))
    print(user, order)
```

`PGPASSWORD` supplies the password here too. The connection context commits both
inserts together, or rolls back both if an exception occurs. Constraint errors
propagate to callers; no function silently ignores them.

## Tests

```powershell
uv run --locked python -m unittest -v
$env:POSTGRES_INTEGRATION = "1"
uv run --locked python -m unittest -v
Remove-Item Env:\POSTGRES_INTEGRATION
```

Unit tests need no database. Opt-in integration tests need the same connection
settings as the CLI. They create uniquely named schemas inside transactions and
roll everything back on connection close, leaving the kata's tables untouched.
They cover CRUD, query filters, missing rows, SQL parameterization, foreign-key
failures, unique emails, and positive order totals.

## Stop and resume

Exit psql with `\q`, then run in PowerShell:

```powershell
docker compose -f compose.local.yaml stop
docker compose -f compose.local.yaml start --wait
```

Reopen psql and check the counts to confirm persistence. To remove this kata's
container while retaining its volume:

```powershell
docker compose -f compose.local.yaml down
```

Use the original `up` command to recreate the container. Do not add `-v` to
`down` unless you intend to delete this kata's database volume and all its data.
Keep the database for entry 3, the indexing exercise.

## Prompt for your AI-assisted session

> Coach me through this README one checkpoint at a time. Have me predict each
> foreign-key failure before executing it and explain the actual error. Do not
> disable constraints, add cascading deletes, reset existing data, or move on
> to indexing.

Reference: [Official PostgreSQL Docker image](https://hub.docker.com/_/postgres).
