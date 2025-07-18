namespace TimeWarp.Cli;

/// <summary>
/// Fluent builder for configuring generic command execution.
/// </summary>
public class RunBuilder : ICommandBuilder<RunBuilder>
{
  private readonly string Executable;
  private readonly List<string> Arguments = new();
  private CommandOptions Options = new();
  private string? StandardInput;

  /// <summary>
  /// Initializes a new instance of the RunBuilder class.
  /// </summary>
  /// <param name="executable">The executable or command to run</param>
  public RunBuilder(string executable)
  {
    Executable = executable ?? throw new ArgumentNullException(nameof(executable));
  }

  /// <summary>
  /// Adds arguments to the command.
  /// </summary>
  /// <param name="arguments">Arguments to add to the command</param>
  /// <returns>The builder instance for method chaining</returns>
  public RunBuilder WithArguments(params string[] arguments)
  {
    if (arguments != null)
    {
      Arguments.AddRange(arguments);
    }
    
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public RunBuilder WithWorkingDirectory(string directory)
  {
    Options = Options.WithWorkingDirectory(directory);
    return this;
  }

  /// <summary>
  /// Adds an environment variable for the command execution.
  /// </summary>
  /// <param name="key">The environment variable name</param>
  /// <param name="value">The environment variable value</param>
  /// <returns>The builder instance for method chaining</returns>
  public RunBuilder WithEnvironmentVariable(string key, string? value)
  {
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public RunBuilder WithNoValidation()
  {
    Options = Options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Provides standard input to the command.
  /// </summary>
  /// <param name="input">The text to provide as standard input</param>
  /// <returns>The builder instance for method chaining</returns>
  public RunBuilder WithStandardInput(string input)
  {
    StandardInput = input ?? throw new ArgumentNullException(nameof(input));
    return this;
  }

  /// <summary>
  /// Builds the command and returns a CommandResult.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    return CommandExtensions.Run(Executable, Arguments.ToArray(), Options, StandardInput);
  }

  /// <summary>
  /// Executes the command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
  
  /// <summary>
  /// Executes the command interactively with stdin, stdout, and stderr connected to the console.
  /// This allows interactive commands like fzf, vim, or interactive prompts to work properly.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The execution result (output strings will be empty since output goes to console)</returns>
  public async Task<ExecutionResult> ExecuteInteractiveAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteInteractiveAsync(cancellationToken);
  }
  
  /// <summary>
  /// Executes the command interactively and captures the output.
  /// The UI is rendered to the console (via stderr) while stdout is captured and returned.
  /// This is ideal for interactive selection tools like fzf.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The captured output string from the interactive command</returns>
  public async Task<string> GetStringInteractiveAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringInteractiveAsync(cancellationToken);
  }

  /// <summary>
  /// Creates a pipeline by chaining this command with another command.
  /// </summary>
  /// <param name="executable">The next command in the pipeline</param>
  /// <param name="arguments">Arguments for the next command</param>
  /// <returns>A CommandResult representing the pipeline</returns>
  public CommandResult Pipe(string executable, params string[] arguments)
  {
    return Build().Pipe(executable, arguments);
  }
}