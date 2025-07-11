# DotNet Fluent API Reference

## Official Microsoft Documentation

- [dotnet run command](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-run) - Official Microsoft documentation for the dotnet run command

## Implementation Status

### dotnet run

✅ **Implemented** - `DotNet.Run()` provides fluent API for dotnet run command

**Supported Options:**
- `--project` - Path to project file
- `--configuration` - Build configuration (Debug/Release)
- `--framework` - Target framework moniker
- `--runtime` - Target runtime identifier
- `--arch` - Target architecture (shorthand for RID)
- `--os` - Target operating system
- `--launch-profile` - Launch profile from launchSettings.json
- `--no-restore` - Skip implicit restore
- `--no-build` - Skip implicit build
- `--no-dependencies` - Skip project-to-project references
- `--no-launch-profile` - Don't use launchSettings.json
- `--force` - Force all dependencies to be resolved
- `--interactive` - Allow command to pause for user input
- `--verbosity` - Set verbosity level
- `--tl` - Terminal logger mode (auto/on/off)
- `--property:NAME=VALUE` - Set MSBuild properties
- `-e KEY=VALUE` - Set environment variables for the process
- Program arguments (after `--`)

**Additional Features:**
- Working directory configuration
- Environment variable configuration (for dotnet process)
- Async execution methods
- Pipeline integration

**Missing Options:**
- `--help` - Not needed (handled by dotnet CLI)
- `--` - Handled automatically when program arguments are provided

### dotnet build

✅ **Implemented** - `DotNet.Build()` provides fluent API for dotnet build command

### dotnet test

✅ **Implemented** - `DotNet.Test()` provides fluent API for dotnet test command

### dotnet clean

✅ **Implemented** - `DotNet.Clean()` provides fluent API for dotnet clean command

**Supported Options:**
- `--project` - Path to project file
- `--configuration` - Build configuration (Debug/Release)
- `--framework` - Target framework moniker
- `--runtime` - Target runtime identifier
- `--output` - Output directory to clean
- `--verbosity` - Set verbosity level
- `--nologo` - Don't display startup banner
- `--property:NAME=VALUE` - Set MSBuild properties

**Additional Features:**
- Working directory configuration
- Environment variable configuration
- Async execution methods
- Project overload: `DotNet.Clean("project.csproj")`

### dotnet restore

✅ **Implemented** - `DotNet.Restore()` provides fluent API for dotnet restore command

**Supported Options:**
- `--project` - Path to project file
- `--runtime` - Target runtime identifier
- `--verbosity` - Set verbosity level
- `--packages` - Directory for restored packages
- `--lock-file-path` - Lock file output location
- `--tl` - Terminal logger mode
- `--no-cache` - Don't cache HTTP requests
- `--no-dependencies` - Restore only root project
- `--interactive` - Allow user input/authentication
- `--locked-mode` - Don't allow lock file updates
- `--force` - Force dependency resolution
- `--source` - NuGet package sources
- `--property:NAME=VALUE` - Set MSBuild properties

### dotnet list package

✅ **Implemented** - `DotNet.ListPackages()` provides fluent API for dotnet list package command

**Supported Options:**
- `--project` - Path to project file
- `--framework` - Target framework moniker
- `--verbosity` - Set verbosity level
- `--format` - Output format (console/json)
- `--output-version` - Output version for format
- `--config` - NuGet configuration file
- `--outdated` - Show packages with newer versions
- `--include-transitive` - Include transitive packages
- `--vulnerable` - Show packages with vulnerabilities
- `--deprecated` - Show deprecated packages
- `--include-prerelease` - Include prerelease packages
- `--highest-minor` - Show highest minor version
- `--highest-patch` - Show highest patch version
- `--source` - NuGet package sources

**Additional Features:**
- `.ToListAsync()` method for Overview.md compatibility

### dotnet publish

✅ **Implemented** - `DotNet.Publish()` provides fluent API for dotnet publish command

**Supported Options:**
- `--project` - Path to project file
- `--configuration` - Build configuration
- `--framework` - Target framework moniker
- `--runtime` - Target runtime identifier
- `--arch` - Target architecture
- `--os` - Target operating system
- `--output` - Output directory
- `--verbosity` - Set verbosity level
- `--tl` - Terminal logger mode
- `--manifest` - Target manifest file
- `--no-restore` - Skip implicit restore
- `--no-build` - Skip implicit build
- `--no-dependencies` - Skip project dependencies
- `--nologo` - Don't display startup banner
- `--self-contained` - Self-contained deployment
- `--no-self-contained` - Framework-dependent deployment
- `--force` - Force dependency resolution
- `--interactive` - Allow user input
- Advanced features: ReadyToRun, SingleFile, Trimmed

### dotnet pack

✅ **Implemented** - `DotNet.Pack()` provides fluent API for dotnet pack command

**Supported Options:**
- `--project` - Path to project file
- `--configuration` - Build configuration
- `--framework` - Target framework moniker
- `--runtime` - Target runtime identifier
- `--output` - Output directory
- `--verbosity` - Set verbosity level
- `--tl` - Terminal logger mode
- `--version-suffix` - Version suffix
- `--no-restore` - Skip implicit restore
- `--no-build` - Skip implicit build
- `--no-dependencies` - Skip project dependencies
- `--nologo` - Don't display startup banner
- `--include-symbols` - Create symbols package
- `--include-source` - Include source in symbols
- `--serviceable` - Set serviceable flag

### dotnet add package

✅ **Implemented** - `DotNet.AddPackage()` provides fluent API for dotnet add package command

**Usage:**
```csharp
DotNet.AddPackage("PackageName")
DotNet.AddPackage("PackageName", "1.0.0")
```

**Supported Options:**
- `--project` - Path to project file
- `--framework` - Target framework moniker
- `--version` - Package version
- `--package-directory` - Package directory
- `--no-restore` - Skip implicit restore
- `--prerelease` - Allow prerelease packages
- `--interactive` - Allow user input
- `--source` - NuGet package sources

### dotnet remove package

✅ **Implemented** - `DotNet.RemovePackage()` provides fluent API for dotnet remove package command

**Usage:**
```csharp
DotNet.RemovePackage("PackageName")
```

**Supported Options:**
- `--project` - Path to project file

## Complete Implementation

All major dotnet CLI commands now have fluent API implementations:

✅ **Core Commands:**
- `DotNet.Run()` - Execute applications
- `DotNet.Build()` - Build projects
- `DotNet.Test()` - Run tests
- `DotNet.Clean()` - Clean build outputs
- `DotNet.Restore()` - Restore dependencies
- `DotNet.Publish()` - Publish applications
- `DotNet.Pack()` - Create NuGet packages

✅ **Package Management:**
- `DotNet.ListPackages()` - List package references
- `DotNet.AddPackage()` - Add package references
- `DotNet.RemovePackage()` - Remove package references

**Universal Features:**
- Working directory configuration
- Environment variable configuration
- Async execution methods (GetStringAsync, GetLinesAsync, ExecuteAsync)
- Project overloads where applicable
- Graceful error handling
- Pipeline integration