namespace TimeWarp.Amuru;

/// <summary>
/// Fluent builder for 'dotnet tool search' commands.
/// </summary>
public class DotNetToolSearchBuilder
{
  private readonly string SearchTerm;
  private readonly CommandOptions Options;
  private bool Detail;
  private int? Skip;
  private int? Take;
  private bool Prerelease;

  public DotNetToolSearchBuilder(string searchTerm, CommandOptions options)
  {
    SearchTerm = searchTerm ?? throw new ArgumentNullException(nameof(searchTerm));
    Options = options;
  }

  /// <summary>
  /// Shows detailed information about the tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithDetail()
  {
    Detail = true;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to skip.
  /// </summary>
  /// <param name="skip">The number of tools to skip</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithSkip(int skip)
  {
    Skip = skip;
    return this;
  }

  /// <summary>
  /// Specifies the number of tools to take.
  /// </summary>
  /// <param name="take">The number of tools to take</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithTake(int take)
  {
    Take = take;
    return this;
  }

  /// <summary>
  /// Includes prerelease tools in the search.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolSearchBuilder WithPrerelease()
  {
    Prerelease = true;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "search", SearchTerm };

    if (Detail)
    {
      arguments.Add("--detail");
    }

    if (Skip.HasValue)
    {
      arguments.Add("--skip");
      arguments.Add(Skip.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (Take.HasValue)
    {
      arguments.Add("--take");
      arguments.Add(Take.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (Prerelease)
    {
      arguments.Add("--prerelease");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}