# Round 3 — merged findings
**Date:** 2026-09-04
**Sources:** general (host review-oracle re-verify of M27 fix)

## Counts (final oracle kitchen)

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 1 | 0 |
| suggestion | 0 | 0 | 1 |
| nit | 0 | 0 | 0 |

Oracle open total: 0. Product round-1 remaining opens stay filed as children **111-001**…**111-005**.

## Issues

### M27 — Severity: bug — Status: fixed
- File: `kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/task.md` How to validate
- Description: Bare `ganda kanban show 111-00N` failed from this claimed worktree.
- Suggestion: Pass `--repo timewarp-amuru`.
- Source: general
- Disposition notes: verified `ganda kanban show --repo timewarp-amuru 111-001` and `111-005` return to-do.

### M28 — Severity: suggestion — Status: wontfix
- File: child briefs on origin-home vs parent `merged.md` only on the task branch
- Description: Children cite parent evidence not yet on `origin/master`.
- Suggestion: Land parent review artifacts via host open-pr.
- Source: general
- Disposition notes: wontfix — see `review/disposition.md`. Decider: review oracle 111.

## Carried round-1 IDs

M1–M8, M10–M24, M26 remain open as children. M9, M25 remain wontfix. No new product findings.

## Duplicates / conflicts

None.
