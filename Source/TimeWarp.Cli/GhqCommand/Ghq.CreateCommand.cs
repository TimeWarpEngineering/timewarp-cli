namespace TimeWarp.Cli;

public partial class GhqBuilder
{
  // Create Command
  /// <summary>
  /// Create a new repository.
  /// </summary>
  /// <param name="repository">Repository to create</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Create(string repository)
  {
    _subCommand = "create";
    _repository = repository;
    return this;
  }
}