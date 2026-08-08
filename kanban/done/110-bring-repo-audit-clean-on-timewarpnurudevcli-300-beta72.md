# Bring repo audit-clean on TimeWarp.Nuru.DevCli 3.0.0-beta.72

## Description

Org wave (timewarp-nuru 458-010 remediation + DevCli 3.0.0-beta.72 adoption —
they are the same wave: the audit's `nuru` check went red org-wide when
beta.72 shipped, by design). Passing `ganda repo audit` now means adopting the
full release toolkit: `dev release`, promotion gates, attestation verifier,
trusted-publishing probe, derived package sets.

## Checklist

- [x] `ganda repo audit --fix` (bumps TimeWarp.Nuru/DevCli to latest, fixes kebab/structure where fixable)
- [x] Verify Directory.Packages.props pins TimeWarp.Nuru.DevCli (and TimeWarp.Nuru where referenced) at 3.0.0-beta.72
- [x] Build — NURU050 names any missing DI registration (e.g. `IPackableProjectService`); add per the DevCli readme migration notes (CS0101 local-CiMode note also applies)
- [x] `dev self-install` (AOT binary is a snapshot; new commands like `release` are absent until reinstalled)
- [x] `ganda repo audit` → PASSES ALL CHECKS (if a check is structurally unfixable here, record it explicitly with a reason instead of forcing)
- [x] Smoke: `dev --help` shows `release`; `dev check-version` derives the packable set (publishers only)
- [x] Commit everything (audit fixes, props, dev.cs, kanban) — local commits fine; ride the repo's normal merge flow

## Notes

Created 2026-08-08 from the nuru 458 program session. timewarp-nuru is the
reference (audit-clean at beta.72, first release shipped through the full
machinery).

## Session

- Implementation: grok 2026-08-08 — assess (14/9) → --fix → DevCli DI/workflow → kebab renames → self-install → audit green

## Results

### Outcome
Audit-clean on TimeWarp.Nuru / DevCli 3.0.0-beta.72. `dev --help` includes
`release`; `dev check-version` derives packable set (TimeWarp.Amuru,
TimeWarp.Amuru.Tools).

### Before
- Passed 14 / Failed 9 (assembly-metadata, bin-dev, capabilities, kebab, memsearch, nuru, runfile-exec, runfile-shebang, vscode-icon)

### After
- Passed 23 / Failed 0

### Key fixes
- `--fix`: Nuru pin, shebangs (82), exec bits, memsearch, vscode icon, Build.Tasks (then de-duped vs source/Directory.Build.props)
- DevCli 3.0.0-beta.72 + Terminal package refs; exclude local clean/check-version/self-install (CS0101)
- DI: IRepoCleanService, NuGetVersionService, IRepoConfigService, IPackableProjectService
- workflow-command: inject services; remove local CiMode (package CS0101)
- Kebab: workspace timestamp filenames + cliwrap-exit-code-tests/readme.md

### How to validate
**Smoke**
```bash
cd /home/steve/worktrees/github.com/TimeWarpEngineering/timewarp-amuru/dev
ganda repo audit
grep Nuru Directory.Packages.props
./bin/dev --help
./bin/dev check-version
```
**Expect:** all audit PASS; help shows `release`; check-version lists Amuru + Amuru.Tools.

**Automated gate**
```bash
ganda repo audit
dotnet build tools/dev-cli/dev.cs
```
