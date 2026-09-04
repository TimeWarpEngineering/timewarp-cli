# Round 2 — merged findings
**Date:** 2026-09-04
**Sources:** general

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 0 | 0 |
| suggestion | 0 | 2 | 0 |
| nit | 0 | 0 | 0 |

Final open count: 0. Counts carry round-1 M1/M2 as fixed; round 2 raised no new findings.

## Issues

### M1 — Severity: suggestion — Status: fixed
- File: tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs:61-64
- Description: Symlink-safety tests silent-returned when `CreateSymbolicLink` failed.
- Suggestion: Throw with a visible Unix / Windows Developer Mode requirement.
- Source: general (round 1)
- Disposition notes: Re-verified in round 2. All three tests throw `InvalidOperationException` instead of returning. Independent run: 6/6 passed.

### M2 — Severity: suggestion — Status: fixed
- File: tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-child-item.cs
- Description: GetChildItem cancellation had no test.
- Suggestion: Add a Direct.GetChildItem cancellation test parallel to GetContent.
- Source: general (round 1)
- Disposition notes: Re-verified in round 2. New file cancels after the first of five entries. Independent run: 1/1 passed.

## Resolved prior

Round-1 M1 and M2 remain fixed; no reopen.

## Duplicates / conflicts

- None.
