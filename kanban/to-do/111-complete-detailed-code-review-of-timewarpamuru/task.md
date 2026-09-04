# Complete detailed code review of TimeWarp.Amuru

## Description

Whole-repo implementation review of **TimeWarp.Amuru as it exists on origin-home `master`**, not a PR delta.

This is a successor pass after the 2026-07-04 release review (engine bugs → **097**, public-API scoping → **094**, error-handling contract → **090–092**). Core is already **1.0.0**; Tools is **1.0.0-beta.2**. Re-review the **current** tree (SHA pinned in `review/review-framework.md`) for new defects, regressions of those remediations, and gaps those tasks did not cover (two-package layout, fluent builders vs live CLI flags, native FS, mocks, CI/AOT, services used by every TimeWarp dev-cli).

Procedure: `tw-implementation-review` with **elevated effort** (area specialists, not default effort-1). Artifacts live under this folder task's `review/` subfolder. Same task through disposition — do **not** create a sibling “apply review findings” task.

## Requirements

### Scope (in)

Review product truth in this repo:

| Area | Paths |
|------|--------|
| Core engine | `source/timewarp-amuru/core/` — `Shell`, `ShellBuilder`, `CommandOutput`/`CommandResult`/`ExecutionResult`, streaming, pipes, cancellation, `.cs` host routing |
| Config / script | `CliConfiguration.cs`, `ScriptContext.cs`, `AppContextExtensions.cs` |
| Testing mocks | `source/timewarp-amuru/testing/` — `CommandMock`, strict vs loose, argument matching |
| Native FS | `source/timewarp-amuru/native/` — Commands vs Direct, `PathResolver`, `Bash` aliases |
| Tools builders | `source/timewarp-amuru-tools/dot-net-commands/`, `git-commands/`, `fzf-command/` |
| Tools services | `source/timewarp-amuru-tools/repo/`, `nu-get/` — used by TimeWarp dev-clis |
| Tests | `tests/timewarp-amuru/` including aggregate runner vs files on disk |
| Samples / tools / infra | `samples/`, `tools/dev-cli/`, MSBuild, `.github/workflows/workflow.yml`, `BannedSymbols.txt`, two-package versioning |

Every finding **must** cite `path:line` evidence in the current tree. Zero issues in an area is a valid outcome. Do not invent findings.

Judge against `AGENTS.md` (error-handling contract, mock strictness, no InternalsVisibleTo, Amuru over raw `Process`) and `tw-csharp`.

### Scope (out)

- Re-opening **097 / 090–092 / 094-001…003** unless the defect is **still present** (prove it with current file:line).
- **094-004** (PublicAPI analyzers) — already tracked; mention status, do not duplicate as a new M#.
- Known open product tasks — do not clone as review findings: **087** (invalid DotNet flags), **088** (Fzf stub), **099** (Git UpdateBranchAsync), **100** (`ICommandBuilder` / `WithNoValidation`), **104** (native FS harden), **105** (missing tests), **106** (pre-1.0 polish leftover), **082** (kebab-case cleanup). If you find a *different* bug in the same files, file it.
- **031/033** Kijamii, **083** native JSON-RPC replacement, **004/005** remaining fluent APIs — product backlog, not this review.
- Strategic packaging forks (core vs Tools vs Zana) — decided on **094**. New *code* defects in that layout are in scope.
- Docs-only polish unless a doc **contradicts** code or ships a broken sample.

### Reviewer roster (effort)

| File | Area |
|------|------|
| `core-engine.md` | Shell/builder, results, streaming/pipes, cancellation, `.cs` execution, ConfigureAwait/AOT |
| `testing-mocks.md` | CommandMock isolation, matching, strict/loose, leak across tests |
| `native-fs.md` | Commands vs Direct, PathResolver, ScriptContext nesting, delete/cwd safety |
| `tools-builders.md` | DotNet / Git / Fzf fluent flags vs live CLI; Windows/Unix |
| `tools-services.md` | repo check-version/clean, NuGet version service; consumer-breaking contracts |
| `tests-infra.md` | CI inclusion, two-package version/release, samples, BannedSymbols, kebab leftovers |
| `security.md` | argv construction (no shell injection), path traversal, delete safety, mock bypass |

Severity: `bug` · `suggestion` · `nit`. Status starts `open`. Prefer strongest severity when merging duplicates.

### Kitchen / procedure

1. Re-pin `review/review-framework.md` to the SHA actually reviewed (`git rev-parse origin/master`).
2. Round 1: spawn area reviewers (read-only on product code; write only under `review/round-1/`).
3. Merge → `review/round-1/merged.md` with stable `M#` IDs and counts table.
4. Evaluate:
   - Independent product fixes → **child tasks** (`ganda kanban create … --parent 111`), one coherent batch per child.
   - Tiny nits that belong on this branch → fix here, then `round-2/` re-review of the fix delta.
   - `wontfix` only with rationale + decider on the live `merged.md`.
5. Write `review/disposition.md` (`clean` or `accepted-exceptions`) when open count is 0 **or** remaining opens are filed as children with IDs recorded in disposition (parent stays in-progress until those children land).
6. `## Results` **must** include rounds, roster, counts by severity/status, disposition, `review/` paths, and `### How to validate`.

**Forbidden:** process files next to `task.md`; a sibling “apply 111 findings” task; clobbering prior `round-N/`.

## Checklist

### Kitchen

- [x] Folder task created (`ganda kanban reserve` + `claim --repo timewarp-amuru`)
- [x] `review/review-framework.md` scaffolded with scope, roster, prior-art notes
- [ ] Worker re-pins SHA at review start if `origin/master` moved

### Round 1

- [ ] Area reviewers write `review/round-1/<area>.md` (7 files)
- [ ] Merge → `review/round-1/merged.md` (counts + stable `M#`)

### Disposition / follow-through

- [ ] Child tasks for independent product fixes (`--parent 111`), or same-task nits committed here
- [ ] `review/disposition.md`
- [ ] `## Results` + `### How to validate`
- [ ] Do not `kanban done` from the implementer; host lifecycle / human gate

## Notes

### Prior art (do not duplicate blindly)

- **097** (done) — 2026-07-04 engine bugs: `StreamToFileAsync` race, Windows `.cs` host, CA2007, SelectAsync cancellation, ScriptContext nesting, GetLines blanks, OutputLines AsReadOnly.
- **090–092** (done) — validation/throw contract, CliWrap types off public API, unified result types.
- **094** (in-progress) — 1.0 surface: core 1.0.0 public; Tools beta; no InternalsVisibleTo; 094-001…003 done; **094-004** still to-do.
- **096** (done) — AOT/trimming declared; **098** (done) — repo-services correctness/delete safety; **101/108/109** — release CI / trusted publishing.

### Snapshot at kitchen create (2026-09-04)

- Origin-home SHA: `6867b67` (`publish kanban 110`)
- Core version: `1.0.0` (`source/Directory.Build.props`)
- Tools version: `1.0.0-beta.2` (`source/timewarp-amuru-tools/timewarp-amuru-tools.csproj`)
- Other open work that may overlap: **094-004**, **087**, **088**, **099**, **100**, **104–107**, **082**, **020**, **028**

### Related skills

- `tw-implementation-review` — procedure, templates, severity, disposition
- `tw-agent-collaboration` — QA workspace `review/`, same-task disposition, Results shape
- `tw-csharp` — conventions
- `tw-amuru` / repo `AGENTS.md` — intended public surface (false positives vs real bugs)

### Dispatch (cockpit — not this session)

```bash
ganda task work 111 --repo timewarp-amuru --host herdr
```

## Session

- Created: Grok cockpit `01a06a77-1631-7543-b181-07ddc524f9fe` (2026-09-04) — reserved/claimed 111, wrote inbound brief
- Ganda claim: cramer@TWE-001 session 3290396 (2026-09-04)
