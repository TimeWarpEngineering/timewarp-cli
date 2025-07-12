namespace TimeWarp.Cli;

/// <summary>
/// Extension methods for integrating FZF with CommandResult.
/// </summary>
public static class FZFExtensions
{
  /// <summary>
  /// Pipes the command output to FZF for interactive selection.
  /// </summary>
  /// <param name="command">The command to pipe from</param>
  /// <param name="configure">Optional FZF configuration</param>
  /// <returns>A CommandResult with FZF selection</returns>
  public static CommandResult SelectWithFzf(this CommandResult command, Action<FZFBuilder>? configure = null)
  {
    var fzfBuilder = new FZFBuilder();
    configure?.Invoke(fzfBuilder);
    
    CommandResult fzfArguments = fzfBuilder.Build();
    return command.Pipe("fzf", ExtractFzfArguments(fzfArguments));
  }

  private static string[] ExtractFzfArguments(CommandResult fzfCommand)
  {
    // This is a simplified implementation
    // In a real implementation, you'd extract the arguments from the FZFBuilder
    return Array.Empty<string>();
  }
}