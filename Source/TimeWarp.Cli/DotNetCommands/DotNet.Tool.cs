namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Tool command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool' command.
  /// </summary>
  /// <returns>A DotNetToolBuilder for configuring the dotnet tool command</returns>
  public static DotNetToolBuilder Tool()
  {
    return new DotNetToolBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet tool' commands.
/// </summary>
public class DotNetToolBuilder
{
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolBuilder WithWorkingDirectory(string directory)
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
  public DotNetToolBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool install' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to install</param>
  /// <returns>A DotNetToolInstallBuilder for configuring the install command</returns>
  public DotNetToolInstallBuilder Install(string packageId)
  {
    return new DotNetToolInstallBuilder(packageId, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool uninstall' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to uninstall</param>
  /// <returns>A DotNetToolUninstallBuilder for configuring the uninstall command</returns>
  public DotNetToolUninstallBuilder Uninstall(string packageId)
  {
    return new DotNetToolUninstallBuilder(packageId, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool update' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to update</param>
  /// <returns>A DotNetToolUpdateBuilder for configuring the update command</returns>
  public DotNetToolUpdateBuilder Update(string packageId)
  {
    return new DotNetToolUpdateBuilder(packageId, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool list' command.
  /// </summary>
  /// <returns>A DotNetToolListBuilder for configuring the list command</returns>
  public DotNetToolListBuilder List()
  {
    return new DotNetToolListBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool run' command.
  /// </summary>
  /// <param name="commandName">The name of the local tool command to run</param>
  /// <returns>A DotNetToolRunBuilder for configuring the run command</returns>
  public DotNetToolRunBuilder Run(string commandName)
  {
    return new DotNetToolRunBuilder(commandName, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool search' command.
  /// </summary>
  /// <param name="searchTerm">The search term to find tools</param>
  /// <returns>A DotNetToolSearchBuilder for configuring the search command</returns>
  public DotNetToolSearchBuilder Search(string searchTerm)
  {
    return new DotNetToolSearchBuilder(searchTerm, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool restore' command.
  /// </summary>
  /// <returns>A DotNetToolRestoreBuilder for configuring the restore command</returns>
  public DotNetToolRestoreBuilder Restore()
  {
    return new DotNetToolRestoreBuilder(_options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool install' commands.
/// </summary>
public class DotNetToolInstallBuilder
{
  private readonly string _packageId;
  private readonly CommandOptions _options;
  private bool _global;
  private bool _local;
  private string? _toolPath;
  private string? _version;
  private string? _configFile;
  private string? _toolManifest;
  private bool _allowPrerelease;
  private bool _ignoreFailedSources;
  private bool _interactive;
  private List<string> _sources = new();
  private string? _framework;
  private string? _arch;

  public DotNetToolInstallBuilder(string packageId, CommandOptions options)
  {
    _packageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    _options = options;
  }

  /// <summary>
  /// Installs the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder Global()
  {
    _global = true;
    _local = false;
    return this;
  }

  /// <summary>
  /// Installs the tool locally (in the current directory).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder Local()
  {
    _local = true;
    _global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool should be installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithToolPath(string toolPath)
  {
    _toolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the version of the tool to install.
  /// </summary>
  /// <param name="version">The version of the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithVersion(string version)
  {
    _version = version;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithToolManifest(string toolManifest)
  {
    _toolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be installed.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithPrerelease()
  {
    _allowPrerelease = true;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithIgnoreFailedSources()
  {
    _ignoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during installation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the target framework for the tool.
  /// </summary>
  /// <param name="framework">The target framework moniker</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target architecture for the tool.
  /// </summary>
  /// <param name="arch">The target architecture</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithArchitecture(string arch)
  {
    _arch = arch;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "install", _packageId };

    if (_global)
    {
      arguments.Add("--global");
    }

    if (_local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(_toolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(_toolPath);
    }

    if (!string.IsNullOrWhiteSpace(_version))
    {
      arguments.Add("--version");
      arguments.Add(_version);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (!string.IsNullOrWhiteSpace(_toolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(_toolManifest);
    }

    if (!string.IsNullOrWhiteSpace(_framework))
    {
      arguments.Add("--framework");
      arguments.Add(_framework);
    }

    if (!string.IsNullOrWhiteSpace(_arch))
    {
      arguments.Add("--arch");
      arguments.Add(_arch);
    }

    foreach (string source in _sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (_allowPrerelease)
    {
      arguments.Add("--prerelease");
    }

    if (_ignoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
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
/// Fluent builder for 'dotnet tool uninstall' commands.
/// </summary>
public class DotNetToolUninstallBuilder
{
  private readonly string _packageId;
  private readonly CommandOptions _options;
  private bool _global;
  private bool _local;
  private string? _toolPath;
  private string? _toolManifest;

  public DotNetToolUninstallBuilder(string packageId, CommandOptions options)
  {
    _packageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    _options = options;
  }

  /// <summary>
  /// Uninstalls the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder Global()
  {
    _global = true;
    _local = false;
    return this;
  }

  /// <summary>
  /// Uninstalls the tool locally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder Local()
  {
    _local = true;
    _global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool is installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder WithToolPath(string toolPath)
  {
    _toolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder WithToolManifest(string toolManifest)
  {
    _toolManifest = toolManifest;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "uninstall", _packageId };

    if (_global)
    {
      arguments.Add("--global");
    }

    if (_local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(_toolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(_toolPath);
    }

    if (!string.IsNullOrWhiteSpace(_toolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(_toolManifest);
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
/// Fluent builder for 'dotnet tool update' commands.
/// </summary>
public class DotNetToolUpdateBuilder
{
  private readonly string _packageId;
  private readonly CommandOptions _options;
  private bool _global;
  private bool _local;
  private string? _toolPath;
  private string? _configFile;
  private string? _toolManifest;
  private bool _allowPrerelease;
  private bool _ignoreFailedSources;
  private bool _interactive;
  private List<string> _sources = new();

  public DotNetToolUpdateBuilder(string packageId, CommandOptions options)
  {
    _packageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    _options = options;
  }

  /// <summary>
  /// Updates the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder Global()
  {
    _global = true;
    _local = false;
    return this;
  }

  /// <summary>
  /// Updates the tool locally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder Local()
  {
    _local = true;
    _global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool is installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithToolPath(string toolPath)
  {
    _toolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithToolManifest(string toolManifest)
  {
    _toolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be updated to.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithPrerelease()
  {
    _allowPrerelease = true;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithIgnoreFailedSources()
  {
    _ignoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during update.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "update", _packageId };

    if (_global)
    {
      arguments.Add("--global");
    }

    if (_local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(_toolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(_toolPath);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (!string.IsNullOrWhiteSpace(_toolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(_toolManifest);
    }

    foreach (string source in _sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (_allowPrerelease)
    {
      arguments.Add("--prerelease");
    }

    if (_ignoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
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
/// Fluent builder for 'dotnet tool list' commands.
/// </summary>
public class DotNetToolListBuilder
{
  private readonly CommandOptions _options;
  private bool _global;
  private bool _local;
  private string? _toolPath;

  public DotNetToolListBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Lists globally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Global()
  {
    _global = true;
    _local = false;
    return this;
  }

  /// <summary>
  /// Lists locally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Local()
  {
    _local = true;
    _global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where tools are installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder WithToolPath(string toolPath)
  {
    _toolPath = toolPath;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "list" };

    if (_global)
    {
      arguments.Add("--global");
    }

    if (_local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(_toolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(_toolPath);
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
/// Fluent builder for 'dotnet tool run' commands.
/// </summary>
public class DotNetToolRunBuilder
{
  private readonly string _commandName;
  private readonly CommandOptions _options;
  private List<string> _toolArguments = new();

  public DotNetToolRunBuilder(string commandName, CommandOptions options)
  {
    _commandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
    _options = options;
  }

  /// <summary>
  /// Adds arguments to pass to the tool.
  /// </summary>
  /// <param name="arguments">The arguments to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArguments(params string[] arguments)
  {
    _toolArguments.AddRange(arguments);
    return this;
  }

  /// <summary>
  /// Adds a single argument to pass to the tool.
  /// </summary>
  /// <param name="argument">The argument to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArgument(string argument)
  {
    _toolArguments.Add(argument);
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "run", _commandName };
    arguments.AddRange(_toolArguments);

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
/// Fluent builder for 'dotnet tool search' commands.
/// </summary>
public class DotNetToolSearchBuilder
{
  private readonly string _searchTerm;
  private readonly CommandOptions _options;
  private bool _detail;
  private int? _skip;
  private int? _take;
  private bool _prerelease;

  public DotNetToolSearchBuilder(string searchTerm, CommandOptions options)
  {
    _searchTerm = searchTerm ?? throw new ArgumentNullException(nameof(searchTerm));
    _options = options;
  }

  /// <summary>
  /// Shows detailed information about the tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithDetail()
  {
    _detail = true;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to skip.
  /// </summary>
  /// <param name="skip">The number of tools to skip</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithSkip(int skip)
  {
    _skip = skip;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to take.
  /// </summary>
  /// <param name="take">The number of tools to take</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithTake(int take)
  {
    _take = take;
    return this;
  }

  /// <summary>
  /// Includes prerelease tools in the search.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithPrerelease()
  {
    _prerelease = true;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "search", _searchTerm };

    if (_detail)
    {
      arguments.Add("--detail");
    }

    if (_skip.HasValue)
    {
      arguments.Add("--skip");
      arguments.Add(_skip.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (_take.HasValue)
    {
      arguments.Add("--take");
      arguments.Add(_take.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (_prerelease)
    {
      arguments.Add("--prerelease");
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
/// Fluent builder for 'dotnet tool restore' commands.
/// </summary>
public class DotNetToolRestoreBuilder
{
  private readonly CommandOptions _options;
  private string? _configFile;
  private string? _toolManifest;
  private bool _ignoreFailedSources;
  private bool _interactive;
  private List<string> _sources = new();

  public DotNetToolRestoreBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithToolManifest(string toolManifest)
  {
    _toolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithIgnoreFailedSources()
  {
    _ignoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during restore.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "tool", "restore" };

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (!string.IsNullOrWhiteSpace(_toolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(_toolManifest);
    }

    foreach (string source in _sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (_ignoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
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