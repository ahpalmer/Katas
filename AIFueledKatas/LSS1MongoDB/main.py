import argparse
import logging
import sys
from datetime import datetime, timezone
from pprint import pprint
from typing import NotRequired, TypedDict

from bson import ObjectId
from pymongo import MongoClient
from pymongo.collection import Collection
from pymongo.errors import PyMongoError


class Task(TypedDict):
    _id: NotRequired[ObjectId]
    title: str
    completed: bool


class PersistenceProbe(TypedDict):
    _id: str
    created_at: datetime


def run_crud(tasks: Collection[Task]) -> None:
    # MongoDB creates the collection on its first write.
    inserted = tasks.insert_one(
        {"title": "Learn MongoDB with Python", "completed": False}
    )
    task_filter = {"_id": inserted.inserted_id}
    print("CREATE: inserted task", inserted.inserted_id)

    task = tasks.find_one(task_filter)
    assert task is not None, "The inserted task must be queryable"
    assert task["title"] == "Learn MongoDB with Python"
    assert task["completed"] is False
    print("READ:")
    pprint(task)

    updated = tasks.update_one(task_filter, {"$set": {"completed": True}})
    assert updated.matched_count == 1
    assert updated.modified_count == 1
    completed_task = tasks.find_one(task_filter)
    assert completed_task is not None
    assert completed_task["completed"] is True
    print("UPDATE:")
    pprint(completed_task)

    deleted = tasks.delete_one(task_filter)
    assert deleted.deleted_count == 1
    assert tasks.find_one(task_filter) is None
    print("DELETE: confirmed the task is gone")
    print("All four CRUD operations passed.")


def main() -> None:
    parser = argparse.ArgumentParser(description="Local MongoDB learning kata")
    parser.add_argument(
        "command",
        choices=["crud", "seed", "list", "verify-present", "verify-empty"],
    )
    args = parser.parse_args()

    with MongoClient(
        "mongodb://127.0.0.1:27017/?directConnection=true",
        serverSelectionTimeoutMS=10_000,
    ) as client:
        db = client.get_database("learning_mongodb")
        tasks: Collection[Task] = db.get_collection("tasks")
        probes: Collection[PersistenceProbe] = db.get_collection("persistence_probe")
        probe_filter = {"_id": "survival-test"}

        match args.command:
            case "crud":
                run_crud(tasks)
            case "seed":
                probes.update_one(
                    probe_filter,
                    {"$setOnInsert": {"created_at": datetime.now(timezone.utc)}},
                    upsert=True,
                )
                print("Persistence marker:")
                pprint(probes.find_one(probe_filter))
            case "list":
                print("Tasks:")
                pprint(list(tasks.find()))
                print("Persistence markers:")
                pprint(list(probes.find()))
            case "verify-present":
                probe = probes.find_one(probe_filter)
                assert probe is not None, (
                    "Expected the persistence marker to survive, but it is missing"
                )
                print("PASS: the persistence marker survived:")
                pprint(probe)
            case "verify-empty":
                assert probes.find_one(probe_filter) is None, (
                    "Expected a fresh database, but the persistence marker still exists"
                )
                print("PASS: the persistence marker is absent in the fresh database")


if __name__ == "__main__":
    try:
        main()
    except (PyMongoError, AssertionError):
        logging.exception(
            "MongoDB kata failed. Check the error below; for connection errors, "
            "start Docker and wait for MongoDB to become healthy."
        )
        sys.exit(1)
