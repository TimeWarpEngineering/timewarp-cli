namespace TimeWarp.Cli;

public class CommandResult
{
  private readonly Command? Command;
  
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
    if (Command == null)
    {
      return new CommandResult(null);
    }
    
    try
    {
      Command nextCommand = CliWrap.Cli.Wrap(executable)
        .WithArguments(arguments)
        .WithValidation(CommandResultValidation.None);
        
      // Chain commands using CliWrap's pipe operator
      Command pipedCommand = Command | nextCommand;
      return new CommandResult(pipedCommand);
    }
    catch
    {
      // Command creation failures return null command (graceful degradation)
      return new CommandResult(null);
    }
  }
}