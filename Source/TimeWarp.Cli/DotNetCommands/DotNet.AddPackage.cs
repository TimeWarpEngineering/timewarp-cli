namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Add package command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet add package' command.
  /// </summary>
  /// <param name="packageName">The name of the package to add</param>
  /// <returns>A DotNetAddPackageBuilder for configuring the dotnet add package command</returns>
  public static DotNetAddPackageBuilder AddPackage(string packageName)
  {
    return new DotNetAddPackageBuilder(packageName);
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet add package' command with a specific version.
  /// </summary>
  /// <param name="packageName">The name of the package to add</param>
  /// <param name="version">The version of the package to add</param>
  /// <returns>A DotNetAddPackageBuilder for configuring the dotnet add package command</returns>
  public static DotNetAddPackageBuilder AddPackage(string packageName, string version)
  {
    return new DotNetAddPackageBuilder(packageName).WithVersion(version);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet add package' commands.
/// </summary>
public class DotNetAddPackageBuilder : ICommandBuilder<DotNetAddPackageBuilder>
{
  private readonly string PackageName;
  private string? Project;
  private string? Framework;
  private string? Version;
  private string? PackageDirectory;
  private bool NoRestore;
  private bool Prerelease;
  private bool Interactive;
  private List<string> Sources = new();
  private CommandOptions Options = new();

  /// <summary>
  /// Initializes a new instance of the DotNetAddPackageBuilder class.
  /// </summary>
  /// <param name="packageName">The name of the package to add</param>
  public DotNetAddPackageBuilder(string packageName)
  {
    PackageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
  }

  /// <summary>
  /// Specifies the project file to add the package to. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithProject(string project)
  {
    Project = project;
    return this;
  }

  /// <summary>
  /// Adds a package reference only when targeting a specific framework.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithFramework(string framework)
  {
    Framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the version of the package to add.
  /// </summary>
  /// <param name="version">The version of the package (e.g., "1.0.0", "2.1.0-preview")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithVersion(string version)
  {
    Version = version;
    return this;
  }

  /// <summary>
  /// Specifies the directory where packages are restored.
  /// </summary>
  /// <param name="directory">The package directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithPackageDirectory(string directory)
  {
    PackageDirectory = directory;
    return this;
  }

  /// <summary>
  /// Disables implicit restore when adding the package reference.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithNoRestore()
  {
    NoRestore = true;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be installed.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithPrerelease()
  {
    Prerelease = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action (e.g., to complete authentication).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithInteractive()
  {
    Interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use when searching for the package.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithSource(string source)
  {
    Sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use when searching for the package.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithSources(params string[] sources)
  {
    Sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithWorkingDirectory(string directory)
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
  public DotNetAddPackageBuilder WithEnvironmentVariable(string key, string? value)
  {
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithNoValidation()
  {
    Options = Options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet add package command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    List<string> arguments = new() { "add", "package", PackageName };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(Project))
    {
      arguments.Insert(1, Project);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(Framework))
    {
      arguments.Add("--framework");
      arguments.Add(Framework);
    }

    // Add version if specified
    if (!string.IsNullOrWhiteSpace(Version))
    {
      arguments.Add("--version");
      arguments.Add(Version);
    }

    // Add package directory if specified
    if (!string.IsNullOrWhiteSpace(PackageDirectory))
    {
      arguments.Add("--package-directory");
      arguments.Add(PackageDirectory);
    }

    // Add sources
    foreach (string source in Sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (NoRestore)
    {
      arguments.Add("--no-restore");
    }

    if (Prerelease)
    {
      arguments.Add("--prerelease");
    }

    if (Interactive)
    {
      arguments.Add("--interactive");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  /// <summary>
  /// Executes the dotnet add package command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet add package command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet add package command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}