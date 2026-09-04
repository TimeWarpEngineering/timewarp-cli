# Fix CaptureAsync RunTime blanks and SelectAsync validation

## Description

Parent **111** round-1 findings **M1–M5**. Core capture/select paths drift from the documented `CommandOutput` contract.

Pinned tree: `fbd5d276fc5a936136a55d981fc121a23b991493`. Evidence lives in parent `kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/review/round-1/merged.md`.

## Requirements

- **M1 (bug)** `core/command-result.cs:520-526` and `:569-570` — set `CommandOutput.RunTime` from CliWrap’s result on `RunAndCaptureAsync` and `CaptureAsync` (passthrough/TTY already do). Add a capture-path assertion.
- **M2 (bug)** `core/command-output.cs:172-188` — string constructor drops whitespace-only lines; `SplitMockLines` (`command-result.cs:127-128`) uses `RemoveEmptyEntries`. Align with `SplitLines` so `RunAndCaptureAsync` and mocks preserve interior blanks like `CaptureAsync`.
- **M3 (bug)** `core/command-result.cs:349-363` — `SelectAsync` catch-all swallows `WithZeroExitCodeValidation()` failures. Let validation/command-execution failures propagate; only TTY is documented as exempt.
- **M4 (suggestion)** six `ExecuteAsync` awaits on `CommandTask` omit `ConfigureAwait(false)` (`command-result.cs:199,351,465,520,569,731`). CA2007 does not see `CommandTask`.
- **M5 (suggestion)** `command-options.cs:60,111,126` — copy `EnvironmentVariables` in every `With*` (do not alias the dictionary).

Do not reopen 090–092 / 097 unless the defect is still present after these fixes.

## Checklist

- [ ] M1 RunTime on CaptureAsync / RunAndCaptureAsync + test
- [ ] M2 string ctor + SplitMockLines preserve interior blanks + test
- [ ] M3 SelectAsync does not swallow zero-exit-code validation
- [ ] M4 ConfigureAwait(false) on CommandTask awaits
- [ ] M5 EnvironmentVariables copied in With* methods
- [ ] `## Results` + `### How to validate`

## Notes

Parent: **111**. Source: `review/round-1/core-engine.md`.
