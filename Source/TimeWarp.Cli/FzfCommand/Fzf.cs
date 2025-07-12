namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for Fzf (fuzzy finder) integration.
/// </summary>
public static class Fzf
{
  /// <summary>
  /// Creates a fluent builder for the 'fzf' command.
  /// </summary>
  /// <returns>A FzfBuilder for configuring the fzf command</returns>
  public static FzfBuilder Run()
  {
    return new FzfBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'fzf' commands.
/// </summary>
public partial class FzfBuilder
{
  private CommandOptions _options = new();
  private List<string> _arguments = new();
  private List<string> _inputItems = new();
  private string? _inputCommand;
  private string? _inputGlob;
  private bool _useStdin;

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithWorkingDirectory(string directory)
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
  public FzfBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "fzf" };
    arguments.AddRange(_arguments);

    CommandResult command = CommandExtensions.Run("fzf", arguments.Skip(1).ToArray(), _options);

    // Handle input sources
    if (_inputItems.Count > 0)
    {
      // Create a pipeline with echo for the input items
      string input = string.Join("\n", _inputItems);
      command = CommandExtensions.Run("echo", new[] { input }, _options).Pipe("fzf", arguments.Skip(1).ToArray());
    }
    else if (!string.IsNullOrEmpty(_inputGlob))
    {
      // Use find command for file glob
      command = CommandExtensions.Run("find", new[] { ".", "-name", _inputGlob }, _options).Pipe("fzf", arguments.Skip(1).ToArray());
    }
    else if (!string.IsNullOrEmpty(_inputCommand))
    {
      // Parse and execute the input command
      string[] parts = _inputCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length > 0)
      {
        string exe = parts[0];
        string[] args = parts.Skip(1).ToArray();
        command = CommandExtensions.Run(exe, args, _options).Pipe("fzf", arguments.Skip(1).ToArray());
      }
    }
    else if (_useStdin)
    {
      // Use fzf directly with stdin (no pipeline needed)
      command = CommandExtensions.Run("fzf", arguments.Skip(1).ToArray(), _options);
    }

    return command;
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