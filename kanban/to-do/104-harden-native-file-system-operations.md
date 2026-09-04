# Harden native file system operations

## Description

Correctness/safety issues in `native/file-system/` found by the release review. The worst: force-remove can mutate files OUTSIDE the target tree via symlinks.

## Checklist

- [ ] `native/file-system/direct/Direct.RemoveItem.cs:63-80` — with `force=true`, `RemoveReadOnlyAttribute` enumerates `SearchOption.AllDirectories`, which follows directory symlinks/reparse points: clears read-only attributes on files outside the target tree and can loop on cyclic links (the `Directory.Delete(recursive:true)` itself doesn't follow links, so deletion is contained). Skip reparse points
- [ ] `native/file-system/direct/Direct.RemoveItem.cs:51-54` — `force` doesn't suppress not-found (unlike `rm -f`): `Rm(path, force:true)` on a missing path throws. Decide semantics and document
- [ ] `native/file-system/direct/Direct.GetContent.cs:22` — async iterator lacks `[EnumeratorCancellation]`, so `WithCancellation()` is a no-op; enumeration of a huge/blocked file can't be cancelled
- [ ] `native/file-system/commands/Commands.GetChildItem.cs:26-38`, `Commands.GetContent.cs:31-38` — sync-over-async `Task.Run(...).GetAwaiter().GetResult()` blocks a threadpool thread per call
- [ ] `native/file-system/direct/Direct.SetLocation.cs:15` — `Cd` mutates process-global `Environment.CurrentDirectory`; document the race with parallel tests/tasks resolving relative paths
- [ ] `native/aliases/Bash.cs:43` — `Rm(path, bool recursive, bool force)` twin bool params; consider a flags enum before freezing (breaking to change later)
- [ ] API-consistency: `Direct.GetChildItem`/`Direct.GetContent` return `IAsyncEnumerable` without the `Async` suffix used everywhere else — pick a convention before 1.0
- [ ] Tests for symlink handling, force-not-found, and cancellation

## Notes

Found by multi-agent release review (2026-07-04). Verified clean: `PathResolver.cs`, `Bash.cs` aliases, `ConvertTimestamp`, `GenerateColor`. Paths relative to `source/timewarp-amuru/`.

### Cockpit brief 2026-09-04 (core 1.0.0 already shipped)

**Must do (non-breaking):**
- Skip reparse points in `RemoveReadOnlyAttribute` (the safety bug)
- Decide `force` + missing path: match bash `rm -f` (no throw) and document
- `[EnumeratorCancellation]` on `GetContent`
- Remove Commands sync-over-async (`Task.Run(…).GetResult()`)
- Document `Cd` process-global cwd race (do not change behavior)
- Tests: symlink/reparse skip, force-not-found, cancellation

**Defer (breaking; 1.0 window closed):**
- Flags enum for `Rm` twin bools — keep current signature
- Rename `IAsyncEnumerable` methods with `Async` suffix — keep shipped names; apply convention on new APIs in 112

**Same-file overlap with 111-003:** M12 (force on file symlink must not mutate the target) and M13 (force+recursive must clear root directory read-only, still skip reparse points). Fold M12/M13 into this pass if they stay in `Direct.RemoveItem`. Leave 111-003 M10/M11/M14 (ScriptContext / PathResolver) alone.

112 depends on this task (remaining native file ops).

## Session

- Cockpit dispatch: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04)
