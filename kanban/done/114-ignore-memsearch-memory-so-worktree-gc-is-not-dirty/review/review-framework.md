# Review framework — task 114

**Date:** 2026-09-09
**Host task:** kanban/in-progress/114-ignore-memsearch-memory-so-worktree-gc-is-not-dirty/
**Diff scope:** branch `task/114-ignore-memsearch-memory-so-worktree-gc-is-not-dirt` vs `origin/master`. Product change is 4 appended lines in root `.gitignore` (`6297157`). Kitchen Results live in `kanban/in-progress/114-…/task.md` (`f9b583d`).
**Plan / brief:** Commit the canonical Ganda memsearch-memory gitignore block (`.memsearch/memory/`) so `EnsurePorcelainGitignores` on claim is a no-op and worktree gc stays clean. Keep existing `.memsearch/` and `*.journal.json`. Do not change Ganda’s matcher.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** review oracle Grok `01a08479-9bfa-7c00-9d54-a088bc0bbe48` (2026-09-09)

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
