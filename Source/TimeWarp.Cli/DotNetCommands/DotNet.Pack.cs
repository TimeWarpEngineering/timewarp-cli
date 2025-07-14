namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Pack command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet pack' command.
  /// </summary>
  /// <returns>A DotNetPackBuilder for configuring the dotnet pack command</returns>
  public static DotNetPackBuilder Pack()
  {
    return new DotNetPackBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet pack' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetPackBuilder for configuring the dotnet pack command</returns>
  public static DotNetPackBuilder Pack(string project)
  {
    return new DotNetPackBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet pack' commands.
/// </summary>
public class DotNetPackBuilder : ICommandBuilder<DotNetPackBuilder>
{
  private string? _project;
  private string? _configuration;
  private string? _framework;
  private string? _runtime;
  private string? _outputPath;
  private string? _verbosity;
  private string? _terminalLogger;
  private string? _versionSuffix;
  private bool _noRestore;
  private bool _noBuild;
  private bool _noDependencies;
  private bool _noLogo;
  private bool _includeSymbols;
  private bool _includeSource;
  private bool _force;
  private bool _interactive;
  private bool _serviceable;
  private List<string> _sources = new();
  private Dictionary<string, string> _properties = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to pack. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to pack (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to pack for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to pack for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the output directory for packed artifacts.
  /// </summary>
  /// <param name="outputPath">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithOutput(string outputPath)
  {
    _outputPath = outputPath;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Sets the terminal logger for pack output.
  /// </summary>
  /// <param name="mode">The terminal logger mode ("auto", "on", "off")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithTerminalLogger(string mode)
  {
    _terminalLogger = mode;
    return this;
  }

  /// <summary>
  /// Specifies the version suffix for the package.
  /// </summary>
  /// <param name="suffix">The version suffix (e.g., "beta", "rc1")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithVersionSuffix(string suffix)
  {
    _versionSuffix = suffix;
    return this;
  }

  /// <summary>
  /// Skips the implicit restore during the pack command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Skips the implicit build during the pack command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithNoBuild()
  {
    _noBuild = true;
    return this;
  }

  /// <summary>
  /// Skips project-to-project references and only packs the root project.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithNoDependencies()
  {
    _noDependencies = true;
    return this;
  }

  /// <summary>
  /// Doesn't display the startup banner or the copyright message.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithNoLogo()
  {
    _noLogo = true;
    return this;
  }

  /// <summary>
  /// Creates the symbols package (.snupkg).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder IncludeSymbols()
  {
    _includeSymbols = true;
    return this;
  }

  /// <summary>
  /// Creates the symbols package with a src folder inside containing the source files.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder IncludeSource()
  {
    _includeSource = true;
    return this;
  }

  /// <summary>
  /// Forces all dependencies to be resolved even if the last restore was successful.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithForce()
  {
    _force = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Sets the serviceable flag in the package manifest.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithServiceable()
  {
    _serviceable = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during the restore operation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithWorkingDirectory(string directory)
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
  public DotNetPackBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackBuilder WithNoValidation()
  {
    _options = _options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet pack command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "pack" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add(_project);
    }

    // Add configuration if specified
    if (!string.IsNullOrWhiteSpace(_configuration))
    {
      arguments.Add("--configuration");
      arguments.Add(_configuration);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(_framework))
    {
      arguments.Add("--framework");
      arguments.Add(_framework);
    }

    // Add runtime if specified
    if (!string.IsNullOrWhiteSpace(_runtime))
    {
      arguments.Add("--runtime");
      arguments.Add(_runtime);
    }

    // Add output path if specified
    if (!string.IsNullOrWhiteSpace(_outputPath))
    {
      arguments.Add("--output");
      arguments.Add(_outputPath);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    // Add terminal logger if specified
    if (!string.IsNullOrWhiteSpace(_terminalLogger))
    {
      arguments.Add("--tl");
      arguments.Add(_terminalLogger);
    }

    // Add version suffix if specified
    if (!string.IsNullOrWhiteSpace(_versionSuffix))
    {
      arguments.Add("--version-suffix");
      arguments.Add(_versionSuffix);
    }

    // Add sources
    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (_noRestore)
    {
      arguments.Add("--no-restore");
    }

    if (_noBuild)
    {
      arguments.Add("--no-build");
    }

    if (_noDependencies)
    {
      arguments.Add("--no-dependencies");
    }

    if (_noLogo)
    {
      arguments.Add("--nologo");
    }

    if (_includeSymbols)
    {
      arguments.Add("--include-symbols");
    }

    if (_includeSource)
    {
      arguments.Add("--include-source");
    }

    if (_force)
    {
      arguments.Add("--force");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_serviceable)
    {
      arguments.Add("--serviceable");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet pack command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet pack command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet pack command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}