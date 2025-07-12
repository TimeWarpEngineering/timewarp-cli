namespace TimeWarp.Cli;

public partial class GhqBuilder
{
  // List Command
  /// <summary>
  /// List local repositories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder List()
  {
    _subCommand = "list";
    return this;
  }

  /// <summary>
  /// Show exact matches only.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithExact()
  {
    _subCommandArguments.Add("--exact");
    return this;
  }

  /// <summary>
  /// Filter by VCS backend.
  /// </summary>
  /// <param name="vcs">VCS backend to filter by</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder FilterByVcs(string vcs)
  {
    _subCommandArguments.Add("--vcs");
    _subCommandArguments.Add(vcs);
    return this;
  }

  /// <summary>
  /// Print full paths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithFullPath()
  {
    _subCommandArguments.Add("--full-path");
    return this;
  }

  /// <summary>
  /// Print unique subpaths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithUnique()
  {
    _subCommandArguments.Add("--unique");
    return this;
  }
}