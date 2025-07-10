namespace TimeWarp.Cli;

public class CommandResult
{
  private readonly Command? Command;
  
  // Constants for line splitting - handle both Unix (\n) and Windows (\r\n) line endings
  private static readonly char[] NewlineCharacters = { '\n', '\r' };
  
  // Singleton for failed commands to avoid creating multiple identical null instances
  internal static readonly CommandResult NullCommandResult = new(null);
  
  // Property to access Command from other CommandResult instances in Pipe() method
  private Command? InternalCommand => Command;
  
  // Caching support
  private BufferedCommandResult? _cachedResult;
  private bool _hasExecuted;
  private readonly bool _enableCaching;
  
  internal CommandResult(Command? command) : this(command, false)
  {
  }
  
  private CommandResult(Command? command, bool enableCaching)
  {
    Command = command;
    _enableCaching = enableCaching;
  }
  
  /// <summary>
  /// Creates a new CommandResult instance with caching enabled.
  /// Subsequent calls to GetStringAsync(), GetLinesAsync(), or ExecuteAsync() 
  /// will return cached results instead of re-executing the command.
  /// </summary>
  /// <returns>A new CommandResult instance with caching enabled</returns>
  public CommandResult Cached()
  {
    // Return a new instance with caching enabled, preserving the command
    return new CommandResult(Command, true);
  }
  
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return string.Empty;
    }
    
    try
    {
      // Check cache if caching is enabled
      if (_enableCaching && _cachedResult != null)
      {
        return _cachedResult.StandardOutput;
      }
      
      BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
      
      // Store in cache if caching is enabled
      if (_enableCaching)
      {
        _cachedResult = result;
      }
      
      return result.StandardOutput;
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) return empty string
      return string.Empty;
    }
  }
  
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return Array.Empty<string>();
    }
    
    try
    {
      // Check cache if caching is enabled
      if (_enableCaching && _cachedResult != null)
      {
        return _cachedResult.StandardOutput.Split
        (
          NewlineCharacters, 
          StringSplitOptions.RemoveEmptyEntries
        );
      }
      
      BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
      
      // Store in cache if caching is enabled
      if (_enableCaching)
      {
        _cachedResult = result;
      }
      
      return result.StandardOutput.Split
      (
        NewlineCharacters, 
        StringSplitOptions.RemoveEmptyEntries
      );
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) return empty array
      return Array.Empty<string>();
    }
  }
  
  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return;
    }
    
    // If caching is enabled and already executed, return immediately
    if (_enableCaching && _hasExecuted)
    {
      return;
    }
    
    try
    {
      await Command.ExecuteAsync(cancellationToken);
      
      // Mark as executed if caching is enabled
      if (_enableCaching)
      {
        _hasExecuted = true;
      }
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) are silently ignored
      // This matches shell behavior where failed commands don't crash the shell
    }
  }
  
  public CommandResult Pipe
  (
    string executable,
    params string[] arguments
  )
  {
    // Input validation
    if (Command == null)
    {
      return NullCommandResult;
    }
    
    if (string.IsNullOrWhiteSpace(executable))
    {
      return NullCommandResult;
    }
    
    try
    {
      // Use Run() to create the next command instead of duplicating logic
      CommandResult nextCommandResult = CommandExtensions.Run(executable, arguments);
      
      // If Run() failed, it returned a CommandResult with null Command
      if (nextCommandResult.InternalCommand == null)
      {
        return NullCommandResult;
      }
      
      // Chain commands using CliWrap's pipe operator
      Command pipedCommand = Command | nextCommandResult.InternalCommand;
      
      // Preserve caching state in the pipeline
      return new CommandResult(pipedCommand, _enableCaching);
    }
    catch
    {
      // Command creation failures return null command (graceful degradation)
      return NullCommandResult;
    }
  }
}