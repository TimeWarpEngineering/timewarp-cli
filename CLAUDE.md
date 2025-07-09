# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a collection of C# scripts designed for file searching and command-line utility tasks. The scripts are written as standalone .NET top-level programs that can be executed directly using `dotnet run`.

## Development Commands

### Running Scripts
```bash
# Run scripts directly using shebang lines (scripts have execute permissions)
./<script-name>.cs

# Examples:
./app.cs
./FindPs1Files.cs
./FindLunaSyncPs1Files.cs
./FindLunaPs1FilesWithFzfAsync.cs
```

### Building and Testing
Since these are standalone scripts without project files, they don't require traditional build commands. Each script can be compiled and run independently.

## Code Architecture

### Script Categories

1. **Simple Console Applications**
   - `app.cs` - Basic "Hello World" style application demonstrating .NET 10 features

2. **File Search Utilities**
   - `FindPs1Files.cs` - Searches for PowerShell (.ps1) files in the home directory
   - `FindLunaSyncPs1Files.cs` - Searches for .ps1 files containing "LunaSync" using command pipelines
   - `FindLunaPs1FilesWithFzfAsync.cs` - Interactive file finder with fzf integration for user selection
   - `FindLunaPs1FilesWithFzfAsync_Optimized.cs` - Optimized version using direct command pipelines

3. **Command Wrapper Applications**
   - `CliWrapApp.cs` - Demonstrates the use of CliWrap library for executing external commands
   - `TestRun.cs` - Tests the CommandExtensions utility class

4. **Utility Libraries**
   - `CommandExtensions.cs` - Provides fluent API wrapper around CliWrap for simplified command execution

### Key Patterns

- **Top-level programs**: All scripts use C# top-level program syntax with `await` calls at the root level
- **CliWrap integration**: File search utilities use the CliWrap library (v3.9.0) for executing system commands like `find` and `grep`
- **Command pipelines**: Advanced scripts demonstrate piping commands together using CliWrap's pipeline operator (`|`)
- **Async/await patterns**: All file operations and command executions use async patterns
- **Error handling**: Scripts include try-catch blocks for robust error handling

### Dependencies

- **CliWrap**: Used for executing external commands and building command pipelines
- **System.Diagnostics.Process**: Used for more complex process management (e.g., fzf integration)

### Script Execution Model

Scripts use shebang lines for direct execution:
- `#!/usr/bin/dotnet run` - Basic execution
- `#!/usr/bin/dotnet run --package CliWrap` - With package dependency
- `#:package CliWrap@3.9.0` - Package reference notation

## Working with Interactive Tools

The `FindLunaPs1FilesWithFzfAsync.cs` script demonstrates integration with external interactive tools like fzf. It uses temporary files and process management to provide user-friendly file selection capabilities.