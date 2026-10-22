"""Insert, query, update, and delete PostgreSQL users and orders."""

import argparse
import json
import os
import sys
from dataclasses import asdict
from datetime import datetime
from decimal import Decimal, InvalidOperation

import psycopg

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


def positive_id(value: str) -> int:
    try:
        number = int(value)
    except ValueError as error:
        raise argparse.ArgumentTypeError("Expected a positive integer.") from error
    if number < 1 or number > 2**63 - 1:
        raise argparse.ArgumentTypeError("Expected a positive PostgreSQL bigint.")
    return number


def money(value: str) -> Decimal:
    try:
        amount = Decimal(value)
    except InvalidOperation as error:
        raise argparse.ArgumentTypeError("Expected a decimal amount.") from error
    if (
        not amount.is_finite()
        or amount <= 0
        or amount > Decimal("99999999.99")
        or amount != amount.quantize(Decimal("0.01"))
    ):
        raise argparse.ArgumentTypeError(
            "Amount must be positive, at most 99999999.99, "
            "and have no fractional cents."
        )
    return amount


def json_value(value: object) -> str:
    if isinstance(value, Decimal):
        return str(value)
    if isinstance(value, datetime):
        return value.isoformat()
    raise TypeError(f"Cannot serialize {type(value).__name__}.")


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description=__doc__)
    tables = parser.add_subparsers(dest="table", required=True)
    for table in ("users", "orders"):
        actions = tables.add_parser(table).add_subparsers(dest="action", required=True)
        for action in ("insert", "query", "update", "delete"):
            command = actions.add_parser(action)
            if action in ("update", "delete"):
                command.add_argument("id", type=positive_id)
            if action == "query":
                command.add_argument("--id", type=positive_id)
                command.add_argument("--limit", type=positive_id, default=100)
                if table == "orders":
                    command.add_argument("--user-id", type=positive_id)
            if action in ("insert", "update"):
                if table == "users":
                    command.add_argument("--name", required=True)
                    command.add_argument("--email", required=True)
                else:
                    command.add_argument("--user-id", type=positive_id, required=True)
                    command.add_argument("--total", type=money, required=True)
    return parser


def main() -> int:
    args = build_parser().parse_args()
    result: dict[str, object] | list[dict[str, object]]
    try:
        with psycopg.connect(
            host=os.environ.get("PGHOST", "127.0.0.1"),
            port=os.environ.get("PGPORT", "5432"),
            user=os.environ.get("PGUSER", "postgres"),
            dbname=os.environ.get("PGDATABASE", "learning_postgres"),
            connect_timeout=5,
            options="-c statement_timeout=10000",
        ) as connection:
            match (args.table, args.action):
                case ("users", "insert"):
                    result = asdict(insert_user(connection, args.name, args.email))
                case ("users", "query"):
                    result = [
                        asdict(row)
                        for row in query_users(
                            connection, user_id=args.id, limit=args.limit
                        )
                    ]
                case ("users", "update"):
                    updated_user = update_user(
                        connection, args.id, args.name, args.email
                    )
                    result = {
                        "updated": updated_user is not None,
                        "row": asdict(updated_user) if updated_user is not None else None,
                    }
                case ("users", "delete"):
                    result = {"deleted_count": delete_user(connection, args.id)}
                case ("orders", "insert"):
                    result = asdict(insert_order(connection, args.user_id, args.total))
                case ("orders", "query"):
                    result = [
                        asdict(row)
                        for row in query_orders(
                            connection,
                            order_id=args.id,
                            user_id=args.user_id,
                            limit=args.limit,
                        )
                    ]
                case ("orders", "update"):
                    updated_order = update_order(
                        connection, args.id, args.user_id, args.total
                    )
                    result = {
                        "updated": updated_order is not None,
                        "row": asdict(updated_order) if updated_order is not None else None,
                    }
                case ("orders", "delete"):
                    result = {"deleted_count": delete_order(connection, args.id)}
        print(json.dumps(result, default=json_value, indent=2))
    except psycopg.Error as error:
        code = f" (SQLSTATE {error.sqlstate})" if error.sqlstate else ""
        print(f"PostgreSQL operation failed{code}: {error}", file=sys.stderr)
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())
