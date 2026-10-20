"""Reusable CRUD operations on a PyMongo collection."""

from collections.abc import Mapping
from typing import Any

from pymongo.collection import Collection
from pymongo.results import DeleteResult, InsertOneResult, UpdateResult

type Document = dict[str, Any]


def insert_document(
    collection: Collection[Document], document: Mapping[str, Any]
) -> InsertOneResult:
    """Insert a document; MongoDB creates the collection on the first write."""
    return collection.insert_one(dict(document))


def query_documents(
    collection: Collection[Document], query: Mapping[str, Any] | None = None
) -> list[Document]:
    """Return all documents matching a MongoDB filter, or all documents."""
    return list(collection.find({} if query is None else query))


def update_document(
    collection: Collection[Document],
    query: Mapping[str, Any],
    fields: Mapping[str, Any],
) -> UpdateResult:
    """Set fields on the first matching document without replacing other fields."""
    if not fields:
        raise ValueError("Provide at least one field to update.")
    return collection.update_one(query, {"$set": dict(fields)})


def delete_document(
    collection: Collection[Document], query: Mapping[str, Any]
) -> DeleteResult:
    """Delete the first matching document."""
    return collection.delete_one(query)
