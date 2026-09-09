# Ignore memsearch memory so worktree gc is not dirty

## Description

After merging amuru **#91** (task **113**), `ganda pr merge` GC refused:

```
Refuse: worktree is dirty: …/task-113-…
   M .gitignore
```

The diff was claim-time porcelain pickup (`EnsurePorcelainGitignores`) appending
the canonical memsearch-memory glob. Cockpit restored `.gitignore` then GC’d.

Commit that glob so the next claim is a no-op and worktree gc stays clean.

Same block as architecture **208-001** / ganda audit
`AuditCheckConstants.MemsearchMemoryGitignorePattern`.

## Requirements

Root `.gitignore` only.

Today the file already has `.memsearch/` (line ~430) **and** `*.journal.json`.
Git already ignores `.memsearch/memory/` via the parent. **Ganda does not:**
`HasMemsearchMemoryGitignore` only accepts these exact spellings:

- `.memsearch/memory/`
- `/.memsearch/memory/`
- `.memsearch/memory`
- `/.memsearch/memory`

Append the canonical comment + glob (do not remove `.memsearch/`):

```
# Memsearch memory auto-writes (plugin daily notes; local until promoted).
# Deliberate promote of distilled memory: git add -f .memsearch/memory/<file>.md
.memsearch/memory/
```

Place it next to the existing routine-journal block at the end of the file
(architecture 208-001 layout).

After this, `EnsurePorcelainGitignores` on claim must not modify `.gitignore`.

### Not in scope

- Changing ganda’s matcher to treat `.memsearch/` as covering (follow-on on
  ganda if wanted)
- Other repos (nuru also lacks the exact glob; not this task)
- WorktreeGcService dirty rules

## Checklist

- [x] Canonical `.memsearch/memory/` block in root `.gitignore`
- [x] Existing `.memsearch/` and `*.journal.json` kept
- [x] Results + How to validate
- [x] Implementation review disposition (`clean`)

## Session

- Created: 2143941 (2026-09-09)
- Cockpit: timewarp-flow Grok `01a03d38-9611-7620-aae5-848e15dafa94`
  (2026-09-09). Do not implement in cockpit.
- Implementer: Grok session `01a08477-6b3b-70a3-8324-a5a55eba38be` (2026-09-09)
- Review oracle: Grok session `01a08479-9bfa-7c00-9d54-a088bc0bbe48` (2026-09-09)
  Effort 1, roster `general`. Artifacts under `review/`.

## Notes

- Trigger: amuru #91 merge GC refuse on task **113**.
- Related: amuru **121** (journals); architecture **208-001**; ganda
  `TaskWorkJournalIgnore.EnsurePorcelainGitignores`.
- Claim already appended the canonical block as uncommitted porcelain. Committed
  it; did not `git restore -- .gitignore`.

## Results

Committed the Ganda claim-pickup memsearch-memory block that was already appended
on this worktree. After it lands on `origin/master`, `EnsurePorcelainGitignores`
on claim is a no-op and `WorktreeGcService` no longer sees `M .gitignore`.

### What was implemented

- Root `.gitignore` now has the canonical comment + `.memsearch/memory/` next to
  the existing `*.journal.json` block (architecture 208-001 layout).
- Existing `.memsearch/` (line 430) and `*.journal.json` (line 433) kept.
- No journal blobs and no `.memsearch/memory/` daily notes in the commit.

### Files changed

- `.gitignore` — 4 lines appended (`6297157`)
- `kanban/in-progress/114-…/task.md` — column move + Results

### Key decisions / deviations

- `.memsearch/memory/` is redundant with `.memsearch/` for git. Committed anyway:
  `HasMemsearchMemoryGitignore` only accepts the four `memory` spellings, not
  `.memsearch/`. Matcher change is out of scope (follow-on on ganda if wanted).
- Did not `git rm --cached` any `.memsearch/memory/*.md` (none tracked). Promote
  stays `git add -f`.
- Other `ganda repo audit` FAILs (`bin-dev`, `kebab-path-names`, `nuru`, …) are
  pre-existing and out of scope.

### Test outcomes

- `git ls-files '*.journal.json'` — empty
- `git ls-files '.memsearch/'` — empty
- `git check-ignore -v` on this kitchen’s `task-work.journal.json` — matched
  `.gitignore:433:*.journal.json`
- `ganda repo audit --fix --checks routine-journals-gitignore,memsearch-memory-gitignore`
  - `routine-journals-gitignore` PASS (`FIXED`: already ignores)
  - `memsearch-memory-gitignore` PASS (`FIXED`: already ignores)
  - `--fix` did not change `.gitignore` further

### How to validate

**Smoke**

```bash
git show HEAD:.gitignore | tail -12
git ls-files '*.journal.json'
git ls-files '.memsearch/'
ganda repo audit --fix --checks routine-journals-gitignore,memsearch-memory-gitignore
```

**Expect**

- Tail includes `.memsearch/` (kept), then the routine-journal block
  (`*.journal.json`), then the memsearch-memory comment + `.memsearch/memory/`.
- Both `git ls-files` commands print nothing.
- Named checks `routine-journals-gitignore` and `memsearch-memory-gitignore`
  PASS. `--fix` reports already-ignores and does not rewrite `.gitignore`.
  Other full-audit FAILs are out of scope.
- After merge to `origin/master`, a fresh `ganda kanban claim` must not dirty
  `.gitignore` (`git status --porcelain -- .gitignore` empty).

### Review disposition

**Outcome:** `clean` (0 open; no issues raised; no exceptions)

- **Rounds:** 1
- **Effort / roster:** 1, `general`
- **Final counts:** bug 0 / suggestion 0 / nit 0 (all open=0, fixed=0, wontfix=0)
- **Paths:**
  - `review/review-framework.md`
  - `review/round-1/general.md`
  - `review/round-1/merged.md`
  - `review/disposition.md`
- **Wontfix / escalations:** none
