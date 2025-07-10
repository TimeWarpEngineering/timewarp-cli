# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TimeWarp.Cli is a fluent API wrapper around CliWrap for elegant C# scripting. The library makes shell command execution feel natural and concise in C#, providing a simple static `Run()` method with async operations and graceful error handling.

**Target Framework:** .NET 10.0  
**Current Version:** 0.1.1  
**Package ID:** TimeWarp.Cli

## Project Structure

- `Source/TimeWarp.Cli/` - Main library (published as NuGet package)
  - `CommandExtensions.cs` - Static `Run()` method entry point
  - `CommandResult.cs` - Fluent result wrapper with async operations
- `Scripts/` - Build automation scripts (all use TimeWarp.Cli itself)
- `Tests/` - Integration tests with custom test runner
- `Spikes/CsScripts/` - Example scripts demonstrating API usage
- `LocalNuGetFeed/` - Local NuGet packages for development

## Development Commands

### Build and Package Management
```bash
# Build the library (Release mode)
./Scripts/Build.cs

# Pack and publish to local feed
./Scripts/Pack.cs

# Clean all artifacts and packages
./Scripts/Clean.cs
```

### Testing
```bash
# Run all integration tests
./Tests/RunTests.cs
```

Tests are executable C# scripts that return exit codes. The test runner uses TimeWarp.Cli itself to execute tests and report results.

**Important**: All test files include `#:property RestoreNoCache true` to ensure fresh package downloads and avoid caching issues during development.

### Local Development Workflow

1. Make changes to `Source/TimeWarp.Cli/`
2. Run `./Scripts/Build.cs` to build
3. Run `./Scripts/Pack.cs` to publish to local feed
4. Test in scripts using `#:package TimeWarp.Cli`
5. Run `./Tests/RunTests.cs` to verify functionality

## API Design

### Core API
```csharp
// Entry point - static Run method
public static CommandResult Run(string executable, params string[] arguments)

// CommandResult methods (all async)
public async Task<string> GetStringAsync()      // Get full output as string
public async Task<string[]> GetLinesAsync()     // Get output as line array
public async Task ExecuteAsync()                // Execute without capturing output
```

### Usage Examples
```csharp
#!/usr/bin/dotnet run
#:package TimeWarp.Cli

// Get command output
var date = await Run("date").GetStringAsync();

// Process lines
var files = await Run("find", ".", "-name", "*.cs").GetLinesAsync();
foreach (var file in files) Console.WriteLine(file);

// Execute without output
await Run("echo", "Hello World").ExecuteAsync();
```

### Error Handling
- Commands that fail return empty results (no exceptions thrown)
- `GetStringAsync()` returns empty string on failure
- `GetLinesAsync()` returns empty array on failure
- Designed for scripting scenarios where graceful degradation is preferred

## Key Architecture Decisions

1. **Minimal API Surface**: Only two classes exposed (CommandExtensions, CommandResult)
2. **Static Entry Point**: Global `Run()` method for simplicity in scripts
3. **Async-First**: All operations are async for non-blocking execution
4. **No Exceptions**: Failed commands return empty results
5. **Dogfooding**: Build scripts and test runner use TimeWarp.Cli itself

## Dependencies

- **CliWrap 3.9.0**: Core command execution and piping functionality
- No other external dependencies

## Script Execution Model

All scripts use shebang lines for direct execution:
```csharp
#!/usr/bin/dotnet run                          // Basic script
#!/usr/bin/dotnet run --package CliWrap        // With external package
#:package TimeWarp.Cli                         // Reference local library
```

Scripts must have execute permissions (`chmod +x script.cs`).

**Important Note on ImplicitUsings**: C# script files automatically include most common namespaces via ImplicitUsings, but `System.Diagnostics` is NOT included. Scripts that use `Process` or `ProcessStartInfo` must explicitly include:
```csharp
#pragma warning disable IDE0005 // Using directive is unnecessary
using System.Diagnostics;
#pragma warning restore IDE0005
```
The pragma warnings suppress false IDE warnings about the using directive being unnecessary.

## NuGet Configuration

The repository includes `nuget.config` with two sources:
1. Official NuGet.org feed
2. Local feed at `./LocalNuGetFeed/`

This enables rapid development iteration with local packages.

## C# Coding Standards

This project follows specific C# coding standards defined in `.ai/04-csharp-coding-standards.md`:

### Formatting
- **Indentation**: 2 spaces (no tabs)
- **Line endings**: LF
- **Bracket style**: Allman style - all brackets on their own line
  ```csharp
  public void Method
  (
    string param1,
    string param2
  )
  {
    // implementation
  }
  ```

### Naming Conventions
- **Private fields**: No underscore prefix (`private readonly HttpClient httpClient;`)
- **Class scope**: PascalCase for all members (fields, properties, methods, events)
- **Method scope**: camelCase for parameters and local variables

### Language Features
- **Type declarations**: Use `var` only when type is apparent from right side
- **New operator**: Use targeted type new (`HttpClient client = new();`)
- **Namespaces**: Use file-scoped namespaces (`namespace TimeWarp.Cli;`)
- **Using statements**: Prefer global usings in GlobalUsings.cs

### Example Following Standards
```csharp
namespace TimeWarp.Cli;

public class CommandResult
{
  private readonly Command Command;
  
  public CommandResult(Command command)
  {
    Command = command;
  }
  
  public async Task<string> GetStringAsync()
  {
    StringBuilder output = new();
    await foreach (string line in Command.ListenAsync())
    {
      output.AppendLine(line);
    }
    return output.ToString();
  }
}
```