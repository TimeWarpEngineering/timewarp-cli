# Round 1 — tools-services
**Date:** 2026-09-04
**Scope reviewed:** `source/timewarp-amuru-tools/repo/`, `nu-get/`, consumers in `tools/dev-cli/`

## Summary

098’s delete-safety (reparse skip + tracked-file skip) and NuGet “exists anywhere” checks are present — do not re-open those. New defects: git-tag still compares only to the latest tag (076 exact-tag fix is not in the tree), unlisted filtering breaks already-published detection, check-version only reads the core `Directory.Build.props` version, and this repo’s `CleanCommand` never calls `RepoCleanService`.

## Issues

### Issue 1 — Severity: bug
- File: `source/timewarp-amuru-tools/repo/RepoCheckVersionService.cs:44-56` and `:160-187`
- Description: When no explicit `tag` is passed, the service takes the single newest tag from `git tag --sort=-v:refname` and sets `IsNewVersion = version != thatTag`. It does not check whether `v{version}` exists. Tags `v1.0.0` and `v1.1.0` with source still `1.0.0` reports `IsNewVersion=true`. 076 (done) asked for `git tag -l "v{version}"`; the current tree still uses latest-only. NuGet was corrected in 098 to “exists anywhere”; git-tag is still asymmetric.
- Suggestion: Keep `LatestReleaseTag` from versionsort for display, but set `IsNewVersion` from exact `git tag -l "v{version}"`.
- Status: open

### Issue 2 — Severity: bug
- File: `source/timewarp-amuru-tools/nu-get/nuget-package-service.cs:256-262` used by `RepoCheckVersionService.cs:92-112`
- Description: Filtering `catalogEntry.listed == false` is correct for latest display. The same filtered list drives already-published. nuget.org rejects republish of an unlisted id/version with 409. An unlisted matching version is invisible → false “safe to release”.
- Suggestion: Include unlisted for existence checks (or a separate API); keep listed-only for “latest”.
- Status: open

### Issue 3 — Severity: bug
- File: `source/timewarp-amuru-tools/repo/RepoCheckVersionService.cs:129-157` vs `source/timewarp-amuru-tools/timewarp-amuru-tools.csproj:12` and `tools/dev-cli/endpoints/workflow-command.cs:314-322`
- Description: `GetVersionFromDirectoryBuildPropsAsync` always reads `source/Directory.Build.props` (`1.0.0`). Tools overrides to `1.0.0-beta.2`. `CheckNuGetVersionAsync(["TimeWarp.Amuru.Tools"])` still compares the feed to core `1.0.0`.
- Suggestion: Per-package version resolution (csproj / props override), or make version an explicit parameter.
- Status: open

### Issue 4 — Severity: bug
- File: `tools/dev-cli/endpoints/clean-command.cs:27-59` vs `RepoCleanService.cs:6-11,76-80,100-138,155`
- Description: Workflow `clean` uses `CleanCommand`, which runs `dotnet clean` on only `timewarp-amuru.csproj`, then `Directory.Delete(bin, recursive: true)` — wipes preserved `dev`/`dev.exe` and does not skip reparse points or git-tracked `bin`/`obj` trees. `RepoCleanService` is unused by this repo’s production CLI (tests only). 098 hardening never applies to `dev clean`.
- Suggestion: Delegate artifact cleanup to `RepoCleanService` (keep feed-cache cleanup as extra).
- Status: open

### Issue 5 — Severity: suggestion
- File: `source/timewarp-amuru-tools/repo/RepoCleanService.cs:146-199`
- Description: Named `bin`/`obj` dirs use `HasTrackedFilesAsync` before delete; root `bin` children are deleted with no tracked check (only `dev`/`dev.exe` preserved). Residual gap relative to the service’s own design comment (`:9-11`).
- Suggestion: Apply the tracked-file guard (and reparse skip) to root-bin children too.
- Status: open

## Verified-clean notes

- Enumeration skips directory symlinks/reparse points (`RepoCleanService.cs:76-80`).
- Skip dirs with git-tracked files; fail-safe on git failure (`:106-109,131-138`).
- NuGet “already published” checks all *returned* versions, not only `[0]` (`RepoCheckVersionService.cs:106-112`).
- `versionsort.suffix=-` on tag sort (`:165-166`).
- No env peeking inside the service; CLI resolves `GITHUB_REF_TYPE=tag`.
- JsonDocument lifetime (085) still looks correct.

## Out-of-scope mentions

- **105** — dedicated delete-safety / version-contains / unlisted tests still deferred.
- **104** — native FS symlink/force-remove; separate from `RepoCleanService`.
