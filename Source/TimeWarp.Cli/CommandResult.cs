namespace TimeWarp.Cli;

public class CommandResult
{
  private readonly Command? Command;
  
  // Internal access for Pipe() method to avoid duplicating command creation logic
  internal Command? InternalCommand => Command;
  
  internal CommandResult(Command? command)
  {
    Command = command;
  }
  
  public async Task<string> GetStringAsync()
  {
    if (Command == null)
    {
      return string.Empty;
    }
    
    try
    {
      BufferedCommandResult result = await Command.ExecuteBufferedAsync();
      return result.StandardOutput;
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) return empty string
      return string.Empty;
    }
  }
  
  public async Task<string[]> GetLinesAsync()
  {
    if (Command == null)
    {
      return Array.Empty<string>();
    }
    
    try
    {
      BufferedCommandResult result = await Command.ExecuteBufferedAsync();
      return result.StandardOutput.Split
      (
        new char[] { '\n', '\r' }, 
        StringSplitOptions.RemoveEmptyEntries
      );
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) return empty array
      return Array.Empty<string>();
    }
  }
  
  public async Task ExecuteAsync()
  {
    if (Command == null)
    {
      return;
    }
    
    try
    {
      await Command.ExecuteAsync();
    }
    catch
    {
      // Process start failures (non-existent commands, etc.) are silently ignored
      // This matches shell behavior where failed commands don't crash the shell
    }
  }
  
  public CommandResult Pipe(string executable, params string[] arguments)
  {
    // Input validation
    if (Command == null)
    {
      return new CommandResult(null);
    }
    
    if (string.IsNullOrWhiteSpace(executable))
    {
      return new CommandResult(null);
    }
    
    try
    {
      // Use Run() to create the next command instead of duplicating logic
      CommandResult nextCommandResult = CommandExtensions.Run(executable, arguments);
      
      // If Run() failed, it returned a CommandResult with null Command
      if (nextCommandResult.InternalCommand == null)
      {
        return new CommandResult(null);
      }
      
      // Chain commands using CliWrap's pipe operator
      Command pipedCommand = Command | nextCommandResult.InternalCommand;
      return new CommandResult(pipedCommand);
    }
    catch
    {
      // Command creation failures return null command (graceful degradation)
      return new CommandResult(null);
    }
  }
}