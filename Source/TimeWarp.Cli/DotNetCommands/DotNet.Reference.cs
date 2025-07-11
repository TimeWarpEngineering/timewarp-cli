namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Reference command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference' command.
  /// </summary>
  /// <returns>A DotNetReferenceBuilder for configuring the dotnet reference command</returns>
  public static DotNetReferenceBuilder Reference()
  {
    return new DotNetReferenceBuilder();
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference' command with a specific project.
  /// </summary>
  /// <param name="project">The project file to operate on</param>
  /// <returns>A DotNetReferenceBuilder for configuring the dotnet reference command</returns>
  public static DotNetReferenceBuilder Reference(string project)
  {
    return new DotNetReferenceBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet reference' commands.
/// </summary>
public class DotNetReferenceBuilder
{
  private string? _project;
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the project file to operate on.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetReferenceBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetReferenceBuilder WithWorkingDirectory(string directory)
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
  public DotNetReferenceBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference add' command.
  /// </summary>
  /// <param name="projectPath">The project path to add as a reference</param>
  /// <returns>A DotNetReferenceAddBuilder for configuring the add command</returns>
  public DotNetReferenceAddBuilder Add(string projectPath)
  {
    return new DotNetReferenceAddBuilder(projectPath, _project, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference add' command with multiple projects.
  /// </summary>
  /// <param name="projectPaths">The project paths to add as references</param>
  /// <returns>A DotNetReferenceAddBuilder for configuring the add command</returns>
  public DotNetReferenceAddBuilder Add(params string[] projectPaths)
  {
    return new DotNetReferenceAddBuilder(projectPaths, _project, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference list' command.
  /// </summary>
  /// <returns>A DotNetReferenceListBuilder for configuring the list command</returns>
  public DotNetReferenceListBuilder List()
  {
    return new DotNetReferenceListBuilder(_project, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference remove' command.
  /// </summary>
  /// <param name="projectPath">The project path to remove as a reference</param>
  /// <returns>A DotNetReferenceRemoveBuilder for configuring the remove command</returns>
  public DotNetReferenceRemoveBuilder Remove(string projectPath)
  {
    return new DotNetReferenceRemoveBuilder(projectPath, _project, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet reference remove' command with multiple projects.
  /// </summary>
  /// <param name="projectPaths">The project paths to remove as references</param>
  /// <returns>A DotNetReferenceRemoveBuilder for configuring the remove command</returns>
  public DotNetReferenceRemoveBuilder Remove(params string[] projectPaths)
  {
    return new DotNetReferenceRemoveBuilder(projectPaths, _project, _options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet reference add' commands.
/// </summary>
public class DotNetReferenceAddBuilder
{
  private readonly string[] _projectPaths;
  private readonly string? _project;
  private readonly CommandOptions _options;

  public DotNetReferenceAddBuilder(string projectPath, string? project, CommandOptions options)
  {
    _projectPaths = [projectPath ?? throw new ArgumentNullException(nameof(projectPath))];
    _project = project;
    _options = options;
  }

  public DotNetReferenceAddBuilder(string[] projectPaths, string? project, CommandOptions options)
  {
    _projectPaths = projectPaths ?? throw new ArgumentNullException(nameof(projectPaths));
    _project = project;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "reference" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet reference list' commands.
/// </summary>
public class DotNetReferenceListBuilder
{
  private readonly string? _project;
  private readonly CommandOptions _options;

  public DotNetReferenceListBuilder(string? project, CommandOptions options)
  {
    _project = project;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "reference" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet reference remove' commands.
/// </summary>
public class DotNetReferenceRemoveBuilder
{
  private readonly string[] _projectPaths;
  private readonly string? _project;
  private readonly CommandOptions _options;

  public DotNetReferenceRemoveBuilder(string projectPath, string? project, CommandOptions options)
  {
    _projectPaths = [projectPath ?? throw new ArgumentNullException(nameof(projectPath))];
    _project = project;
    _options = options;
  }

  public DotNetReferenceRemoveBuilder(string[] projectPaths, string? project, CommandOptions options)
  {
    _projectPaths = projectPaths ?? throw new ArgumentNullException(nameof(projectPaths));
    _project = project;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "reference" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}