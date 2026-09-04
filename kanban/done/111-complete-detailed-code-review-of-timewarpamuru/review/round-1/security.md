# Round 1 — security
**Date:** 2026-09-04
**Scope reviewed:** argv construction, path traversal, delete safety, mock bypass, secrets in env/logs, Windows vs Unix command construction

## Summary

No new security defects on pinned SHA `fbd5d276fc5a936136a55d981fc121a23b991493`. Core execution builds argv via CliWrap (`UseShellExecute = false` on the TTY path). Repo-clean 098 guards are still present. Strict `CommandMock` does not silently fall through to real processes. Native force-remove symlink mutation remains open only as already-tracked **104**. Cross-area delete-safety items (CleanCommand, ScriptContext, file-symlink force) are filed in their area reports, not duplicated here as new security M#s.

## Issues

None.

## Verified-clean notes

**Argv / no shell injection**
- Construction is `Cli.Wrap(executable).WithArguments(arguments ?? [])` at `command-extensions.cs:87-88`.
- No `/bin/sh -c`, `bash -c`, or `cmd.exe /c` in `source/`.
- TTY path sets `UseShellExecute = false` (`command-result.cs:254-261`).
- Tool wrappers pass discrete string args via `Shell.Builder` / `Shell.Run`.
- `Fzf.FromCommand` splits then runs argv (`Fzf.cs:76-84`) — not a shell. Fragile quoting is **088**.

**Windows vs Unix**
- `.cs` scripts get `--`; on Windows they rewrite to `dotnet <script> -- <args>` (`command-extensions.cs:71-84`). Still argv-based.

**Delete safety — repo clean (098 still holds)**
- `FindDirectories` skips `ReparsePoint` (`RepoCleanService.cs:76-80`).
- Candidates with git-tracked files are skipped; git failure fails closed (`:106-110,131-138`).

**Mock bypass**
- Default Strict throws on unmatched setups (`command-result.cs:102-108`).
- `Pipe` drops mock identity; under strict that throws rather than running real commands.
- Loose intentionally allows real execution.

**Secrets**
- Env is applied to the child only; Amuru does not echo env into its own logs.
- NuGet API keys / cert passwords are passed as upstream CLI argv — same contract as `dotnet` itself.

## Out-of-scope mentions

- **104** (still open): `Direct.RemoveItem` `force=true` → `RemoveReadOnlyAttribute` uses `SearchOption.AllDirectories` (`Direct.RemoveItem.cs:63-80`).
- **088**: Fzf `FromCommand` / `FromInput` / `FromFiles` fragility — correctness/UX, not injection.
- **098**: Done; re-verified present.
- Cross-area (not cloned here): native-fs file-symlink force (new, different from 104 dirs); tools-services `CleanCommand` `Directory.Delete(bin)` without 098 guards.
