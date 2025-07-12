namespace TimeWarp.Cli;

public partial class GwqBuilder
{
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
}