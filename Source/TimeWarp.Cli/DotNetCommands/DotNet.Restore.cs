namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Restore command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet restore' command.
  /// </summary>
  /// <returns>A DotNetRestoreBuilder for configuring the dotnet restore command</returns>
  public static DotNetRestoreBuilder Restore()
  {
    return new DotNetRestoreBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet restore' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetRestoreBuilder for configuring the dotnet restore command</returns>
  public static DotNetRestoreBuilder Restore(string project)
  {
    return new DotNetRestoreBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet restore' commands.
/// </summary>
public class DotNetRestoreBuilder : ICommandBuilder<DotNetRestoreBuilder>
{
  private string? _project;
  private string? _runtime;
  private string? _verbosity;
  private string? _packagesDirectory;
  private string? _lockFilePath;
  private string? _terminalLogger;
  private bool _noCache;
  private bool _noDependencies;
  private bool _interactive;
  private bool _lockedMode;
  private bool _force;
  private List<string> _sources = new();
  private Dictionary<string, string> _properties = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to restore. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to restore for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the directory for restored packages.
  /// </summary>
  /// <param name="directory">The packages directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithPackagesDirectory(string directory)
  {
    _packagesDirectory = directory;
    return this;
  }

  /// <summary>
  /// Specifies the output location where project lock file is written.
  /// </summary>
  /// <param name="lockFilePath">The lock file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithLockFilePath(string lockFilePath)
  {
    _lockFilePath = lockFilePath;
    return this;
  }

  /// <summary>
  /// Sets the terminal logger for restore output.
  /// </summary>
  /// <param name="mode">The terminal logger mode ("auto", "on", "off")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithTerminalLogger(string mode)
  {
    _terminalLogger = mode;
    return this;
  }

  /// <summary>
  /// Specifies to not cache HTTP requests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithNoCache()
  {
    _noCache = true;
    return this;
  }

  /// <summary>
  /// When restoring a project with project-to-project (P2P) references, restores the root project and not the references.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithNoDependencies()
  {
    _noDependencies = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action (e.g., to complete authentication).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Don't allow updating project lock file.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithLockedMode()
  {
    _lockedMode = true;
    return this;
  }

  /// <summary>
  /// Forces all dependencies to be resolved even if the last restore was successful.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithForce()
  {
    _force = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during the restore operation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use during the restore operation.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithSources(params string[] sources)
  {
    _sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithProperties(Dictionary<string, string> properties)
  {
    foreach (KeyValuePair<string, string> kvp in properties)
    {
      _properties[kvp.Key] = kvp.Value;
    }
    
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithWorkingDirectory(string directory)
  {
    _options = _options.WithWorkingDirectory(directory);
    return this;
  }

  /// <summary>
  /// Adds an environment variable for the command execution.
  /// </summary>
  /// <param name="key">The environment variable name</param>
  /// <param name="value">The environment variable value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRestoreBuilder WithNoValidation()
  {
    _options = _options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet restore command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "restore" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add(_project);
    }

    // Add runtime if specified
    if (!string.IsNullOrWhiteSpace(_runtime))
    {
      arguments.Add("--runtime");
      arguments.Add(_runtime);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    // Add packages directory if specified
    if (!string.IsNullOrWhiteSpace(_packagesDirectory))
    {
      arguments.Add("--packages");
      arguments.Add(_packagesDirectory);
    }

    // Add lock file path if specified
    if (!string.IsNullOrWhiteSpace(_lockFilePath))
    {
      arguments.Add("--lock-file-path");
      arguments.Add(_lockFilePath);
    }

    // Add terminal logger if specified
    if (!string.IsNullOrWhiteSpace(_terminalLogger))
    {
      arguments.Add("--tl");
      arguments.Add(_terminalLogger);
    }

    // Add sources
    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (_noCache)
    {
      arguments.Add("--no-cache");
    }

    if (_noDependencies)
    {
      arguments.Add("--no-dependencies");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_lockedMode)
    {
      arguments.Add("--locked-mode");
    }

    if (_force)
    {
      arguments.Add("--force");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet restore command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet restore command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet restore command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}