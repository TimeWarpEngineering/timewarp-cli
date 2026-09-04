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
- [x] Worker re-pins SHA at review start if `origin/master` moved (`fbd5d276fc5a936136a55d981fc121a23b991493`)

### Round 1

- [x] Area reviewers write `review/round-1/<area>.md` (7 files)
- [x] Merge → `review/round-1/merged.md` (counts + stable `M#`)

### Disposition / follow-through

- [x] Child tasks for independent product fixes (`--parent 111`): **111-001** … **111-005** published to origin-home to-do
- [x] `review/disposition.md`
- [x] `## Results` + `### How to validate`
- [x] Host review-oracle (effort 1 general): `review/round-2/` re-verified M1–M26; fixed How to validate (`--repo`); M28 wontfix
- [ ] Do not `kanban done` from the implementer / review oracle; host lifecycle / human gate

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
- Implementer: Grok `01a06a90-5f33-7a63-a842-6d6fa9c5ce92` / ganda claim 3295439 (2026-09-04) — SHA re-pin, round-1 merge, children 111-001…005, disposition
- Review oracle: Grok `01a06aa7-938b-7400-9ca1-fbc64b625972` (2026-09-04) — effort-1 general re-verification, M27 How to validate fix, disposition update

## Results

Whole-repo implementation review of TimeWarp.Amuru on origin-home `master` at **`fbd5d276fc5a936136a55d981fc121a23b991493`** (`publish kanban 111`). Product code is unchanged since kitchen pin `6867b67`; the delta is kanban 111 itself. Core remains `1.0.0`; Tools remains `1.0.0-beta.2`.

**Rounds:** 1 (product, elevated 7 specialists) + 2 (host review-oracle, effort 1 general) + 3 (re-verify How to validate fix).

**Roster:** round 1 `core-engine`, `testing-mocks`, `native-fs`, `tools-builders`, `tools-services`, `tests-infra`, `security`. Round 2–3 `general`.

**Counts (round 1 product ledger — remaining opens filed as children):**

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 13 | 0 | 0 |
| suggestion | 9 | 0 | 0 |
| nit | 2 | 0 | 2 |

**Counts (round 2–3 oracle kitchen):**

| Severity | open | fixed | wontfix |
|----------|------|-------|---------|
| bug | 0 | 1 | 0 |
| suggestion | 0 | 0 | 1 |
| nit | 0 | 0 | 0 |

**Disposition:** `accepted-exceptions` (`review/disposition.md`). Product remaining opens filed as children (parent stays in-progress until they land). Wontfix: **M9** (MockSetup Throws-then-Returns leftover Exception), **M25** (`cliwrap-exit-code-tests/` historical tree), **M28** (children cite parent `merged.md` not yet on origin-home; host open-pr lands `09715af`; Requirements already inlined). Oracle **M27** (How to validate child `show`) is **fixed**.

**Children (published to origin-home `kanban/to-do/`):**

| Id | Findings | Batch |
|----|----------|-------|
| **111-001** | M1–M5 | CaptureAsync RunTime / blank lines / SelectAsync validation / ConfigureAwait / env-dict copy |
| **111-002** | M6–M8 | CommandMock Pipe identity + path-override matching |
| **111-003** | M10–M14 | ScriptContext leak/deadlock + native force extras (not 104) |
| **111-004** | M16–M20 | check-version exact-tag / unlisted / Tools version / `dev clean` |
| **111-005** | M15, M21–M24, M26 | skill.md + DotNet reference + CI/test-inclusion |

**Verified still fixed (not re-opened):** 097 engine remediations, 090–092 error-handling/CliWrap-off-API, 098 FindDirectories reparse+tracked skip, no InternalsVisibleTo, builders use `Shell.Run`/`Shell.Builder`, 103 runner includes all 78 on-disk tests.

**Known open product tasks confirmed, not cloned:** 087, 088, 094-004, 099, 100, 104, 105, 106, 082.

**Oracle re-verification:** every round-1 M1–M26 citation confirmed against current source; none refuted or cloned.

**Files changed (this parent):** review artifacts under `kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/review/` plus this `task.md`. No product-code edits on 111.

**Review paths:** `review/review-framework.md`, `review/round-1/*.md`, `review/round-1/merged.md`, `review/round-2/general.md`, `review/round-2/merged.md`, `review/round-3/general.md`, `review/round-3/merged.md`, `review/disposition.md`.

### How to validate

**Smoke**
```bash
cd /home/steve/worktrees/github.com/TimeWarpEngineering/timewarp-amuru/task-111-complete-detailed-code-review-of-timewarpamuru
git rev-parse HEAD
# expect: a commit that only adds 111 review artifacts (09715af or later on this task branch)

ls kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/review/round-1/
# expect: core-engine.md testing-mocks.md native-fs.md tools-builders.md
#         tools-services.md tests-infra.md security.md merged.md

ls kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/review/round-2/
# expect: general.md merged.md

grep -E '^\*\*Outcome:\*\*' kanban/in-progress/111-complete-detailed-code-review-of-timewarpamuru/review/disposition.md
# expect: accepted-exceptions

# Children live on origin-home, not this worktree's board — pass --repo.
ganda kanban show --repo timewarp-amuru 111-001
ganda kanban show --repo timewarp-amuru 111-002
ganda kanban show --repo timewarp-amuru 111-003
ganda kanban show --repo timewarp-amuru 111-004
ganda kanban show --repo timewarp-amuru 111-005
# expect: each Column: to-do
```

**Expect**
- `review/round-1/merged.md` counts table: bug 13 open, suggestion 9 open, nit 2 open / 2 wontfix.
- Every product `M#` cites `path:line` in the current tree.
- Security area file has no Issues section findings.
- Children 111-001…005 exist as origin-home inbox items; 111 remains in-progress (not board-done).
- Round-2/3 oracle open count is 0 (`M27` fixed, `M28` wontfix).
- Parent review evidence (`review/round-1/merged.md`) lives on this task branch until host open-pr; children already inline Requirements.

**Automated gate**
None on this parent — no product-code change. Product proofs belong on the children (`dotnet build timewarp-amuru.slnx`, `cd tests/timewarp-amuru/multi-file-runners && dotnet run run-tests.cs`).

**Not in scope:** re-running 087/088/099/100/104/105/106/082 work on this id; `ganda kanban done`; opening a PR from this review-oracle node.
