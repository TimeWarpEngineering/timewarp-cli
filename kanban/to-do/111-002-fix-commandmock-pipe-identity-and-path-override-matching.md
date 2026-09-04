# Fix CommandMock pipe identity and path-override matching

## Description

Parent **111** round-1 findings **M6–M8**. Documented mock contracts are wrong at the Pipe boundary and at `CliConfiguration` path overrides.

Pinned tree: `fbd5d276fc5a936136a55d981fc121a23b991493`. Evidence: parent `review/round-1/merged.md`.

## Requirements

- **M6 (bug)** `core/command-result.cs:93` and `:413` — Pipe currently drops `MockExecutable`/`MockArguments` then falls back to last-stage CliWrap identity. Under Loose, `Setup("grep", "World")` can match `echo … | grep World` and skip the left stage. When mock identity is null (piped composition), do not fall back to CliWrap identity. Strict: pipe-specific throw. Loose: run the real pipeline. Regression test required.
- **M7 (suggestion)** `testing/CommandMock.cs:49` — disposing the scope from another async context cannot clear the originating `AsyncLocal`. Tombstone `MockState` on dispose so leftover AsyncLocal is ignored.
- **M8 (suggestion)** `core/command-extensions.cs:64-68` — capture logical mock executable **before** `CliConfiguration.GetCommandPath` so `Setup("git")` still matches a path-overridden git.

Do not reopen **089** (ordinary-path argument matching is fixed). M9 (Throws-then-Returns leftover Exception) is wontfix on 111.

## Checklist

- [ ] M6 Pipe does not match last-stage setups; Loose runs the real pipeline; test
- [ ] M7 disposed MockState is ignored even if AsyncLocal still points at it
- [ ] M8 Setup uses the caller’s logical executable name, not the path override
- [ ] `## Results` + `### How to validate`

## Notes

Parent: **111**. Source: `review/round-1/testing-mocks.md`.
