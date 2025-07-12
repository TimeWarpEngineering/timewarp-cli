namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for gwq (Git worktree manager) integration.
/// </summary>
public static class Gwq
{
  /// <summary>
  /// Creates a fluent builder for the 'gwq' command.
  /// </summary>
  /// <returns>A GwqBuilder for configuring the gwq command</returns>
  public static GwqBuilder Run()
  {
    return new GwqBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'gwq' commands.
/// </summary>
public partial class GwqBuilder
{
  private CommandOptions _options = new();
  private List<string> _arguments = new();
  private string? _subCommand;
  private string? _target;
  private List<string> _subCommandArguments = new();
  private List<string> _execCommand = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithWorkingDirectory(string directory)
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
  public GwqBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  // Build methods
  public CommandResult Build()
  {
    var arguments = new List<string> { "gwq" };
    
    // Add subcommand
    if (!string.IsNullOrEmpty(_subCommand))
    {
      arguments.Add(_subCommand);
    }
    
    // Add subcommand arguments (flags before positional args)
    arguments.AddRange(_subCommandArguments);
    
    // Add target if specified
    if (!string.IsNullOrEmpty(_target))
    {
      arguments.Add(_target);
    }
    
    // Add exec command if specified
    if (_execCommand.Count > 0 && _subCommand == "exec")
    {
      arguments.Add("--");
      arguments.AddRange(_execCommand);
    }
    
    // Add global arguments
    arguments.AddRange(_arguments);

    return CommandExtensions.Run("gwq", arguments.Skip(1).ToArray(), _options);
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