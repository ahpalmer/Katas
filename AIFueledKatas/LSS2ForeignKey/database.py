"""Parameterized PostgreSQL CRUD operations; the caller owns the transaction."""

from dataclasses import dataclass
from datetime import datetime
from decimal import Decimal

from psycopg import Connection
from psycopg.rows import class_row


@dataclass(frozen=True)
class User:
    id: int
    name: str
    email: str


@dataclass(frozen=True)
class Order:
    id: int
    user_id: int
    total: Decimal
    created_at: datetime


def insert_user(connection: Connection, name: str, email: str) -> User:
    with connection.cursor(row_factory=class_row(User)) as cursor:
        cursor.execute(
            "INSERT INTO users (name, email) VALUES (%s, %s) "
            "RETURNING id, name, email",
            (name, email),
        )
        row = cursor.fetchone()
        if row is None:
            raise RuntimeError("User insert did not return a row.")
        return row


def query_users(
    connection: Connection, *, user_id: int | None = None, limit: int = 100
) -> list[User]:
    if limit < 1:
        raise ValueError("limit must be positive.")
    with connection.cursor(row_factory=class_row(User)) as cursor:
        cursor.execute(
            "SELECT id, name, email FROM users "
            "WHERE (%s::bigint IS NULL OR id = %s) ORDER BY id LIMIT %s",
            (user_id, user_id, limit),
        )
        return cursor.fetchall()


def update_user(
    connection: Connection, user_id: int, name: str, email: str
) -> User | None:
    with connection.cursor(row_factory=class_row(User)) as cursor:
        cursor.execute(
            "UPDATE users SET name = %s, email = %s WHERE id = %s "
            "RETURNING id, name, email",
            (name, email, user_id),
        )
        return cursor.fetchone()


def delete_user(connection: Connection, user_id: int) -> int:
    with connection.cursor() as cursor:
        cursor.execute("DELETE FROM users WHERE id = %s", (user_id,))
        return cursor.rowcount


def insert_order(connection: Connection, user_id: int, total: Decimal) -> Order:
    with connection.cursor(row_factory=class_row(Order)) as cursor:
        cursor.execute(
            "INSERT INTO orders (user_id, total) VALUES (%s, %s) "
            "RETURNING id, user_id, total, created_at",
            (user_id, total),
        )
        row = cursor.fetchone()
        if row is None:
            raise RuntimeError("Order insert did not return a row.")
        return row


def query_orders(
    connection: Connection,
    *,
    order_id: int | None = None,
    user_id: int | None = None,
    limit: int = 100,
) -> list[Order]:
    if limit < 1:
        raise ValueError("limit must be positive.")
    with connection.cursor(row_factory=class_row(Order)) as cursor:
        cursor.execute(
            "SELECT id, user_id, total, created_at FROM orders "
            "WHERE (%s::bigint IS NULL OR id = %s) "
            "AND (%s::bigint IS NULL OR user_id = %s) ORDER BY id LIMIT %s",
            (order_id, order_id, user_id, user_id, limit),
        )
        return cursor.fetchall()


def update_order(
    connection: Connection, order_id: int, user_id: int, total: Decimal
) -> Order | None:
    with connection.cursor(row_factory=class_row(Order)) as cursor:
        cursor.execute(
            "UPDATE orders SET user_id = %s, total = %s WHERE id = %s "
            "RETURNING id, user_id, total, created_at",
            (user_id, total, order_id),
        )
        return cursor.fetchone()


def delete_order(connection: Connection, order_id: int) -> int:
    with connection.cursor() as cursor:
        cursor.execute("DELETE FROM orders WHERE id = %s", (order_id,))
        return cursor.rowcount
