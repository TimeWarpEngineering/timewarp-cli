# Review framework — task 111

**Date:** 2026-09-04
**Host task:** `kanban/to-do/111-complete-detailed-code-review-of-timewarpamuru/`
**Diff scope:** whole-repo review of origin-home `master` (not a PR delta)
**Pinned SHA at kitchen create:** `6867b6747cc1c1b48ffe47e21c0840c6f17d69dd`
**Pinned versions:** TimeWarp.Amuru `1.0.0` · TimeWarp.Amuru.Tools `1.0.0-beta.2`
**Plan / brief:** `task.md` — successor to 2026-07-04 release review (097/094/090–092); re-review current tree
**Effort:** elevated — 7 area reviewers (not default effort-1)
**Reviewer roster:** core-engine, testing-mocks, native-fs, tools-builders, tools-services, tests-infra, security
**Session IDs:** kitchen created Grok `01a06a77-1631-7543-b181-07ddc524f9fe` / ganda claim 3290396; review-round sessions TBD

**Re-pin before round 1:** if `origin/master` has moved, update **Pinned SHA** here and record the new `git rev-parse origin/master` / `git log -1 --oneline`.

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome for an area
- Address the current tree and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
- Do not re-open 097 / 090–092 / 094-001…003 unless the defect is still present (cite current file:line)
- Do not duplicate already-tracked product tasks (**087, 088, 094-004, 099, 100, 104, 105, 106, 082**) as new M#s; note status only
- 094 packaging decisions (two packages, no InternalsVisibleTo, Tools stays beta) are policy — not findings
- Default validation is `None` (AGENTS.md) — non-zero exit without `WithZeroExitCodeValidation()` is not a bug

## Finding template

Each reviewer writes `review/round-N/<reviewer>.md` using the `tw-implementation-review` finding template (`bug` / `suggestion` / `nit`, `Status: open`, file:line, suggestion).

## Merge

After all seven reviewers finish, write `review/round-N/merged.md` with counts table, stable `M#` IDs, source attribution, and duplicate collapse notes.

## Disposition

Exit bar: 0 `open` findings on this task *or* remaining opens filed as `--parent 111` children with IDs listed in `review/disposition.md`. Outcome is `clean` or `accepted-exceptions`.
