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
- 104 must land first: force-remove currently follows directory symlinks.

## Session

- Created: 01a06a4a-807d-7143-9d21-330f32238619 (2026-09-04) — split from 020 so amuru WIP can close
