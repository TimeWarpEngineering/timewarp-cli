# Implement DotNet Fluent API

## Description

Create a fluent API abstraction for .NET CLI commands, similar to the FileSystem.Find() example in Overview.md. This will provide strongly-typed, IntelliSense-friendly methods for common dotnet operations while building on the existing TimeWarp.Cli foundation.

Start with DotNet.Run() as the first implementation, then expand to Build, Test, and package management commands.

## Requirements

- Create `DotNet` static class with fluent interface methods
- Implement `DotNet.Run()` with fluent configuration options
- Support common dotnet CLI parameters (project, configuration, arguments, etc.)
- Maintain async-first design consistent with existing API
- Follow project's C# coding standards (Allman braces, 2-space indentation)
- Build on existing `CommandExtensions.Run()` rather than replacing it

## Checklist

### Design
- [ ] Design DotNet static class API surface
- [ ] Define fluent interface pattern for dotnet commands
- [ ] Plan integration with existing CommandResult pipeline
- [ ] Consider backward compatibility with raw Run() method

### Implementation
- [ ] Create DotNet.Run() with fluent builder pattern
- [ ] Add support for --project parameter
- [ ] Add support for --configuration parameter  
- [ ] Add support for program arguments (after --)
- [ ] Add support for common dotnet run options
- [ ] Implement async execution methods
- [ ] Add integration tests for DotNet.Run()

### Documentation
- [ ] Update API documentation
- [ ] Add usage examples to Overview.md
- [ ] Update CLAUDE.md with new API patterns

## Notes

The fluent API should feel natural and provide strong typing while maintaining the simplicity of the current Run() method. Example target syntax:

```csharp
// Simple execution
var output = await DotNet.Run()
    .WithProject("MyApp.csproj")
    .WithConfiguration("Release")
    .WithArguments("arg1", "arg2")
    .GetStringAsync();

// Integration with existing pipeline
var result = await DotNet.Run()
    .WithProject("MyConsoleApp.csproj")
    .Pipe("grep", "ERROR")
    .GetLinesAsync();
```

This should be the foundation for expanding to other dotnet commands like Build(), Test(), etc.