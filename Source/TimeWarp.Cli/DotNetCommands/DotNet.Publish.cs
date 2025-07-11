namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Publish command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet publish' command.
  /// </summary>
  /// <returns>A DotNetPublishBuilder for configuring the dotnet publish command</returns>
  public static DotNetPublishBuilder Publish()
  {
    return new DotNetPublishBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet publish' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetPublishBuilder for configuring the dotnet publish command</returns>
  public static DotNetPublishBuilder Publish(string project)
  {
    return new DotNetPublishBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet publish' commands.
/// </summary>
public class DotNetPublishBuilder
{
  private string? _project;
  private string? _configuration;
  private string? _framework;
  private string? _runtime;
  private string? _architecture;
  private string? _operatingSystem;
  private string? _outputPath;
  private string? _verbosity;
  private string? _terminalLogger;
  private string? _manifestFile;
  private bool _noRestore;
  private bool _noBuild;
  private bool _noDependencies;
  private bool _noLogo;
  private bool _selfContained;
  private bool _noSelfContained;
  private bool _force;
  private bool _interactive;
  private bool _publishReadyToRun;
  private bool _publishSingleFile;
  private bool _publishTrimmed;
  private List<string> _sources = new();
  private Dictionary<string, string> _properties = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to publish. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to publish (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to publish for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to publish for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the target architecture.
  /// </summary>
  /// <param name="architecture">The target architecture (e.g., "x64", "arm64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithArchitecture(string architecture)
  {
    _architecture = architecture;
    return this;
  }

  /// <summary>
  /// Specifies the target operating system.
  /// </summary>
  /// <param name="operatingSystem">The target OS (e.g., "win", "linux", "osx")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithOperatingSystem(string operatingSystem)
  {
    _operatingSystem = operatingSystem;
    return this;
  }

  /// <summary>
  /// Specifies the output directory for published artifacts.
  /// </summary>
  /// <param name="outputPath">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithOutput(string outputPath)
  {
    _outputPath = outputPath;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Sets the terminal logger for publish output.
  /// </summary>
  /// <param name="mode">The terminal logger mode ("auto", "on", "off")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithTerminalLogger(string mode)
  {
    _terminalLogger = mode;
    return this;
  }

  /// <summary>
  /// Specifies the path to a target manifest file.
  /// </summary>
  /// <param name="manifestFile">The manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithManifest(string manifestFile)
  {
    _manifestFile = manifestFile;
    return this;
  }

  /// <summary>
  /// Skips the implicit restore during the publish command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Skips the implicit build during the publish command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithNoBuild()
  {
    _noBuild = true;
    return this;
  }

  /// <summary>
  /// Skips project-to-project references and only publishes the root project.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithNoDependencies()
  {
    _noDependencies = true;
    return this;
  }

  /// <summary>
  /// Doesn't display the startup banner or the copyright message.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithNoLogo()
  {
    _noLogo = true;
    return this;
  }

  /// <summary>
  /// Publishes the .NET runtime with the application (self-contained deployment).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithSelfContained()
  {
    _selfContained = true;
    _noSelfContained = false;
    return this;
  }

  /// <summary>
  /// Publishes the application as a framework-dependent application.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithNoSelfContained()
  {
    _noSelfContained = true;
    _selfContained = false;
    return this;
  }

  /// <summary>
  /// Forces all dependencies to be resolved even if the last restore was successful.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithForce()
  {
    _force = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Compiles application assemblies as ReadyToRun (R2R) format.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithReadyToRun()
  {
    _publishReadyToRun = true;
    return this;
  }

  /// <summary>
  /// Produces a single-file application.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithSingleFile()
  {
    _publishSingleFile = true;
    return this;
  }

  /// <summary>
  /// Trims unused code from the application.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithTrimmed()
  {
    _publishTrimmed = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during the restore operation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use during the restore operation.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithSources(params string[] sources)
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
  public DotNetPublishBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPublishBuilder WithProperties(Dictionary<string, string> properties)
  {
    foreach (var kvp in properties)
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
  public DotNetPublishBuilder WithWorkingDirectory(string directory)
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
  public DotNetPublishBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet publish command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "publish" };

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

    // Add architecture if specified
    if (!string.IsNullOrWhiteSpace(_architecture))
    {
      arguments.Add("--arch");
      arguments.Add(_architecture);
    }

    // Add operating system if specified
    if (!string.IsNullOrWhiteSpace(_operatingSystem))
    {
      arguments.Add("--os");
      arguments.Add(_operatingSystem);
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

    // Add manifest file if specified
    if (!string.IsNullOrWhiteSpace(_manifestFile))
    {
      arguments.Add("--manifest");
      arguments.Add(_manifestFile);
    }

    // Add sources
    foreach (var source in _sources)
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

    if (_selfContained)
    {
      arguments.Add("--self-contained");
    }

    if (_noSelfContained)
    {
      arguments.Add("--no-self-contained");
    }

    if (_force)
    {
      arguments.Add("--force");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    // Add MSBuild properties for advanced features
    if (_publishReadyToRun)
    {
      _properties["PublishReadyToRun"] = "true";
    }

    if (_publishSingleFile)
    {
      _properties["PublishSingleFile"] = "true";
    }

    if (_publishTrimmed)
    {
      _properties["PublishTrimmed"] = "true";
    }

    // Add MSBuild properties
    foreach (var property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet publish command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet publish command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet publish command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}