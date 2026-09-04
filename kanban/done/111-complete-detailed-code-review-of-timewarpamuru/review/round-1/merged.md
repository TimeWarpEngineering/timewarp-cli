# Round 1 — merged findings
**Date:** 2026-09-04
**Sources:** core-engine, testing-mocks, native-fs, tools-builders, tools-services, tests-infra, security

## Counts

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 13 | 0 | 0 |
| suggestion | 9 | 0 | 0 |
| nit | 2 | 0 | 2 |

Open total: 24. Wontfix: 2 (M9, M25). Zero issues from security (new).

## Issues

### M1 — Severity: bug — Status: open
- File: `source/timewarp-amuru/core/command-result.cs:520-526` and `:569-570`
- Description: `CaptureAsync` / `RunAndCaptureAsync` never set `CommandOutput.RunTime` from CliWrap’s result. Passthrough/TTY do. Readme claims RunTime from every execution mode.
- Suggestion: Propagate `result.RunTime`; add capture-path tests.
- Source: core-engine
- Disposition notes: child **111-001**

### M2 — Severity: bug — Status: open
- File: `source/timewarp-amuru/core/command-output.cs:172-188`
- Description: String constructor drops whitespace-only lines, so `RunAndCaptureAsync` and mock capture/`SplitMockLines` (`command-result.cs:127-128`) violate the documented interior-blank `GetLines` contract that `CaptureAsync` honors.
- Suggestion: Align string ctor and `SplitMockLines` with `SplitLines`.
- Source: core-engine
- Disposition notes: child **111-001**

### M3 — Severity: bug — Status: open
- File: `source/timewarp-amuru/core/command-result.cs:349-363`
- Description: `SelectAsync` catch-all swallows `WithZeroExitCodeValidation()` failures; only TTY is documented as exempt.
- Suggestion: Let validation exceptions propagate.
- Source: core-engine
- Disposition notes: child **111-001**

### M4 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru/core/command-result.cs:199`, `:351`, `:465`, `:520`, `:569`, `:731`
- Description: Six `CommandTask` `ExecuteAsync` awaits omit `ConfigureAwait(false)`. CA2007 does not see `CommandTask`.
- Suggestion: Add `.ConfigureAwait(false)` on every `CommandTask` await.
- Source: core-engine
- Disposition notes: child **111-001**

### M5 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru/core/command-options.cs:60`, `:111`, `:126`
- Description: Several `With*` methods alias the `EnvironmentVariables` dictionary instead of copying it.
- Suggestion: Copy the dictionary in every `With*`.
- Source: core-engine
- Disposition notes: child **111-001**

### M6 — Severity: bug — Status: open
- File: `source/timewarp-amuru/core/command-result.cs:93` and `:413`
- Description: Documented Pipe mock-bypass is incomplete: piped `CommandResult` falls back to last-stage CliWrap identity, so Loose setups for the last stage can swallow the left-hand command.
- Suggestion: Null mock identity on pipes must not fall back to CliWrap identity.
- Source: testing-mocks
- Disposition notes: child **111-002**

### M7 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru/testing/CommandMock.cs:49`
- Description: Disposing the mock scope from a different async context cannot clear the originating `AsyncLocal` (documented at `:35-36`).
- Suggestion: Tombstone the `MockState` on dispose so leftover AsyncLocal is ignored.
- Source: testing-mocks
- Disposition notes: child **111-002**

### M8 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru/core/command-extensions.cs:64-68`
- Description: Mock executable identity is captured after `CliConfiguration.GetCommandPath`, so `Setup("git")` misses path-overridden git.
- Suggestion: Capture logical name before the path override.
- Source: testing-mocks
- Disposition notes: child **111-002**

### M9 — Severity: nit — Status: wontfix
- File: `source/timewarp-amuru/testing/MockSetup.cs:31`
- Description: Chaining `Throws` then `Returns` leaves `Exception` set.
- Suggestion: Reset unused prelude fields per terminal configurator.
- Source: testing-mocks
- Disposition notes: wontfix — operator-error on an uncommon fluent chain; not worth a public-behavior change. Decider: implementer 111.

### M10 — Severity: bug — Status: open
- File: `source/timewarp-amuru/ScriptContext.cs:73-76` and `:97-100`
- Description: Factory pushes onto `LiveContexts` before `SetCurrentDirectory`. A throw leaks a live stack entry the caller cannot dispose.
- Suggestion: Change cwd before push, or pop/cleanup on failure.
- Source: native-fs
- Disposition notes: child **111-003**

### M11 — Severity: bug — Status: open
- File: `source/timewarp-amuru/ScriptContext.cs:143`
- Description: `OnExit` runs while `SyncLock` is held; nested ScriptContext in `onExit` deadlocks.
- Suggestion: Invoke `OnExit` outside the lock.
- Source: native-fs
- Disposition notes: child **111-003**

### M12 — Severity: bug — Status: open
- File: `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs:21-32`
- Description: Distinct from 104’s directory `AllDirectories` walk: `force` on a *file* symlink can clear read-only on the target, then delete only the link.
- Suggestion: Delete the link without mutating target attributes.
- Source: native-fs
- Disposition notes: child **111-003**

### M13 — Severity: bug — Status: open
- File: `source/timewarp-amuru/native/file-system/direct/Direct.RemoveItem.cs:63-80`
- Description: `RemoveReadOnlyAttribute` never clears read-only on the root directory itself, so `force`+`recursive` can still fail on Windows.
- Suggestion: Clear the root dir’s read-only bit after children (still skip reparse points per 104).
- Source: native-fs
- Disposition notes: child **111-003**

### M14 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru/native/PathResolver.cs:166-175`
- Description: Unix search uses `File.Exists` only; docs claim `which` equivalence (`:8-9`).
- Suggestion: Check execute bits or narrow the docs.
- Source: native-fs
- Disposition notes: child **111-003**

### M15 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru-tools/dot-net-commands/dot-net.md:3-24`
- Description: Documents `ExecuteAsync()`, `GetStringAsync()`, and TimeWarp.Cli — none exist on current builders.
- Suggestion: Rewrite to `RunAsync`/`CaptureAsync` or delete if unpacked.
- Source: tools-builders
- Disposition notes: child **111-005**

### M16 — Severity: bug — Status: open
- File: `source/timewarp-amuru-tools/repo/RepoCheckVersionService.cs:44-56` and `:160-187`
- Description: Git-tag check compares only to the latest tag, not exact `v{version}`. 076 (done) asked for `git tag -l "v{version}"`; the defect is still present. False “safe to release” when an older version is already tagged.
- Suggestion: Exact tag existence for `IsNewVersion`; keep latest tag for display.
- Source: tools-services
- Disposition notes: child **111-004**

### M17 — Severity: bug — Status: open
- File: `source/timewarp-amuru-tools/nu-get/nuget-package-service.cs:256-262` used by `RepoCheckVersionService.cs:92-112`
- Description: Unlisted versions are excluded from the same list used for already-published. Republish of an unlisted version 409s.
- Suggestion: Include unlisted for existence; listed-only for latest.
- Source: tools-services
- Disposition notes: child **111-004**

### M18 — Severity: bug — Status: open
- File: `source/timewarp-amuru-tools/repo/RepoCheckVersionService.cs:129-157` vs `timewarp-amuru-tools.csproj:12`
- Description: Version is always read from core `source/Directory.Build.props`. Tools has its own `<Version>1.0.0-beta.2</Version>`. `CheckNuGetVersionAsync(["TimeWarp.Amuru.Tools"])` compares the wrong number.
- Suggestion: Per-package version, or pass version in.
- Source: tools-services
- Disposition notes: child **111-004**

### M19 — Severity: bug — Status: open
- File: `tools/dev-cli/endpoints/clean-command.cs:27-59`
- Description: `dev clean` does not call `RepoCleanService`. It `dotnet clean`s only the core csproj and `Directory.Delete(bin, recursive: true)` without 098 reparse/tracked-file guards, and deletes preserved `dev`/`dev.exe`.
- Suggestion: Delegate artifact cleanup to `RepoCleanService`.
- Source: tools-services
- Disposition notes: child **111-004**

### M20 — Severity: suggestion — Status: open
- File: `source/timewarp-amuru-tools/repo/RepoCleanService.cs:146-199`
- Description: Root-bin children skip `HasTrackedFilesAsync` (only `dev`/`dev.exe` preserved).
- Suggestion: Apply the tracked-file and reparse guards to root-bin children.
- Source: tools-services
- Disposition notes: child **111-004**

### M21 — Severity: bug — Status: open
- File: `skills/amuru/SKILL.md:9`, `:268-297`
- Description: Skill claims it is authoritative and still says default throw-on-nonzero, `ExecutionResult`, and `AsJsonRpcClient`. Contradicts `AGENTS.md` / 090–092 / 094-001.
- Suggestion: Align with `CommandOutput` and default validation `None`; drop JSON-RPC/`ExecutionResult`.
- Source: tests-infra
- Disposition notes: child **111-005**

### M22 — Severity: suggestion — Status: open
- File: `.github/workflows/workflow.yml:7-17` and `:21-31`
- Description: Path filters omit `BannedSymbols.txt`, `.editorconfig`, `source/.editorconfig`, `timewarp-amuru.slnx`.
- Suggestion: Add those paths.
- Source: tests-infra
- Disposition notes: child **111-005**

### M23 — Severity: suggestion — Status: open
- File: `tests/timewarp-amuru/multi-file-runners/Directory.Build.props:14-25`
- Description: Hand-maintained non-recursive Compile globs; a new sibling folder is invisible to CI (the 103 failure class).
- Suggestion: Recursive `**/*.cs` or a disk-vs-Compile check.
- Source: tests-infra
- Disposition notes: child **111-005**

### M24 — Severity: nit — Status: open
- File: `msbuild/repository.props:8`
- Description: `TestsDirectory` is `Tests/` (wrong case); on-disk is `tests/`. Unused today.
- Suggestion: Change to `tests/`.
- Source: tests-infra
- Disposition notes: child **111-005**

### M25 — Severity: nit — Status: wontfix
- File: `cliwrap-exit-code-tests/`
- Description: Historical CliWrap investigation tree; not on CI; uses raw Process.
- Suggestion: Archive or delete.
- Source: tests-infra
- Disposition notes: wontfix — not shipped, not CI, not a living sample. Decider: implementer 111.

### M26 — Severity: nit — Status: open
- File: `tests/timewarp-amuru/Directory.Build.props:27`
- Description: Tests import banned `System.Console` statically; no current `Console.*` calls.
- Suggestion: Remove the using so RS0030 fires on accidents.
- Source: tests-infra
- Disposition notes: child **111-005**

## Duplicates / conflicts

- tests-infra “clean only core project” collapsed into **M19** (tools-services owns the consumer + the 098 bypass).
- security filed zero new issues; native-fs file-symlink force (**M12**) and CleanCommand (**M19**) are the delete-safety items, kept in their area with strongest severity.
- GetChildItem `Task.Yield` per entry folded into **104** (same missing-cancellation family) — not a new M#.
- 104 / 087 / 088 / 099 / 100 / 082 / 094-004 / 105 / 106 confirmed still present; not cloned.
