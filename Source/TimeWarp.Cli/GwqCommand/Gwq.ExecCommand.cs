namespace TimeWarp.Cli;

public partial class GwqBuilder
{
  // Exec Command
  /// <summary>
  /// Execute command in worktree directory.
  /// </summary>
  /// <param name="pattern">Branch pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Exec(string pattern)
  {
    SubCommand = "exec";
    Target = pattern;
    return this;
  }

  /// <summary>
  /// Specify command to execute.
  /// </summary>
  /// <param name="command">Command and arguments</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithCommand(params string[] command)
  {
    ExecCommand.AddRange(command);
    return this;
  }

  /// <summary>
  /// Stay in worktree directory after execution.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder WithStay()
  {
    SubCommandArguments.Add("-s");
    return this;
  }
}