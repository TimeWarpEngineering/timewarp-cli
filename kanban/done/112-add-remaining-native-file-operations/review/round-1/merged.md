# Round 1 — merged findings
**Date:** 2026-09-04
**Sources:** general, orchestrator

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 3 | 0 |
| suggestion | 0 | 1 | 0 |
| nit | 0 | 0 | 0 |

## Issues

### M1 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.TestPath.cs:56
- Description: `TestPath(string, ItemType)` documents and attempts to throw `ArgumentOutOfRangeException` for an unknown `itemType`, but the `catch (Exception)` around the switch returns `false` instead. `Commands.TestPath` / `Bash.Test` then report exit 1 / failure rather than an invalid-argument failure. The CA1031 suppression is justified for malformed paths, not for invalid enum values.
- Suggestion: Validate `itemType` before the try/catch (or catch only path-related exceptions such as `ArgumentException`, `NotSupportedException`, `PathTooLongException`, `System.Security.SecurityException`). Add a Direct test that `(ItemType)999` throws.
- Source: general
- Disposition notes: ItemType validated before try/catch; Direct test covers `(ItemType)999` and typed blank paths.

### M2 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs:90
- Description: `MoveDirectory` with `overwrite: true` deletes an existing file or directory at the final destination before `Directory.Move`. The public docs say overwrite “replaces an existing file,” but the code also recursively `RemoveItem`s an existing directory. If `Directory.Move` then fails, the original destination contents are already gone; a mid-copy failure leaves a partial tree and no original.
- Suggestion: Refuse directory overwrite (and directory-onto-file) to match the XML contract: throw `IOException` when the final destination already exists as a file or directory. Keep file-to-file overwrite on `File.Move(..., overwrite)`. Add a test that `overwrite: true` does not delete an existing destination directory.
- Source: general
- Disposition notes: MoveDirectory throws if final dest exists as file or directory; dest contents preserved. File-to-file overwrite unchanged.

### M3 — Severity: suggestion — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.MoveItem.cs:83
- Description: Cross-volume fallback is `CopyItem` then `RemoveItem`. If copy succeeds and delete fails, both locations exist. If copy fails mid-tree, a partial destination remains.
- Suggestion: Document the copy+delete fallback and non-atomic failure modes on the public `MoveItem` XML remarks and the Direct.MoveItem Design region. Do not add a two-volume fixture.
- Source: general
- Disposition notes: XML remarks + Design region document copy+delete and partial/dual-location failure modes.

### M4 — Severity: bug — Status: fixed
- File: source/timewarp-amuru/native/file-system/direct/Direct.CopyItem.cs:136
- Description: `CopyDirectory` uses `EnumerateFiles()` / `EnumerateDirectories()` with default `EnumerationOptions.AttributesToSkip` (`Hidden | System`). `FileSystemWalk` sets `AttributesToSkip = None` and treats Unix dot-prefix names as hidden. .NET maps Unix dotfiles to `FileAttributes.Hidden`, so recursive copy omits `.env` / `.gitignore` / `.git` and Windows Hidden/System entries. Cross-volume `MoveDirectory` then `RemoveItem`s the source, deleting the skipped files (data loss).
- Suggestion: Enumerate with `AttributesToSkip = FileAttributes.None` (share options with `FileSystemWalk.Walk`). Add a test that recursive copy includes a dotfile (and that the dest tree contains it).
- Source: orchestrator
- Disposition notes: Shared `FileSystemWalk.CreateEnumerationOptions()`; recursive copy test includes `.secret`.

## Duplicates / conflicts

- None. M3 is the documented limitation remaining after M2 refuses dest-directory delete-first.
