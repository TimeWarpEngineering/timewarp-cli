# Round 2 — merged findings
**Date:** 2026-09-04
**Sources:** general

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 3 | 0 |
| suggestion | 0 | 1 | 0 |
| nit | 0 | 0 | 0 |

## Issues

### M1 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.TestPath.cs
- Description: Invalid `ItemType` was swallowed by `catch (Exception)` and returned false.
- Source: general
- Disposition notes: Round 2 confirmed throw-before-try and Direct tests.

### M2 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs
- Description: `MoveDirectory` deleted an existing destination before `Directory.Move`.
- Source: general
- Disposition notes: Round 2 confirmed refuse-if-exists; marker-file test holds.

### M3 — Severity: suggestion — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs
- Description: Cross-volume copy+delete failure modes were undocumented.
- Source: general
- Disposition notes: Round 2 confirmed XML remarks + Design region.

### M4 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.CopyItem.cs
- Description: Recursive copy skipped Hidden/System (Unix dotfiles).
- Source: orchestrator
- Disposition notes: Round 2 confirmed shared `CreateEnumerationOptions` and `.secret` copy test.

## Duplicates / conflicts

- None. No new findings in round 2.
