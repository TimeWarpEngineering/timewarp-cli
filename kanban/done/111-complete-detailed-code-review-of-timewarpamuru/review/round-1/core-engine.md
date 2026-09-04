# Round 1 — core-engine
**Date:** 2026-09-04
**Scope reviewed:** `source/timewarp-amuru/core/`, `CliConfiguration.cs`, `ScriptContext.cs`, `AppContextExtensions.cs`, `interfaces/ICommandBuilder.cs`

## Summary

090–092 contracts hold on this tree: default validation is `None`, never-ran uses `NeverRanExitCode` (-1), and CliWrap types stay off public signatures. 097 remediations (StreamToFileAsync lock, Windows `.cs` host, SelectAsync cancellation, ScriptContext nesting, GetLines SplitLines, OutputLines AsReadOnly) are still present. Three real gaps remain on capture/select paths, plus two maintainability items.

## Issues

### Issue 1 — Severity: bug
- File: `source/timewarp-amuru/core/command-result.cs:520-526` and `:569-570`
- Description: `RunAndCaptureAsync` and `CaptureAsync` build `CommandOutput` from the exit code only and never set `RunTime = result.RunTime`. `PassthroughAsync` (`:202`) and `TtyPassthroughAsync` (`:306`) do set it. `readme.md:167` claims RunTime is available from every execution mode; `CommandOutput.RunTime` docs only exempt never-ran and mocks.
- Suggestion: Propagate `result.RunTime` on both capture paths. Add a CaptureAsync/RunAndCaptureAsync assertion. Leave mock/`Empty` at zero.
- Status: open

### Issue 2 — Severity: bug
- File: `source/timewarp-amuru/core/command-output.cs:172-188`
- Description: The string constructor skips lines with `!string.IsNullOrWhiteSpace(line)`, so interior blank lines never enter `lines`. `GetLines` documents interior blank preservation (`:115-128`) and `CaptureAsync` preserves them via the list constructor — but `RunAndCaptureAsync` (`command-result.cs:522-526`) and mock capture (`:546`, `:498`) go through the string constructor and drop blanks. `SplitMockLines` (`command-result.cs:127-128`) uses `RemoveEmptyEntries`, so mock streaming also drops blanks.
- Suggestion: Align the string constructor and `SplitMockLines` with `SplitLines`. Add a `RunAndCaptureAsync` blank-line test.
- Status: open

### Issue 3 — Severity: bug
- File: `source/timewarp-amuru/core/command-result.cs:349-363`
- Description: `SelectAsync` rethrows `OperationCanceledException` (097) but catches all other exceptions and returns `""`. With `WithZeroExitCodeValidation()`, CliWrap’s non-zero failure is swallowed. Task 090 / docs exempt only `TtyPassthroughAsync` (`:223-225`).
- Suggestion: Let validation/command-execution failures propagate (same as `CaptureAsync`); keep graceful degradation only for unexpected non-validation failures, or document an explicit SelectAsync exemption like TTY.
- Status: open

### Issue 4 — Severity: suggestion
- File: `source/timewarp-amuru/core/command-result.cs:199`, `:351`, `:465`, `:520`, `:569`, `:731`
- Description: All six `ExecuteAsync` awaits omit `.ConfigureAwait(false)`. CA2007 is error-severity (`source/.editorconfig`) but only applies to `Task`; CliWrap returns `CommandTask<T>`, which supports ConfigureAwait but is invisible to CA2007.
- Suggestion: `await cmd.ExecuteAsync(ct).ConfigureAwait(false)` on every `CommandTask` await.
- Status: open

### Issue 5 — Severity: suggestion
- File: `source/timewarp-amuru/core/command-options.cs:60`, `:111`, `:126`
- Description: Fluent `WithWorkingDirectory` / `WithNoValidation` / `WithZeroExitCodeValidation` reuse the same `EnvironmentVariables` dictionary instance. `WithEnvironmentVariable` copies; the others alias. Mutating the dictionary on one options instance can affect a derived instance.
- Suggestion: Copy the dictionary in every `With*` (same pattern as `WithEnvironmentVariable`).
- Status: open

## Verified-clean notes

- `CommandOptions.ApplyTo` always applies `Validation ?? None` (`command-options.cs:152-154`).
- Null/`NullCommandResult` paths return `NeverRanExitCode`.
- No CliWrap types in public signatures; no `InternalsVisibleTo`.
- StreamToFileAsync stdout/stderr writes serialized under `Lock` (`command-result.cs:710-729`).
- Windows `.cs` host inserts `--` then rewrites to `dotnet <script.cs> -- <args>` (`command-extensions.cs:71-84`).
- Streaming methods have `[EnumeratorCancellation]`.
- Core csproj `IsAotCompatible` is true. TTY raw `Process` is the documented RS0030 exception.

## Out-of-scope mentions

- **100** — `ICommandBuilder` still has `WithNoValidation` but not `WithZeroExitCodeValidation` (`interfaces/ICommandBuilder.cs:23`).
- **106** — stale `RunBuilder` XML on `ShellBuilder` ctor (`core/shell-builder.cs:18`).
- **094-004** — PublicAPI analyzers still to-do (status only).
- **082** — kebab-case cleanup still open.
