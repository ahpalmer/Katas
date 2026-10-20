import argparse
import os
import unittest
from unittest.mock import MagicMock
from uuid import uuid4

from bson import ObjectId
from pymongo import MongoClient
from pymongo.errors import DuplicateKeyError, OperationFailure

from database import (
    Document,
    delete_document,
    insert_document,
    query_documents,
    update_document,
)
from main import parse_document


class CrudTests(unittest.TestCase):
    def setUp(self) -> None:
        self.collection = MagicMock()

    def test_insert_copies_input_and_returns_driver_result(self) -> None:
        document = {"title": "Learn MongoDB"}
        result = insert_document(self.collection, document)
        self.collection.insert_one.assert_called_once_with(document)
        self.assertIsNot(self.collection.insert_one.call_args.args[0], document)
        self.assertIs(result, self.collection.insert_one.return_value)

    def test_query_defaults_to_all_documents(self) -> None:
        self.collection.find.return_value = iter([{"title": "Learn MongoDB"}])
        self.assertEqual(
            query_documents(self.collection), [{"title": "Learn MongoDB"}]
        )
        self.collection.find.assert_called_once_with({})

    def test_query_passes_filter(self) -> None:
        query_documents(self.collection, {"completed": False})
        self.collection.find.assert_called_once_with({"completed": False})

    def test_update_sets_fields_and_returns_driver_result(self) -> None:
        result = update_document(
            self.collection, {"_id": "task"}, {"completed": True}
        )
        self.collection.update_one.assert_called_once_with(
            {"_id": "task"}, {"$set": {"completed": True}}
        )
        self.assertIs(result, self.collection.update_one.return_value)

    def test_empty_update_is_rejected(self) -> None:
        with self.assertRaises(ValueError):
            update_document(self.collection, {"_id": "task"}, {})
        self.collection.update_one.assert_not_called()

    def test_delete_returns_driver_result(self) -> None:
        result = delete_document(self.collection, {"_id": "task"})
        self.collection.delete_one.assert_called_once_with({"_id": "task"})
        self.assertIs(result, self.collection.delete_one.return_value)

    def test_driver_errors_are_not_hidden(self) -> None:
        self.collection.insert_one.side_effect = DuplicateKeyError("duplicate")
        with self.assertRaises(DuplicateKeyError):
            insert_document(self.collection, {"_id": "task"})

    def test_extended_json_can_address_object_ids(self) -> None:
        identifier = ObjectId()
        self.assertEqual(
            parse_document(f'{{"_id": {{"$oid": "{identifier}"}}}}'),
            {"_id": identifier},
        )

    def test_invalid_documents_are_rejected(self) -> None:
        for value in ["[]", "null", "1", "not-json"]:
            with self.subTest(value=value):
                with self.assertRaises(argparse.ArgumentTypeError):
                    parse_document(value)


@unittest.skipUnless(
    os.environ.get("MONGODB_INTEGRATION") == "1",
    "Set MONGODB_INTEGRATION=1 to test against a running MongoDB.",
)
class MongoIntegrationTests(unittest.TestCase):
    def test_crud_against_real_mongodb(self) -> None:
        with MongoClient[Document](
            os.environ.get(
                "MONGODB_URI", "mongodb://127.0.0.1:27017/?directConnection=true"
            ),
            serverSelectionTimeoutMS=5_000,
            timeoutMS=10_000,
        ) as client:
            collection = client["learning_mongodb"][f"test_{uuid4().hex}"]
            try:
                document = {"title": "Learn MongoDB", "completed": False}
                inserted = insert_document(collection, document)
                self.assertNotIn("_id", document)
                query = {"_id": inserted.inserted_id}
                self.assertEqual(
                    query_documents(collection, query)[0]["title"], "Learn MongoDB"
                )
                updated = update_document(collection, query, {"completed": True})
                self.assertEqual(
                    (updated.matched_count, updated.modified_count), (1, 1)
                )
                self.assertTrue(query_documents(collection, query)[0]["completed"])
                self.assertEqual(
                    query_documents(collection, query)[0]["title"], "Learn MongoDB"
                )
                unchanged = update_document(collection, query, {"completed": True})
                self.assertEqual(unchanged.modified_count, 0)
                with self.assertRaises(DuplicateKeyError):
                    insert_document(collection, {"_id": inserted.inserted_id})
                with self.assertRaises(OperationFailure):
                    update_document(collection, query, {"_id": "different"})
                self.assertEqual(delete_document(collection, query).deleted_count, 1)
                self.assertEqual(query_documents(collection, query), [])
                self.assertEqual(delete_document(collection, query).deleted_count, 0)
                self.assertEqual(
                    update_document(
                        collection, query, {"completed": False}
                    ).matched_count,
                    0,
                )
            finally:
                collection.drop()


if __name__ == "__main__":
    unittest.main()
