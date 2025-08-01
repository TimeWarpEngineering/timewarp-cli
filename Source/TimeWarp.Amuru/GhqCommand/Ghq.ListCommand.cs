namespace TimeWarp.Amuru;

public partial class GhqBuilder
{
  // List Command
  /// <summary>
  /// List local repositories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder List()
  {
    SubCommand = "list";
    return this;
  }

  /// <summary>
  /// Show exact matches only.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithExact()
  {
    SubCommandArguments.Add("--exact");
    return this;
  }

  /// <summary>
  /// Filter by VCS backend.
  /// </summary>
  /// <param name="vcs">VCS backend to filter by</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder FilterByVcs(string vcs)
  {
    SubCommandArguments.Add("--vcs");
    SubCommandArguments.Add(vcs);
    return this;
  }

  /// <summary>
  /// Print full paths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithFullPath()
  {
    SubCommandArguments.Add("--full-path");
    return this;
  }

  /// <summary>
  /// Print unique subpaths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithUnique()
  {
    SubCommandArguments.Add("--unique");
    return this;
  }
}