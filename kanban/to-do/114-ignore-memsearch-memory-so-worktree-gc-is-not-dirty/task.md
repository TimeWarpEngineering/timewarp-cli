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

- [ ] Canonical `.memsearch/memory/` block in root `.gitignore`
- [ ] Existing `.memsearch/` and `*.journal.json` kept
- [ ] Results + How to validate

## Session

- Created: 2143941 (2026-09-09)
- Cockpit: timewarp-flow Grok `01a03d38-9611-7620-aae5-848e15dafa94`
  (2026-09-09). Do not implement in cockpit.

## Notes

- Trigger: amuru #91 merge GC refuse on task **113**.
- Related: amuru **121** (journals); architecture **208-001**; ganda
  `TaskWorkJournalIgnore.EnsurePorcelainGitignores`.
