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
  private BufferedCommandResult? CachedResult;
  private ExecutionResult? CachedExecuteResult;
  private bool HasExecuted;
  private readonly bool EnableCaching;
  
  internal CommandResult(Command? command) : this(command, false)
  {
  }
  
  private CommandResult(Command? command, bool enableCaching)
  {
    Command = command;
    EnableCaching = enableCaching;
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
    
    // Check cache if caching is enabled
    if (EnableCaching && CachedResult != null)
    {
      return CachedResult.StandardOutput;
    }
    
    BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
    
    // Store in cache if caching is enabled
    if (EnableCaching)
    {
      CachedResult = result;
    }
    
    return result.StandardOutput;
  }
  
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return [];
    }
    
    // Check cache if caching is enabled
    if (EnableCaching && CachedResult != null)
    {
      return CachedResult.StandardOutput.Split
      (
        NewlineCharacters, 
        StringSplitOptions.RemoveEmptyEntries
      );
    }
    
    BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
    
    // Store in cache if caching is enabled
    if (EnableCaching)
    {
      CachedResult = result;
    }
    
    return result.StandardOutput.Split
    (
      NewlineCharacters, 
      StringSplitOptions.RemoveEmptyEntries
    );
  }
  
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return new ExecutionResult(
        new CliWrap.CommandResult(0, DateTimeOffset.MinValue, DateTimeOffset.MinValue),
        string.Empty,
        string.Empty
      );
    }
    
    // If caching is enabled and already executed, return cached result
    if (EnableCaching && HasExecuted && CachedExecuteResult != null)
    {
      return CachedExecuteResult;
    }
    
    // Use ExecuteBufferedAsync to capture stdout and stderr
    BufferedCommandResult bufferedResult = await Command.ExecuteBufferedAsync(cancellationToken);
    
    // Create our result with captured output
    var result = new ExecutionResult(
      new CliWrap.CommandResult(
        bufferedResult.ExitCode,
        bufferedResult.StartTime,
        bufferedResult.ExitTime
      ),
      bufferedResult.StandardOutput,
      bufferedResult.StandardError
    );
    
    // Store result and mark as executed if caching is enabled
    if (EnableCaching)
    {
      HasExecuted = true;
      CachedExecuteResult = result;
      // Also cache the buffered result for GetStringAsync/GetLinesAsync
      CachedResult = bufferedResult;
    }
    
    return result;
  }
  
  public CommandResult Pipe
  (
    string executable,
    params string[]? arguments
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
      return new CommandResult(pipedCommand, EnableCaching);
    }
    catch
    {
      // Command creation failures return null command (graceful degradation)
      return NullCommandResult;
    }
  }
}