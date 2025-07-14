namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Clean command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet clean' command.
  /// </summary>
  /// <returns>A DotNetCleanBuilder for configuring the dotnet clean command</returns>
  public static DotNetCleanBuilder Clean()
  {
    return new DotNetCleanBuilder();
  }
  
  /// <summary>
  /// Creates a fluent builder for the 'dotnet clean' command with a specific project.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>A DotNetCleanBuilder for configuring the dotnet clean command</returns>
  public static DotNetCleanBuilder Clean(string project)
  {
    return new DotNetCleanBuilder().WithProject(project);
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet clean' commands.
/// </summary>
public class DotNetCleanBuilder : ICommandBuilder<DotNetCleanBuilder>
{
  private string? Project;
  private string? Configuration;
  private string? Framework;
  private string? Runtime;
  private string? OutputPath;
  private string? Verbosity;
  private bool NoLogo;
  private Dictionary<string, string> Properties = new();
  private CommandOptions Options = new();

  /// <summary>
  /// Specifies the project file to clean. If not specified, searches the current directory for one.
  /// </summary>
  /// <param name="project">Path to the project file (.csproj, .fsproj, .vbproj) or directory containing one</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProject(string project)
  {
    Project = project;
    return this;
  }

  /// <summary>
  /// Specifies the configuration to clean (Debug or Release).
  /// </summary>
  /// <param name="configuration">The configuration name (e.g., "Debug", "Release")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithConfiguration(string configuration)
  {
    Configuration = configuration;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to clean for.
  /// </summary>
  /// <param name="framework">The target framework moniker (e.g., "net8.0", "net10.0")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithFramework(string framework)
  {
    Framework = framework;
    return this;
  }

  /// <summary>
  /// Specifies the target runtime to clean for.
  /// </summary>
  /// <param name="runtime">The runtime identifier (e.g., "win-x64", "linux-x64")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithRuntime(string runtime)
  {
    Runtime = runtime;
    return this;
  }

  /// <summary>
  /// Specifies the output directory to clean.
  /// </summary>
  /// <param name="outputPath">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithOutput(string outputPath)
  {
    OutputPath = outputPath;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, detailed, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithVerbosity(string verbosity)
  {
    Verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Doesn't display the startup banner or the copyright message.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithNoLogo()
  {
    NoLogo = true;
    return this;
  }

  /// <summary>
  /// Sets an MSBuild property.
  /// </summary>
  /// <param name="name">The property name</param>
  /// <param name="value">The property value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProperty(string name, string value)
  {
    Properties[name] = value;
    return this;
  }

  /// <summary>
  /// Sets multiple MSBuild properties.
  /// </summary>
  /// <param name="properties">Dictionary of property names and values</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithProperties(Dictionary<string, string> properties)
  {
    foreach (KeyValuePair<string, string> kvp in properties)
    {
      Properties[kvp.Key] = kvp.Value;
    }
    
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithWorkingDirectory(string directory)
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
  public DotNetCleanBuilder WithEnvironmentVariable(string key, string? value)
  {
    Options = Options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetCleanBuilder WithNoValidation()
  {
    Options = Options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet clean command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    List<string> arguments = new() { "clean" };

    // Add project if specified
    if (!string.IsNullOrWhiteSpace(Project))
    {
      arguments.Add(Project);
    }

    // Add configuration if specified
    if (!string.IsNullOrWhiteSpace(Configuration))
    {
      arguments.Add("--configuration");
      arguments.Add(Configuration);
    }

    // Add framework if specified
    if (!string.IsNullOrWhiteSpace(Framework))
    {
      arguments.Add("--framework");
      arguments.Add(Framework);
    }

    // Add runtime if specified
    if (!string.IsNullOrWhiteSpace(Runtime))
    {
      arguments.Add("--runtime");
      arguments.Add(Runtime);
    }

    // Add output path if specified
    if (!string.IsNullOrWhiteSpace(OutputPath))
    {
      arguments.Add("--output");
      arguments.Add(OutputPath);
    }

    // Add verbosity if specified
    if (!string.IsNullOrWhiteSpace(Verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(Verbosity);
    }

    // Add boolean flags
    if (NoLogo)
    {
      arguments.Add("--nologo");
    }

    // Add MSBuild properties
    foreach (KeyValuePair<string, string> property in Properties)
    {
      arguments.Add($"--property:{property.Key}={property.Value}");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), Options);
  }

  /// <summary>
  /// Executes the dotnet clean command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet clean command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet clean command and returns the execution result.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>ExecutionResult containing command output and execution details</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}