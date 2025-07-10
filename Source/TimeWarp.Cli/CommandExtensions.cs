namespace TimeWarp.Cli;

public static class CommandExtensions
{
  public static CommandResult Run
  (
    string executable,
    params string[] arguments
  )
  {
    // Input validation
    if (string.IsNullOrWhiteSpace(executable))
    {
      return new CommandResult(null);
    }
    
    try
    {
      Command cliCommand = CliWrap.Cli.Wrap(executable)
        .WithArguments(arguments)
        .WithValidation(CommandResultValidation.None);
        
      return new CommandResult(cliCommand);
    }
    catch
    {
      // If command creation fails, return a result that will return empty values
      return new CommandResult(null);
    }
  }
}