# Review framework — task 113

**Date:** 2026-09-09
**Host task:** `kanban/in-progress/113-keep-last-two-packages-artifacts-in-ci/`
**Diff scope:** branch `task/113-keep-last-two-packages-artifacts-in-ci` vs `origin/master` (product commit `d779563` — `.github/workflows/workflow.yml`; kitchen `task.md` is board hygiene)
**Plan / brief:** Stop Actions artifact quota refill. Upload Artifacts only on green master (`success()` + `github.ref == 'refs/heads/master'`), skip PR / release / probe / failed runs. Path stays `artifacts/packages/*.nupkg`. `if-no-files-found: error`. `retention-days: 7`. After upload, prune older artifacts whose name starts with `Packages-`, keep the two newest. `permissions.actions: write` for DELETE. Paginate with `gh api` + `${{ github.token }}`. If the just-uploaded name is not listed yet, do not fail. Do not delete `Executables-*` / `Installer-*`. YAML-only; do not touch ganda/nuru YAML.
**Effort:** 1 (general only)
**Reviewer roster:** general
**Session IDs:** implementer grok `01a083df-bd5d-70a3-9b78-3f8cc3cd3c23` (2026-09-09); review-oracle grok `01a083e4-42c2-7f40-899f-2c22b52a49a1` (2026-09-09)

## Ground rules

- Reviewers are read-only on product code; they write only under `review/round-N/`
- Severity: bug | suggestion | nit — Status starts as open
- Do not invent issues to fill space; zero issues is a valid outcome
- Address the diff and surrounding call sites; re-verify falsifiable claims against the repo
- Prior rounds are immutable; new work goes in `round-(N+1)/`
- Unrelated unstaged `.gitignore` `.memsearch/memory/` dirt is out of scope; do not treat it as this task's change
- Do not treat pre-existing audit failures as findings for this task
- Siblings ganda 277 / nuru 471 are policy context, not required byte-identical YAML
