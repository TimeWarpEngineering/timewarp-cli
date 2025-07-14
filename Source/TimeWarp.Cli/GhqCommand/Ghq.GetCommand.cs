namespace TimeWarp.Cli;

public partial class GhqBuilder
{
  // Get/Clone Command
  /// <summary>
  /// Clone/sync with a remote repository.
  /// </summary>
  /// <param name="repository">Repository to clone (e.g., 'github.com/user/repo')</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Get(string repository)
  {
    SubCommand = "get";
    Repository = repository;
    return this;
  }

  /// <summary>
  /// Clone/sync with a remote repository (alias for Get).
  /// </summary>
  /// <param name="repository">Repository to clone</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Clone(string repository)
  {
    return Get(repository);
  }

  /// <summary>
  /// Look for existing clones and fetch them.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithLook()
  {
    SubCommandArguments.Add("--look");
    return this;
  }

  /// <summary>
  /// Update existing clones.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithUpdate()
  {
    SubCommandArguments.Add("--update");
    return this;
  }

  /// <summary>
  /// Clone with shallow history.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithShallow()
  {
    SubCommandArguments.Add("--shallow");
    return this;
  }

  /// <summary>
  /// Clone only the specified branch.
  /// </summary>
  /// <param name="branch">Branch name to clone</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithBranch(string branch)
  {
    SubCommandArguments.Add("--branch");
    SubCommandArguments.Add(branch);
    return this;
  }

  /// <summary>
  /// Clone as a bare repository.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithBare()
  {
    SubCommandArguments.Add("--bare");
    return this;
  }

  /// <summary>
  /// Skip recursive fetching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithNoRecursive()
  {
    SubCommandArguments.Add("--no-recursive");
    return this;
  }

  /// <summary>
  /// Silence git output.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithSilent()
  {
    SubCommandArguments.Add("--silent");
    return this;
  }

  /// <summary>
  /// Enable parallel fetching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithParallel()
  {
    SubCommandArguments.Add("--parallel");
    return this;
  }

  /// <summary>
  /// Use specified VCS backend (git, github, gitlab, etc.).
  /// </summary>
  /// <param name="vcs">VCS backend name</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithVcs(string vcs)
  {
    SubCommandArguments.Add("--vcs");
    SubCommandArguments.Add(vcs);
    return this;
  }
}