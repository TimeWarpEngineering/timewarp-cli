# Keep last two Packages artifacts in CI

## Description

Org GitHub Actions artifact quota filled (ganda master upload after #155
failed while tests were green). Amuru had **436** artifacts / ~13.8 GB
(`Packages-*` plus old `Executables-*` / `Installer-*`) because
**Upload Artifacts is `if: always()`** with **no `retention-days`**.
Operator pruned to one newest pack. Stop the refill.

Same policy is landing on **ganda 277** and **nuru 471**.

## Requirements

File: `.github/workflows/workflow.yml` only.

### Upload gates (match ganda)

Today:

```yaml
if: always()
# no retention-days
# if-no-files-found: ignore
# path: artifacts/packages/*.nupkg
```

Change to **success() + `github.ref == 'refs/heads/master'`**, skip PR,
skip release, skip probe (`workflow_dispatch` mode probe). Do not upload
on failed runs.

Keep `path: artifacts/packages/*.nupkg`. `if-no-files-found`: **error**
on that master upload (honest miss).

### Retention backstop

`retention-days: 7`.

### Keep last two

After a successful Upload Artifacts step, delete older artifacts whose
**name starts with `Packages-`**. Keep the **two newest**. Do not delete
other names (if any `Executables-*` / `Installer-*` remain, leave them;
they expire on GitHub default and must not be recreated).

- Add `permissions.actions: write` (job today has only `contents` +
  `id-token`). Comment why write is required for prune. Release download
  still works with write.
- `${{ github.token }}` / `GH_TOKEN`. Paginate.
- If the new artifact is not listed yet, do not fail the job.

### Not in scope

- Org retention UI
- Ganda/nuru YAML (those are 277 / 471)
- Reintroducing RID executable/installer artifact uploads

## Checklist

- [x] Upload only green master (not `always()`, not PRs)
- [x] `retention-days: 7`
- [x] `actions: write` + keep-last-two `Packages-*` prune
- [x] Results + How to validate
- [x] Implementation review (effort 1, general) under `review/`
- [x] Disposition `clean` (0 open); Results include review disposition

## Session

- Created: 1966299 (2026-09-09)
- Cockpit: timewarp-flow Grok `01a03d38-9611-7620-aae5-848e15dafa94`
  (2026-09-09). Do not implement in cockpit.
- Implementer: Grok `01a083df-bd5d-70a3-9b78-3f8cc3cd3c23` (2026-09-09)
- Review oracle: grok `01a083e4-42c2-7f40-899f-2c22b52a49a1` (2026-09-09) —
  `tw-implementation-review` effort 1, general only; reviewer subagent
  `01a083e5-bf33-7ca1-801e-35788d2ff240`

## Notes

- Siblings: ganda **277**, nuru **471**.
- Upload was `if: always()` at the end of `.github/workflows/workflow.yml`;
  replaced with green-master gates + 7-day retention + Packages-* prune.
- Implementation review trail: `review/review-framework.md`,
  `review/round-1/merged.md`, `review/disposition.md` (outcome `clean`).

## Results

`.github/workflows/workflow.yml` only. Upload Artifacts now runs on
**success() + `github.ref == 'refs/heads/master'`**, skipping PR, release,
probe (`workflow_dispatch` mode probe), and failed runs. Path stays
`artifacts/packages/*.nupkg`. `if-no-files-found` is **error**. Retention
is **7 days**. `permissions.actions: write` is set so the post-upload
step can DELETE older `Packages-*` artifacts (paginated `gh api`,
`GH_TOKEN=${{ github.token }}`), keeping the two newest. Other names
(`Executables-*`, `Installer-*`) are not selected. If the just-uploaded
name is not listed yet, prune exits 0.

**Files changed:** `.github/workflows/workflow.yml`; kitchen `task.md` + `review/` (framework, round-1 general/merged, disposition)

**Decisions / deviations:** none vs brief. Amuru has no
`workflow_dispatch` release mode, so the skip list is release event +
probe only (ganda-shaped gates otherwise).

**Test outcomes:** YAML parse + policy assertions passed. Local
keep-last-two sort/tail fixture passed (four `Packages-*` → delete the
two oldest; missing current name skips). No product test suite for this
YAML-only change.

### How to validate

**Smoke**

```bash
python3 - <<'PY'
import yaml
doc = yaml.safe_load(open(".github/workflows/workflow.yml"))
ci = doc["jobs"]["ci"]
assert ci["permissions"]["actions"] == "write"
upload = next(s for s in ci["steps"] if s["name"] == "Upload Artifacts")
prune = next(s for s in ci["steps"] if s["name"] == "Keep last two Packages artifacts")
assert "always()" not in upload["if"]
assert "success()" in upload["if"] and "refs/heads/master" in upload["if"]
assert "release" in upload["if"] and "probe" in upload["if"]
assert upload["with"]["path"] == "artifacts/packages/*.nupkg"
assert upload["with"]["if-no-files-found"] == "error"
assert upload["with"]["retention-days"] == 7
assert prune["env"]["GH_TOKEN"] == "${{ github.token }}"
run = prune["run"]
assert "gh api --paginate" in run and 'startswith("Packages-")' in run
assert "tail -n +3" in run and "not listed yet" in run
print("ok")
PY
```

**Expect:** prints `ok`. `rg 'if: always\\(\\)' .github/workflows/workflow.yml`
matches nothing.

After merge, the next **green master** CI run uploads `Packages-{run_number}`
and the prune step leaves at most two non-expired `Packages-*` artifacts.
PR / failed / probe / release runs do not upload.

**Not in scope:** org retention UI; live DELETE against GitHub from this
worktree; deleting leftover `Executables-*` / `Installer-*`.

### Review disposition

- **Rounds:** 1
- **Effort / roster:** 1 — general only (`review/round-1/general.md`)
- **Counts (final):** bug 0 / suggestion 0 / nit 0 — all `open`/`fixed`/`wontfix` are 0
- **Disposition:** `clean` (no issues raised; no fix loop; no exceptions)
- **Paths:** `review/review-framework.md`, `review/round-1/general.md`,
  `review/round-1/merged.md`, `review/disposition.md`
