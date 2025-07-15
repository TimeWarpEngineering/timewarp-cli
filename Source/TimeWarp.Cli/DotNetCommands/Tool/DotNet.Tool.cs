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