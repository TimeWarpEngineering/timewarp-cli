# Round 1 — tools-builders
**Date:** 2026-09-04
**Scope reviewed:** `source/timewarp-amuru-tools/dot-net-commands/`, `git-commands/`, `fzf-command/`

## Summary

DotNet/Fzf builders construct via public `Shell.Run`; Git helpers via public `Shell.Builder`. Default `CommandOptions` validation remains `None`. No new invalid-flag / argv / Windows-breakage findings beyond the already-tracked tasks (087/088/099/100/082). One in-tree reference doc still describes deleted APIs.

## Issues

### Issue 1 — Severity: suggestion
- File: `source/timewarp-amuru-tools/dot-net-commands/dot-net.md:3-24`
- Description: The DotNet fluent reference documents `ExecuteAsync()`, `GetStringAsync()`, and “TimeWarp.Cli”. None of those members exist on the current builders (`ExecuteAsync` grep over `source/timewarp-amuru-tools/**/*.cs` is empty). Callers copying the samples will not compile. Distinct from 106’s XML-doc polish.
- Suggestion: Rewrite examples to `RunAsync` / `CaptureAsync` / `CommandOutput` and drop the TimeWarp.Cli name, or delete the file if it is not packed.
- Status: open

## Verified-clean notes

- Every DotNet `Build()` and Fzf `Build()` path ends in `Shell.Run(...)`. Git helpers use `Shell.Builder("git")`. No InternalsVisibleTo; no raw Process in these areas.
- Builders start from `new CommandOptions()`; core `ApplyTo` forces `Validation ?? None`.
- Spot-checked against current Microsoft docs (not re-opening 087): Pack `--runtime`/`--force`/`--source`, Clean `--output`, Run `-e`, Add/Remove package positional project insert, Tool Install/Update `--add-source`, worktree add argument order.
- Fzf option tokens outside input/`SelectWithFzf` remain coherent with fzf’s `=` form.

## Out-of-scope mentions (known tasks confirmed)

- **087** — `--tl` two-token form at `DotNet.Build.cs:341` (and Restore/Run/Test/Publish/Pack copies); bare `WithCollect()` `DotNet.Test.cs:247`; Pack `WithFramework` `:85`; DevCerts `WithExport` `DotNet.DevCerts.cs:108`; nuget why `--project` `DotNet.NuGet.cs:1159`; nuget delete `--configfile` `:494`; Watch `--include`/`--exclude`/`--property` `DotNet.Watch.cs:334`; Run `WithProject`+`WithFile` `DotNet.Run.cs:53-64`.
- **088** — `ExtractFzfArguments` stub `Fzf.Extensions.cs:28-33`; `FromInput` via `echo` `Fzf.cs:68-69`; `FromFiles` via `find` `:74`; `FromCommand` space-split `:79`.
- **099** — `UpdateBranchAsync` non-worktree `fetch origin branch:branch` `Git.UpdateBranch.cs:53-56`; `master` defaults `Git.GetWorktreePath.cs:28`.
- **100** — many builders lack `ICommandBuilder` / `WithZeroExitCodeValidation` (Watch, NuGet*, Workload*, Tool*, UserSecrets*, FzfBuilder).
- **082** — PascalCase `DotNet.*.cs` / `Git.*.cs` / `Fzf.*.cs`.
