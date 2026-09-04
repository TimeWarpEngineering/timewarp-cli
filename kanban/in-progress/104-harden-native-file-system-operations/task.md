# Harden native file system operations

## Description

Correctness/safety issues in `native/file-system/` found by the release review. The worst: force-remove can mutate files OUTSIDE the target tree via symlinks.

## Checklist

- [x] `native/file-system/direct/Direct.RemoveItem.cs:63-80` — with `force=true`, `RemoveReadOnlyAttribute` enumerates `SearchOption.AllDirectories`, which follows directory symlinks/reparse points: clears read-only attributes on files outside the target tree and can loop on cyclic links (the `Directory.Delete(recursive:true)` itself doesn't follow links, so deletion is contained). Skip reparse points
- [x] `native/file-system/direct/Direct.RemoveItem.cs:51-54` — `force` doesn't suppress not-found (unlike `rm -f`): `Rm(path, force:true)` on a missing path throws. Decide semantics and document
- [x] `native/file-system/direct/Direct.GetContent.cs:22` — async iterator lacks `[EnumeratorCancellation]`, so `WithCancellation()` is a no-op; enumeration of a huge/blocked file can't be cancelled
- [x] `native/file-system/commands/Commands.GetChildItem.cs:26-38`, `Commands.GetContent.cs:31-38` — sync-over-async `Task.Run(...).GetAwaiter().GetResult()` blocks a threadpool thread per call
- [x] `native/file-system/direct/Direct.SetLocation.cs:15` — `Cd` mutates process-global `Environment.CurrentDirectory`; document the race with parallel tests/tasks resolving relative paths
- [x] `native/aliases/Bash.cs:43` — `Rm(path, bool recursive, bool force)` twin bool params; consider a flags enum before freezing (breaking to change later) — **deferred** (1.0 shipped; keep signature)
- [x] API-consistency: `Direct.GetChildItem`/`Direct.GetContent` return `IAsyncEnumerable` without the `Async` suffix used everywhere else — pick a convention before 1.0 — **deferred** (keep shipped names; new APIs in 112)
- [x] Tests for symlink handling, force-not-found, and cancellation
- [x] Fold 111-003 M12 (file-symlink force must not mutate target) and M13 (force+recursive clears root directory read-only)
- [x] Implementation review (effort 1 general): round 1 + fix M1/M2 + round 2 + disposition clean

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

### Implementation review 2026-09-04

Kitchen: `review/`. Effort 1 general, 2 rounds, disposition **clean**. See Results.

## Session

- Cockpit dispatch: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04)
- Implementer: Grok session 01a06bc1-e12b-76e0-9e5f-d56972d7fe37 (2026-09-04)
- Review oracle: Grok session 01a06bcc-fe00-7362-923e-8bd7631c95c6 (2026-09-04)

## Results

Harden native file-system ops without breaking 1.0.0 signatures.

### What was implemented

- `RemoveReadOnlyAttribute` walks one directory level at a time and skips `FileAttributes.ReparsePoint`, so force-remove no longer follows directory symlinks/junctions into foreign trees or loops on cyclic links.
- File-symlink force (111-003 **M12**): delete the link; do not clear `IsReadOnly` on the target.
- Force+recursive (111-003 **M13**): after children, clear read-only on the root directory itself (still skipping reparse points).
- `force: true` on a missing path matches bash `rm -f` (no throw / Commands exit 0). Without force, still `FileNotFoundException` / exit 1.
- `Direct.GetContent` and `Direct.GetChildItem` take `[EnumeratorCancellation] CancellationToken` so `WithCancellation()` cancels enumeration.
- `Commands.GetContent` / `Commands.GetChildItem` use sync BCL APIs (`File.ReadAllLines`, `EnumerateFileSystemInfos`) instead of `Task.Run(...).GetResult()`.
- `Direct.SetLocation` / `Commands.SetLocation` XML remarks document the process-global `Environment.CurrentDirectory` race. Behavior unchanged.

### Files changed

- `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs`
- `source/timewarp-amuru/native/file-system/direct/Direct.GetContent.cs`
- `source/timewarp-amuru/native/file-system/direct/Direct.GetChildItem.cs`
- `source/timewarp-amuru/native/file-system/direct/Direct.SetLocation.cs`
- `source/timewarp-amuru/native/file-system/commands/Commands.GetContent.cs`
- `source/timewarp-amuru/native/file-system/commands/Commands.GetChildItem.cs`
- `source/timewarp-amuru/native/file-system/commands/Commands.RemoveItem.cs`
- `source/timewarp-amuru/native/file-system/commands/Commands.SetLocation.cs`
- `source/timewarp-amuru/native/aliases/Bash.cs`
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs` (new)
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-content.cs`
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-child-item.cs` (new, review M2)
- `tests/timewarp-amuru/single-file-tests/native/file-system/commands.remove-item.cs`

### Key decisions / deviations

- Rm twin bools and `IAsyncEnumerable` names without `Async` stay as shipped (breaking; 1.0 window closed). Convention for new APIs is task 112.
- 111-003 M12/M13 folded here because they live in `Direct.RemoveItem`. M10/M11/M14 (ScriptContext / PathResolver) left for 111-003.
- Optional `CancellationToken` on GetContent/GetChildItem is additive so `WithCancellation` works; method names unchanged.

### Test outcomes

- `dotnet build timewarp-amuru.slnx` — 0 errors, 0 warnings
- Native file-system tests (direct.remove-item 6, direct.get-content 3, direct.get-child-item 1, commands.remove-item 6, commands.get-content 3, commands.get-child-item 2, bash-aliases 6) — all passed
- Aggregate `tests/timewarp-amuru/multi-file-runners/run-tests.cs` — **424 passed, 1 skipped, 0 failed** (425 total) at implement
- Review M2 adds `direct.get-child-item` cancellation (standalone 1/1 passed). Expect aggregate **425 passed, 1 skipped, 0 failed** (426 total)

### Review disposition

- **Outcome:** clean
- **Rounds:** 2
- **Effort / roster:** 1, general only
- **Final counts:** bug 0/0/0 open/fixed/wontfix; suggestion 0 open, 2 fixed, 0 wontfix; nit 0
- **Final open count:** 0
- Round 1: no bugs; **M1** vacuous symlink-test pass; **M2** missing GetChildItem cancellation test
- Fixes on this task id (no sibling apply-review task): throw on symlink-create failure; add `direct.get-child-item.cs`
- Round 2: M1/M2 confirmed fixed; no new findings
- Paths: `review/review-framework.md`, `review/round-1/{general,merged}.md`, `review/round-2/{general,merged}.md`, `review/disposition.md`

### How to validate

**Smoke**

```bash
cd /path/to/timewarp-amuru
dotnet build timewarp-amuru.slnx
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-content.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-child-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.remove-item.cs
```

**Expect**

- Build: `Build succeeded.` with `0 Warning(s)` `0 Error(s)`
- `direct.remove-item`: 6 passed, including file-symlink force (target stays read-only), outside directory symlink left intact, cyclic symlink completes, missing+force does not throw. Symlink-create failure must throw `InvalidOperationException` (not a silent pass)
- `direct.get-content`: 3 passed, including `Cancellation_Should_ThrowOperationCanceledException`
- `direct.get-child-item`: 1 passed (`Cancellation_Should_ThrowOperationCanceledException`)
- `commands.remove-item`: 6 passed, including `MissingPathWithForce_Should_Succeed` (exit 0) and `MissingPath_Should_Fail` without force (exit 1)

**Automated gate**

```bash
cd tests/timewarp-amuru/multi-file-runners && dotnet run run-tests.cs
# expect: Grand Total Passed: 425, Failed: 0, Skipped: 1, Total: 426
```

If a standalone `dotnet run <test>.cs` disagrees with the source you just changed, clear the runfile cache (`ganda runfile cache --clear` or `rm -rf ~/.local/share/dotnet/runfile/<test-name>-*`) and re-run.

**Not in scope:** Rm flags enum; renaming GetContent/GetChildItem with an `Async` suffix; ScriptContext / PathResolver (111-003 M10/M11/M14).
