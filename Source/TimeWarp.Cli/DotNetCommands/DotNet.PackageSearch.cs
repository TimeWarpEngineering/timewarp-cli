namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Package search command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet package search' command.
  /// </summary>
  /// <param name="searchTerm">The search term to filter package names, descriptions, and tags</param>
  /// <returns>A DotNetPackageSearchBuilder for configuring the dotnet package search command</returns>
  public static DotNetPackageSearchBuilder PackageSearch(string searchTerm)
  {
    return new DotNetPackageSearchBuilder(searchTerm);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet package search' command without a search term.
  /// </summary>
  /// <returns>A DotNetPackageSearchBuilder for configuring the dotnet package search command</returns>
  public static DotNetPackageSearchBuilder PackageSearch()
  {
    return new DotNetPackageSearchBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet package search' commands.
/// </summary>
public class DotNetPackageSearchBuilder
{
  private readonly string? _searchTerm;
  private List<string> _sources = new();
  private int? _take;
  private int? _skip;
  private bool _exactMatch;
  private bool _interactive;
  private bool _prerelease;
  private string? _configFile;
  private string? _format;
  private string? _verbosity;
  private CommandOptions _options = new();

  /// <summary>
  /// Initializes a new instance of the DotNetPackageSearchBuilder class.
  /// </summary>
  /// <param name="searchTerm">The search term (optional)</param>
  public DotNetPackageSearchBuilder(string? searchTerm = null)
  {
    _searchTerm = searchTerm;
  }

  /// <summary>
  /// Adds a package source to search. You can pass multiple sources to search multiple package sources.
  /// </summary>
  /// <param name="source">The package source URL</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple package sources to search.
  /// </summary>
  /// <param name="sources">The package source URLs</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithSources(params string[] sources)
  {
    _sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Specifies the number of results to return.
  /// </summary>
  /// <param name="take">Number of results to return (default 20)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithTake(int take)
  {
    _take = take;
    return this;
  }

  /// <summary>
  /// Specifies the number of results to skip, to allow pagination.
  /// </summary>
  /// <param name="skip">Number of results to skip (default 0)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithSkip(int skip)
  {
    _skip = skip;
    return this;
  }

  /// <summary>
  /// Requires that the search term exactly match the name of the package.
  /// Causes Take and Skip options to be ignored.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithExactMatch()
  {
    _exactMatch = true;
    return this;
  }

  /// <summary>
  /// Allows the command to stop and wait for user input or action (for example to complete authentication).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Includes prerelease packages in the search results.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithPrerelease()
  {
    _prerelease = true;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Formats the output accordingly.
  /// </summary>
  /// <param name="format">Either "table" or "json" (default is "table")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithFormat(string format)
  {
    _format = format;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (normal, minimal, detailed)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetPackageSearchBuilder WithWorkingDirectory(string directory)
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
  public DotNetPackageSearchBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet package search command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "package", "search" };

    // Add search term if specified
    if (!string.IsNullOrWhiteSpace(_searchTerm))
    {
      arguments.Add(_searchTerm);
    }

    // Add sources
    foreach (var source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add take if specified
    if (_take.HasValue)
    {
      arguments.Add("--take");
      arguments.Add(_take.Value.ToString());
    }

    // Add skip if specified
    if (_skip.HasValue)
    {
      arguments.Add("--skip");
      arguments.Add(_skip.Value.ToString());
    }

    // Add config file if specified
    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    // Add format if specified
    if (!string.IsNullOrWhiteSpace(_format))
    {
      arguments.Add("--format");
      arguments.Add(_format);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    // Add boolean flags
    if (_exactMatch)
    {
      arguments.Add("--exact-match");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_prerelease)
    {
      arguments.Add("--prerelease");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet package search command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet package search command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet package search command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}