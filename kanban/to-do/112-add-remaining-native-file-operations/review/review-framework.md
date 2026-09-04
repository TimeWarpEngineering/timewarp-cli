# Review framework — task 112

**Date:** 2026-09-04
**Host task:** `kanban/to-do/112-add-remaining-native-file-operations/`
**Diff scope:** branch `task/112-add-remaining-native-file-operations` vs `origin/master` (product commit `776feae` — remaining native file ops + 1.1.0 bump; kitchen brief `aa97ea8`)
**Plan / brief:** Post-1.0 additive native FS verbs (CopyItem/MoveItem/NewItem/TestPath/FindItem/GetItemProperty + GetChildItem globbing) with Commands + Direct split, bash aliases, tests, core version 1.1.0. Do not redo shipped ops; safety on the 1.0 slice is task 104. Skip reparse points on recursive copy/move walks. Streaming APIs keep names without `Async`.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer grok `01a06c9c-28b4-7800-be92-3b8c06f90f42` (2026-09-04); review-oracle grok `01a06cb0-bc19-7910-a1cd-30c022a19867` (2026-09-04); fix-pass grok `01a06cb7-d33d-71c0-81ac-30164cf9cade` (2026-09-04)

## Round 2

Re-verify M1–M4 against commit `ae3a35f` (post-fix delta vs `776feae`). Do not clobber `review/round-1/`.

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
- Do not treat pre-existing issues outside this diff as findings
- Do not create a sibling “apply review” task; disposition stays on 112
