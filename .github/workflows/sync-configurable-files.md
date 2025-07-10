<!--
🔄 Based on TimeWarpEngineering/timewarp-flow sync workflow documentation
Adapted for TimeWarp.Cli repository structure and requirements
-->

# Sync Configurable Files Workflow

This workflow automatically synchronizes configurable files from TimeWarp.Architecture to maintain consistency across TimeWarp projects. It features an enhanced repos-based configuration system with advanced path transformation capabilities.

## Overview

The sync workflow runs on a scheduled basis (every Monday at 9:00 AM UTC) and can also be triggered manually. It downloads specified files from configured repositories with intelligent path mapping and creates pull requests when updates are detected.

## Files

- `sync-configurable-files.yml` - The main GitHub Actions workflow
- `sync-config.yml` - Configuration file defining which files to sync
- `sync-configurable-files.md` - This documentation file

## Configuration for TimeWarp.Cli

The workflow is configured to sync development quality files from TimeWarp.Architecture:

```yaml
# Current sync configuration
repos:
  - repo: 'TimeWarpEngineering/timewarp-architecture'
    branch: 'master'
    path_transform:
      remove_prefix: 'TimeWarp.Architecture/'
    files:
      - source_path: 'TimeWarp.Architecture/.gitignore'
      - source_path: 'TimeWarp.Architecture/.editorconfig'
      - source_path: 'TimeWarp.Architecture/.ai/'
```

## How It Works

1. **Configuration Loading**: Loads settings from `.github/sync-config.yml`
2. **Path Processing**: Applies path transformations and defaults destination paths
3. **File Download**: Downloads files from TimeWarp.Architecture
4. **File Comparison**: Compares downloaded files with current repository files
5. **PR Creation**: Creates a pull request with detailed changes if differences are found

## Manual Trigger

The workflow can be manually triggered with custom parameters:

- **parent_repo**: Override the parent repository
- **parent_branch**: Override the parent branch  
- **files_to_sync**: Comma-separated list of files (overrides config file)
- **use_config_file**: Whether to use the configuration file (default: true)

## Security

- Uses `GITHUB_TOKEN` or `SYNC_PAT` for authentication
- Only syncs explicitly configured files
- Creates PRs for review rather than direct commits
- Excludes sensitive files and workflows from automatic sync

## Customization

### Adding Files to Sync

To sync additional files from TimeWarp.Architecture, add them to `.github/sync-config.yml`:

```yaml
files:
  - source_path: 'TimeWarp.Architecture/new-file.txt'
  - source_path: 'TimeWarp.Architecture/templates/script.ps1'
    dest_path: 'Scripts/template-script.ps1'  # Custom destination
```

### Changing the Schedule

Modify the cron expression in `sync-config.yml`:

```yaml
schedule:
  cron: "0 9 * * 1"  # Every Monday at 9 AM UTC
```

## Best Practices for TimeWarp.Cli

1. **Review PRs**: Always review generated PRs before merging
2. **Test Configuration**: Use manual triggers to test configuration changes
3. **Minimal Sync**: Only sync files that should be consistent across TimeWarp projects
4. **Custom Files**: Keep TimeWarp.Cli-specific files out of sync configuration
5. **Templates**: Use `.template` suffix for files that need manual review before adoption