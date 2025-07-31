namespace TimeWarp.Amuru;

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
    SubCommand = "create";
    Repository = repository;
    return this;
  }
}