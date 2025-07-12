namespace TimeWarp.Cli;

public partial class GwqBuilder
{
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
}