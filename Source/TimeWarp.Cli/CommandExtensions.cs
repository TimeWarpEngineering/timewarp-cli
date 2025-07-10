namespace TimeWarp.Cli;

public static class CommandExtensions
{
  public static CommandResult Run
  (
    string executable,
    params string[] arguments
  )
  {
    return Run(executable, arguments, new CommandOptions());
  }
  
  public static CommandResult Run
  (
    string executable,
    string[] arguments,
    CommandOptions options
  )
  {
    // Input validation
    if (string.IsNullOrWhiteSpace(executable))
    {
      return CommandResult.NullCommandResult;
    }
    
    if (options == null)
    {
      return CommandResult.NullCommandResult;
    }
    
    try
    {
      Command cliCommand = CliWrap.Cli.Wrap(executable)
        .WithArguments(arguments)
        .WithValidation(CommandResultValidation.None);
      
      // Apply configuration options
      cliCommand = options.ApplyTo(cliCommand);
        
      return new CommandResult(cliCommand);
    }
    catch
    {
      // If command creation fails, return a result that will return empty values
      return CommandResult.NullCommandResult;
    }
  }
}