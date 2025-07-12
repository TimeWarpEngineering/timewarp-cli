namespace TimeWarp.Cli;

public partial class GwqBuilder
{
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
}