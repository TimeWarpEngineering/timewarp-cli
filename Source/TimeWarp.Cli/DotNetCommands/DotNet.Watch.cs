namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Watch command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet watch' command.
  /// </summary>
  /// <returns>A DotNetWatchBuilder for configuring the dotnet watch command</returns>
  public static DotNetWatchBuilder Watch()
  {
    return new DotNetWatchBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet watch' commands.
/// </summary>
public class DotNetWatchBuilder
{
  private CommandOptions _options = new();
  private string? _project;
  private bool _quiet;
  private bool _verbose;
  private bool _list;
  private bool _noRestore;
  private bool _noLaunchProfile;
  private bool _noHotReload;
  private bool _noBuild;
  private List<string> _include = new();
  private List<string> _exclude = new();
  private List<string> _properties = new();
  private string? _targetFramework;
  private string? _configuration;
  private string? _runtime;
  private string? _verbosity;
  private string? _launchProfile;
  private List<string> _additionalArguments = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithWorkingDirectory(string directory)
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
  public DotNetWatchBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Specifies the project file to watch.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Suppresses informational messages.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithQuiet()
  {
    _quiet = true;
    return this;
  }

  /// <summary>
  /// Shows verbose output.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithVerbose()
  {
    _verbose = true;
    return this;
  }

  /// <summary>
  /// Lists all watched files and exits.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithList()
  {
    _list = true;
    return this;
  }

  /// <summary>
  /// Doesn't restore the project before building.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Doesn't use launch profiles when running the application.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithNoLaunchProfile()
  {
    _noLaunchProfile = true;
    return this;
  }

  /// <summary>
  /// Disables hot reload functionality.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithNoHotReload()
  {
    _noHotReload = true;
    return this;
  }

  /// <summary>
  /// Doesn't build the project before running.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithNoBuild()
  {
    _noBuild = true;
    return this;
  }

  /// <summary>
  /// Adds a file pattern to include in watching.
  /// </summary>
  /// <param name="pattern">The file pattern to include</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithInclude(string pattern)
  {
    _include.Add(pattern);
    return this;
  }

  /// <summary>
  /// Adds a file pattern to exclude from watching.
  /// </summary>
  /// <param name="pattern">The file pattern to exclude</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithExclude(string pattern)
  {
    _exclude.Add(pattern);
    return this;
  }

  /// <summary>
  /// Adds a build property.
  /// </summary>
  /// <param name="property">The build property in key=value format</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithProperty(string property)
  {
    _properties.Add(property);
    return this;
  }

  /// <summary>
  /// Specifies the target framework to build for.
  /// </summary>
  /// <param name="framework">The target framework moniker</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithTargetFramework(string framework)
  {
    _targetFramework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the build configuration.
  /// </summary>
  /// <param name="configuration">The build configuration</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithConfiguration(string configuration)
  {
    _configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to build for.
  /// </summary>
  /// <param name="runtime">The target runtime identifier</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithRuntime(string runtime)
  {
    _runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the launch profile to use.
  /// </summary>
  /// <param name="launchProfile">The launch profile name</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithLaunchProfile(string launchProfile)
  {
    _launchProfile = launchProfile;
    return this;
  }

  /// <summary>
  /// Adds additional arguments to pass to the application.
  /// </summary>
  /// <param name="arguments">The arguments to pass to the application</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithArguments(params string[] arguments)
  {
    _additionalArguments.AddRange(arguments);
    return this;
  }

  /// <summary>
  /// Adds a single additional argument to pass to the application.
  /// </summary>
  /// <param name="argument">The argument to pass to the application</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWatchBuilder WithArgument(string argument)
  {
    _additionalArguments.Add(argument);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet watch run' command.
  /// </summary>
  /// <returns>A DotNetWatchRunBuilder for configuring the watch run command</returns>
  public DotNetWatchRunBuilder Run()
  {
    return new DotNetWatchRunBuilder(this);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet watch test' command.
  /// </summary>
  /// <returns>A DotNetWatchTestBuilder for configuring the watch test command</returns>
  public DotNetWatchTestBuilder Test()
  {
    return new DotNetWatchTestBuilder(this);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet watch build' command.
  /// </summary>
  /// <returns>A DotNetWatchBuildBuilder for configuring the watch build command</returns>
  public DotNetWatchBuildBuilder Build()
  {
    return new DotNetWatchBuildBuilder(this);
  }

  internal List<string> BuildBaseArguments()
  {
    var arguments = new List<string> { "watch" };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (_quiet)
    {
      arguments.Add("--quiet");
    }

    if (_verbose)
    {
      arguments.Add("--verbose");
    }

    if (_list)
    {
      arguments.Add("--list");
    }

    if (_noRestore)
    {
      arguments.Add("--no-restore");
    }

    if (_noLaunchProfile)
    {
      arguments.Add("--no-launch-profile");
    }

    if (_noHotReload)
    {
      arguments.Add("--no-hot-reload");
    }

    if (_noBuild)
    {
      arguments.Add("--no-build");
    }

    foreach (string pattern in _include)
    {
      arguments.Add("--include");
      arguments.Add(pattern);
    }

    foreach (string pattern in _exclude)
    {
      arguments.Add("--exclude");
      arguments.Add(pattern);
    }

    foreach (string property in _properties)
    {
      arguments.Add("--property");
      arguments.Add(property);
    }

    if (!string.IsNullOrWhiteSpace(_targetFramework))
    {
      arguments.Add("--framework");
      arguments.Add(_targetFramework);
    }

    if (!string.IsNullOrWhiteSpace(_configuration))
    {
      arguments.Add("--configuration");
      arguments.Add(_configuration);
    }

    if (!string.IsNullOrWhiteSpace(_runtime))
    {
      arguments.Add("--runtime");
      arguments.Add(_runtime);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    if (!string.IsNullOrWhiteSpace(_launchProfile))
    {
      arguments.Add("--launch-profile");
      arguments.Add(_launchProfile);
    }

    return arguments;
  }

  internal CommandOptions GetOptions() => _options;
  internal List<string> GetAdditionalArguments() => _additionalArguments;
}

/// <summary>
/// Fluent builder for 'dotnet watch run' commands.
/// </summary>
public class DotNetWatchRunBuilder
{
  private readonly DotNetWatchBuilder _watchBuilder;

  public DotNetWatchRunBuilder(DotNetWatchBuilder watchBuilder)
  {
    _watchBuilder = watchBuilder;
  }

  public CommandResult Build()
  {
    List<string> arguments = _watchBuilder.BuildBaseArguments();
    arguments.Add("run");
    arguments.AddRange(_watchBuilder.GetAdditionalArguments());

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _watchBuilder.GetOptions());
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet watch test' commands.
/// </summary>
public class DotNetWatchTestBuilder
{
  private readonly DotNetWatchBuilder _watchBuilder;

  public DotNetWatchTestBuilder(DotNetWatchBuilder watchBuilder)
  {
    _watchBuilder = watchBuilder;
  }

  public CommandResult Build()
  {
    List<string> arguments = _watchBuilder.BuildBaseArguments();
    arguments.Add("test");
    arguments.AddRange(_watchBuilder.GetAdditionalArguments());

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _watchBuilder.GetOptions());
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet watch build' commands.
/// </summary>
public class DotNetWatchBuildBuilder
{
  private readonly DotNetWatchBuilder _watchBuilder;

  public DotNetWatchBuildBuilder(DotNetWatchBuilder watchBuilder)
  {
    _watchBuilder = watchBuilder;
  }

  public CommandResult Build()
  {
    List<string> arguments = _watchBuilder.BuildBaseArguments();
    arguments.Add("build");
    arguments.AddRange(_watchBuilder.GetAdditionalArguments());

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _watchBuilder.GetOptions());
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}