# Round 2 — general
**Date:** 2026-09-04
**Scope reviewed:** post-fix delta ae3a35f (M1–M4)

## Summary

Re-verified ae3a35f against the live Direct.TestPath / MoveItem / CopyItem / FileSystemWalk sources and the three Direct test files. All four prior findings (M1–M4) are present as claimed: ItemType is validated before the catch, MoveDirectory refuses existing dest without delete-first, cross-volume failure modes are documented, and recursive copy enumerates with AttributesToSkip=None via the shared helper. No new defects in the fix delta.

## Prior findings

- M1 — CONFIRMED fixed — `Direct.TestPath.cs` validates `itemType is not ItemType.File and not ItemType.Directory` and throws `ArgumentOutOfRangeException` before the try/catch; blank/whitespace paths still return false (including typed blanks). Test `UnknownItemType_Should_ThrowArgumentOutOfRangeException` covers `(ItemType)999`; `MissingOrBlank_Should_BeFalse` covers typed blanks.
- M2 — CONFIRMED fixed — `MoveDirectory` throws `IOException` when final dest exists as file (`cannot overwrite non-directory`) or directory (`directory exists`) with no prior delete/`RemoveItem`. `MoveFile` still uses `File.Move(sourcePath, destinationPath, overwrite)`. Test `ExistingDestinationDirectoryWithOverwrite_Should_ThrowAndPreserve` asserts throw + marker `"keep-me"` preserved under overwrite true.
- M3 — CONFIRMED fixed — Public `MoveItem` `<remarks>` document copy-then-delete fallback, partial destination on copy failure, and dual-location data on delete failure; Design region mirrors the same. No two-volume fixture added.
- M4 — CONFIRMED fixed — `CopyDirectory` uses `FileSystemWalk.CreateEnumerationOptions()` (`AttributesToSkip = FileAttributes.None`); `Walk` uses the same helper. Test `RecursiveDirectory_Should_IncludeDotfiles` asserts `.secret` is copied. Reparse children still skipped via `IsReparsePoint` before copy/recurse.

## Issues

