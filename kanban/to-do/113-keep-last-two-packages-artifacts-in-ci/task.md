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

- [ ] Upload only green master (not `always()`, not PRs)
- [ ] `retention-days: 7`
- [ ] `actions: write` + keep-last-two `Packages-*` prune
- [ ] Results + How to validate

## Session

- Created: 1966299 (2026-09-09)
- Cockpit: timewarp-flow Grok `01a03d38-9611-7620-aae5-848e15dafa94`
  (2026-09-09). Do not implement in cockpit.

## Notes

- Siblings: ganda **277**, nuru **471**.
- Current upload is `if: always()` at the end of `.github/workflows/workflow.yml`.
