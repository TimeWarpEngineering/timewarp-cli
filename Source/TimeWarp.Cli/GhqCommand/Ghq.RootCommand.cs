namespace TimeWarp.Cli;

public partial class GhqBuilder
{
  // Root Command
  /// <summary>
  /// Show repositories' root.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Root()
  {
    _subCommand = "root";
    return this;
  }

  /// <summary>
  /// Show all root directories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithAll()
  {
    _subCommandArguments.Add("--all");
    return this;
  }
}