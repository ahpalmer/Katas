"""Unit tests and opt-in PostgreSQL tests using rolled-back temporary schemas."""

import argparse
import contextlib
import io
import json
import os
from datetime import datetime, timezone
from decimal import Decimal
from pathlib import Path
import unittest
from unittest.mock import patch
from uuid import uuid4

import psycopg
from psycopg import sql

from database import (
    delete_order,
    delete_user,
    insert_order,
    insert_user,
    query_orders,
    query_users,
    update_order,
    update_user,
)
from main import build_parser, json_value, main, money, positive_id


class InputTests(unittest.TestCase):
    def test_positive_ids(self):
        self.assertEqual(positive_id("42"), 42)
        for value in ("0", "-1", "1.5", "abc", str(2**63)):
            with self.subTest(value=value):
                with self.assertRaises(argparse.ArgumentTypeError):
                    positive_id(value)

    def test_money(self):
        self.assertEqual(money("12.30"), Decimal("12.30"))
        self.assertEqual(money("99999999.99"), Decimal("99999999.99"))
        for value in ("0", "-1", "NaN", "Infinity", "1.001", "100000000", "abc"):
            with self.subTest(value=value):
                with self.assertRaises(argparse.ArgumentTypeError):
                    money(value)

    def test_json_types(self):
        timestamp = datetime(2026, 1, 1, tzinfo=timezone.utc)
        self.assertEqual(
            json.loads(json.dumps([Decimal("12.30"), timestamp], default=json_value)),
            ["12.30", timestamp.isoformat()],
        )
        with self.assertRaises(TypeError):
            json_value(object())

    def test_cli_requires_delete_id(self):
        with contextlib.redirect_stderr(io.StringIO()):
            with self.assertRaises(SystemExit) as error:
                build_parser().parse_args(["users", "delete"])
        self.assertEqual(error.exception.code, 2)

    def test_connection_failure_is_not_success(self):
        output = io.StringIO()
        with (
            patch("sys.argv", ["main.py", "users", "query"]),
            patch("main.psycopg.connect", side_effect=psycopg.OperationalError("offline")),
            contextlib.redirect_stderr(output),
        ):
            self.assertEqual(main(), 1)
        self.assertIn("offline", output.getvalue())


@unittest.skipUnless(
    os.environ.get("POSTGRES_INTEGRATION") == "1",
    "Set POSTGRES_INTEGRATION=1 to test against PostgreSQL.",
)
class DatabaseTests(unittest.TestCase):
    def setUp(self):
        self.connection = psycopg.connect(
            host=os.environ.get("PGHOST", "127.0.0.1"),
            port=os.environ.get("PGPORT", "5432"),
            user=os.environ.get("PGUSER", "postgres"),
            dbname=os.environ.get("PGDATABASE", "learning_postgres"),
            connect_timeout=5,
            options="-c statement_timeout=10000",
        )
        self.addCleanup(self.connection.close)
        schema = sql.Identifier("test_" + uuid4().hex)
        self.connection.execute(sql.SQL("CREATE SCHEMA {}").format(schema))
        self.connection.execute(
            sql.SQL("SET LOCAL search_path TO {}").format(schema)
        )
        script = Path(__file__).with_name("schema.sql").read_text(encoding="utf-8")
        statements = "\n".join(
            line
            for line in script.splitlines()
            if not line.startswith("\\") and line not in ("BEGIN;", "COMMIT;")
        )
        self.connection.execute(statements)

    def test_user_crud_and_parameterized_text(self):
        name = "O'Brien'); DROP TABLE users; --"
        user = insert_user(self.connection, name, "crud@example.com")
        self.assertEqual(user.name, name)
        self.assertEqual(query_users(self.connection, user_id=user.id), [user])
        updated = update_user(
            self.connection, user.id, "Updated", "updated@example.com"
        )
        self.assertIsNotNone(updated)
        self.assertEqual(query_users(self.connection), [updated])
        self.assertEqual(delete_user(self.connection, user.id), 1)
        self.assertEqual(query_users(self.connection), [])
        self.assertEqual(delete_user(self.connection, user.id), 0)
        self.assertIsNone(update_user(self.connection, user.id, "Missing", "x@example.com"))

    def test_order_crud_and_filters(self):
        user = insert_user(self.connection, "User", "orders@example.com")
        other = insert_user(self.connection, "Other", "other@example.com")
        order = insert_order(self.connection, user.id, Decimal("12.30"))
        self.assertEqual(order.total, Decimal("12.30"))
        self.assertEqual(
            query_orders(self.connection, order_id=order.id, user_id=user.id),
            [order],
        )
        self.assertEqual(query_orders(self.connection, user_id=other.id), [])
        updated = update_order(self.connection, order.id, other.id, Decimal("20.00"))
        self.assertEqual(query_orders(self.connection, user_id=other.id), [updated])
        self.assertEqual(delete_order(self.connection, order.id), 1)
        self.assertEqual(query_orders(self.connection), [])
        self.assertEqual(delete_order(self.connection, order.id), 0)
        self.assertIsNone(update_order(self.connection, order.id, user.id, Decimal("1")))

    def test_foreign_keys_and_rollback(self):
        user = insert_user(self.connection, "Parent", "parent@example.com")
        order = insert_order(self.connection, user.id, Decimal("10"))
        with self.assertRaises(psycopg.errors.ForeignKeyViolation):
            with self.connection.transaction():
                insert_order(self.connection, user.id + 1, Decimal("10"))
        with self.assertRaises(psycopg.errors.ForeignKeyViolation):
            with self.connection.transaction():
                update_order(self.connection, order.id, user.id + 1, Decimal("20"))
        with self.assertRaises(psycopg.errors.ForeignKeyViolation):
            with self.connection.transaction():
                delete_user(self.connection, user.id)
        self.assertEqual(query_users(self.connection), [user])
        self.assertEqual(query_orders(self.connection), [order])
        self.assertEqual(delete_order(self.connection, order.id), 1)
        self.assertEqual(delete_user(self.connection, user.id), 1)

    def test_unique_email_and_positive_total(self):
        user = insert_user(self.connection, "User", "unique@example.com")
        with self.assertRaises(psycopg.errors.UniqueViolation):
            with self.connection.transaction():
                insert_user(self.connection, "Duplicate", user.email)
        with self.assertRaises(psycopg.errors.CheckViolation):
            with self.connection.transaction():
                insert_order(self.connection, user.id, Decimal("0"))

    def test_limits_and_empty_results(self):
        insert_user(self.connection, "First", "first@example.com")
        insert_user(self.connection, "Second", "second@example.com")
        self.assertEqual(len(query_users(self.connection, limit=1)), 1)
        self.assertEqual(query_users(self.connection, user_id=999999), [])
        for query in (query_users, query_orders):
            with self.assertRaises(ValueError):
                query(self.connection, limit=0)


if __name__ == "__main__":
    unittest.main()
