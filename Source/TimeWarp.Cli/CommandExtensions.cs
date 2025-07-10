namespace TimeWarp.Cli;

public static class CommandExtensions
{
  public static CommandResult Run
  (
    string command,
    params string[] args
  )
  {
    try
    {
      Command cliCommand = CliWrap.Cli.Wrap(command)
        .WithArguments(args)
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