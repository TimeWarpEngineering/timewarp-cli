namespace TimeWarp.Cli;

public partial class GwqBuilder
{
  // List Command
  /// <summary>
  /// Display worktree list.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder List()
  {
    SubCommand = "list";
    return this;
  }

  /// <summary>
  /// Show all worktrees from configured base directory.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithGlobal()
  {
    SubCommandArguments.Add("-g");
    return this;
  }

  /// <summary>
  /// Show detailed information.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithVerbose()
  {
    SubCommandArguments.Add("-v");
    return this;
  }

  /// <summary>
  /// Output in JSON format.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithJson()
  {
    SubCommandArguments.Add("--json");
    return this;
  }
}