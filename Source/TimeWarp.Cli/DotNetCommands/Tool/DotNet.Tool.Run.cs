namespace TimeWarp.Cli;

/// <summary>
/// Fluent builder for 'dotnet tool run' commands.
/// </summary>
public class DotNetToolRunBuilder
{
  private readonly string CommandName;
  private readonly CommandOptions Options;
  private List<string> ToolArguments = new();

  public DotNetToolRunBuilder(string commandName, CommandOptions options)
  {
    CommandName = commandName ?? throw new ArgumentNullException(nameof(commandName));
    Options = options;
  }

  /// <summary>
  /// Adds arguments to pass to the tool.
  /// </summary>
  /// <param name="arguments">The arguments to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArguments(params string[] arguments)
  {
    ToolArguments.AddRange(arguments);
    return this;
  }

  /// <summary>
  /// Adds a single argument to pass to the tool.
  /// </summary>
  /// <param name="argument">The argument to pass to the tool</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetToolRunBuilder WithArgument(string argument)
  {
    ToolArguments.Add(argument);
    return this;
  }

  public CommandResult Build()
  {
    List<string> arguments = new() { "tool", "run", CommandName };
    arguments.AddRange(ToolArguments);

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