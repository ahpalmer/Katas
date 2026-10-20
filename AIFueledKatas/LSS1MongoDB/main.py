"""Command-line CRUD playground for a local MongoDB database."""

import argparse
import os
import sys

from bson import json_util
from pymongo import MongoClient
from pymongo.errors import PyMongoError

from database import (
    Document,
    delete_document,
    insert_document,
    query_documents,
    update_document,
)


def parse_document(value: str) -> Document:
    try:
        document = json_util.loads(value)
    except (ValueError, TypeError, KeyError) as error:
        raise argparse.ArgumentTypeError(f"Invalid JSON: {error}") from error
    if not isinstance(document, dict):
        raise argparse.ArgumentTypeError("Expected a JSON object.")
    return document


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument(
        "--uri",
        default=os.environ.get(
            "MONGODB_URI", "mongodb://127.0.0.1:27017/?directConnection=true"
        ),
    )
    parser.add_argument("--database", default="learning_mongodb")
    parser.add_argument("--collection", default="tasks")
    commands = parser.add_subparsers(dest="command", required=True)
    insert = commands.add_parser("insert", help="Insert one JSON document")
    insert.add_argument("document", type=parse_document)
    query = commands.add_parser("query", help="Query matching documents")
    query.add_argument("--filter", type=parse_document, default={})
    update = commands.add_parser("update", help="Set fields on one matching document")
    update.add_argument("--filter", type=parse_document, required=True)
    update.add_argument("--set", dest="fields", type=parse_document, required=True)
    delete = commands.add_parser("delete", help="Delete one matching document")
    delete.add_argument("--filter", type=parse_document, required=True)
    args = parser.parse_args()
    if args.command == "update" and not args.fields:
        parser.error("--set requires at least one field.")

    try:
        with MongoClient[Document](
            args.uri, serverSelectionTimeoutMS=5_000, timeoutMS=10_000
        ) as client:
            collection = client[args.database][args.collection]
            result: Document | list[Document]
            match args.command:
                case "insert":
                    inserted = insert_document(collection, args.document)
                    result = {"inserted_id": inserted.inserted_id}
                case "query":
                    result = query_documents(collection, args.filter)
                case "update":
                    updated = update_document(collection, args.filter, args.fields)
                    result = {
                        "matched_count": updated.matched_count,
                        "modified_count": updated.modified_count,
                    }
                case "delete":
                    result = {
                        "deleted_count": delete_document(
                            collection, args.filter
                        ).deleted_count
                    }
            print(json_util.dumps(result, indent=2))
    except PyMongoError as error:
        print(
            f"MongoDB operation failed: {error}\n"
            "Check Docker and wait for MongoDB to be healthy before retrying.",
            file=sys.stderr,
        )
        return 1
    return 0


if __name__ == "__main__":
    sys.exit(main())