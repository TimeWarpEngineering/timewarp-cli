namespace TimeWarp.Cli;

public partial class GwqBuilder
{
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
}