# CommandExtensions Class

The `CommandExtensions` class provides the main entry point for the TimeWarp.Cli library through its static `Run()` method.

## Purpose

This class serves as the primary API surface for executing shell commands with a fluent interface. It wraps CliWrap functionality to provide a simplified, PowerShell-like experience for C# scripting.

## Public API

### `Run(string command, params string[] args)`

Creates a new command execution context that can be used to run shell commands asynchronously.

**Parameters:**
- `command` (string): The executable command to run (e.g., "echo", "ls", "git")
- `args` (params string[]): Variable number of arguments to pass to the command

**Returns:**
- `CommandResult`: A fluent wrapper that provides async methods for executing and capturing output

**Example Usage:**
```csharp
// Simple command execution
var output = await Run("echo", "Hello World").GetStringAsync();

// Command with multiple arguments
var files = await Run("find", ".", "-name", "*.cs").GetLinesAsync();

// Execute without capturing output
await Run("git", "add", ".").ExecuteAsync();
```

## Error Handling Philosophy

The `Run()` method follows a **graceful failure** approach:

- **No exceptions thrown**: Failed command creation returns a `CommandResult` that will produce empty results
- **Shell-like behavior**: Mirrors how shell environments handle command failures without crashing
- **Defensive programming**: Catches all exceptions during command setup and returns safe defaults

## Implementation Details

### Command Creation Process

1. **Wrap Command**: Uses `CliWrap.Cli.Wrap()` to create the base command
2. **Add Arguments**: Applies provided arguments using `WithArguments()`
3. **Disable Validation**: Sets `CommandResultValidation.None` to allow non-zero exit codes
4. **Return Wrapper**: Wraps the CliWrap command in a `CommandResult` for fluent API

### Exception Handling

```csharp
try
{
  // Create CliWrap command with arguments
  Command cliCommand = CliWrap.Cli.Wrap(command)
    .WithArguments(args)
    .WithValidation(CommandResultValidation.None);
    
  return new CommandResult(cliCommand);
}
catch
{
  // Any failure during command creation returns a null-command result
  // This will produce empty strings/arrays when executed
  return new CommandResult(null);
}
```

## Design Decisions

### Static Entry Point
- **Global accessibility**: `Run()` is available anywhere without instantiation
- **Scripting-friendly**: Reduces boilerplate code in C# scripts
- **PowerShell-inspired**: Mimics the simplicity of PowerShell cmdlets

### Parameter Design
- **Fluent interface**: Returns `CommandResult` for method chaining
- **Flexible arguments**: `params string[]` allows variable argument lists
- **Type safety**: Strongly-typed parameters prevent common scripting errors

### Integration with CliWrap
- **Leverages proven library**: Built on top of the robust CliWrap library
- **Adds convenience layer**: Simplifies common use cases while maintaining power
- **Async-first design**: All operations are naturally asynchronous

## Related Classes

- **CommandResult**: The return type that provides async execution methods
- **GlobalUsings**: Provides necessary using statements for the class

## Usage in Scripts

This class is designed to be used in C# scripts with the following pattern:

```csharp
#!/usr/bin/dotnet run
#:package TimeWarp.Cli

// Direct usage - Run is available globally
var result = await Run("date").GetStringAsync();
Console.WriteLine($"Current date: {result}");

// Multiple commands
var files = await Run("ls", "-la").GetLinesAsync();
foreach (var file in files)
{
  Console.WriteLine(file);
}
```

## Thread Safety

The `CommandExtensions` class is thread-safe:
- **Stateless design**: No instance state to synchronize
- **Immutable operations**: Each `Run()` call creates a new `CommandResult`
- **CliWrap safety**: Underlying CliWrap commands are thread-safe