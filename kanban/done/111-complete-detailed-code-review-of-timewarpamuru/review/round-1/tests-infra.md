# Round 1 — tests-infra
**Date:** 2026-09-04
**Scope reviewed:** `tests/timewarp-amuru/`, `samples/`, `tools/dev-cli/`, MSBuild, `.github/workflows/workflow.yml`, `BannedSymbols.txt`, two-package versioning, `skills/amuru/SKILL.md`

## Summary

CI still runs the full pipeline via `tools/dev-cli/dev.cs workflow`, and the aggregate Jaribu runner includes all 78 on-disk single-file tests (103’s gap stays closed). Two-package versioning is wired for push. Samples use current APIs. Remaining items are infra footguns plus a skill file that contradicts the 090 error-handling contract.

## Issues

### Issue 1 — Severity: bug
- File: `skills/amuru/SKILL.md:9`, `:268-297`, `:48-52`
- Description: The skill claims it is “the authoritative skill file” and still says “By default, commands throw on non-zero exit codes” (`:270`), documents `ExecutionResult` from `PassthroughAsync` (`:49`, `:283-297`), `AsJsonRpcClient` (`:213-218`), and `DotNet.Build("MyProject.csproj")` positional form. That contradicts `AGENTS.md` (default validation `None`, `CommandOutput`) and the 090–092/094-001 surface. Agents following the skill will write non-compiling or contract-wrong code.
- Suggestion: Align the skill with current `Shell.Builder` / `CommandOutput` / default-None validation; remove JSON-RPC and `ExecutionResult`.
- Status: open

### Issue 2 — Severity: suggestion
- File: `.github/workflows/workflow.yml:7-17` and `:21-31`
- Description: Push/PR path filters include `source/**`, `tests/**`, `Directory.Build.props`, etc., but not `BannedSymbols.txt`, `.editorconfig`, `source/.editorconfig`, or `timewarp-amuru.slnx`. A PR that only tightens banned APIs, formatting rules, or the solution file can merge without CI.
- Suggestion: Add those paths (or drop path filters for `master` PRs).
- Status: open

### Issue 3 — Severity: suggestion
- File: `tests/timewarp-amuru/multi-file-runners/Directory.Build.props:14-25`
- Description: Comment says “Include all test files”, but inclusion is a hand-maintained set of non-recursive `../single-file-tests/<folder>/*.cs` globs. Current tree matches (78 files). A new sibling folder is invisible to CI until props are updated — the class of silent omission 103 fixed.
- Suggestion: One recursive include (`../single-file-tests/**/*.cs`) or a check that fails if disk files are not in the Compile set.
- Status: open

### Issue 4 — Severity: nit
- File: `msbuild/repository.props:8`
- Description: `<TestsDirectory>$(RepositoryRoot)Tests/</TestsDirectory>` — on-disk path is lowercase `tests/`. Property appears unused by current tools/tests.
- Suggestion: Change to `tests/`.
- Status: open

### Issue 5 — Severity: nit
- File: `cliwrap-exit-code-tests/`
- Description: Not on CI, uses raw `ProcessStartInfo`, documents obsolete CliWrap/`ExecuteAsync` investigation. Easy to mistake for living tests.
- Suggestion: Delete, archive under `analysis/`, or clearly mark as historical.
- Status: open

### Issue 6 — Severity: nit
- File: `tests/timewarp-amuru/Directory.Build.props:27`
- Description: `<Using Include="System.Console" Static="true" />` while root `BannedSymbols.txt:1` bans `System.Console`. No current test calls `Console.*`.
- Suggestion: Remove the static using so accidental `WriteLine` fails RS0030.
- Status: open

## Verified-clean notes

- 103 stayed fixed: slnx lists both library projects; `global.json` pins SDK `10.0.301`; aggregate Compile includes `native/*.cs` and `repo-services/*.cs`; Cd/SetLocation restore CWD in try/finally.
- CI: `workflow.yml:88-97` → `dev.cs workflow` → `test-command.cs` → `run-tests.cs`.
- Runner ↔ disk: 78 single-file tests, every folder on disk is listed.
- Samples use `Shell.Builder` / `CaptureAsync` / `ScriptContext`; `*.cs.skip` is not compiled by verify-samples.
- Two-package versioning: core `1.0.0` in `source/Directory.Build.props:8`; Tools `1.0.0-beta.2` in csproj; release push resolves both.
- No InternalsVisibleTo. Library ProcessStartInfo use is only `TtyPassthroughAsync` with RS0030 pragma.

## Out-of-scope mentions

- **082** kebab leftovers (`AppContextExtensions.cs`, `TestHelpers.cs`, `Commands.*.cs`).
- **105** missing tests (tool builders, git members, Direct APIs).
- **094-004** PublicAPI analyzers; **101/108/109** release CI — not re-opened.
- `CleanCommand` only cleans core — filed under tools-services, not duplicated here.
