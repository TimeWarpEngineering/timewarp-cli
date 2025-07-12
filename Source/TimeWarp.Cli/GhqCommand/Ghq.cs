namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for ghq (GitHub repository manager) integration.
/// </summary>
public static class Ghq
{
  /// <summary>
  /// Creates a fluent builder for the 'ghq' command.
  /// </summary>
  /// <returns>A GhqBuilder for configuring the ghq command</returns>
  public static GhqBuilder Run()
  {
    return new GhqBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'ghq' commands.
/// </summary>
public partial class GhqBuilder
{
  private CommandOptions _options = new();
  private List<string> _arguments = new();
  private string? _subCommand;
  private string? _repository;
  private List<string> _subCommandArguments = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithWorkingDirectory(string directory)
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
  public GhqBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  // Build methods
  public CommandResult Build()
  {
    var arguments = new List<string> { "ghq" };
    
    // Add subcommand
    if (!string.IsNullOrEmpty(_subCommand))
    {
      arguments.Add(_subCommand);
    }
    
    // Add subcommand arguments
    arguments.AddRange(_subCommandArguments);
    
    // Add repository if specified
    if (!string.IsNullOrEmpty(_repository))
    {
      arguments.Add(_repository);
    }
    
    // Add global arguments
    arguments.AddRange(_arguments);

    return CommandExtensions.Run("ghq", arguments.Skip(1).ToArray(), _options);
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