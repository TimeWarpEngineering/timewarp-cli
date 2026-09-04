# Round 1 — native-fs
**Date:** 2026-09-04
**Scope reviewed:** `source/timewarp-amuru/native/file-system/`, `PathResolver.cs`, `native/aliases/Bash.cs`, `ScriptContext.cs`

## Summary

Task **104** items are still present and were not re-filed. **097** ScriptContext nesting still looks correct on the happy path. New findings: ScriptContext factory leak + `OnExit` under lock; force-delete mutating symlink *file* targets and skipping the root directory’s read-only bit.

## Issues

### Issue 1 — Severity: bug
- File: `source/timewarp-amuru/ScriptContext.cs:73-76` and `:97-100`
- Description: The ctor pushes onto `LiveContexts` and may register process-exit handlers; only then do factories call `Directory.SetCurrentDirectory`. If that throws, the instance is never returned, so the caller cannot `Dispose`. The orphan stays on the stack, keeps handlers alive, and can run `OnExit` at process exit.
- Suggestion: Change cwd before push, or try/catch after push and `Cleanup`/pop on failure.
- Status: open

### Issue 2 — Severity: bug
- File: `source/timewarp-amuru/ScriptContext.cs:143` (called from Dispose `:158-175` / UnwindAll `:117-123`)
- Description: User `OnExit` runs while `SyncLock` is held. An `onExit` that constructs/disposes another `ScriptContext` deadlocks. Docs advertise arbitrary cleanup in `onExit`. Nesting itself (097) is fine.
- Suggestion: Pop/mark disposed under the lock; invoke `OnExit` (and preferably cwd restore) outside the lock.
- Status: open

### Issue 3 — Severity: bug
- File: `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs:21-32`
- Description: Distinct from 104’s directory `SearchOption.AllDirectories` issue. For a symlink-to-file, `File.Exists` is true; `force` clears `FileInfo.IsReadOnly` (attributes follow the target on Unix); `File.Delete` removes only the symlink. Outside-tree permission mutation without deleting the target.
- Suggestion: If `ReparsePoint`/`LinkTarget` is set, delete the link without clearing target attributes.
- Status: open

### Issue 4 — Severity: bug
- File: `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs:63-80`
- Description: `RemoveReadOnlyAttribute` clears files and *sub*directories, not `directory` itself. On Windows, a read-only root dir can still make `Directory.Delete(..., recursive: true)` fail despite `force: true`.
- Suggestion: Clear read-only on the root `DirectoryInfo` as well (after children), still skipping reparse points per 104.
- Status: open

### Issue 5 — Severity: suggestion
- File: `source/timewarp-amuru/native/PathResolver.cs:8-9` (docs) and `:166-175` (Unix search)
- Description: Documented as equivalent to `which`, but Unix path only uses `File.Exists`. Non-executable PATH hits are returned; real `which` typically needs `X_OK`.
- Suggestion: Check execute bits, or narrow the docs.
- Status: open

## Verified-clean notes

- 097 ScriptContext nesting: stack push/pop, innermost-first restore, out-of-order dispose unwind still correct for successful create/dispose.
- Commands error contract: GetContent/GetChildItem/RemoveItem/SetLocation catch and return `CommandOutput` with non-zero exit.
- Bash aliases are thin delegates (`Bash.cs:19-79`).
- PathResolver: null/whitespace validation, path-separator short-circuit, empty PATH, malformed PATH entries swallowed, Windows PATHEXT + bare-name fallback.

## Out-of-scope mentions (104 still present)

- Force-remove follows dir symlinks: `Direct.RemoveItem.cs:63-80` (`SearchOption.AllDirectories`)
- `force` does not suppress not-found: `Direct.RemoveItem.cs:51-54`
- GetContent missing `[EnumeratorCancellation]`: `Direct.GetContent.cs:21-28` (GetChildItem `:16-26` same gap — extend 104, do not clone; also `await Task.Yield()` per entry at `Direct.GetChildItem.cs:24`)
- Commands sync-over-async: `Commands.GetChildItem.cs:26-38`, `Commands.GetContent.cs:31-38`
- Cd mutates process-global cwd: `Direct.SetLocation.cs:14-17`
- Bash `Rm` twin bools: `Bash.cs:43-44`
- `IAsyncEnumerable` naming without `Async` suffix: `Direct.GetChildItem.cs:16`, `Direct.GetContent.cs:21`
