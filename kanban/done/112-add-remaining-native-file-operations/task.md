# Add Remaining Native File Operations

## Description

Post-1.0 additive work split out of archived task 020. Core 1.0 already ships a thin native FS slice (`GetChildItem`, `GetContent`, `GetLocation`, `SetLocation`, `RemoveItem` + bash `Ls`/`Cat`/`Pwd`/`Cd`/`Rm`). This card is the rest of the high-ROI file ops (cp/mv/mkdir/test/find/stat) plus globbing on listing.

Do **not** redo shipped commands here. Safety/correctness on the existing slice is task **104**.

## Requirements

- Commands + Direct APIs, consistent with the existing `native/file-system/` split
- Cross-platform behavior; normalize paths internally
- Bash aliases for the new verbs only (`Cp`, `Mv`, `Mkdir`/`Touch`, `Test`, `Find`, `Stat`)
- Tests for happy path and common error conditions

## Checklist

### New operations
- [x] **CopyItem** (cp) — file, recursive directory, overwrite, preserve attributes
- [x] **MoveItem** (mv) — file/directory, rename vs move, cross-drive
- [x] **NewItem** (mkdir/touch) — create directory with parents, create empty file, existing-item behavior
- [x] **TestPath** (test -e) — exists / is-file / is-directory
- [x] **GetChildItem** globbing — patterns, recursive, include/exclude, hidden files (basic listing already exists)
- [x] **FindItem** (find) — name / size / date / attributes
- [x] **GetItemProperty** (stat) — size, dates, attributes, symlink target

### Aliases and tests
- [x] Bash aliases for the new verbs (`Cp`, `Mv`, `Mkdir`, `Touch`, `Test`, `Find`, `Stat`)
- [x] Tests: copy, move/rename, create, test-path, glob, errors (missing path, permissions)

## Depends on

- 104

## Notes

- Archived parent: 020. Sibling native expansion still in to-do: 021–027 (text/process/system/interactive/archive) — out of scope here.
- 104 merged 2026-09-04 (PR #89, `5e9602e`): force-remove skips reparse points; `rm -f` missing path is a no-op; GetContent/GetChildItem honor cancellation. Copy those patterns.

### Cockpit brief 2026-09-04

- Do **not** reimplement shipped ops (`GetChildItem` basic listing, `GetContent`, `GetLocation`, `SetLocation`, `RemoveItem`, existing bash aliases).
- Match Commands vs Direct split and Purpose/Design regions used in `native/file-system/` after 104.
- Keep shipped `IAsyncEnumerable` names **without** an `Async` suffix; same convention on new streaming APIs.
- Skip reparse points on recursive copy/move/delete-style walks.
- **Version:** additive public API → bump core `source/Directory.Build.props` `<Version>` from `1.0.1` to **`1.1.0`**. Do not bump Tools (`1.0.0-beta.2`) unless this PR changes it. CI `check-version` fails if source equals the latest GitHub release.
- Write `## Results` + `### How to validate`. Do not `kanban done` yourself; host walk does that. Do not `gh pr create`.
- Implementation review kitchen: `review/` (framework, round-1/round-2, disposition). Disposition **clean**.

## Session

- Created: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04) — split from 020 so amuru WIP can close
- Cockpit dispatch: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04)
- Implementer: Grok session 01a06c9c-28b4-7800-be92-3b8c06f90f42 (2026-09-04)
- Review oracle: Grok session 01a06cb0-bc19-7910-a1cd-30c022a19867 (2026-09-04)
- Review fix pass: Grok session 01a06cb7-d33d-71c0-81ac-30164cf9cade (2026-09-04)

## Results

Additive native file ops on the existing Commands/Direct split. Core version **1.1.0**. Tools stays **1.0.0-beta.2**.

### What was implemented

- **CopyItem / Cp** — file, recursive directory, glob source, overwrite, preserve timestamps/attributes/Unix mode. Recursive walks skip reparse points (named source is copied; symlink children are not followed or copied).
- **MoveItem / Mv** — rename vs move-into-directory, overwrite. `File.Move` / `Directory.Move` first; IOException with source still present falls back to copy+delete (cross-volume).
- **NewItem / Mkdir / Touch** — `mkdir -p` (existing directory succeeds; existing file fails). Touch creates an empty file (parents created) or updates last-write time (existing directory fails).
- **TestPath / Test** — exists / is-file / is-directory. Commands: exit 0/1 like `test -e`. Direct: `bool`. Blank or malformed paths are non-existent, not thrown.
- **GetChildItem globbing** — optional `recursive`, `pattern`, `exclude`, `includeHidden`. Globs in `path` (`*.txt`, `**/*.cs`) work. Default listing stays non-recursive and includes hidden (1.0 behavior).
- **FindItem / Find** — always recursive; filters via `FindCriteria` (name glob, min/max size, modified after/before, required attribute bits). Streaming name is `FindItem` without an `Async` suffix.
- **GetItemProperty / Stat** — `ItemProperty` snapshot: size, dates, attributes, symlink `LinkTarget`.
- Bash aliases for the new verbs plus Direct counterparts (`CpDirect`, `MvDirect`, …).

### Files changed

Library

- `source/Directory.Build.props` — `1.0.1` → `1.1.0`
- `source/timewarp-amuru/native/file-system/item-type.cs`
- `source/timewarp-amuru/native/file-system/item-property.cs`
- `source/timewarp-amuru/native/file-system/find-criteria.cs`
- `source/timewarp-amuru/native/file-system/glob-matcher.cs`
- `source/timewarp-amuru/native/file-system/file-system-walk.cs`
- `source/timewarp-amuru/native/file-system/direct/Direct.{CopyItem,MoveItem,NewItem,TestPath,FindItem,GetItemProperty}.cs`
- `source/timewarp-amuru/native/file-system/direct/Direct.GetChildItem.cs` — glob/recursive overloads
- `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs` — shared `FileSystemWalk.IsReparsePoint`
- `source/timewarp-amuru/native/file-system/commands/Commands.{CopyItem,MoveItem,NewItem,TestPath,FindItem,GetItemProperty,Shared}.cs`
- `source/timewarp-amuru/native/file-system/commands/Commands.GetChildItem.cs` — glob/recursive
- `source/timewarp-amuru/native/aliases/Bash.cs`

Tests

- `tests/timewarp-amuru/single-file-tests/native/file-system/commands.{copy-item,move-item,new-item,test-path,find-item,get-item-property}.cs`
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.{copy-item,move-item,new-item,test-path,find-item,get-item-property}.cs`
- Existing `commands.get-child-item`, `direct.get-child-item`, `bash-aliases`, `commands.get-content` extended

### Key decisions / deviations

- Streaming APIs keep names **without** `Async` (cockpit brief), including `FindItem` and glob `GetChildItem`.
- `includeHidden` defaults to **true** so shipped `GetChildItem` listing does not hide dotfiles.
- Cross-drive move is try-Move then copy+delete when source still exists; no dedicated two-volume CI fixture.
- Progress reporting from archived 020 is out of scope (not on this card).
- Unix permission mapping is covered by `Commands.GetContent` on a `chmod 0` file; root may still read, so the assertion is conditional.

### Test outcomes

- `dotnet build timewarp-amuru.slnx` — 0 errors, 0 warnings; pack `TimeWarp.Amuru.1.1.0`, Tools still `1.0.0-beta.2`
- Native file-system single-file tests — all passed
- Aggregate `tests/timewarp-amuru/multi-file-runners/run-tests.cs` — **474 passed, 1 skipped, 0 failed** (475 total) at implement
- Review-fix Direct tests after `ae3a35f`: `direct.test-path` 3/3, `direct.move-item` 5/5, `direct.copy-item` 4/4 (three new methods vs implement)

### Review disposition

- **Outcome:** clean
- **Effort / roster:** 1, general only
- **Rounds:** 2
- **Final counts:** bug 3 fixed; suggestion 1 fixed; nit 0; open 0
- **Round 1:** M1 invalid `TestPath` `ItemType` swallowed; M2 `MoveDirectory` delete-before-move; M3 document copy+delete fallback; M4 recursive copy skipped Hidden/System (Unix dotfiles)
- **Fixes (same task id, `ae3a35f`):** validate `ItemType` before try/catch; refuse existing dest directory on move; XML remarks for cross-volume copy+delete; shared `CreateEnumerationOptions` (`AttributesToSkip=None`)
- **Round 2:** re-verified M1–M4; no new issues
- **Paths:** `review/review-framework.md`, `review/round-1/{general,merged}.md`, `review/round-2/{general,merged}.md`, `review/disposition.md`

### How to validate

**Smoke**

```bash
cd /path/to/timewarp-amuru
dotnet build timewarp-amuru.slnx
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.copy-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.move-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.new-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.test-path.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.find-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.get-item-property.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/commands.get-child-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.test-path.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.move-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/direct.copy-item.cs
dotnet run tests/timewarp-amuru/single-file-tests/native/file-system/bash-aliases.cs
```

**Expect**

- Build: 0 errors, 0 warnings; nupkg `TimeWarp.Amuru.1.1.0` (not Tools `1.0.0-beta.2` bump)
- Each `dotnet run` above: all tests PASSED, exit 0
- `commands.get-child-item`: glob `*.txt`, recursive exclude, and `includeHidden: false` cases pass
- `direct.test-path`: `(ItemType)999` throws `ArgumentOutOfRangeException`
- `direct.move-item`: overwrite true on an existing dest directory throws and preserves a marker file
- `direct.copy-item`: recursive copy includes `.secret`
- `bash-aliases`: `Cp`/`Mv`/`Mkdir`/`Touch`/`Test`/`Find`/`Stat` pass

**Automated gate**

```bash
cd tests/timewarp-amuru/multi-file-runners && dotnet run run-tests.cs
# expect: 477 passed, 1 skipped, 0 failed (478 total) — implement 474 plus three review-fix tests
```

After editing library source, clear stale runfiles first: `ganda runfile cache --clear`.

**Not in scope:** live two-volume cross-drive fixture; following directory symlinks on copy (they are skipped).
