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
public class GwqBuilder
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

  // Add Command
  /// <summary>
  /// Create a new worktree.
  /// </summary>
  /// <param name="branch">Branch name or pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Add(string branch)
  {
    _subCommand = "add";
    _target = branch;
    return this;
  }

  /// <summary>
  /// Create a new worktree with path.
  /// </summary>
  /// <param name="branch">Branch name or pattern</param>
  /// <param name="path">Path for the worktree</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Add(string branch, string path)
  {
    _subCommand = "add";
    _target = branch;
    _subCommandArguments.Add(path);
    return this;
  }

  /// <summary>
  /// Create a new worktree interactively.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder AddInteractive()
  {
    _subCommand = "add";
    _subCommandArguments.Add("-i");
    return this;
  }

  /// <summary>
  /// Create new branch with worktree.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithNewBranch()
  {
    _subCommandArguments.Add("-b");
    return this;
  }

  /// <summary>
  /// Force overwrite existing directory.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithForce()
  {
    _subCommandArguments.Add("-f");
    return this;
  }

  /// <summary>
  /// Select branch using fuzzy finder.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithInteractive()
  {
    _subCommandArguments.Add("-i");
    return this;
  }

  // List Command
  /// <summary>
  /// Display worktree list.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder List()
  {
    _subCommand = "list";
    return this;
  }

  /// <summary>
  /// Show all worktrees from configured base directory.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithGlobal()
  {
    _subCommandArguments.Add("-g");
    return this;
  }

  /// <summary>
  /// Show detailed information.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithVerbose()
  {
    _subCommandArguments.Add("-v");
    return this;
  }

  /// <summary>
  /// Output in JSON format.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithJson()
  {
    _subCommandArguments.Add("--json");
    return this;
  }

  // Remove Command
  /// <summary>
  /// Delete worktree.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Remove()
  {
    _subCommand = "remove";
    return this;
  }

  /// <summary>
  /// Delete worktree by pattern.
  /// </summary>
  /// <param name="pattern">Branch pattern to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Remove(string pattern)
  {
    _subCommand = "remove";
    _target = pattern;
    return this;
  }

  /// <summary>
  /// Delete worktree (alias for Remove).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Rm()
  {
    return Remove();
  }

  /// <summary>
  /// Delete worktree by pattern (alias for Remove).
  /// </summary>
  /// <param name="pattern">Branch pattern to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Rm(string pattern)
  {
    return Remove(pattern);
  }

  /// <summary>
  /// Also delete the branch.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithDeleteBranch()
  {
    _subCommandArguments.Add("-b");
    return this;
  }

  /// <summary>
  /// Show deletion targets only.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithDryRun()
  {
    _subCommandArguments.Add("-d");
    return this;
  }

  /// <summary>
  /// Force delete branch even if not merged.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithForceDeleteBranch()
  {
    _subCommandArguments.Add("--force-delete-branch");
    return this;
  }

  // Status Command
  /// <summary>
  /// Show status of all worktrees.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Status()
  {
    _subCommand = "status";
    return this;
  }

  /// <summary>
  /// Output as CSV.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithCsv()
  {
    _subCommandArguments.Add("--csv");
    return this;
  }

  /// <summary>
  /// Filter by status.
  /// </summary>
  /// <param name="filter">Status filter (changed, up to date, inactive)</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithFilter(string filter)
  {
    _subCommandArguments.Add("-f");
    _subCommandArguments.Add(filter);
    return this;
  }

  /// <summary>
  /// Refresh interval for watch mode.
  /// </summary>
  /// <param name="seconds">Interval in seconds</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithInterval(int seconds)
  {
    _subCommandArguments.Add("-i");
    _subCommandArguments.Add($"{seconds}s");
    return this;
  }

  /// <summary>
  /// Skip remote status check.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithNoFetch()
  {
    _subCommandArguments.Add("--no-fetch");
    return this;
  }

  /// <summary>
  /// Include running processes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithShowProcesses()
  {
    _subCommandArguments.Add("--show-processes");
    return this;
  }

  /// <summary>
  /// Sort by field.
  /// </summary>
  /// <param name="field">Sort field (branch, modified, activity)</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithSort(string field)
  {
    _subCommandArguments.Add("-s");
    _subCommandArguments.Add(field);
    return this;
  }

  /// <summary>
  /// Days before marking as stale.
  /// </summary>
  /// <param name="days">Number of days</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithStaleDays(int days)
  {
    _subCommandArguments.Add("--stale-days");
    _subCommandArguments.Add(days.ToString(System.Globalization.CultureInfo.InvariantCulture));
    return this;
  }

  /// <summary>
  /// Auto-refresh mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithWatch()
  {
    _subCommandArguments.Add("-w");
    return this;
  }

  // Get Command
  /// <summary>
  /// Get worktree path.
  /// </summary>
  /// <param name="pattern">Branch pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Get(string pattern)
  {
    _subCommand = "get";
    _target = pattern;
    return this;
  }

  /// <summary>
  /// Output null-terminated path.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithNull()
  {
    _subCommandArguments.Add("-0");
    return this;
  }

  // Exec Command
  /// <summary>
  /// Execute command in worktree directory.
  /// </summary>
  /// <param name="pattern">Branch pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Exec(string pattern)
  {
    _subCommand = "exec";
    _target = pattern;
    return this;
  }

  /// <summary>
  /// Specify command to execute.
  /// </summary>
  /// <param name="command">Command and arguments</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithCommand(params string[] command)
  {
    _execCommand.AddRange(command);
    return this;
  }

  /// <summary>
  /// Stay in worktree directory after execution.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithStay()
  {
    _subCommandArguments.Add("-s");
    return this;
  }

  // Config Command
  /// <summary>
  /// Configuration management.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Config()
  {
    _subCommand = "config";
    return this;
  }

  /// <summary>
  /// List all configuration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigList()
  {
    _subCommand = "config";
    _subCommandArguments.Add("list");
    return this;
  }

  /// <summary>
  /// Get configuration value.
  /// </summary>
  /// <param name="key">Configuration key</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigGet(string key)
  {
    _subCommand = "config";
    _subCommandArguments.Add("get");
    _subCommandArguments.Add(key);
    return this;
  }

  /// <summary>
  /// Set configuration value.
  /// </summary>
  /// <param name="key">Configuration key</param>
  /// <param name="value">Configuration value</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigSet(string key, string value)
  {
    _subCommand = "config";
    _subCommandArguments.Add("set");
    _subCommandArguments.Add(key);
    _subCommandArguments.Add(value);
    return this;
  }

  // Prune Command
  /// <summary>
  /// Clean up deleted worktree information.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Prune()
  {
    _subCommand = "prune";
    return this;
  }

  // Version Command
  /// <summary>
  /// Show version information.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Version()
  {
    _subCommand = "version";
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

/// <summary>
/// Extension methods for integrating gwq with CommandResult.
/// </summary>
public static class GwqExtensions
{
  /// <summary>
  /// Lists worktrees and pipes to another command.
  /// </summary>
  /// <param name="builder">The gwq builder</param>
  /// <param name="command">Command to pipe to</param>
  /// <param name="arguments">Arguments for the piped command</param>
  /// <returns>A CommandResult for the piped command</returns>
  public static CommandResult PipeTo(this GwqBuilder builder, string command, params string[] arguments)
  {
    return builder.Build().Pipe(command, arguments);
  }

  /// <summary>
  /// Lists worktrees and selects one with FZF.
  /// </summary>
  /// <param name="builder">The gwq builder configured for list</param>
  /// <param name="configureFzf">Optional FZF configuration</param>
  /// <returns>A CommandResult with the selected worktree</returns>
  public static CommandResult SelectWithFzf(this GwqBuilder builder, Action<FZFBuilder>? configureFzf = null)
  {
    FZFBuilder fzfBuilder = FZF.Run();
    configureFzf?.Invoke(fzfBuilder);
    
    return builder.Build().Pipe("fzf", ExtractFzfArguments(fzfBuilder));
  }

  private static string[] ExtractFzfArguments(FZFBuilder fzfBuilder)
  {
    // This would need to be implemented to extract arguments from FZFBuilder
    // For now, return empty array
    return Array.Empty<string>();
  }
}