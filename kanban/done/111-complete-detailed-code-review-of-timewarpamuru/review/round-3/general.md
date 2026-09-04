# Round 3 — general
**Date:** 2026-09-04
**Scope reviewed:** post-fix delta for round-2 M27 (How to validate `--repo`) and M28 (wontfix). Re-ran child `show` against origin-home.

## Summary

How to validate now uses `ganda kanban show --repo timewarp-amuru 111-00N`. Spot-checked 111-001 and 111-005: both return Column `to-do` on origin-home. No new kitchen or product defects on the fix delta. M28 remains wontfix with documented rationale.

## Issues

None.

## Verified prior IDs

- M27 — FIXED — `ganda kanban show --repo timewarp-amuru 111-001` and `111-005` succeed from this claimed worktree
- M28 — WONTFIX — unchanged; children still cite parent `merged.md` not on origin-home; Requirements remain inlined
- M1–M26 — unchanged (product ledger; still filed as children)
