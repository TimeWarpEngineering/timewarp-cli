namespace TimeWarp.Cli;

public static class CommandExtensions
{
  private const string CSharpScriptExtension = ".cs";
  
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
    CommandOptions commandOptions
  )
  {
    // Input validation
    if (string.IsNullOrWhiteSpace(executable))
    {
      return CommandResult.NullCommandResult;
    }
    
    if (commandOptions == null)
    {
      return CommandResult.NullCommandResult;
    }
    
    // Check for configured command path override
    executable = CliConfiguration.GetCommandPath(executable);
    
    // Handle .cs script files specially
    if (executable.EndsWith(CSharpScriptExtension, StringComparison.OrdinalIgnoreCase))
    {
      // Insert -- at the beginning of arguments to prevent dotnet from intercepting them
      List<string> newArgs = ["--", .. arguments];
      arguments = [.. newArgs];
    }
    
    Command cliCommand = CliWrap.Cli.Wrap(executable)
      .WithArguments(arguments);
    
    // Apply configuration options
    cliCommand = commandOptions.ApplyTo(cliCommand);
      
    return new CommandResult(cliCommand);
  }
}