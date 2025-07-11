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
public class DotNetAddPackageBuilder
{
  private readonly string _packageName;
  private string? _project;
  private string? _framework;
  private string? _version;
  private string? _packageDirectory;
  private bool _noRestore;
  private bool _prerelease;
  private bool _interactive;
  private List<string> _sources = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Initializes a new instance of the DotNetAddPackageBuilder class.
  /// </summary>
  /// <param name="packageName">The name of the package to add</param>
  public DotNetAddPackageBuilder(string packageName)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
  }

  /// <summary>
  /// Specifies the project file to add the package to. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Adds a package reference only when targeting a specific framework.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the version of the package to add.
  /// </summary>
  /// <param name="version">The version of the package (e.g., "1.0.0", "2.1.0-preview")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithVersion(string version)
  {
    _version = version;
    return this;
  }

  /// <summary>
  /// Specifies the directory where packages are restored.
  /// </summary>
  /// <param name="directory">The package directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithPackageDirectory(string directory)
  {
    _packageDirectory = directory;
    return this;
  }

  /// <summary>
  /// Disables implicit restore when adding the package reference.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithNoRestore()
  {
    _noRestore = true;
    return this;
  }

  /// <summary>
  /// Allows prerelease packages to be installed.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithPrerelease()
  {
    _prerelease = true;
    return this;
  }

  /// <summary>
  /// Allows the command to pause for user input or action (e.g., to complete authentication).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Adds a NuGet package source to use when searching for the package.
  /// </summary>
  /// <param name="source">The URI of the NuGet package source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithSource(string source)
  {
    _sources.Add(source);
    return this;
  }

  /// <summary>
  /// Adds multiple NuGet package sources to use when searching for the package.
  /// </summary>
  /// <param name="sources">The URIs of the NuGet package sources</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithSources(params string[] sources)
  {
    _sources.AddRange(sources);
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetAddPackageBuilder WithWorkingDirectory(string directory)
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
  public DotNetAddPackageBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet add package command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "add", "package", _packageName };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Insert(1, _project);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(_framework))
    {
      arguments.Add("--framework");
      arguments.Add(_framework);
    }

    // Add version if specified
    if (!string.IsNullOrWhiteSpace(_version))
    {
      arguments.Add("--version");
      arguments.Add(_version);
    }

    // Add package directory if specified
    if (!string.IsNullOrWhiteSpace(_packageDirectory))
    {
      arguments.Add("--package-directory");
      arguments.Add(_packageDirectory);
    }

    // Add sources
    foreach (var source in _sources)
    {
      arguments.Add("--source");
      arguments.Add(source);
    }

    // Add boolean flags
    if (_noRestore)
    {
      arguments.Add("--no-restore");
    }

    if (_prerelease)
    {
      arguments.Add("--prerelease");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
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
  /// Executes the dotnet add package command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}