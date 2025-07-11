namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Remove package command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet remove package' command.
  /// </summary>
  /// <param name="packageName">The name of the package to remove</param>
  /// <returns>A DotNetRemovePackageBuilder for configuring the dotnet remove package command</returns>
  public static DotNetRemovePackageBuilder RemovePackage(string packageName)
  {
    return new DotNetRemovePackageBuilder(packageName);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet remove package' commands.
/// </summary>
public class DotNetRemovePackageBuilder
{
  private readonly string _packageName;
  private string? _project;
  private CommandOptions _options = new();

  /// <summary>
  /// Initializes a new instance of the DotNetRemovePackageBuilder class.
  /// </summary>
  /// <param name="packageName">The name of the package to remove</param>
  public DotNetRemovePackageBuilder(string packageName)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
  }

  /// <summary>
  /// Specifies the project file to remove the package from. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRemovePackageBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetRemovePackageBuilder WithWorkingDirectory(string directory)
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
  public DotNetRemovePackageBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet remove package command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "remove", "package", _packageName };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Insert(1, _project);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet remove package command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet remove package command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet remove package command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}