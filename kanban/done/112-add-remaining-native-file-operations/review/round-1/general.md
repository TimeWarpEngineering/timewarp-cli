# Round 1 — general
**Date:** 2026-09-04
**Scope reviewed:** branch task/112-add-remaining-native-file-operations vs origin/master (776feae)

## Summary

Additive 1.1.0 native FS surface (CopyItem/MoveItem/NewItem/TestPath/FindItem/GetItemProperty, GetChildItem globbing, Bash aliases) matches the Commands/Direct split, path normalization, reparse-skip walks, and streaming-name conventions from task 104. Overall risk is moderate: happy paths and common errors are well covered, including symlink-child skip on copy and FindCriteria filters. The main concerns are a documented TestPath contract hole (invalid `ItemType` swallowed) and destructive delete-before-move on directory overwrite.

## Issues

### Issue 1 — Severity: bug
- File: source/timewarp-amuru/native/file-system/direct/Direct.TestPath.cs:56
- Description: `TestPath(string, ItemType)` documents and attempts to throw `ArgumentOutOfRangeException` for an unknown `itemType`, but the `catch (Exception)` around the switch returns `false` instead. `Commands.TestPath` / `Bash.Test` then report exit 1 / failure rather than an invalid-argument failure. The CA1031 suppression is justified for malformed paths, not for invalid enum values.
- Suggestion: Validate `itemType` before the try/catch (or catch only path-related exceptions such as `ArgumentException`, `NotSupportedException`, `PathTooLongException`, `System.Security.SecurityException`). Add a Direct (and optionally Commands) test that `(ItemType)999` throws / maps to a non-“missing path” error.
- Status: open

### Issue 2 — Severity: bug
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs:90
- Description: `MoveDirectory` with `overwrite: true` deletes an existing file or directory at the final destination before `Directory.Move`. The public docs say overwrite “replaces an existing file,” but the code also recursively `RemoveItem`s an existing directory. If `Directory.Move` then fails (permissions, in-use entry, or cross-volume falling into copy), the original destination contents are already gone; a mid-copy failure leaves a partial tree and no original. Same-volume file-at-final-path overwrite has the same delete-first hazard.
- Suggestion: Do not destroy the destination until the source is safely relocated. Options: refuse directory overwrite to match the XML contract; or stage via copy-to-temp / move-into-place and only then remove the old destination and source. Prefer failing closed when `Directory.Exists(finalDestination)` unless overwrite of directories is explicitly documented and tested.
- Status: open

### Issue 3 — Severity: suggestion
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs:83
- Description: Cross-volume fallback is `CopyItem` then `RemoveItem`. If copy succeeds and delete fails (or delete stops mid-tree), the caller gets an exception with a full duplicate at the destination and a partial or full source left behind. If copy fails mid-tree, a partial destination remains. Acceptable as a known limitation without a two-volume fixture, but worth documenting on `MoveItem` and considering best-effort cleanup of a partial destination on copy failure.
- Suggestion: Document the copy+delete fallback and non-atomic failure modes on the public API. On copy failure during fallback, attempt to remove the incomplete destination when it was created by this call. Leave source deletion failure as a thrown error after a successful copy (current behavior) but mention duplicate-data risk in XML docs.
- Status: open
