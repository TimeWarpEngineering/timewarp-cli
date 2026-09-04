# Align amuru skill docs and CI test-inclusion filters

## Description

Parent **111** round-1 findings **M15, M21–M24, M26**. Docs that contradict the 1.0 contract, plus CI/test-runner footguns. M25 (`cliwrap-exit-code-tests/`) is wontfix on 111.

Pinned tree: `fbd5d276fc5a936136a55d981fc121a23b991493`. Evidence: parent `review/round-1/merged.md`.

## Requirements

- **M21 (bug)** `skills/amuru/SKILL.md:9`, `:268-297` — skill claims it is authoritative and still says default throw-on-nonzero, `ExecutionResult`, and `AsJsonRpcClient`. Contradicts `AGENTS.md` / 090–092 / 094-001. Align with `CommandOutput` and default validation `None`; drop JSON-RPC/`ExecutionResult`.
- **M15 (suggestion)** `source/timewarp-amuru-tools/dot-net-commands/dot-net.md:3-24` — documents `ExecuteAsync()`, `GetStringAsync()`, TimeWarp.Cli. Rewrite to `RunAsync`/`CaptureAsync` or delete if unpacked.
- **M22 (suggestion)** `.github/workflows/workflow.yml:7-17` and `:21-31` — path filters omit `BannedSymbols.txt`, `.editorconfig`, `source/.editorconfig`, `timewarp-amuru.slnx`.
- **M23 (suggestion)** `tests/timewarp-amuru/multi-file-runners/Directory.Build.props:14-25` — hand-maintained non-recursive Compile globs. Use recursive `**/*.cs` or a disk-vs-Compile check (the 103 failure class).
- **M24 (nit)** `msbuild/repository.props:8` — `TestsDirectory` is `Tests/`; on-disk is `tests/`.
- **M26 (nit)** `tests/timewarp-amuru/Directory.Build.props:27` — tests import banned `System.Console` statically. Remove it.

Do not clone **082** kebab, **105** missing tests, **094-004** PublicAPI analyzers, or **106** XML-doc polish (stale RunBuilder / CliConfiguration TimeWarp.Cli text).

## Checklist

- [ ] M21 skill matches AGENTS.md error-handling and current types
- [ ] M15 DotNet reference examples compile against current builders
- [ ] M22 CI path filters cover BannedSymbols / editorconfig / slnx
- [ ] M23 aggregate runner cannot silently omit a new test folder
- [ ] M24 TestsDirectory casing
- [ ] M26 remove static System.Console using from tests
- [ ] `## Results` + `### How to validate`

## Notes

Parent: **111**. Sources: `review/round-1/tests-infra.md`, `tools-builders.md`.
