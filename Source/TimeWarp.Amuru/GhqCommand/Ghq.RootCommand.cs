namespace TimeWarp.Amuru;

public partial class GhqBuilder
{
  // Root Command
  /// <summary>
  /// Show repositories' root.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Root()
  {
    SubCommand = "root";
    return this;
  }

  /// <summary>
  /// Show all root directories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithAll()
  {
    SubCommandArguments.Add("--all");
    return this;
  }
}