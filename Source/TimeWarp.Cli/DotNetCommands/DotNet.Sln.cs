namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Solution command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln' command.
  /// </summary>
  /// <returns>A DotNetSlnBuilder for configuring the dotnet sln command</returns>
  public static DotNetSlnBuilder Sln()
  {
    return new DotNetSlnBuilder();
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln' command with a specific solution file.
  /// </summary>
  /// <param name="slnFile">The solution file to operate on</param>
  /// <returns>A DotNetSlnBuilder for configuring the dotnet sln command</returns>
  public static DotNetSlnBuilder Sln(string slnFile)
  {
    return new DotNetSlnBuilder().WithSolutionFile(slnFile);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet sln' commands.
/// </summary>
public class DotNetSlnBuilder
{
  private string? _slnFile;
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the solution file to operate on.
  /// </summary>
  /// <param name="slnFile">The solution file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetSlnBuilder WithSolutionFile(string slnFile)
  {
    _slnFile = slnFile;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetSlnBuilder WithWorkingDirectory(string directory)
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
  public DotNetSlnBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln add' command.
  /// </summary>
  /// <param name="projectPath">The project path to add to the solution</param>
  /// <returns>A DotNetSlnAddBuilder for configuring the add command</returns>
  public DotNetSlnAddBuilder Add(string projectPath)
  {
    return new DotNetSlnAddBuilder(projectPath, _slnFile, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln add' command with multiple projects.
  /// </summary>
  /// <param name="projectPaths">The project paths to add to the solution</param>
  /// <returns>A DotNetSlnAddBuilder for configuring the add command</returns>
  public DotNetSlnAddBuilder Add(params string[] projectPaths)
  {
    return new DotNetSlnAddBuilder(projectPaths, _slnFile, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln list' command.
  /// </summary>
  /// <returns>A DotNetSlnListBuilder for configuring the list command</returns>
  public DotNetSlnListBuilder List()
  {
    return new DotNetSlnListBuilder(_slnFile, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln remove' command.
  /// </summary>
  /// <param name="projectPath">The project path to remove from the solution</param>
  /// <returns>A DotNetSlnRemoveBuilder for configuring the remove command</returns>
  public DotNetSlnRemoveBuilder Remove(string projectPath)
  {
    return new DotNetSlnRemoveBuilder(projectPath, _slnFile, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln remove' command with multiple projects.
  /// </summary>
  /// <param name="projectPaths">The project paths to remove from the solution</param>
  /// <returns>A DotNetSlnRemoveBuilder for configuring the remove command</returns>
  public DotNetSlnRemoveBuilder Remove(params string[] projectPaths)
  {
    return new DotNetSlnRemoveBuilder(projectPaths, _slnFile, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet sln migrate' command.
  /// </summary>
  /// <returns>A DotNetSlnMigrateBuilder for configuring the migrate command</returns>
  public DotNetSlnMigrateBuilder Migrate()
  {
    return new DotNetSlnMigrateBuilder(_slnFile, _options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet sln add' commands.
/// </summary>
public class DotNetSlnAddBuilder
{
  private readonly string[] _projectPaths;
  private readonly string? _slnFile;
  private readonly CommandOptions _options;

  public DotNetSlnAddBuilder(string projectPath, string? slnFile, CommandOptions options)
  {
    _projectPaths = [projectPath ?? throw new ArgumentNullException(nameof(projectPath))];
    _slnFile = slnFile;
    _options = options;
  }

  public DotNetSlnAddBuilder(string[] projectPaths, string? slnFile, CommandOptions options)
  {
    _projectPaths = projectPaths ?? throw new ArgumentNullException(nameof(projectPaths));
    _slnFile = slnFile;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "sln" };

    // Add solution file if specified
    if (!string.IsNullOrWhiteSpace(_slnFile))
    {
      arguments.Add(_slnFile);
    }

    arguments.Add("add");
    arguments.AddRange(_projectPaths);

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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet sln list' commands.
/// </summary>
public class DotNetSlnListBuilder
{
  private readonly string? _slnFile;
  private readonly CommandOptions _options;

  public DotNetSlnListBuilder(string? slnFile, CommandOptions options)
  {
    _slnFile = slnFile;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "sln" };

    // Add solution file if specified
    if (!string.IsNullOrWhiteSpace(_slnFile))
    {
      arguments.Add(_slnFile);
    }

    arguments.Add("list");

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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet sln remove' commands.
/// </summary>
public class DotNetSlnRemoveBuilder
{
  private readonly string[] _projectPaths;
  private readonly string? _slnFile;
  private readonly CommandOptions _options;

  public DotNetSlnRemoveBuilder(string projectPath, string? slnFile, CommandOptions options)
  {
    _projectPaths = [projectPath ?? throw new ArgumentNullException(nameof(projectPath))];
    _slnFile = slnFile;
    _options = options;
  }

  public DotNetSlnRemoveBuilder(string[] projectPaths, string? slnFile, CommandOptions options)
  {
    _projectPaths = projectPaths ?? throw new ArgumentNullException(nameof(projectPaths));
    _slnFile = slnFile;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "sln" };

    // Add solution file if specified
    if (!string.IsNullOrWhiteSpace(_slnFile))
    {
      arguments.Add(_slnFile);
    }

    arguments.Add("remove");
    arguments.AddRange(_projectPaths);

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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet sln migrate' commands.
/// </summary>
public class DotNetSlnMigrateBuilder
{
  private readonly string? _slnFile;
  private readonly CommandOptions _options;

  public DotNetSlnMigrateBuilder(string? slnFile, CommandOptions options)
  {
    _slnFile = slnFile;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "sln" };

    // Add solution file if specified
    if (!string.IsNullOrWhiteSpace(_slnFile))
    {
      arguments.Add(_slnFile);
    }

    arguments.Add("migrate");

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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}