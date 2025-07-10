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
  
  internal CommandResult(Command? command)
  {
    Command = command;
  }
  
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    if (Command == null)
    {
      return string.Empty;
    }
    
    try
    {
      BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
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
      BufferedCommandResult result = await Command.ExecuteBufferedAsync(cancellationToken);
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
    
    try
    {
      await Command.ExecuteAsync(cancellationToken);
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
      return new CommandResult(pipedCommand);
    }
    catch
    {
      // Command creation failures return null command (graceful degradation)
      return NullCommandResult;
    }
  }
}