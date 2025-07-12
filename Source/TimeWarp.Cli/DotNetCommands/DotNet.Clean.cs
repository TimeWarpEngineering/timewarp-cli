namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Clean command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet clean' command.
  /// </summary>
  /// <returns>A DotNetCleanBuilder for configuring the dotnet clean command</returns>
  public static DotNetCleanBuilder Clean()
  {
    return new DotNetCleanBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet clean' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetCleanBuilder for configuring the dotnet clean command</returns>
  public static DotNetCleanBuilder Clean(string project)
  {
    return new DotNetCleanBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet clean' commands.
/// </summary>
public class DotNetCleanBuilder
{
  private string? _project;
  private string? _configuration;
  private string? _framework;
  private string? _runtime;
  private string? _outputPath;
  private string? _verbosity;
  private bool _noLogo;
  private Dictionary<string, string> _properties = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to clean. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to clean (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to clean for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to clean for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the output directory to clean.
  /// </summary>
  /// <param name="outputPath">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithOutput(string outputPath)
  {
    _outputPath = outputPath;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Doesn't display the startup banner or the copyright message.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithNoLogo()
  {
    _noLogo = true;
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProperties(Dictionary<string, string> properties)
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
  public DotNetCleanBuilder WithWorkingDirectory(string directory)
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
  public DotNetCleanBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet clean command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "clean" };

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

    // Add boolean flags
    if (_noLogo)
    {
      arguments.Add("--nologo");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet clean command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet clean command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet clean command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}