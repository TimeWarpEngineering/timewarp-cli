# Disposition — task 111

**Date:** 2026-09-04
**Outcome:** accepted-exceptions
**Rounds:** 1
**Final open count on this parent:** 24 open findings, all filed as `--parent 111` children (IDs below). Two nits `wontfix`. Parent stays in-progress until those children land.

## Summary

Whole-repo re-review of origin-home `master` at `fbd5d276fc5a936136a55d981fc121a23b991493` (product code unchanged since kitchen pin `6867b67`; only kanban 111 itself moved). Seven area specialists. Security filed zero new issues. 097 / 090–092 remediations still hold. Independent product fixes went to five child tasks rather than a sibling “apply findings” task. Two nits were wontfix’d with rationale.

## Children (remaining opens)

| Child | Findings | Title |
|-------|----------|-------|
| **111-001** | M1–M5 | Fix CaptureAsync RunTime blanks and SelectAsync validation |
| **111-002** | M6–M8 | Fix CommandMock pipe identity and path-override matching |
| **111-003** | M10–M14 | Fix ScriptContext leak deadlock and native force extras |
| **111-004** | M16–M20 | Fix check-version exact-tag Tools version and clean wiring |
| **111-005** | M15, M21–M24, M26 | Align amuru skill docs and CI test-inclusion filters |

Children published to origin-home `kanban/to-do/` (kanban-only). Parent does not `kanban done` until they land.

## Exception log

| ID | Severity | Rationale | Decided by |
|----|----------|-----------|------------|
| M9 | nit | `Throws` then `Returns` leftover `Exception` is an uncommon fluent chain; not worth a public-behavior change | implementer 111 |
| M25 | nit | `cliwrap-exit-code-tests/` is historical CliWrap investigation, not CI, not a shipped sample | implementer 111 |

## Escalations

None. Known open product tasks (087, 088, 094-004, 099, 100, 104, 105, 106, 082) were confirmed still present and not cloned.

## Review paths

- `review/review-framework.md`
- `review/round-1/{core-engine,testing-mocks,native-fs,tools-builders,tools-services,tests-infra,security}.md`
- `review/round-1/merged.md`
- `review/disposition.md`
