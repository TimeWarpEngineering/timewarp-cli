namespace TimeWarp.Cli;

/// <summary>
/// Fluent builder for 'dotnet tool list' commands.
/// </summary>
public class DotNetToolListBuilder
{
  private readonly CommandOptions Options;
  private bool IsGlobal;
  private bool IsLocal;
  private string? ToolPath;

  public DotNetToolListBuilder(CommandOptions options)
  {
    Options = options;
  }

  /// <summary>
  /// Lists globally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Global()
  {
    IsGlobal = true;
    IsLocal = false;
    return this;
  }

  /// <summary>
  /// Lists locally installed tools.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder Local()
  {
    IsLocal = true;
    IsGlobal = false;
    return this;
  }

  /// <summary>
  /// Specifies the path where tools are installed.
  /// </summary>
  /// <param name="toolPath">The installation path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolListBuilder WithToolPath(string toolPath)
  {
    ToolPath = toolPath;
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "list" };

    if (IsGlobal)
    {
      arguments.Add("--global");
    }

    if (IsLocal)
    {
      arguments.Add("--local");
    }

    if (!string.IsNullOrWhiteSpace(ToolPath))
    {
      arguments.Add("--tool-path");
      arguments.Add(ToolPath);
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