# Review framework — task 104

**Date:** 2026-09-04
**Host task:** kanban/in-progress/104-harden-native-file-system-operations/
**Diff scope:** branch `task/104-harden-native-file-system-operations` vs `origin/master` (commits `1f5b644`, `5592b47`)
**Plan / brief:** Harden native file-system ops without breaking 1.0.0 signatures. Skip reparse points in force-remove; honor `rm -f` missing-path; `[EnumeratorCancellation]` on GetContent/GetChildItem; drop Commands sync-over-async; document Cd process-global cwd; fold 111-003 M12/M13. Defer Rm flags enum and Async suffix.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** review oracle Grok session 01a06bcc-fe00-7362-923e-8bd7631c95c6 (2026-09-04)

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`

## Round 2

**Date:** 2026-09-04
**Scope:** Re-verify round-1 M1/M2 against the post-fix test delta; scan that delta for new defects. Product sources under `source/timewarp-amuru/native/` are unchanged since round 1.

**Fix delta:**
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.remove-item.cs` — silent `return` on symlink-create failure → `InvalidOperationException`
- `tests/timewarp-amuru/single-file-tests/native/file-system/direct.get-child-item.cs` — new GetChildItem cancellation test
