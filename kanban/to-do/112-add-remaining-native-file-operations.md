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
- [ ] **CopyItem** (cp) — file, recursive directory, overwrite, preserve attributes
- [ ] **MoveItem** (mv) — file/directory, rename vs move, cross-drive
- [ ] **NewItem** (mkdir/touch) — create directory with parents, create empty file, existing-item behavior
- [ ] **TestPath** (test -e) — exists / is-file / is-directory
- [ ] **GetChildItem** globbing — patterns, recursive, include/exclude, hidden files (basic listing already exists)
- [ ] **FindItem** (find) — name / size / date / attributes
- [ ] **GetItemProperty** (stat) — size, dates, attributes, symlink target

### Aliases and tests
- [ ] Bash aliases for the new verbs (`Cp`, `Mv`, `Mkdir`, `Touch`, `Test`, `Find`, `Stat`)
- [ ] Tests: copy, move/rename, create, test-path, glob, errors (missing path, permissions)

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

## Session

- Created: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04) — split from 020 so amuru WIP can close
- Cockpit dispatch: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04)
