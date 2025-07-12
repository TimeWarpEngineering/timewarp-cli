namespace TimeWarp.Cli;

public partial class GhqBuilder
{
  // Remove Command
  /// <summary>
  /// Remove local repository.
  /// </summary>
  /// <param name="repository">Repository to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Remove(string repository)
  {
    _subCommand = "rm";
    _repository = repository;
    return this;
  }

  /// <summary>
  /// Remove local repository (alias for Remove).
  /// </summary>
  /// <param name="repository">Repository to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Rm(string repository)
  {
    return Remove(repository);
  }

  /// <summary>
  /// Dry run mode for remove.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithDryRun()
  {
    _subCommandArguments.Add("--dry-run");
    return this;
  }
}