# Prefix amuru skill with tw- for invoke filtering

## Description

Rename the owned Amuru skill `skills/amuru` → `skills/tw-amuru` (folder + frontmatter `name:`) so it matches the TimeWarp ownership prefix policy (flow task 092). A Jul 18 commit (`cb6dec3`) did this on the local master worktree and was never pushed; redo it on a feature branch from origin/dev.

## Requirements

- Folder `skills/tw-amuru/SKILL.md` exists; `skills/amuru/` is gone
- Frontmatter `name: tw-amuru`
- Live references to `skills/amuru` updated (leave historical `kanban/done/**` alone)
- Work is on `Cramer/2026-08-15/Prefix_Amuru_Skill`, never on local master

## Checklist

- [x] Create this task via `ganda kanban create` and commit
- [x] `git mv` skill folder and set frontmatter `name: tw-amuru`
- [x] Grep-clean live refs (`skills/amuru`, `name: amuru`) excluding `kanban/done/**`
- [x] Reset of local master worktree is orchestrator-owned (do not do it)
- [x] PR opened to `dev` (orchestrator-owned if you do not open it)

## Notes

- Cherry-pick source: `cb6dec3` (local master only) — same 1-line frontmatter change + rename
- Ganda SkillSource re-pointed to this feature worktree until the rename exists on master again

## Session

- Implementation: Grok 2026-08-15
- Orchestrator: Grok 2026-08-15 — reset local master to origin/master; re-point ganda tw-amuru source

## Results

- Cherry-picked `cb6dec3` cleanly as `5a02a88` (`refactor(skills): prefix owned skill with tw- for invoke filtering`)
- `skills/amuru` → `skills/tw-amuru`; frontmatter `name: tw-amuru`
- Left `BannedSymbols.txt` ban-message text unchanged (ganda exact-rule audit)
- Live refs clean; historical `kanban/done/068-*` left alone
- Local master worktree reset to `origin/master` (`c109775`); no longer ahead/behind
- `ganda skills add tw-amuru` now resolves to this feature worktree; re-point to `…/master/skills/tw-amuru` after the rename is on master
- PR: https://github.com/TimeWarpEngineering/timewarp-amuru/pull/84
