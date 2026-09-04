# Review framework — task 110

**Date:** 2026-09-04
**Host task:** `kanban/in-progress/110-ignore-routine-journals-so-worktree-gc-is-not-dirty/`
**Diff scope:** branch `task/110-ignore-routine-journals-so-worktree-gc-is-not-dirt` vs `origin/master` (commit `fb5d1de` — `.gitignore` glob + kitchen `task.md`)
**Plan / brief:** Consumer sweep so `ganda task work` journals do not dirty porcelain / block `worktree gc`. Root `.gitignore` must contain `*.journal.json` (one glob, not the six 262 exact names). Prefer `ganda repo audit --fix --checks routine-journals-gitignore`. Tracked journals would need `git rm --cached`; none were tracked. Do not commit journal contents.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer grok `01a06ba2-ecfa-70e3-b8fd-523f73282fd5` (2026-09-04); review-oracle grok `01a06ba6-1ec5-7d43-b9b7-077e0f3ae530` (2026-09-04)

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
- Do not treat pre-existing audit failures (`memsearch-memory-gitignore`, `bin-dev`, kebab paths, etc.) as findings for this task
- Do not commit or stage `*.journal.json`
