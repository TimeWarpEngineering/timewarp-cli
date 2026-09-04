# Round 1 — general
**Date:** 2026-09-04
**Scope reviewed:** branch task/104-harden-native-file-system-operations vs origin/master (source/ and tests/)

## Summary

The change hardens `Direct.RemoveItem` force-remove against reparse-point follow (M12/M13), aligns missing-path `force: true` with bash `rm -f`, wires `[EnumeratorCancellation]` into GetContent/GetChildItem, and replaces Commands sync-over-async with sync BCL reads/enumeration while documenting Cd’s process-global cwd race. Product risk looks low: BCL `Directory.Delete(recursive: true)` already detaches name-surrogate reparse points without walking targets (confirmed against docs, runtime source, and a local Linux experiment), and Unix `File.Exists` returns true for dangling symlinks so force-delete still removes them. Remaining gaps are test coverage quality, not incorrect production logic.

## Issues

### Issue 1 — Severity: suggestion
- File: tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs:61-64
- Description: The three symlink-safety tests (`FileSymlinkWithForce_Should_DeleteLinkWithoutMutatingTarget`, `DirectoryWithOutsideSymlink_ForceRecursive_Should_LeaveOutsideIntact`, `CyclicDirectorySymlink_ForceRecursive_Should_Complete`) catch `IOException` / `UnauthorizedAccessException` from `CreateSymbolicLink` and `return`, which is a vacuous green pass. On hosts where symlink creation is denied (typical Windows without Developer Mode / elevation), the core M12/reparse hardening claims of this task are never asserted. The repo already has an explicit `[Skip("…")]` pattern elsewhere.
- Suggestion: Prefer `[Skip("…")]` or fail/skip with a visible reason when link creation is unavailable, so CI cannot silently claim symlink coverage that did not run.
- Status: open

### Issue 2 — Severity: suggestion
- File: source/timewarp-amuru/native/file-system/direct/Direct.GetChildItem.cs:24-37
- Description: `GetChildItem` gained `[EnumeratorCancellation]` and per-iteration `ThrowIfCancellationRequested` (WithCancellation is effective for multi-entry directories; verified locally), matching GetContent. There is a cancellation test for GetContent but none for GetChildItem, so a future regression that drops the token parameter or checks would not be caught.
- Suggestion: Add a Direct.GetChildItem cancellation test parallel to `Cancellation_Should_ThrowOperationCanceledException` in `direct.get-content.cs` (cancel after the first entry in a multi-entry temp directory).
- Status: open
