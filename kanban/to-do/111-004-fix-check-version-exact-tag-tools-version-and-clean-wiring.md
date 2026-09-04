# Fix check-version exact-tag Tools version and clean wiring

## Description

Parent **111** round-1 findings **M16–M20**. Repo services used by every TimeWarp `dev-cli`. 098 delete-safety for `FindDirectories` still holds — do not reopen it. These are different holes.

Pinned tree: `fbd5d276fc5a936136a55d981fc121a23b991493`. Evidence: parent `review/round-1/merged.md`.

## Requirements

- **M16 (bug)** `repo/RepoCheckVersionService.cs:44-56` and `:160-187` — git-tag check compares only to the latest tag. 076 (done) asked for exact `git tag -l "v{version}"`; the defect is still present. Tags `v1.0.0` + `v1.1.0` with source `1.0.0` reports `IsNewVersion=true`. Keep latest tag for display; set `IsNewVersion` from exact tag existence.
- **M17 (bug)** `nu-get/nuget-package-service.cs:256-262` used by `RepoCheckVersionService.cs:92-112` — unlisted versions are excluded from the same list used for already-published. Republish of an unlisted version 409s. Include unlisted for existence; listed-only for latest.
- **M18 (bug)** `RepoCheckVersionService.cs:129-157` vs `timewarp-amuru-tools.csproj:12` — version is always read from core `source/Directory.Build.props`. Tools has its own `<Version>1.0.0-beta.2</Version>`. Per-package version, or pass version in.
- **M19 (bug)** `tools/dev-cli/endpoints/clean-command.cs:27-59` — `dev clean` does not call `RepoCleanService`. It `dotnet clean`s only the core csproj and `Directory.Delete(bin, recursive: true)` without 098 reparse/tracked-file guards, and deletes preserved `dev`/`dev.exe`. Delegate artifact cleanup to `RepoCleanService`.
- **M20 (suggestion)** `RepoCleanService.cs:146-199` — root-bin children skip `HasTrackedFilesAsync`. Apply the tracked-file and reparse guards.

## Checklist

- [ ] M16 exact git tag existence for IsNewVersion
- [ ] M17 unlisted versions count as already published
- [ ] M18 Tools package version is the one compared to the feed
- [ ] M19 `dev clean` uses RepoCleanService (keep nupkg/feed-cache extra)
- [ ] M20 root-bin children get tracked-file + reparse guards
- [ ] `## Results` + `### How to validate`

## Notes

Parent: **111**. Source: `review/round-1/tools-services.md`. 098/076 are prior art; this task is the remaining holes, not a reopen of 098’s FindDirectories walk.
