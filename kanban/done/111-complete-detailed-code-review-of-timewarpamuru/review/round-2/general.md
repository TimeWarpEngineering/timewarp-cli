# Round 2 — general
**Date:** 2026-09-04
**Scope reviewed:** implementer commit 09715af (review kitchen + Results) and independent re-verification of round-1 M1–M26 against current source

## Summary

Independent re-read of every M1–M26 citation against current product source confirms all 24 opens and both wontfix nits; none are false, overstated into a different severity class, or clones of the excluded product tasks. Security’s zero-new-issues claim holds. Kitchen quality is mostly solid (counts, disposition ↔ Results, child batching, excluded-task discipline), but How to validate as written fails from the claimed worktree, and children on origin-home cite parent `merged.md` that is not yet on `origin/master`.

## Issues

### Issue 1 — Severity: bug
- File: kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/task.md:175-177
- Description: Smoke validation `cd`s into this claimed worktree, then runs bare `ganda kanban show 111-001` … `111-005` expecting each in to-do on origin-home. From that worktree those commands fail with `Task '111-00N' not found on this board` because the children live only on origin-home (`kanban/to-do/111-00N-….md`) and are absent from this worktree’s board tree. The same ids succeed with `ganda kanban show --repo timewarp-amuru 111-00N` (or from the origin-home master checkout).
- Suggestion: Change How to validate to `ganda kanban show --repo timewarp-amuru 111-001` (etc.), or document that the child smoke must run against origin-home, not the claimed 111 worktree.
- Status: open

### Issue 2 — Severity: suggestion
- File: kanban/to-do/111-001-fix-captureasync-runtime-blanks-and-selectasync-validation.md:7 (on origin/master) vs absence of `kanban/.../review/round-1/merged.md` on origin/master
- Description: Children 111-001…005 are published to origin-home and point at parent evidence `review/round-1/merged.md`, but implementer commit `09715af` (which adds `merged.md`, `disposition.md`, and Results) is not an ancestor of `origin/master`. On origin-home the parent folder still only has the kitchen scaffold (`task.md` + `review-framework.md` under `kanban/to-do/111-…`). Requirements text is inlined so children remain workable; the cited evidence path is not.
- Suggestion: Publish/merge `09715af` (or equivalent) to origin-home before or with the child briefs, or point child Evidence at inlined Requirements only until the parent review tree lands.
- Status: open

## Verified round-1 findings

- M1 — CONFIRMED — `source/timewarp-amuru/core/command-result.cs:520-526`, `:569-570` (no `RunTime`); contrast `:202`, `:306`; `readme.md:167`
- M2 — CONFIRMED — `source/timewarp-amuru/core/command-output.cs:172-188` (`IsNullOrWhiteSpace` drop); `command-result.cs:127-128` (`RemoveEmptyEntries`); `GetLines`/`SplitLines` at `command-output.cs:117-144`
- M3 — CONFIRMED — `source/timewarp-amuru/core/command-result.cs:349-363` catch-all; TTY exemption documented at `:223-226`; validation applied via `command-options.cs:152-154`
- M4 — CONFIRMED — `command-result.cs:199`, `:351`, `:465`, `:520`, `:569`, `:731` (bare `ExecuteAsync` awaits)
- M5 — CONFIRMED — `command-options.cs:60`, `:111`, `:126` alias `EnvironmentVariables`; copy only in `:76-78`, `:96`
- M6 — CONFIRMED — `command-result.cs:93` fallback; Pipe at `:413` drops mock identity; CliWrap piped `TargetFilePath` is last stage (probed)
- M7 — CONFIRMED — `testing/CommandMock.cs:49` clear-only; documented AsyncLocal limit at `:35-36`
- M8 — CONFIRMED — `command-extensions.cs:64-68` captures `mockExecutable` after `GetCommandPath`
- M9 — CONFIRMED (wontfix ok) — `testing/MockSetup.cs:31-37` `Returns` does not clear `Exception` set by `Throws` (`:63-64`, `:75`)
- M10 — CONFIRMED — `ScriptContext.cs:54` push in ctor; `SetCurrentDirectory` after at `:75-76`, `:99-100`
- M11 — CONFIRMED — `ScriptContext.cs:143` `OnExit` inside `Cleanup`; called under `SyncLock` from `:159-170`, `:117-122`
- M12 — CONFIRMED — `native/file-system/direct/Direct.RemoveItem.cs:21-32` force clears `FileInfo.IsReadOnly` then `File.Delete` (distinct from 104’s dir `AllDirectories` walk)
- M13 — CONFIRMED — `Direct.RemoveItem.cs:63-80` clears children only; root `directory` never cleared
- M14 — CONFIRMED — `native/PathResolver.cs:8-9` (`which` claim) vs `:166-175` (`File.Exists` only)
- M15 — CONFIRMED — `source/timewarp-amuru-tools/dot-net-commands/dot-net.md:3-24` (`TimeWarp.Cli`, `ExecuteAsync`, `GetStringAsync`)
- M16 — CONFIRMED — `repo/RepoCheckVersionService.cs:44-56` latest-tag compare; `:160-187` `GetLatestGitTagAsync` only
- M17 — CONFIRMED — `nu-get/nuget-package-service.cs:256-262` skips unlisted; used at `RepoCheckVersionService.cs:92-112`
- M18 — CONFIRMED — `RepoCheckVersionService.cs:129-157` reads core `Directory.Build.props`; Tools version at `timewarp-amuru-tools.csproj:12`
- M19 — CONFIRMED — `tools/dev-cli/endpoints/clean-command.cs:27-59` no `RepoCleanService`; core-only clean; `Directory.Delete(bin, true)`
- M20 — CONFIRMED — `RepoCleanService.cs:146-199` root-bin children lack `HasTrackedFilesAsync` / reparse guards (those exist at `:77`, `:106`)
- M21 — CONFIRMED — `skills/amuru/SKILL.md:9` authoritative; `:270` throw-on-nonzero; `:283-297` `ExecutionResult`; `AsJsonRpcClient` at `:213-214` (cite range slightly narrow, claim still true)
- M22 — CONFIRMED — `.github/workflows/workflow.yml:7-17`, `:21-31` omit `BannedSymbols.txt`, `.editorconfig`, `source/.editorconfig`, `timewarp-amuru.slnx` (all present on disk)
- M23 — CONFIRMED — `tests/timewarp-amuru/multi-file-runners/Directory.Build.props:14-25` non-recursive per-folder globs
- M24 — CONFIRMED — `msbuild/repository.props:8` `Tests/`; on-disk `tests/`
- M25 — CONFIRMED (wontfix ok) — `cliwrap-exit-code-tests/` present; not CI
- M26 — CONFIRMED — `tests/timewarp-amuru/Directory.Build.props:27` static `System.Console`; no current `Console.*` under `tests/timewarp-amuru/`

## Kitchen checks

- Children 111-001..005 on origin-home vs merged disposition notes: present under `origin/master:kanban/to-do/111-00N-….md`; finding batches match disposition (`M1–M5`, `M6–M8`, `M10–M14`, `M16–M20`, `M15+M21–M24+M26`); Requirements cite the same paths/lines as merged.md
- Counts table vs listed M#: bug 13 / suggestion 9 / nit 2 open + 2 wontfix = 26 (`M1`–`M26`); open total 24 matches children coverage; M9/M25 wontfix only
- How to validate commands from this worktree: round-1 listing + disposition Outcome smoke pass; bare `ganda kanban show 111-00N` fails (Issue 1); HEAD is `09715af` (allowed as later review-only commit)
- Excluded tasks not cloned: merged.md notes 087/088/094-004/099/100/104/105/106/082 as present-not-cloned; M12 correctly distinguished from 104
- Disposition outcome vs remaining opens / children / wontfix: `accepted-exceptions` matches 24 opens filed as children + M9/M25 wontfix; Results table and child table agree; security area file has Issues = None
- Additional: parent review tree (`merged.md` / `disposition.md`) still local to the task branch (`09715af` not on `origin/master`) while children already published — Issue 2
