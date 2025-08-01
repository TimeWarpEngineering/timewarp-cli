namespace TimeWarp.Amuru;

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
public class DotNetListPackagesBuilder : ICommandBuilder<DotNetListPackagesBuilder>
{
  private string? Project;
  private string? Framework;
  private string? Verbosity;
  private string? Format;
  private string? OutputVersion;
  private string? Config;
  private bool ShowOutdated;
  private bool IncludesTransitive;
  private bool ShowVulnerable;
  private bool ShowDeprecated;
  private bool Interactive;
  private bool IncludesPrerelease;
  private bool UseHighestMinor;
  private bool UseHighestPatch;
  private List<string> Sources = new();
  private CommandOptions Options = new();

  /// <summary>
  /// Specifies the project file to list packages for. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithProject(string project)
  {
    Project = project;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to show packages for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithFramework(string framework)
  {
    Framework = framework;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithVerbosity(string verbosity)
  {
    Verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Specifies the output format.
  /// </summary>
  /// <param name="format">The output format ("console" or "json")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithFormat(string format)
  {
    Format = format;
    return this;
  }

  /// <summary>
  /// Specifies the output version for the format.
  /// </summary>
  /// <param name="version">The output version</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithOutputVersion(string version)
  {
    OutputVersion = version;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="config">The configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithConfig(string config)
  {
    Config = config;
    return this;
  }

  /// <summary>
  /// Lists packages that have newer versions available.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Outdated()
  {
    ShowOutdated = true;
    return this;
  }

  /// <summary>
  /// Lists transitive packages, in addition to the top-level packages.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder IncludeTransitive()
  {
    IncludesTransitive = true;
    return this;
  }

  /// <summary>
  /// Lists packages that have known vulnerabilities.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Vulnerable()
  {
    ShowVulnerable = true;
    return this;
  }

  /// <summary>
  /// Lists packages that have been deprecated.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder Deprecated()
  {
    ShowDeprecated = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithInteractive()
  {
    Interactive = true;
    return this;
  }

  /// <summary>
  /// Includes prerelease packages in the results.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder IncludePrerelease()
  {
    IncludesPrerelease = true;
    return this;
  }

  /// <summary>
  /// When used with --outdated, displays the highest minor version instead of the latest version.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder HighestMinor()
  {
    UseHighestMinor = true;
    return this;
  }

  /// <summary>
  /// When used with --outdated, displays the highest patch version instead of the latest version.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder HighestPatch()
  {
    UseHighestPatch = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use when searching for newer packages.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithSource(string source)
  {
    Sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use when searching for newer packages.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithSources(params string[] sources)
  {
    Sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithWorkingDirectory(string directory)
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
  public DotNetListPackagesBuilder WithEnvironmentVariable(string key, string? value)
  {
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetListPackagesBuilder WithNoValidation()
  {
    Options = Options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet list package command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    List<string> arguments = new() { "list", "package" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(Project))
    {
      arguments.Add(Project);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(Framework))
    {
      arguments.Add("--framework");
      arguments.Add(Framework);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(Verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(Verbosity);
    }

    // Add format if specified
    if (!string.IsNullOrWhiteSpace(Format))
    {
      arguments.Add("--format");
      arguments.Add(Format);
    }

    // Add output version if specified
    if (!string.IsNullOrWhiteSpace(OutputVersion))
    {
      arguments.Add("--output-version");
      arguments.Add(OutputVersion);
    }

    // Add config if specified
    if (!string.IsNullOrWhiteSpace(Config))
    {
      arguments.Add("--config");
      arguments.Add(Config);
    }

    // Add sources
    foreach (string source in Sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (ShowOutdated)
    {
      arguments.Add("--outdated");
    }

    if (IncludesTransitive)
    {
      arguments.Add("--include-transitive");
    }

    if (ShowVulnerable)
    {
      arguments.Add("--vulnerable");
    }

    if (ShowDeprecated)
    {
      arguments.Add("--deprecated");
    }

    if (Interactive)
    {
      arguments.Add("--interactive");
    }

    if (IncludesPrerelease)
    {
      arguments.Add("--include-prerelease");
    }

    if (UseHighestMinor)
    {
      arguments.Add("--highest-minor");
    }

    if (UseHighestPatch)
    {
      arguments.Add("--highest-patch");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
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
  /// Executes the dotnet list package command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}