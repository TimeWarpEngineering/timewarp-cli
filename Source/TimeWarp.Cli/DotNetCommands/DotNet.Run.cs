namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Run command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet run' command.
  /// </summary>
  /// <returns>A DotNetRunBuilder for configuring the dotnet run command</returns>
  public static DotNetRunBuilder Run()
  {
    return new DotNetRunBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet run' commands.
/// </summary>
public class DotNetRunBuilder
{
  private string? _project;
  private string? _configuration;
  private string? _framework;
  private string? _runtime;
  private string? _architecture;
  private string? _operatingSystem;
  private string? _launchProfile;
  private bool _noRestore;
  private bool _noBuild;
  private bool _noDependencies;
  private bool _noLaunchProfile;
  private bool _force;
  private bool _interactive;
  private string? _verbosity;
  private string? _terminalLogger;
  private Dictionary<string, string> _properties = new();
  private Dictionary<string, string> _environmentVariables = new();
  private string[] _programArguments = Array.Empty<string>();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to run. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to use (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to run for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to run for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Skips the implicit restore during the run command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Skips the implicit build during the run command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithNoBuild()
  {
    _noBuild = true;
    return this;
  }

  /// <summary>
  /// Skips project-to-project references and only restores the root project.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithNoDependencies()
  {
    _noDependencies = true;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies arguments to pass to the program being run.
  /// </summary>
  /// <param name="arguments">Arguments to pass to the program</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithArguments(params string[] arguments)
  {
    _programArguments = arguments;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithWorkingDirectory(string directory)
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
  public DotNetRunBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Specifies the target architecture (shorthand for runtime identifier).
  /// </summary>
  /// <param name="architecture">The target architecture (e.g., "x64", "arm64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithArchitecture(string architecture)
  {
    _architecture = architecture;
    return this;
  }

  /// <summary>
  /// Specifies the target operating system.
  /// </summary>
  /// <param name="operatingSystem">The target OS (e.g., "win", "linux", "osx")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithOperatingSystem(string operatingSystem)
  {
    _operatingSystem = operatingSystem;
    return this;
  }

  /// <summary>
  /// Specifies the launch profile from launchSettings.json.
  /// </summary>
  /// <param name="profileName">The name of the launch profile to use</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithLaunchProfile(string profileName)
  {
    _launchProfile = profileName;
    return this;
  }

  /// <summary>
  /// Doesn't use launchSettings.json when running the application.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithNoLaunchProfile()
  {
    _noLaunchProfile = true;
    return this;
  }

  /// <summary>
  /// Forces all dependencies to be resolved even if the last restore was successful.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithForce()
  {
    _force = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Sets the terminal logger for build output.
  /// </summary>
  /// <param name="mode">The terminal logger mode ("auto", "on", "off")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithTerminalLogger(string mode)
  {
    _terminalLogger = mode;
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithProperties(Dictionary<string, string> properties)
  {
    foreach (KeyValuePair<string, string> kvp in properties)
    {
      _properties[kvp.Key] = kvp.Value;
    }
    
    return this;
  }

  /// <summary>
  /// Sets an environment variable for the running process (uses -e option).
  /// </summary>
  /// <param name="key">The environment variable name</param>
  /// <param name="value">The environment variable value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRunBuilder WithProcessEnvironmentVariable(string key, string value)
  {
    _environmentVariables[key] = value;
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet run command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "run" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
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

    // Add launch profile if specified
    if (!string.IsNullOrWhiteSpace(_launchProfile))
    {
      arguments.Add("--launch-profile");
      arguments.Add(_launchProfile);
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

    if (_noLaunchProfile)
    {
      arguments.Add("--no-launch-profile");
    }

    if (_force)
    {
      arguments.Add("--force");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    // Add process environment variables
    foreach (KeyValuePair<string, string> envVar in _environmentVariables)
    {
      arguments.Add("-e");
      arguments.Add($"{envVar.Key}={envVar.Value}");
    }

    // Add program arguments if specified
    if (_programArguments.Length > 0)
    {
      arguments.Add("--");
      arguments.AddRange(_programArguments);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet run command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet run command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet run command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}