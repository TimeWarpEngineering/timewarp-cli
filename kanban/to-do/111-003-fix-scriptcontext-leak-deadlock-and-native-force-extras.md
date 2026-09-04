# Fix ScriptContext leak deadlock and native force extras

## Description

Parent **111** round-1 findings **M10–M14**. New native/ScriptContext defects **not** already tracked by **104**.

Pinned tree: `fbd5d276fc5a936136a55d981fc121a23b991493`. Evidence: parent `review/round-1/merged.md`.

## Requirements

- **M10 (bug)** `ScriptContext.cs:73-76` and `:97-100` — factory pushes onto `LiveContexts` before `SetCurrentDirectory`. A throw leaks a live stack entry the caller cannot dispose. Change cwd before push, or pop/cleanup on failure.
- **M11 (bug)** `ScriptContext.cs:143` — `OnExit` runs while `SyncLock` is held; nested ScriptContext in `onExit` deadlocks. Invoke `OnExit` outside the lock.
- **M12 (bug)** `native/file-system/direct/Direct.RemoveItem.cs:21-32` — **different from 104** (104 is directory `SearchOption.AllDirectories`). `force` on a *file* symlink can clear read-only on the target, then delete only the link. Delete the link without mutating target attributes.
- **M13 (bug)** `Direct.RemoveItem.cs:63-80` — `RemoveReadOnlyAttribute` never clears read-only on the **root** directory itself, so `force`+`recursive` can still fail on Windows. Clear the root bit after children; still skip reparse points (104).
- **M14 (suggestion)** `PathResolver.cs:166-175` — Unix search is `File.Exists` only while docs claim `which` equivalence. Check execute bits or narrow the docs.

Do **not** clone 104 items (dir-symlink AllDirectories walk, force not-found, EnumeratorCancellation, Commands sync-over-async, Cd global cwd, Rm twin bools, IAsyncEnumerable naming). GetChildItem `Task.Yield` per entry folds into 104.

## Checklist

- [ ] M10 failed SetCurrentDirectory does not leak LiveContexts
- [ ] M11 OnExit not under SyncLock
- [ ] M12 force on file symlink does not mutate the target
- [ ] M13 force+recursive clears root directory read-only
- [ ] M14 PathResolver Unix execute-bit or docs
- [ ] `## Results` + `### How to validate`

## Notes

Parent: **111**. Source: `review/round-1/native-fs.md`. Coordinate with **104** if both touch `Direct.RemoveItem`.
