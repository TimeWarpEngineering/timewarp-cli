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
  private CommandOptions Options = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolBuilder WithWorkingDirectory(string directory)
  {
    Options = Options.WithWorkingDirectory(directory);
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
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool install' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to install</param>
  /// <returns>A DotNetToolInstallBuilder for configuring the install command</returns>
  public DotNetToolInstallBuilder Install(string packageId)
  {
    return new DotNetToolInstallBuilder(packageId, Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool uninstall' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to uninstall</param>
  /// <returns>A DotNetToolUninstallBuilder for configuring the uninstall command</returns>
  public DotNetToolUninstallBuilder Uninstall(string packageId)
  {
    return new DotNetToolUninstallBuilder(packageId, Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool update' command.
  /// </summary>
  /// <param name="packageId">The package ID of the tool to update</param>
  /// <returns>A DotNetToolUpdateBuilder for configuring the update command</returns>
  public DotNetToolUpdateBuilder Update(string packageId)
  {
    return new DotNetToolUpdateBuilder(packageId, Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool list' command.
  /// </summary>
  /// <returns>A DotNetToolListBuilder for configuring the list command</returns>
  public DotNetToolListBuilder List()
  {
    return new DotNetToolListBuilder(Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool run' command.
  /// </summary>
  /// <param name="commandName">The name of the local tool command to run</param>
  /// <returns>A DotNetToolRunBuilder for configuring the run command</returns>
  public DotNetToolRunBuilder Run(string commandName)
  {
    return new DotNetToolRunBuilder(commandName, Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool search' command.
  /// </summary>
  /// <param name="searchTerm">The search term to find tools</param>
  /// <returns>A DotNetToolSearchBuilder for configuring the search command</returns>
  public DotNetToolSearchBuilder Search(string searchTerm)
  {
    return new DotNetToolSearchBuilder(searchTerm, Options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet tool restore' command.
  /// </summary>
  /// <returns>A DotNetToolRestoreBuilder for configuring the restore command</returns>
  public DotNetToolRestoreBuilder Restore()
  {
    return new DotNetToolRestoreBuilder(Options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool install' commands.
/// </summary>
public class DotNetToolInstallBuilder
{
  private readonly string PackageId;
  private readonly CommandOptions Options;
  private bool Global;
  private bool Local;
  private string? ToolPath;
  private string? Version;
  private string? ConfigFile;
  private string? ToolManifest;
  private bool AllowPrerelease;
  private bool IgnoreFailedSources;
  private bool Interactive;
  private List<string> Sources = new();
  private string? Framework;
  private string? Arch;

  public DotNetToolInstallBuilder(string packageId, CommandOptions options)
  {
    PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    Options = options;
  }

  /// <summary>
  /// Installs the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder Global()
  {
    Global = true;
    Local = false;
    return this;
  }

  /// <summary>
  /// Installs the tool locally (in the current directory).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder Local()
  {
    Local = true;
    Global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool should be installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithToolPath(string toolPath)
  {
    ToolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the version of the tool to install.
  /// </summary>
  /// <param name="version">The version of the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithVersion(string version)
  {
    Version = version;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithConfigFile(string configFile)
  {
    ConfigFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithToolManifest(string toolManifest)
  {
    ToolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be installed.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithPrerelease()
  {
    AllowPrerelease = true;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithIgnoreFailedSources()
  {
    IgnoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithInteractive()
  {
    Interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during installation.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithSource(string source)
  {
    Sources.Add(source);
    return this;
  }

  /// <summary>
  /// Specifies the target framework for the tool.
  /// </summary>
  /// <param name="framework">The target framework moniker</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithFramework(string framework)
  {
    Framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target architecture for the tool.
  /// </summary>
  /// <param name="arch">The target architecture</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolInstallBuilder WithArchitecture(string arch)
  {
    Arch = arch;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "install", PackageId };

    if (Global)
    {
      arguments.Add("--global");
    }

    if (Local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(ToolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(ToolPath);
    }

    if (!string.IsNullOrWhiteSpace(Version))
    {
      arguments.Add("--version");
      arguments.Add(Version);
    }

    if (!string.IsNullOrWhiteSpace(ConfigFile))
    {
      arguments.Add("--configfile");
      arguments.Add(ConfigFile);
    }

    if (!string.IsNullOrWhiteSpace(ToolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(ToolManifest);
    }

    if (!string.IsNullOrWhiteSpace(Framework))
    {
      arguments.Add("--framework");
      arguments.Add(Framework);
    }

    if (!string.IsNullOrWhiteSpace(Arch))
    {
      arguments.Add("--arch");
      arguments.Add(Arch);
    }

    foreach (string source in Sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (AllowPrerelease)
    {
      arguments.Add("--prerelease");
    }

    if (IgnoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (Interactive)
    {
      arguments.Add("--interactive");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool uninstall' commands.
/// </summary>
public class DotNetToolUninstallBuilder
{
  private readonly string PackageId;
  private readonly CommandOptions Options;
  private bool Global;
  private bool Local;
  private string? ToolPath;
  private string? ToolManifest;

  public DotNetToolUninstallBuilder(string packageId, CommandOptions options)
  {
    PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    Options = options;
  }

  /// <summary>
  /// Uninstalls the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder Global()
  {
    Global = true;
    Local = false;
    return this;
  }

  /// <summary>
  /// Uninstalls the tool locally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder Local()
  {
    Local = true;
    Global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool is installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder WithToolPath(string toolPath)
  {
    ToolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUninstallBuilder WithToolManifest(string toolManifest)
  {
    ToolManifest = toolManifest;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "uninstall", PackageId };

    if (Global)
    {
      arguments.Add("--global");
    }

    if (Local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(ToolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(ToolPath);
    }

    if (!string.IsNullOrWhiteSpace(ToolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(ToolManifest);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool update' commands.
/// </summary>
public class DotNetToolUpdateBuilder
{
  private readonly string PackageId;
  private readonly CommandOptions Options;
  private bool Global;
  private bool Local;
  private string? ToolPath;
  private string? ConfigFile;
  private string? ToolManifest;
  private bool AllowPrerelease;
  private bool IgnoreFailedSources;
  private bool Interactive;
  private List<string> Sources = new();

  public DotNetToolUpdateBuilder(string packageId, CommandOptions options)
  {
    PackageId = packageId ?? throw new ArgumentNullException(nameof(packageId));
    Options = options;
  }

  /// <summary>
  /// Updates the tool globally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder Global()
  {
    Global = true;
    Local = false;
    return this;
  }

  /// <summary>
  /// Updates the tool locally.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder Local()
  {
    Local = true;
    Global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where the tool is installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithToolPath(string toolPath)
  {
    ToolPath = toolPath;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithConfigFile(string configFile)
  {
    ConfigFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithToolManifest(string toolManifest)
  {
    ToolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be updated to.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithPrerelease()
  {
    AllowPrerelease = true;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithIgnoreFailedSources()
  {
    IgnoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithInteractive()
  {
    Interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during update.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolUpdateBuilder WithSource(string source)
  {
    Sources.Add(source);
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "update", PackageId };

    if (Global)
    {
      arguments.Add("--global");
    }

    if (Local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(ToolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(ToolPath);
    }

    if (!string.IsNullOrWhiteSpace(ConfigFile))
    {
      arguments.Add("--configfile");
      arguments.Add(ConfigFile);
    }

    if (!string.IsNullOrWhiteSpace(ToolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(ToolManifest);
    }

    foreach (string source in Sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (AllowPrerelease)
    {
      arguments.Add("--prerelease");
    }

    if (IgnoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (Interactive)
    {
      arguments.Add("--interactive");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool list' commands.
/// </summary>
public class DotNetToolListBuilder
{
  private readonly CommandOptions Options;
  private bool Global;
  private bool Local;
  private string? ToolPath;

  public DotNetToolListBuilder(CommandOptions options)
  {
    Options = options;
  }

  /// <summary>
  /// Lists globally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Global()
  {
    Global = true;
    Local = false;
    return this;
  }

  /// <summary>
  /// Lists locally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Local()
  {
    Local = true;
    Global = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where tools are installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder WithToolPath(string toolPath)
  {
    ToolPath = toolPath;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "list" };

    if (Global)
    {
      arguments.Add("--global");
    }

    if (Local)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(ToolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(ToolPath);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool run' commands.
/// </summary>
public class DotNetToolRunBuilder
{
  private readonly string CommandName;
  private readonly CommandOptions Options;
  private List<string> ToolArguments = new();

  public DotNetToolRunBuilder(string commandName, CommandOptions options)
  {
    CommandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
    Options = options;
  }

  /// <summary>
  /// Adds arguments to pass to the tool.
  /// </summary>
  /// <param name="arguments">The arguments to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArguments(params string[] arguments)
  {
    ToolArguments.AddRange(arguments);
    return this;
  }

  /// <summary>
  /// Adds a single argument to pass to the tool.
  /// </summary>
  /// <param name="argument">The argument to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArgument(string argument)
  {
    ToolArguments.Add(argument);
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "run", CommandName };
    arguments.AddRange(ToolArguments);

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool search' commands.
/// </summary>
public class DotNetToolSearchBuilder
{
  private readonly string SearchTerm;
  private readonly CommandOptions Options;
  private bool Detail;
  private int? Skip;
  private int? Take;
  private bool Prerelease;

  public DotNetToolSearchBuilder(string searchTerm, CommandOptions options)
  {
    SearchTerm = searchTerm ?? throw new ArgumentNullException(nameof(searchTerm));
    Options = options;
  }

  /// <summary>
  /// Shows detailed information about the tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithDetail()
  {
    Detail = true;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to skip.
  /// </summary>
  /// <param name="skip">The number of tools to skip</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithSkip(int skip)
  {
    Skip = skip;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to take.
  /// </summary>
  /// <param name="take">The number of tools to take</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithTake(int take)
  {
    Take = take;
    return this;
  }

  /// <summary>
  /// Includes prerelease tools in the search.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithPrerelease()
  {
    Prerelease = true;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "search", SearchTerm };

    if (Detail)
    {
      arguments.Add("--detail");
    }

    if (Skip.HasValue)
    {
      arguments.Add("--skip");
      arguments.Add(Skip.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (Take.HasValue)
    {
      arguments.Add("--take");
      arguments.Add(Take.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (Prerelease)
    {
      arguments.Add("--prerelease");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet tool restore' commands.
/// </summary>
public class DotNetToolRestoreBuilder
{
  private readonly CommandOptions Options;
  private string? ConfigFile;
  private string? ToolManifest;
  private bool IgnoreFailedSources;
  private bool Interactive;
  private List<string> Sources = new();

  public DotNetToolRestoreBuilder(CommandOptions options)
  {
    Options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithConfigFile(string configFile)
  {
    ConfigFile = configFile;
    return this;
  }

  /// <summary>
  /// Specifies the path to the tool manifest file.
  /// </summary>
  /// <param name="toolManifest">The tool manifest file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithToolManifest(string toolManifest)
  {
    ToolManifest = toolManifest;
    return this;
  }

  /// <summary>
  /// Treats package source failures as warnings.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithIgnoreFailedSources()
  {
    IgnoreFailedSources = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithInteractive()
  {
    Interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use during restore.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRestoreBuilder WithSource(string source)
  {
    Sources.Add(source);
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "restore" };

    if (!string.IsNullOrWhiteSpace(ConfigFile))
    {
      arguments.Add("--configfile");
      arguments.Add(ConfigFile);
    }

    if (!string.IsNullOrWhiteSpace(ToolManifest))
    {
      arguments.Add("--tool-manifest");
      arguments.Add(ToolManifest);
    }

    foreach (string source in Sources)
    {
      arguments.Add("--add-source");
      arguments.Add(source);
    }

    if (IgnoreFailedSources)
    {
      arguments.Add("--ignore-failed-sources");
    }

    if (Interactive)
    {
      arguments.Add("--interactive");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}