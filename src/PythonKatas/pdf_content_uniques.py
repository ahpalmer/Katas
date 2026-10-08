from __future__ import annotations

import argparse
import hashlib
import json
from pathlib import Path
from typing import Iterable

from pypdf import PdfReader


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Group PDFs by content fingerprint instead of file name.",
    )
    parser.add_argument(
        "paths",
        nargs="+",
        type=Path,
        help="PDF files or directories containing PDFs.",
    )
    parser.add_argument(
        "--recursive",
        action="store_true",
        help="Search directories recursively.",
    )
    parser.add_argument(
        "--json",
        action="store_true",
        help="Emit machine-readable JSON instead of text output.",
    )
    return parser.parse_args()


def iter_pdf_paths(paths: Iterable[Path], recursive: bool) -> list[Path]:
    pdf_paths: list[Path] = []

    for path in paths:
        if path.is_file() and path.suffix.lower() == ".pdf":
            pdf_paths.append(path)
            continue

        if path.is_dir():
            pattern = "**/*.pdf" if recursive else "*.pdf"
            pdf_paths.extend(candidate for candidate in path.glob(pattern) if candidate.is_file())

    return sorted({candidate.resolve() for candidate in pdf_paths})


def normalize_text(text: str) -> str:
    return " ".join(text.split())


def page_stream_bytes(page) -> bytes:
    contents = page.get_contents()
    if contents is None:
        return b""

    if isinstance(contents, list):
        return b"".join(item.get_data() for item in contents)

    return contents.get_data()


def fingerprint_pdf(pdf_path: Path) -> str:
    reader = PdfReader(str(pdf_path))
    digest = hashlib.sha256()
    digest.update(f"pages:{len(reader.pages)}".encode("utf-8"))

    for page in reader.pages:
        page_text = normalize_text(page.extract_text() or "")
        digest.update(b"\x1ePAGE\x1e")

        if page_text:
            digest.update(b"TEXT")
            digest.update(page_text.encode("utf-8", errors="ignore"))
        else:
            digest.update(b"STREAM")
            digest.update(page_stream_bytes(page))

    return digest.hexdigest()


def group_pdfs(pdf_paths: Iterable[Path]) -> tuple[dict[str, list[str]], list[dict[str, str]]]:
    groups: dict[str, list[str]] = {}
    failures: list[dict[str, str]] = []

    for pdf_path in pdf_paths:
        try:
            fingerprint = fingerprint_pdf(pdf_path)
            groups.setdefault(fingerprint, []).append(str(pdf_path))
        except Exception as exc:  # pragma: no cover - throw-away utility
            failures.append({"path": str(pdf_path), "error": str(exc)})

    return groups, failures


def build_report(groups: dict[str, list[str]], failures: list[dict[str, str]]) -> dict[str, object]:
    duplicate_groups = {
        fingerprint: sorted(paths)
        for fingerprint, paths in sorted(groups.items(), key=lambda item: (len(item[1]), item[0]), reverse=True)
        if len(paths) > 1
    }
    unique_pdfs = sorted(paths[0] for paths in groups.values() if len(paths) == 1)

    return {
        "total_processed": sum(len(paths) for paths in groups.values()),
        "unique_content_count": len(groups),
        "unique_pdfs": unique_pdfs,
        "duplicate_groups": duplicate_groups,
        "failures": failures,
    }


def print_text_report(report: dict[str, object]) -> None:
    print(f"Processed: {report['total_processed']}")
    print(f"Unique content fingerprints: {report['unique_content_count']}")
    print()

    unique_pdfs = report["unique_pdfs"]
    print("Unique PDFs:")
    if unique_pdfs:
        for path in unique_pdfs:
            print(f"  {path}")
    else:
        print("  None")

    print()
    print("Duplicate groups:")
    duplicate_groups = report["duplicate_groups"]
    if duplicate_groups:
        for fingerprint, paths in duplicate_groups.items():
            print(f"  {fingerprint}")
            for path in paths:
                print(f"    {path}")
    else:
        print("  None")

    failures = report["failures"]
    if failures:
        print()
        print("Failures:")
        for failure in failures:
            print(f"  {failure['path']}: {failure['error']}")


def main() -> int:
    args = parse_args()
    pdf_paths = iter_pdf_paths(args.paths, recursive=args.recursive)

    if not pdf_paths:
        print("No PDF files found.")
        return 1

    groups, failures = group_pdfs(pdf_paths)
    report = build_report(groups, failures)

    if args.json:
        print(json.dumps(report, indent=2))
    else:
        print_text_report(report)

    return 0 if report["total_processed"] else 1


if __name__ == "__main__":
    raise SystemExit(main())