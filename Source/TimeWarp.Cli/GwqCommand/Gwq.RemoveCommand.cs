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
    SubCommand = "remove";
    return this;
  }

  /// <summary>
  /// Delete worktree by pattern.
  /// </summary>
  /// <param name="pattern">Branch pattern to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Remove(string pattern)
  {
    SubCommand = "remove";
    Target = pattern;
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
    SubCommandArguments.Add("-b");
    return this;
  }

  /// <summary>
  /// Show deletion targets only.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithDryRun()
  {
    SubCommandArguments.Add("-d");
    return this;
  }

  /// <summary>
  /// Force delete branch even if not merged.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithForceDeleteBranch()
  {
    SubCommandArguments.Add("--force-delete-branch");
    return this;
  }
}