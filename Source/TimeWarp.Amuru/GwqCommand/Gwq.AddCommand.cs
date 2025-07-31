namespace TimeWarp.Amuru;

public partial class GwqBuilder
{
  // Add Command
  /// <summary>
  /// Create a new worktree.
  /// </summary>
  /// <param name="branch">Branch name or pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Add(string branch)
  {
    SubCommand = "add";
    Target = branch;
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
    SubCommand = "add";
    Target = branch;
    SubCommandArguments.Add(path);
    return this;
  }

  /// <summary>
  /// Create a new worktree interactively.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder AddInteractive()
  {
    SubCommand = "add";
    SubCommandArguments.Add("-i");
    return this;
  }

  /// <summary>
  /// Create new branch with worktree.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithNewBranch()
  {
    SubCommandArguments.Add("-b");
    return this;
  }

  /// <summary>
  /// Force overwrite existing directory.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithForce()
  {
    SubCommandArguments.Add("-f");
    return this;
  }

  /// <summary>
  /// Select branch using fuzzy finder.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithInteractive()
  {
    SubCommandArguments.Add("-i");
    return this;
  }
}