namespace TimeWarp.Cli;

/// <summary>
/// Configuration options for command execution, providing control over working directory,
/// environment variables, and other process settings.
/// </summary>
public class CommandOptions
{
  /// <summary>
  /// Gets or sets the working directory for the command execution.
  /// If not specified, uses the current working directory.
  /// </summary>
  public string? WorkingDirectory { get; set; }
  
  /// <summary>
  /// Gets or sets additional environment variables for the command execution.
  /// These are added to the inherited environment variables from the parent process.
  /// </summary>
  public Dictionary<string, string?>? EnvironmentVariables { get; set; }
  
  /// <summary>
  /// Creates a new instance of CommandOptions with default settings.
  /// </summary>
  public CommandOptions()
  {
    // Default constructor with no configuration
  }
  
  /// <summary>
  /// Sets the working directory for command execution.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>A new CommandOptions instance with the working directory set</returns>
  public CommandOptions WithWorkingDirectory(string directory)
  {
    return new CommandOptions
    {
      WorkingDirectory = directory,
      EnvironmentVariables = EnvironmentVariables
    };
  }
  
  /// <summary>
  /// Adds a single environment variable for command execution.
  /// </summary>
  /// <param name="key">The environment variable name</param>
  /// <param name="value">The environment variable value</param>
  /// <returns>A new CommandOptions instance with the environment variable added</returns>
  public CommandOptions WithEnvironmentVariable(string key, string? value)
  {
    var newOptions = new CommandOptions
    {
      WorkingDirectory = WorkingDirectory,
      EnvironmentVariables = EnvironmentVariables != null 
        ? new Dictionary<string, string?>(EnvironmentVariables)
        : new Dictionary<string, string?>()
    };
    
    newOptions.EnvironmentVariables[key] = value;
    return newOptions;
  }
  
  /// <summary>
  /// Sets multiple environment variables for command execution.
  /// </summary>
  /// <param name="variables">Dictionary of environment variables to set</param>
  /// <returns>A new CommandOptions instance with the environment variables set</returns>
  public CommandOptions WithEnvironmentVariables(Dictionary<string, string?> variables)
  {
    return new CommandOptions
    {
      WorkingDirectory = WorkingDirectory,
      EnvironmentVariables = new Dictionary<string, string?>(variables)
    };
  }
  
  /// <summary>
  /// Applies the configuration options to a CliWrap Command.
  /// </summary>
  /// <param name="command">The CliWrap Command to configure</param>
  /// <returns>The configured CliWrap Command</returns>
  internal Command ApplyTo(Command command)
  {
    var configuredCommand = command;
    
    // Apply working directory if specified
    if (!string.IsNullOrWhiteSpace(WorkingDirectory))
    {
      configuredCommand = configuredCommand.WithWorkingDirectory(WorkingDirectory);
    }
    
    // Apply environment variables if specified
    if (EnvironmentVariables != null && EnvironmentVariables.Count > 0)
    {
      configuredCommand = configuredCommand.WithEnvironmentVariables(EnvironmentVariables);
    }
    
    return configuredCommand;
  }
}