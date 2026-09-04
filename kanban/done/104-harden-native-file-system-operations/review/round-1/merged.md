# Round 1 — merged findings
**Date:** 2026-09-04
**Sources:** general

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 0 | 0 |
| suggestion | 0 | 2 | 0 |
| nit | 0 | 0 | 0 |

## Issues

### M1 — Severity: suggestion — Status: fixed
- File: tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs:61-64
- Description: The three symlink-safety tests (`FileSymlinkWithForce_Should_DeleteLinkWithoutMutatingTarget`, `DirectoryWithOutsideSymlink_ForceRecursive_Should_LeaveOutsideIntact`, `CyclicDirectorySymlink_ForceRecursive_Should_Complete`) catch `IOException` / `UnauthorizedAccessException` from `CreateSymbolicLink` and `return`, which is a vacuous green pass. On hosts where symlink creation is denied (typical Windows without Developer Mode / elevation), the core M12/reparse hardening claims of this task are never asserted. Jaribu has no runtime skip; `[Skip]` would disable the tests on Linux CI as well. Fail with a visible reason when link creation is unavailable so CI cannot silently claim coverage that did not run.
- Suggestion: Replace silent `return` with a throw that names the requirement (Unix host or Windows Developer Mode). Do not apply unconditional `[Skip]`.
- Source: general
- Disposition notes: Fixed. All three `CreateSymbolicLink` catch blocks throw `InvalidOperationException` with a Unix/Developer Mode message instead of returning. Linux run: 6/6 passed.

### M2 — Severity: suggestion — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.GetChildItem.cs:24-37
- Description: `GetChildItem` gained `[EnumeratorCancellation]` and per-iteration `ThrowIfCancellationRequested` (WithCancellation is effective for multi-entry directories). There is a cancellation test for GetContent but none for GetChildItem, so a future regression that drops the token parameter or checks would not be caught.
- Suggestion: Add `tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-child-item.cs` with a cancellation test parallel to `Cancellation_Should_ThrowOperationCanceledException` in `direct.get-content.cs` (cancel after the first entry in a multi-entry temp directory).
- Source: general
- Disposition notes: Fixed. New `direct.get-child-item.cs` cancels after the first entry in a 5-file temp dir. Standalone run: 1/1 passed.

## Duplicates / conflicts

- None. Two independent suggestions from the single general reviewer.
