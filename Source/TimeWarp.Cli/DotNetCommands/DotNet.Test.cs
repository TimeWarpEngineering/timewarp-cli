namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Test command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet test' command.
  /// </summary>
  /// <returns>A DotNetTestBuilder for configuring the dotnet test command</returns>
  public static DotNetTestBuilder Test()
  {
    return new DotNetTestBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet test' commands.
/// </summary>
public class DotNetTestBuilder
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
  private string? _filter;
  private string? _testAdapterPath;
  private string? _resultsDirectory;
  private string? _settingsFile;
  private bool _noRestore;
  private bool _noBuild;
  private bool _noLogo;
  private bool _blame;
  private bool _collect;
  private List<string> _loggers = new();
  private Dictionary<string, string> _properties = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to test. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to test (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to test for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to test for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the target architecture.
  /// </summary>
  /// <param name="architecture">The target architecture (e.g., "x64", "arm64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithArchitecture(string architecture)
  {
    _architecture = architecture;
    return this;
  }

  /// <summary>
  /// Specifies the target operating system.
  /// </summary>
  /// <param name="operatingSystem">The target OS (e.g., "win", "linux", "osx")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithOperatingSystem(string operatingSystem)
  {
    _operatingSystem = operatingSystem;
    return this;
  }

  /// <summary>
  /// Specifies the output directory for built artifacts.
  /// </summary>
  /// <param name="outputPath">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithOutput(string outputPath)
  {
    _outputPath = outputPath;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Sets the terminal logger for test output.
  /// </summary>
  /// <param name="mode">The terminal logger mode ("auto", "on", "off")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithTerminalLogger(string mode)
  {
    _terminalLogger = mode;
    return this;
  }

  /// <summary>
  /// Filters tests based on the given expression.
  /// </summary>
  /// <param name="filter">The filter expression (e.g., "Category=Unit", "TestCategory=Integration")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithFilter(string filter)
  {
    _filter = filter;
    return this;
  }

  /// <summary>
  /// Specifies the path to the test adapter.
  /// </summary>
  /// <param name="path">The path to the test adapter</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithTestAdapterPath(string path)
  {
    _testAdapterPath = path;
    return this;
  }

  /// <summary>
  /// Specifies the directory where test results are stored.
  /// </summary>
  /// <param name="directory">The results directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithResultsDirectory(string directory)
  {
    _resultsDirectory = directory;
    return this;
  }

  /// <summary>
  /// Specifies the settings file to use for running tests.
  /// </summary>
  /// <param name="settingsFile">The path to the settings file</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithSettings(string settingsFile)
  {
    _settingsFile = settingsFile;
    return this;
  }

  /// <summary>
  /// Adds a logger for test results.
  /// </summary>
  /// <param name="logger">The logger specification (e.g., "console", "trx", "html")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithLogger(string logger)
  {
    _loggers.Add(logger);
    return this;
  }

  /// <summary>
  /// Skips the implicit restore during the test command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Skips the implicit build during the test command.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithNoBuild()
  {
    _noBuild = true;
    return this;
  }

  /// <summary>
  /// Doesn't display the startup banner or the copyright message.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithNoLogo()
  {
    _noLogo = true;
    return this;
  }

  /// <summary>
  /// Runs the tests in blame mode, which is helpful for diagnosing test host crashes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithBlame()
  {
    _blame = true;
    return this;
  }

  /// <summary>
  /// Enables code coverage data collection.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithCollect()
  {
    _collect = true;
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithProperty(string name, string value)
  {
    _properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetTestBuilder WithProperties(Dictionary<string, string> properties)
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
  public DotNetTestBuilder WithWorkingDirectory(string directory)
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
  public DotNetTestBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet test command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "test" };

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

    // Add filter if specified
    if (!string.IsNullOrWhiteSpace(_filter))
    {
      arguments.Add("--filter");
      arguments.Add(_filter);
    }

    // Add test adapter path if specified
    if (!string.IsNullOrWhiteSpace(_testAdapterPath))
    {
      arguments.Add("--test-adapter-path");
      arguments.Add(_testAdapterPath);
    }

    // Add results directory if specified
    if (!string.IsNullOrWhiteSpace(_resultsDirectory))
    {
      arguments.Add("--results-directory");
      arguments.Add(_resultsDirectory);
    }

    // Add settings file if specified
    if (!string.IsNullOrWhiteSpace(_settingsFile))
    {
      arguments.Add("--settings");
      arguments.Add(_settingsFile);
    }

    // Add loggers
    foreach (string logger in _loggers)
    {
      arguments.Add("--logger");
      arguments.Add(logger);
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

    if (_noLogo)
    {
      arguments.Add("--nologo");
    }

    if (_blame)
    {
      arguments.Add("--blame");
    }

    if (_collect)
    {
      arguments.Add("--collect");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in _properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet test command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet test command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet test command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}