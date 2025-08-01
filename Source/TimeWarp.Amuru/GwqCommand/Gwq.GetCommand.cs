namespace TimeWarp.Amuru;

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
    SubCommand = "get";
    Target = pattern;
    return this;
  }

  /// <summary>
  /// Output null-terminated path.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithNull()
  {
    SubCommandArguments.Add("-0");
    return this;
  }
}