namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - User-secrets command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets' command.
  /// </summary>
  /// <returns>A DotNetUserSecretsBuilder for configuring the dotnet user-secrets command</returns>
  public static DotNetUserSecretsBuilder UserSecrets()
  {
    return new DotNetUserSecretsBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet user-secrets' commands.
/// </summary>
public class DotNetUserSecretsBuilder
{
  private CommandOptions _options = new();
  private string? _project;
  private string? _id;

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetUserSecretsBuilder WithWorkingDirectory(string directory)
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
  public DotNetUserSecretsBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Specifies the project file to use.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetUserSecretsBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the user secrets ID.
  /// </summary>
  /// <param name="id">The user secrets ID</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetUserSecretsBuilder WithId(string id)
  {
    _id = id;
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets init' command.
  /// </summary>
  /// <returns>A DotNetUserSecretsInitBuilder for configuring the init command</returns>
  public DotNetUserSecretsInitBuilder Init()
  {
    return new DotNetUserSecretsInitBuilder(_project, _id, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets set' command.
  /// </summary>
  /// <param name="key">The secret key</param>
  /// <param name="value">The secret value</param>
  /// <returns>A DotNetUserSecretsSetBuilder for configuring the set command</returns>
  public DotNetUserSecretsSetBuilder Set(string key, string value)
  {
    return new DotNetUserSecretsSetBuilder(key, value, _project, _id, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets remove' command.
  /// </summary>
  /// <param name="key">The secret key to remove</param>
  /// <returns>A DotNetUserSecretsRemoveBuilder for configuring the remove command</returns>
  public DotNetUserSecretsRemoveBuilder Remove(string key)
  {
    return new DotNetUserSecretsRemoveBuilder(key, _project, _id, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets list' command.
  /// </summary>
  /// <returns>A DotNetUserSecretsListBuilder for configuring the list command</returns>
  public DotNetUserSecretsListBuilder List()
  {
    return new DotNetUserSecretsListBuilder(_project, _id, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet user-secrets clear' command.
  /// </summary>
  /// <returns>A DotNetUserSecretsClearBuilder for configuring the clear command</returns>
  public DotNetUserSecretsClearBuilder Clear()
  {
    return new DotNetUserSecretsClearBuilder(_project, _id, _options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet user-secrets init' commands.
/// </summary>
public class DotNetUserSecretsInitBuilder
{
  private readonly string? _project;
  private readonly string? _id;
  private readonly CommandOptions _options;

  public DotNetUserSecretsInitBuilder(string? project, string? id, CommandOptions options)
  {
    _project = project;
    _id = id;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "user-secrets", "init" };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_id))
    {
      arguments.Add("--id");
      arguments.Add(_id);
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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet user-secrets set' commands.
/// </summary>
public class DotNetUserSecretsSetBuilder
{
  private readonly string _key;
  private readonly string _value;
  private readonly string? _project;
  private readonly string? _id;
  private readonly CommandOptions _options;

  public DotNetUserSecretsSetBuilder(string key, string value, string? project, string? id, CommandOptions options)
  {
    _key = key ?? throw new ArgumentNullException(nameof(key));
    _value = value ?? throw new ArgumentNullException(nameof(value));
    _project = project;
    _id = id;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "user-secrets", "set", _key, _value };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_id))
    {
      arguments.Add("--id");
      arguments.Add(_id);
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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet user-secrets remove' commands.
/// </summary>
public class DotNetUserSecretsRemoveBuilder
{
  private readonly string _key;
  private readonly string? _project;
  private readonly string? _id;
  private readonly CommandOptions _options;

  public DotNetUserSecretsRemoveBuilder(string key, string? project, string? id, CommandOptions options)
  {
    _key = key ?? throw new ArgumentNullException(nameof(key));
    _project = project;
    _id = id;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "user-secrets", "remove", _key };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_id))
    {
      arguments.Add("--id");
      arguments.Add(_id);
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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet user-secrets list' commands.
/// </summary>
public class DotNetUserSecretsListBuilder
{
  private readonly string? _project;
  private readonly string? _id;
  private readonly CommandOptions _options;

  public DotNetUserSecretsListBuilder(string? project, string? id, CommandOptions options)
  {
    _project = project;
    _id = id;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "user-secrets", "list" };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_id))
    {
      arguments.Add("--id");
      arguments.Add(_id);
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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet user-secrets clear' commands.
/// </summary>
public class DotNetUserSecretsClearBuilder
{
  private readonly string? _project;
  private readonly string? _id;
  private readonly CommandOptions _options;

  public DotNetUserSecretsClearBuilder(string? project, string? id, CommandOptions options)
  {
    _project = project;
    _id = id;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "user-secrets", "clear" };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_id))
    {
      arguments.Add("--id");
      arguments.Add(_id);
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

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}