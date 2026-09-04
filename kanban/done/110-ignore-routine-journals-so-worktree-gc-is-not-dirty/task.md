# Ignore routine journals so worktree gc is not dirty

## Description

`ganda task work` writes `task-work.journal.json` beside the kitchen. Unless
root `.gitignore` lists that basename, `git status --porcelain` shows `??`
and `ganda pr merge` / `worktree gc` **refuses** a dirty worktree.

This is a **consumer sweep**. Ganda **262** added audit check
`routine-journals-gitignore` and `--fix`, then left “sweep every org repo”
out of scope. That was wrong: we have hit this on merge at least six times
(Taratibu 252/253/254, mediator 004-001/004-002, architecture 207/208,
timewarp-software **033**). Each origin that never ran `--fix` is another
dirty-gc.

This origin (`timewarp-amuru`) is missing the ignore. Org SSOT: `ganda repo audit`
check `routine-journals-gitignore`. `--fix` appends the missing basename
lines. Tracked journals are **Failed / not fixable** — `git rm --cached`
is required (gitignore does not hide tracked files).

Do **not** commit journal contents.

## Requirements

Root `.gitignore` must contain this glob (comments/blanks ok):

```
*.journal.json
```

One line covers every routine journal (`task-work`, stacked-task-set, planning,
rfc, debate, advisor, and the next one). Ganda **268** updates the audit check
to PASS on this glob; do not add the six 262 exact names.

Prefer `ganda repo audit --fix --checks routine-journals-gitignore` (this
CLI requires `--fix` when `--checks` is set) so the commented block matches
other origins:

```gitignore
# Routine journals beside kitchens (local; not product)
*.journal.json
```

Then:

- `git rm --cached` any `*.journal.json` that `git ls-files` still lists.
  Delete empty leftover dirs if they exist only because of the journal.
- Do **not** `git rm` product `task.md` files.
- `git ls-files '*.journal.json'` must be empty.
- Audit check `routine-journals-gitignore` PASSes.
- `git check-ignore -v` on a journal basename path hits the new line.

## Checklist

- [x] Root `.gitignore` has `*.journal.json`
- [x] `git ls-files '*.journal.json'` is empty
- [x] Audit `routine-journals-gitignore` PASSes
- [x] `git check-ignore -v` confirms ignore; porcelain does not list journals
- [x] Do not implement on `master`
- [x] Implementation review (effort 1, general) under `review/`
- [x] Disposition `clean` (0 open); Results include review disposition

## Notes

- Predecessor: ganda `kanban/done/262-audit-gitignore-for-task-work-journal-so-worktree-gc-is-not-dirty/`
- Consumer precedent: architecture **208**, timewarp-software **034**
- Host hole (ganda kitchen, separate): unstage **any** `kanban/**/*.journal.json`
  on kitchen commits; consider a hook that runs `repo audit --fix`.
- 262 out-of-scope (“do not sweep every org repo”) is why this kitchen exists.
- Implementation review trail: `review/review-framework.md`, `review/round-1/merged.md`, `review/disposition.md` (outcome `clean`).

### How to validate

**Automated**
```bash
git check-ignore -v kanban/to-do/task-work.journal.json || true
# expect: .gitignore:…:*.journal.json (path may be untracked)

git ls-files '*.journal.json'
# expect: empty

ganda repo audit --fix --checks routine-journals-gitignore
# expect: routine-journals-gitignore PASS (fix is a no-op once present)
```

**Not in scope:** changing `WorktreeGcService` to treat untracked journals as
clean; host unstage-all (ganda).

## Session

- Created: grok `01a06304-cbf6-7d83-b5a2-4a99e9d09d40` (2026-09-03) cockpit timewarp-flow
- Trigger: `/tw-merge` software 033 — GC refused, then leftover journal
  committed; 262 left consumer sweep out of scope
- Pattern: `*.journal.json` (cockpit, 2026-09-03) — one glob, not six names
- Implementer: grok `01a06ba2-ecfa-70e3-b8fd-523f73282fd5` (2026-09-04) task worktree
- Review oracle: grok `01a06ba6-1ec5-7d43-b9b7-077e0f3ae530` (2026-09-04) — `tw-implementation-review` effort 1, general only; reviewer subagent `01a06ba7-f310-7572-9164-a29bea502f12`

## Results

Root `.gitignore` now ignores routine journals with the one-glob pattern from ganda **268**. `ganda task work` / `worktree gc` will no longer see `?? *.journal.json` on this origin.

**What was implemented**

- Ran `ganda repo audit --fix --checks routine-journals-gitignore` in the claimed task worktree (`task/110-ignore-routine-journals-so-worktree-gc-is-not-dirt`).
- That appended the commented block:

  ```gitignore
  # Routine journals beside kitchens (local; not product)
  *.journal.json
  ```

- `git ls-files '*.journal.json'` was already empty — no `git rm --cached`, no leftover dirs, no product `task.md` removals.
- Did **not** add the six 262 exact journal basenames.
- Did **not** commit `task-work.journal.json`.
- Did **not** fix unrelated audit failures (`memsearch-memory-gitignore`, `bin-dev`, kebab paths, etc.).

**Files changed**

- `.gitignore` — append `*.journal.json` block
- `kanban/in-progress/110-ignore-routine-journals-so-worktree-gc-is-not-dirty/task.md` — column move + Results + review disposition
- `kanban/in-progress/110-ignore-routine-journals-so-worktree-gc-is-not-dirty/review/` — framework, round-1 general/merged, disposition

**Key decisions / deviations**

- One glob, not six names (ganda 268 / cockpit 2026-09-03).
- Left `memsearch-memory-gitignore` FAIL in place; it is a different check and out of this kitchen’s scope. An earlier unscoped `--fix` had also appended `.memsearch/memory/`; that was discarded so this commit stays journals-only.

**Test outcomes**

- `git check-ignore -v kanban/to-do/task-work.journal.json` → `.gitignore:433:*.journal.json	kanban/to-do/task-work.journal.json`
- `git ls-files '*.journal.json'` → empty
- `ganda repo audit --fix --checks routine-journals-gitignore` → `routine-journals-gitignore` **PASS** (fix is a no-op once present). The CLI still prints the full audit and exits non-zero because other checks fail on this origin; that is pre-existing and not this task.
- `git add -n kanban/` stages only `task.md` (journal stays untracked/ignored).
- Branch is `task/110-ignore-routine-journals-so-worktree-gc-is-not-dirt`, not `master`.

### How to validate

**Smoke**

```bash
git check-ignore -v kanban/to-do/task-work.journal.json || true
git ls-files '*.journal.json'
git status --porcelain | grep '\.journal\.json' || true
ganda repo audit --fix --checks routine-journals-gitignore
```

**Expect**

- `git check-ignore -v` prints `.gitignore:<line>:*.journal.json` and the path (the file may be untracked).
- `git ls-files '*.journal.json'` prints nothing.
- `git status --porcelain` does not list `*.journal.json` (`grep '\.journal\.json'` is empty; the kitchen folder name itself contains "journals").
- Audit table row `routine-journals-gitignore` is **PASS**; `--fix` is a no-op. Overall `ganda repo audit` may still exit 1 on unrelated checks.

**Not in scope:** changing `WorktreeGcService` to treat untracked journals as clean; host unstage-all (ganda); `memsearch-memory-gitignore` / other audit failures on this origin.

### Review disposition

- **Rounds:** 1
- **Effort / roster:** 1 — general only (`review/round-1/general.md`)
- **Counts (final):** bug 0 / suggestion 0 / nit 0 — all `open`/`fixed`/`wontfix` are 0
- **Disposition:** `clean` (no issues raised; no fix loop; no exceptions)
- **Paths:** `review/review-framework.md`, `review/round-1/general.md`, `review/round-1/merged.md`, `review/disposition.md`

