namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Workload command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload' command.
  /// </summary>
  /// <returns>A DotNetWorkloadBuilder for configuring the dotnet workload command</returns>
  public static DotNetWorkloadBuilder Workload()
  {
    return new DotNetWorkloadBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet workload' commands.
/// </summary>
public class DotNetWorkloadBuilder
{
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadBuilder WithWorkingDirectory(string directory)
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
  public DotNetWorkloadBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Shows detailed information about installed workloads.
  /// </summary>
  /// <returns>A DotNetWorkloadInfoBuilder for configuring the info command</returns>
  public DotNetWorkloadInfoBuilder Info()
  {
    return new DotNetWorkloadInfoBuilder(_options);
  }

  /// <summary>
  /// Shows the current workload set version.
  /// </summary>
  /// <returns>A DotNetWorkloadVersionBuilder for configuring the version command</returns>
  public DotNetWorkloadVersionBuilder Version()
  {
    return new DotNetWorkloadVersionBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload install' command.
  /// </summary>
  /// <param name="workloadIds">The workload IDs to install</param>
  /// <returns>A DotNetWorkloadInstallBuilder for configuring the install command</returns>
  public DotNetWorkloadInstallBuilder Install(params string[] workloadIds)
  {
    return new DotNetWorkloadInstallBuilder(workloadIds, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload list' command.
  /// </summary>
  /// <returns>A DotNetWorkloadListBuilder for configuring the list command</returns>
  public DotNetWorkloadListBuilder List()
  {
    return new DotNetWorkloadListBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload search' command.
  /// </summary>
  /// <returns>A DotNetWorkloadSearchBuilder for configuring the search command</returns>
  public DotNetWorkloadSearchBuilder Search()
  {
    return new DotNetWorkloadSearchBuilder(null, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload search' command with a search term.
  /// </summary>
  /// <param name="searchString">The search term to filter workloads</param>
  /// <returns>A DotNetWorkloadSearchBuilder for configuring the search command</returns>
  public DotNetWorkloadSearchBuilder Search(string searchString)
  {
    return new DotNetWorkloadSearchBuilder(searchString, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload uninstall' command.
  /// </summary>
  /// <param name="workloadIds">The workload IDs to uninstall</param>
  /// <returns>A DotNetWorkloadUninstallBuilder for configuring the uninstall command</returns>
  public DotNetWorkloadUninstallBuilder Uninstall(params string[] workloadIds)
  {
    return new DotNetWorkloadUninstallBuilder(workloadIds, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload update' command.
  /// </summary>
  /// <returns>A DotNetWorkloadUpdateBuilder for configuring the update command</returns>
  public DotNetWorkloadUpdateBuilder Update()
  {
    return new DotNetWorkloadUpdateBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload repair' command.
  /// </summary>
  /// <returns>A DotNetWorkloadRepairBuilder for configuring the repair command</returns>
  public DotNetWorkloadRepairBuilder Repair()
  {
    return new DotNetWorkloadRepairBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload clean' command.
  /// </summary>
  /// <returns>A DotNetWorkloadCleanBuilder for configuring the clean command</returns>
  public DotNetWorkloadCleanBuilder Clean()
  {
    return new DotNetWorkloadCleanBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload restore' command.
  /// </summary>
  /// <returns>A DotNetWorkloadRestoreBuilder for configuring the restore command</returns>
  public DotNetWorkloadRestoreBuilder Restore()
  {
    return new DotNetWorkloadRestoreBuilder(null, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload restore' command with a project or solution.
  /// </summary>
  /// <param name="projectOrSolution">The project or solution file path</param>
  /// <returns>A DotNetWorkloadRestoreBuilder for configuring the restore command</returns>
  public DotNetWorkloadRestoreBuilder Restore(string projectOrSolution)
  {
    return new DotNetWorkloadRestoreBuilder(projectOrSolution, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet workload config' command.
  /// </summary>
  /// <returns>A DotNetWorkloadConfigBuilder for configuring the config command</returns>
  public DotNetWorkloadConfigBuilder Config()
  {
    return new DotNetWorkloadConfigBuilder(_options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet workload --info' commands.
/// </summary>
public class DotNetWorkloadInfoBuilder
{
  private readonly CommandOptions _options;

  public DotNetWorkloadInfoBuilder(CommandOptions options)
  {
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "--info" };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload --version' commands.
/// </summary>
public class DotNetWorkloadVersionBuilder
{
  private readonly CommandOptions _options;

  public DotNetWorkloadVersionBuilder(CommandOptions options)
  {
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "--version" };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload install' commands.
/// </summary>
public class DotNetWorkloadInstallBuilder
{
  private readonly string[] _workloadIds;
  private readonly CommandOptions _options;
  private string? _configFile;
  private bool _includePreview;
  private bool _skipManifestUpdate;
  private List<string> _sources = new();
  private string? _version;

  public DotNetWorkloadInstallBuilder(string[] workloadIds, CommandOptions options)
  {
    _workloadIds = workloadIds ?? throw new ArgumentNullException(nameof(workloadIds));
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadInstallBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Allows prerelease workload manifests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadInstallBuilder WithIncludePreview()
  {
    _includePreview = true;
    return this;
  }

  /// <summary>
  /// Skips updating workload manifests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadInstallBuilder WithSkipManifestUpdate()
  {
    _skipManifestUpdate = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during installation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadInstallBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the workload set version to install.
  /// </summary>
  /// <param name="version">The workload set version</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadInstallBuilder WithVersion(string version)
  {
    _version = version;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "install" };
    arguments.AddRange(_workloadIds);

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_includePreview)
    {
      arguments.Add("--include-previews");
    }

    if (_skipManifestUpdate)
    {
      arguments.Add("--skip-manifest-update");
    }

    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    if (!string.IsNullOrWhiteSpace(_version))
    {
      arguments.Add("--version");
      arguments.Add(_version);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload list' commands.
/// </summary>
public class DotNetWorkloadListBuilder
{
  private readonly CommandOptions _options;
  private string? _verbosity;

  public DotNetWorkloadListBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadListBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "list" };

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload search' commands.
/// </summary>
public class DotNetWorkloadSearchBuilder
{
  private readonly string? _searchString;
  private readonly CommandOptions _options;
  private string? _verbosity;

  public DotNetWorkloadSearchBuilder(string? searchString, CommandOptions options)
  {
    _searchString = searchString;
    _options = options;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadSearchBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "search" };

    if (!string.IsNullOrWhiteSpace(_searchString))
    {
      arguments.Add(_searchString);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload uninstall' commands.
/// </summary>
public class DotNetWorkloadUninstallBuilder
{
  private readonly string[] _workloadIds;
  private readonly CommandOptions _options;

  public DotNetWorkloadUninstallBuilder(string[] workloadIds, CommandOptions options)
  {
    _workloadIds = workloadIds ?? throw new ArgumentNullException(nameof(workloadIds));
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "uninstall" };
    arguments.AddRange(_workloadIds);

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload update' commands.
/// </summary>
public class DotNetWorkloadUpdateBuilder
{
  private readonly CommandOptions _options;
  private bool _advertisingManifestsOnly;
  private string? _configFile;
  private bool _disableParallel;
  private bool _fromPreviousSdk;
  private bool _includePreview;
  private bool _interactive;
  private bool _noCache;
  private List<string> _sources = new();
  private string? _tempDir;
  private string? _verbosity;

  public DotNetWorkloadUpdateBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Downloads advertising manifests without updating workloads.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithAdvertisingManifestsOnly()
  {
    _advertisingManifestsOnly = true;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Prevents parallel workload restoration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithDisableParallel()
  {
    _disableParallel = true;
    return this;
  }

  /// <summary>
  /// Includes workloads from previous SDK versions.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithFromPreviousSdk()
  {
    _fromPreviousSdk = true;
    return this;
  }

  /// <summary>
  /// Allows prerelease workload manifests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithIncludePreview()
  {
    _includePreview = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Prevents package and HTTP request caching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithNoCache()
  {
    _noCache = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during update.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the temporary directory for package downloads.
  /// </summary>
  /// <param name="tempDir">The temporary directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithTempDir(string tempDir)
  {
    _tempDir = tempDir;
    return this;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadUpdateBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "update" };

    if (_advertisingManifestsOnly)
    {
      arguments.Add("--advertising-manifests-only");
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_disableParallel)
    {
      arguments.Add("--disable-parallel");
    }

    if (_fromPreviousSdk)
    {
      arguments.Add("--from-previous-sdk");
    }

    if (_includePreview)
    {
      arguments.Add("--include-previews");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_noCache)
    {
      arguments.Add("--no-cache");
    }

    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    if (!string.IsNullOrWhiteSpace(_tempDir))
    {
      arguments.Add("--temp-dir");
      arguments.Add(_tempDir);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload repair' commands.
/// </summary>
public class DotNetWorkloadRepairBuilder
{
  private readonly CommandOptions _options;
  private string? _configFile;
  private bool _disableParallel;
  private bool _ignoreFailedSources;
  private bool _interactive;
  private bool _noCache;
  private List<string> _sources = new();
  private string? _tempDir;
  private string? _verbosity;

  public DotNetWorkloadRepairBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Prevents parallel project restoration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithDisableParallel()
  {
    _disableParallel = true;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithIgnoreFailedSources()
  {
    _ignoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Prevents package and HTTP request caching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithNoCache()
  {
    _noCache = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during repair.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the temporary directory for package downloads.
  /// </summary>
  /// <param name="tempDir">The temporary directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithTempDir(string tempDir)
  {
    _tempDir = tempDir;
    return this;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRepairBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "repair" };

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_disableParallel)
    {
      arguments.Add("--disable-parallel");
    }

    if (_ignoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_noCache)
    {
      arguments.Add("--no-cache");
    }

    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    if (!string.IsNullOrWhiteSpace(_tempDir))
    {
      arguments.Add("--temp-dir");
      arguments.Add(_tempDir);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload clean' commands.
/// </summary>
public class DotNetWorkloadCleanBuilder
{
  private readonly CommandOptions _options;
  private bool _all;

  public DotNetWorkloadCleanBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Cleans all workload packs except those installed by Visual Studio.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadCleanBuilder WithAll()
  {
    _all = true;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "clean" };

    if (_all)
    {
      arguments.Add("--all");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload restore' commands.
/// </summary>
public class DotNetWorkloadRestoreBuilder
{
  private readonly string? _projectOrSolution;
  private readonly CommandOptions _options;
  private string? _configFile;
  private bool _disableParallel;
  private bool _includePreview;
  private bool _interactive;
  private bool _noCache;
  private List<string> _sources = new();
  private string? _tempDir;
  private string? _verbosity;
  private string? _version;

  public DotNetWorkloadRestoreBuilder(string? projectOrSolution, CommandOptions options)
  {
    _projectOrSolution = projectOrSolution;
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Prevents parallel project restoration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithDisableParallel()
  {
    _disableParallel = true;
    return this;
  }

  /// <summary>
  /// Allows prerelease workload manifests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithIncludePreview()
  {
    _includePreview = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Prevents package and HTTP request caching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithNoCache()
  {
    _noCache = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during restore.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the temporary directory for package downloads.
  /// </summary>
  /// <param name="tempDir">The temporary directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithTempDir(string tempDir)
  {
    _tempDir = tempDir;
    return this;
  }

  /// <summary>
  /// Specifies the verbosity level of the output.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the workload set version to restore.
  /// </summary>
  /// <param name="version">The workload set version</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadRestoreBuilder WithVersion(string version)
  {
    _version = version;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "restore" };

    if (!string.IsNullOrWhiteSpace(_projectOrSolution))
    {
      arguments.Add(_projectOrSolution);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_disableParallel)
    {
      arguments.Add("--disable-parallel");
    }

    if (_includePreview)
    {
      arguments.Add("--include-previews");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_noCache)
    {
      arguments.Add("--no-cache");
    }

    foreach (string source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    if (!string.IsNullOrWhiteSpace(_tempDir))
    {
      arguments.Add("--temp-dir");
      arguments.Add(_tempDir);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    if (!string.IsNullOrWhiteSpace(_version))
    {
      arguments.Add("--version");
      arguments.Add(_version);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
/// Fluent builder for 'dotnet workload config' commands.
/// </summary>
public class DotNetWorkloadConfigBuilder
{
  private readonly CommandOptions _options;
  private string? _updateMode;

  public DotNetWorkloadConfigBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Sets the update mode to workload-set.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadConfigBuilder WithUpdateModeWorkloadSet()
  {
    _updateMode = "workload-set";
    return this;
  }

  /// <summary>
  /// Sets the update mode to manifests.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadConfigBuilder WithUpdateModeManifests()
  {
    _updateMode = "manifests";
    return this;
  }

  /// <summary>
  /// Sets the update mode to a custom value.
  /// </summary>
  /// <param name="updateMode">The update mode (workload-set or manifests)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetWorkloadConfigBuilder WithUpdateMode(string updateMode)
  {
    _updateMode = updateMode;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "workload", "config" };

    if (!string.IsNullOrWhiteSpace(_updateMode))
    {
      arguments.Add("--update-mode");
      arguments.Add(_updateMode);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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