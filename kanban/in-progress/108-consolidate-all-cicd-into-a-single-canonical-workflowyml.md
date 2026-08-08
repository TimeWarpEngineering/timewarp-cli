# Consolidate all CI/CD into a single canonical workflow.yml

## Description

Org convention (timewarp-nuru 458 program; operator ruling 2026-08-08): every
repo has exactly ONE `.github/workflows/workflow.yml` carrying ALL CI/CD
functionality — modes/params are passed in (dispatch inputs, event detection),
never expressed as separate workflow files. **timewarp-nuru is the reference
implementation.** Trusted publishing policies target `workflow.yml` only.
The later 458 conversion (reusable-workflow caller) replaces workflow.yml's
CONTENT; this task fixes the SHAPE now.

Current workflow files in this repo: workflow.yml, sync-configurable-files.md

Disposition: workflow.yml already canonical — just delete the stray sync-configurable-files.md from the workflows dir.



## Checklist

- [ ] Exactly one `.github/workflows/workflow.yml` remains carrying all CI/CD (or, for cruft-only repos, zero workflows — do NOT invent CI where none is needed)
- [ ] `sync-configurable-files.*` deleted (abandoned org mechanism)
- [ ] `*.disabled` / `*.bak` cruft deleted
- [ ] Assistant workflows (claude*.yml), if present: explicitly kept (not CI/CD) or folded — record the call here
- [ ] CI still green after consolidation (where CI exists)

## Notes

Created from timewarp-nuru 458-009/458 rollout session, 2026-08-08.
