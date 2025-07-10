# CommandResult Class

The `CommandResult` class provides a fluent interface for executing shell commands and capturing their output in various formats. It wraps CliWrap's `Command` objects to provide a simplified, PowerShell-like experience with graceful error handling.

## Purpose

This class serves as the execution wrapper returned by `CommandExtensions.Run()`. It provides three distinct ways to interact with command execution:

1. **String Output**: Capture full command output as a single string
2. **Line-by-Line Output**: Capture output as an array of lines
3. **Fire-and-Forget**: Execute without capturing output

## Public API

### `GetStringAsync()`

Executes the command and returns the complete standard output as a single string.

**Returns:**
- `Task<string>`: The full standard output of the command, including newlines

**Behavior:**
- Preserves original formatting and whitespace
- Includes all newlines and carriage returns
- Returns empty string on command failure or null command

**Example Usage:**
```csharp
// Get current date as formatted string
var dateOutput = await Run("date").GetStringAsync();
Console.WriteLine($"Current date: {dateOutput}");

// Get git status with full formatting
var gitStatus = await Run("git", "status").GetStringAsync();
Console.WriteLine(gitStatus);
```

### `GetLinesAsync()`

Executes the command and returns the standard output split into individual lines.

**Returns:**
- `Task<string[]>`: Array of strings, each representing a line of output

**Behavior:**
- Splits on both `\n` and `\r` characters
- Removes empty lines automatically (`StringSplitOptions.RemoveEmptyEntries`)
- Returns empty array on command failure or null command
- Ideal for processing output line-by-line

### `Pipe(string executable, params string[] arguments)`

Chains the current command with another command, creating a pipeline where the output of the first command becomes the input of the second command.

**Parameters:**
- `executable`: The command to pipe to
- `arguments`: Command line arguments for the piped command

**Returns:**
- `CommandResult`: A new CommandResult representing the entire pipeline

**Behavior:**
- **Input validation**: Validates that both current command and target executable are valid
- **Uses CommandExtensions.Run()**: Internally calls `CommandExtensions.Run()` to create the next command
- **CliWrap integration**: Uses CliWrap's pipe operator (`|`) to chain commands
- **Graceful error handling**: If any command in the pipeline fails, returns `NullCommandResult`
- **Chaining support**: Supports chaining multiple pipes together
- **Memory efficient**: Streams data directly between processes without buffering

**Implementation Details:**
```csharp
public CommandResult Pipe(string executable, params string[] arguments)
{
  // Input validation
  if (Command == null)
  {
    return NullCommandResult;
  }
  
  if (string.IsNullOrWhiteSpace(executable))
  {
    return NullCommandResult;
  }
  
  try
  {
    // Use Run() to create the next command instead of duplicating logic
    CommandResult nextCommandResult = CommandExtensions.Run(executable, arguments);
    
    // If Run() failed, it returned a CommandResult with null Command
    if (nextCommandResult.InternalCommand == null)
    {
      return NullCommandResult;
    }
    
    // Chain commands using CliWrap's pipe operator
    Command pipedCommand = Command | nextCommandResult.InternalCommand;
    return new CommandResult(pipedCommand);
  }
  catch
  {
    // Command creation failures return null command (graceful degradation)
    return NullCommandResult;
  }
}
```

**Example Usage:**
```csharp
// Basic pipeline - find and filter
var filteredFiles = await Run("find", ".", "-name", "*.cs")
    .Pipe("grep", "async")
    .GetLinesAsync();

// Multi-stage pipeline - find, filter, and count
var count = await Run("echo", "line1\nline2\nline3\nline4")
    .Pipe("grep", "line")
    .Pipe("wc", "-l")
    .GetStringAsync();

// Real-world example - git log with formatting
var recentCommits = await Run("git", "log", "--oneline", "-n", "10")
    .Pipe("head", "-5")
    .GetLinesAsync();

// Text processing pipeline
var words = await Run("echo", "The quick brown fox jumps over the lazy dog")
    .Pipe("tr", " ", "\n")
    .Pipe("grep", "o")
    .GetLinesAsync();
```

**Additional Examples:**
```csharp
// Process each file from ls command
var files = await Run("ls", "-la").GetLinesAsync();
foreach (var file in files)
{
  Console.WriteLine($"File: {file}");
}

// Find all C# files and process them
var csharpFiles = await Run("find", ".", "-name", "*.cs").GetLinesAsync();
var fileCount = csharpFiles.Length;
Console.WriteLine($"Found {fileCount} C# files");
```

### `ExecuteAsync()`

Executes the command without capturing any output, useful for commands that perform actions rather than produce output.

**Returns:**
- `Task`: Completes when command execution finishes

**Behavior:**
- Does not capture or return any output
- Allows output to flow to console/terminal normally
- Silently handles command failures without throwing exceptions
- Optimal for commands like `git add`, `mkdir`, `rm`, etc.

**Implementation:**
```csharp
if (Command == null)
{
  return;
}

try
{
  await Command.ExecuteAsync();
}
catch
{
  // Process start failures (non-existent commands, etc.) are silently ignored
  // This matches shell behavior where failed commands don't crash the shell
}
```

**Example Usage:**
```csharp
// Execute git commands without capturing output
await Run("git", "add", ".").ExecuteAsync();
await Run("git", "commit", "-m", "Update files").ExecuteAsync();

// Create directories or copy files
await Run("mkdir", "-p", "build/output").ExecuteAsync();
await Run("cp", "file1.txt", "backup/").ExecuteAsync();
```

## Error Handling Philosophy

The `CommandResult` class implements a **graceful failure** approach across all methods:

### Null Command Handling
- **Safe defaults**: When the internal `Command` is null, methods return safe empty values
- **No exceptions**: Never throws exceptions for null commands
- **Predictable behavior**: Consistent return values regardless of command validity

### Exception Handling
- **Catch-all approach**: All exceptions during command execution are caught
- **Shell-like behavior**: Mirrors shell environments where failed commands don't crash the session
- **Empty results**: Failed commands return empty strings/arrays instead of throwing

### Command Failure Scenarios
```csharp
// Non-existent command - returns empty string/array
var result = await Run("nonexistentcommand").GetStringAsync(); // Returns string.Empty
var lines = await Run("nonexistentcommand").GetLinesAsync();   // Returns Array.Empty<string>()

// Command with non-zero exit code - returns captured output
var output = await Run("ls", "/nonexistent/path").GetStringAsync(); // Returns error message

// Permission denied - returns empty results
var restricted = await Run("cat", "/etc/shadow").GetStringAsync(); // Returns string.Empty
```

## Implementation Details

### Command Execution Pattern

Each public method follows the same defensive pattern:

1. **Null Check**: Verify the internal `Command` is not null
2. **Try-Catch Wrapper**: Wrap CliWrap execution in exception handling
3. **Safe Return**: Return appropriate empty value on any failure

```csharp
// GetStringAsync pattern
if (Command == null)
{
  return string.Empty;
}

try
{
  BufferedCommandResult result = await Command.ExecuteBufferedAsync();
  return result.StandardOutput;
}
catch
{
  // Process start failures (non-existent commands, etc.) return empty string
  return string.Empty;
}

// GetLinesAsync pattern
if (Command == null)
{
  return Array.Empty<string>();
}

try
{
  BufferedCommandResult result = await Command.ExecuteBufferedAsync();
  return result.StandardOutput.Split
  (
    new char[] { '\n', '\r' }, 
    StringSplitOptions.RemoveEmptyEntries
  );
}
catch
{
  // Process start failures (non-existent commands, etc.) return empty array
  return Array.Empty<string>();
}
```

### Output Processing

#### String Output (`GetStringAsync`)
- Uses `ExecuteBufferedAsync()` to capture all output
- Returns `result.StandardOutput` directly
- Preserves all formatting, whitespace, and newlines

#### Line Output (`GetLinesAsync`)
- Uses `ExecuteBufferedAsync()` to capture all output
- Splits output using `Split()` with newline characters
- Removes empty entries to provide clean line array

#### No Output (`ExecuteAsync`)
- Uses `ExecuteAsync()` for streaming execution
- Allows output to flow to console naturally
- No return value processing needed

### CliWrap Integration

The class leverages CliWrap's robust command execution:
- **Buffered Execution**: Uses `ExecuteBufferedAsync()` for output capture
- **Streaming Execution**: Uses `ExecuteAsync()` for fire-and-forget scenarios
- **Validation Disabled**: Commands are created with `CommandResultValidation.None`

## Internal API

### NullCommandResult Singleton
- **Static readonly field**: `internal static readonly CommandResult NullCommandResult = new(null)`
- **Shared instance**: All failure scenarios return the same singleton instance
- **Memory efficiency**: Avoids creating multiple identical null command instances
- **Thread safety**: Static readonly initialization is inherently thread-safe
- **Performance**: No allocation overhead for common failure scenarios

### InternalCommand Property
- **Private access**: `private Command? InternalCommand => Command`
- **Purpose**: Allows `Pipe()` method to access the `Command` field from other `CommandResult` instances
- **Same-class access**: C# allows private member access between instances of the same class
- **Type safety**: Returns nullable `Command` to handle null cases safely

```csharp
// Usage in Pipe() method
if (nextCommandResult.InternalCommand == null)
{
  return NullCommandResult;
}

Command pipedCommand = Command | nextCommandResult.InternalCommand;
```

## Design Decisions

### Internal Constructor
- **Controlled Creation**: Only `CommandExtensions.Run()` can create instances
- **Encapsulation**: Prevents direct instantiation by external code
- **Null Safety**: Accepts nullable `Command` for graceful failure handling
- **Singleton Integration**: Works with `NullCommandResult` pattern

### Async-First Design
- **Non-blocking**: All operations are asynchronous by default
- **Scalable**: Supports concurrent command execution
- **Modern C#**: Leverages async/await patterns throughout

### Return Type Choices
- **String vs StringBuilder**: Returns `string` for simplicity and immutability
- **Array vs List**: Returns `string[]` for performance and memory efficiency
- **Task vs ValueTask**: Uses `Task` for consistency with CliWrap

## Thread Safety

The `CommandResult` class is thread-safe:
- **Immutable State**: The internal `Command` field is readonly
- **Stateless Operations**: Methods don't modify instance state
- **CliWrap Safety**: Underlying CliWrap commands are thread-safe
- **Concurrent Execution**: Multiple methods can be called concurrently

## Performance Considerations

### Memory Usage
- **Buffered Output**: `GetStringAsync()` and `GetLinesAsync()` load full output into memory
- **Streaming Alternative**: `ExecuteAsync()` doesn't buffer output
- **String Splitting**: `GetLinesAsync()` creates string array allocation

### Execution Efficiency
- **Single Execution**: Each method call executes the command once
- **No Caching**: Results are not cached between method calls
- **Exception Overhead**: Catch-all blocks have minimal performance impact

## Usage Patterns

### Command Chaining
```csharp
// Multiple operations on same command setup
CommandResult cmd = Run("git", "log", "--oneline", "-10");
string fullOutput = await cmd.GetStringAsync();
string[] lines = await cmd.GetLinesAsync();
```

### Error Resilience
```csharp
// Graceful handling of potentially failing commands
var backupFiles = await Run("find", "/backup", "-name", "*.bak").GetLinesAsync();
if (backupFiles.Length > 0)
{
  Console.WriteLine($"Found {backupFiles.Length} backup files");
}
else
{
  Console.WriteLine("No backup files found (or command failed)");
}
```

### Mixed Output Styles
```csharp
// Choose appropriate method based on use case
await Run("git", "add", ".").ExecuteAsync();                    // Action command
var status = await Run("git", "status", "--short").GetStringAsync(); // Formatted output
var files = await Run("git", "ls-files").GetLinesAsync();       // List processing
```

## Related Classes

- **CommandExtensions**: Factory class that creates `CommandResult` instances
- **Command**: CliWrap command object wrapped by this class
- **BufferedCommandResult**: CliWrap result object used internally