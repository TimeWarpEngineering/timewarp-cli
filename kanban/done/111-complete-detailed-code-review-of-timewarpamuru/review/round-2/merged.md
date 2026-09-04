# Round 2 — merged findings
**Date:** 2026-09-04
**Sources:** general (host review-oracle, effort 1)

Independent re-verification of round-1 M1–M26 against current source. New IDs are kitchen defects in the implementer deliverable, not new product bugs.

## Counts (new this round)

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 1 | 0 |
| suggestion | 0 | 0 | 1 |
| nit | 0 | 0 | 0 |

Round-1 product ledger (frozen): bug 13 open, suggestion 9 open, nit 2 open / 2 wontfix — all remaining opens filed as children **111-001**…**111-005**.

## Issues

### M27 — Severity: bug — Status: fixed
- File: `kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/task.md:175-177`
- Description: How to validate `cd`s into this claimed worktree then runs bare `ganda kanban show 111-00N`. Those ids are not on this worktree’s board (children live on origin-home). Commands fail with `Task '111-00N' not found on this board`.
- Suggestion: Use `ganda kanban show --repo timewarp-amuru 111-00N`.
- Source: general
- Disposition notes: fixed on this parent — How to validate now uses `--repo timewarp-amuru`. Verified `ganda kanban show --repo timewarp-amuru 111-001` and `111-005` return to-do on origin-home.

### M28 — Severity: suggestion — Status: wontfix
- File: `kanban/to-do/111-001-fix-captureasync-runtime-blanks-and-selectasync-validation.md:7` (origin/master) vs absence of `review/round-1/merged.md` on origin/master
- Description: Children 111-001…005 are already on origin-home and cite parent `review/round-1/merged.md`, but implementer commit `09715af` is not on `origin/master`. Parent kitchen there is still the to-do scaffold. Requirements are inlined on each child.
- Suggestion: Land `09715af` on origin-home (host open-pr) or retarget Evidence at inlined Requirements.
- Source: general
- Disposition notes: wontfix — task brief required publishing children to origin-home immediately; parent review artifacts stay on the task branch until host open-pr/merge. Children already inline full Requirements, so they remain workable. Decider: review oracle 111.

## Carried round-1 IDs (frozen; not re-filed)

M1–M8, M10–M24, M26 remain **open** and filed as children (see `review/round-1/merged.md`). M9, M25 remain **wontfix**. Independent re-read confirmed every citation; none refuted or cloned from 087/088/094-004/099/100/104/105/106/082.

## Duplicates / conflicts

None. Round-2 issues are kitchen-only.
