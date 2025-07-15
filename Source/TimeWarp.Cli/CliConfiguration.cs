namespace TimeWarp.Cli;

using System.Threading;

/// <summary>
/// Global configuration for TimeWarp.Cli, including command path overrides for testing.
/// </summary>
public static class CliConfiguration
{
  private static readonly Dictionary<string, string> CommandPaths = new();
  private static readonly Lock Lock = new();
  
  /// <summary>
  /// Sets a custom path for a command executable. Useful for testing with mock executables.
  /// </summary>
  /// <param name="command">The command name (e.g., "fzf", "git")</param>
  /// <param name="path">The full path to the executable</param>
  /// <example>
  /// CliConfiguration.SetCommandPath("fzf", "/tmp/mock-bin/fzf");
  /// </example>
  public static void SetCommandPath(string command, string path)
  {
    ArgumentNullException.ThrowIfNull(command);
    ArgumentNullException.ThrowIfNull(path);
    
    lock (Lock)
    {
      CommandPaths[command] = path;
    }
  }
  
  /// <summary>
  /// Clears a custom command path, reverting to the default behavior.
  /// </summary>
  /// <param name="command">The command name to clear</param>
  public static void ClearCommandPath(string command)
  {
    ArgumentNullException.ThrowIfNull(command);
    
    lock (Lock)
    {
      CommandPaths.Remove(command);
    }
  }
  
  /// <summary>
  /// Clears all custom command paths, reverting all commands to default behavior.
  /// </summary>
  public static void Reset()
  {
    lock (Lock)
    {
      CommandPaths.Clear();
    }
  }
  
  /// <summary>
  /// Gets the configured path for a command, or returns the command itself if no custom path is set.
  /// </summary>
  /// <param name="command">The command to look up</param>
  /// <returns>The configured path if set, otherwise the original command</returns>
  internal static string GetCommandPath(string command)
  {
    lock (Lock)
    {
      return CommandPaths.TryGetValue(command, out string? path) ? path : command;
    }
  }
  
  /// <summary>
  /// Checks if a command has a custom path configured.
  /// </summary>
  /// <param name="command">The command to check</param>
  /// <returns>True if a custom path is configured, false otherwise</returns>
  public static bool HasCustomPath(string command)
  {
    ArgumentNullException.ThrowIfNull(command);
    
    lock (Lock)
    {
      return CommandPaths.ContainsKey(command);
    }
  }
  
  /// <summary>
  /// Gets all currently configured command paths.
  /// </summary>
  /// <returns>A read-only dictionary of command to path mappings</returns>
  public static IReadOnlyDictionary<string, string> AllCommandPaths
  {
    get
    {
      lock (Lock)
      {
        return new Dictionary<string, string>(CommandPaths);
      }
    }
  }
}