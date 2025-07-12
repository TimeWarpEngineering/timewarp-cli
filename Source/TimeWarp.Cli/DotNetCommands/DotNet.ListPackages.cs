namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - List packages command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet list package' command.
  /// </summary>
  /// <returns>A DotNetListPackagesBuilder for configuring the dotnet list package command</returns>
  public static DotNetListPackagesBuilder ListPackages()
  {
    return new DotNetListPackagesBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet list package' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetListPackagesBuilder for configuring the dotnet list package command</returns>
  public static DotNetListPackagesBuilder ListPackages(string project)
  {
    return new DotNetListPackagesBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet list package' commands.
/// </summary>
public class DotNetListPackagesBuilder
{
  private string? _project;
  private string? _framework;
  private string? _verbosity;
  private string? _format;
  private string? _outputVersion;
  private string? _config;
  private bool _outdated;
  private bool _includeTransitive;
  private bool _vulnerable;
  private bool _deprecated;
  private bool _interactive;
  private bool _includePrerelease;
  private bool _highestMinor;
  private bool _highestPatch;
  private List<string> _sources = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to list packages for. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to show packages for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the output format.
  /// </summary>
  /// <param name="format">The output format ("console" or "json")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithFormat(string format)
  {
    _format = format;
    return this;
  }

  /// <summary>
  /// Specifies the output version for the format.
  /// </summary>
  /// <param name="version">The output version</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithOutputVersion(string version)
  {
    _outputVersion = version;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="config">The configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithConfig(string config)
  {
    _config = config;
    return this;
  }

  /// <summary>
  /// Lists packages that have newer versions available.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Outdated()
  {
    _outdated = true;
    return this;
  }

  /// <summary>
  /// Lists transitive packages, in addition to the top-level packages.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder IncludeTransitive()
  {
    _includeTransitive = true;
    return this;
  }

  /// <summary>
  /// Lists packages that have known vulnerabilities.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Vulnerable()
  {
    _vulnerable = true;
    return this;
  }

  /// <summary>
  /// Lists packages that have been deprecated.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Deprecated()
  {
    _deprecated = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Includes prerelease packages in the results.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder IncludePrerelease()
  {
    _includePrerelease = true;
    return this;
  }

  /// <summary>
  /// When used with --outdated, displays the highest minor version instead of the latest version.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder HighestMinor()
  {
    _highestMinor = true;
    return this;
  }

  /// <summary>
  /// When used with --outdated, displays the highest patch version instead of the latest version.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder HighestPatch()
  {
    _highestPatch = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use when searching for newer packages.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use when searching for newer packages.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithSources(params string[] sources)
  {
    _sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithWorkingDirectory(string directory)
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
  public DotNetListPackagesBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet list package command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "list", "package" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add(_project);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(_framework))
    {
      arguments.Add("--framework");
      arguments.Add(_framework);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    // Add format if specified
    if (!string.IsNullOrWhiteSpace(_format))
    {
      arguments.Add("--format");
      arguments.Add(_format);
    }

    // Add output version if specified
    if (!string.IsNullOrWhiteSpace(_outputVersion))
    {
      arguments.Add("--output-version");
      arguments.Add(_outputVersion);
    }

    // Add config if specified
    if (!string.IsNullOrWhiteSpace(_config))
    {
      arguments.Add("--config");
      arguments.Add(_config);
    }

    // Add sources
    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (_outdated)
    {
      arguments.Add("--outdated");
    }

    if (_includeTransitive)
    {
      arguments.Add("--include-transitive");
    }

    if (_vulnerable)
    {
      arguments.Add("--vulnerable");
    }

    if (_deprecated)
    {
      arguments.Add("--deprecated");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_includePrerelease)
    {
      arguments.Add("--include-prerelease");
    }

    if (_highestMinor)
    {
      arguments.Add("--highest-minor");
    }

    if (_highestPatch)
    {
      arguments.Add("--highest-patch");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet list package command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet list package command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet list package command and returns the output as an array of lines.
  /// This method provides compatibility with the Overview.md example usage.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> ToListAsync(CancellationToken cancellationToken = default)
  {
    return await GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet list package command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}