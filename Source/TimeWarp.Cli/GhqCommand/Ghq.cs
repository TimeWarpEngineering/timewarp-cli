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
  private CommandOptions Options = new();
  private List<string> Arguments = new();
  private string? SubCommand;
  private string? Repository;
  private List<string> SubCommandArguments = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithWorkingDirectory(string directory)
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
  public GhqBuilder WithEnvironmentVariable(string key, string? value)
  {
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  // Build methods
  public CommandResult Build()
  {
    List<string> arguments = new() { "ghq" };
    
    // Add subcommand
    if (!string.IsNullOrEmpty(SubCommand))
    {
      arguments.Add(SubCommand);
    }
    
    // Add subcommand arguments
    arguments.AddRange(SubCommandArguments);
    
    // Add repository if specified
    if (!string.IsNullOrEmpty(Repository))
    {
      arguments.Add(Repository);
    }
    
    // Add global arguments
    arguments.AddRange(Arguments);

    return CommandExtensions.Run("ghq", arguments.Skip(1).ToArray(), Options);
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