# Round 1 — testing-mocks
**Date:** 2026-09-04
**Scope reviewed:** `source/timewarp-amuru/testing/`, mock interception in `core/command-result.cs` / `command-extensions.cs`, tests under `tests/timewarp-amuru/` that use `CommandMock`

## Summary

CommandMock’s core story holds: `AsyncLocal` scoping, Strict as the default, double-`Enable` rejection, raw `string[]` mock identity on ordinary `CommandResult`s, NUL-separated keys, and interception on run/capture/stream/select/passthrough/TTY. 089’s argument-matching fallthrough for ordinary commands is not still present. The documented Pipe bypass is incomplete: last-stage CliWrap identity can still match under Loose.

## Issues

### Issue 1 — Severity: bug
- File: `source/timewarp-amuru/core/command-result.cs:93` and `:413`
- Description: Docs (`command-result.cs:21`, `CommandMock.cs:33-34`, `readme.md:351`) say Pipe compositions bypass mock matching. Piped results drop `MockExecutable`/`MockArguments` (`new CommandResult(pipedCommand)`), then `ResolveMockSetup` falls back to CliWrap’s last-stage `TargetFilePath` + space-split `Arguments`. Under Loose, `Setup("grep", "World")` can match `echo … | grep World` and skip the left stage. Under Strict, unmatched pipes throw (documented); a matched last-stage setup would still wrongly mock the composition. This is not a re-open of 089.
- Suggestion: When `MockExecutable` is null (piped composition), do not fall back to CliWrap identity. Strict: throw a pipe-specific message. Loose: return null and run the real pipeline. Add a regression test.
- Status: open

### Issue 2 — Severity: suggestion
- File: `source/timewarp-amuru/testing/CommandMock.cs:49`
- Description: Cleanup is `() => CurrentMockState.Value = null`, which only clears the dispose context’s `AsyncLocal`. Dispose from another async context leaves the enable context’s `MockState` live. Documented at `CommandMock.cs:35-36`.
- Suggestion: Mark the captured `MockState` disposed on scope dispose; have `State`/`ResolveMockSetup` ignore disposed state even if `AsyncLocal` still points at it.
- Status: open

### Issue 3 — Severity: suggestion
- File: `source/timewarp-amuru/core/command-extensions.cs:64-68`
- Description: `GetCommandPath` runs before `mockExecutable` is captured. If `SetCommandPath("git", "/custom/git")` is active, `Setup("git", …)` will not match. The comment claims logical identity “before any normalization”; only the `.cs` rewrite is excluded.
- Suggestion: Capture logical `mockExecutable` from the caller’s name before `GetCommandPath`.
- Status: open

### Issue 4 — Severity: nit
- File: `source/timewarp-amuru/testing/MockSetup.cs:31`
- Description: `Returns` / `ReturnsError` do not clear `Exception`; `Throws` does not clear stdout/stderr. `Setup(…).Throws(ex).Returns("ok")` still throws in `ApplyMockPreludeAsync`.
- Suggestion: Each terminal configurator should reset fields it does not own.
- Status: open

## Verified-clean notes

- Strict default throws on unmatched commands (`command-result.cs:102-107`). Covered by `StrictMode_Should_ThrowOnUnmockedCommand`.
- Loose fallthrough works; repo-service tests use Loose for mixed real/mocked git.
- 089: raw `string[]` on `CommandResult`; NUL key separator (`MockState.cs:97-101`); space-arg and `|`-arg tests present.
- All execution modes call `ResolveMockSetup`. `Delays()` registers (`MockSetup.cs:86-92`).
- Every `CommandMock.Enable` under `tests/timewarp-amuru/` is in a `using` (42 call sites).

## Out-of-scope mentions

- **082** kebab-case renames of testing files.
- **CliConfiguration** process-global path overrides are intentional vs AsyncLocal.
