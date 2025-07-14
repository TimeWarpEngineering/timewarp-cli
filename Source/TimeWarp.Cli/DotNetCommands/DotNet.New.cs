namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - New command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet new' command to create a new project or item.
  /// </summary>
  /// <param name="templateName">The short name of the template to create (e.g., "console", "web", "classlib")</param>
  /// <returns>A DotNetNewBuilder for configuring the dotnet new command</returns>
  public static DotNetNewBuilder New(string templateName)
  {
    return new DotNetNewBuilder(templateName);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new' command without specifying a template.
  /// Use this with subcommands like List(), Search(), etc.
  /// </summary>
  /// <returns>A DotNetNewBuilder for configuring the dotnet new command</returns>
  public static DotNetNewBuilder New()
  {
    return new DotNetNewBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet new' commands.
/// </summary>
public class DotNetNewBuilder : ICommandBuilder<DotNetNewBuilder>
{
  private readonly string? _templateName;
  private string? _output;
  private string? _name;
  private string? _project;
  private string? _verbosity;
  private bool _dryRun;
  private bool _force;
  private bool _noUpdateCheck;
  private bool _diagnostics;
  private List<string> _templateArgs = new();
  private CommandOptions _options = new();

  /// <summary>
  /// Initializes a new instance of the DotNetNewBuilder class.
  /// </summary>
  /// <param name="templateName">The template name (optional)</param>
  public DotNetNewBuilder(string? templateName = null)
  {
    _templateName = templateName;
  }

  /// <summary>
  /// Specifies the location to place the generated output.
  /// </summary>
  /// <param name="output">The output directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithOutput(string output)
  {
    _output = output;
    return this;
  }

  /// <summary>
  /// Specifies the name for the output being created.
  /// </summary>
  /// <param name="name">The name for the output</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithName(string name)
  {
    _name = name;
    return this;
  }

  /// <summary>
  /// Specifies the project that should be used for context evaluation.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Sets the verbosity level of the command.
  /// </summary>
  /// <param name="verbosity">The verbosity level (quiet, minimal, normal, diagnostic)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithVerbosity(string verbosity)
  {
    _verbosity = verbosity;
    return this;
  }

  /// <summary>
  /// Displays a summary of what would happen without actually creating the template.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithDryRun()
  {
    _dryRun = true;
    return this;
  }

  /// <summary>
  /// Forces content to be generated even if it would change existing files.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithForce()
  {
    _force = true;
    return this;
  }

  /// <summary>
  /// Disables checking for template package updates when instantiating a template.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithNoUpdateCheck()
  {
    _noUpdateCheck = true;
    return this;
  }

  /// <summary>
  /// Enables diagnostic output.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithDiagnostics()
  {
    _diagnostics = true;
    return this;
  }

  /// <summary>
  /// Adds a template-specific argument.
  /// </summary>
  /// <param name="templateArg">The template-specific argument</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithTemplateArg(string templateArg)
  {
    _templateArgs.Add(templateArg);
    return this;
  }

  /// <summary>
  /// Adds multiple template-specific arguments.
  /// </summary>
  /// <param name="templateArgs">The template-specific arguments</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithTemplateArgs(params string[] templateArgs)
  {
    _templateArgs.AddRange(templateArgs);
    return this;
  }

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithWorkingDirectory(string directory)
  {
    _options = _options.WithWorkingDirectory(directory);
    return this;
  }

  /// <summary>
  /// Adds an environment variable for the command execution.
  /// </summary>
  /// <param name="key">The environment variable name</param>
  /// <param name="value">The environment variable value</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Disables command validation, allowing the command to complete without throwing exceptions on non-zero exit codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNewBuilder WithNoValidation()
  {
    _options = _options.WithNoValidation();
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new list' command.
  /// </summary>
  /// <param name="templateName">Optional template name to filter by</param>
  /// <returns>A DotNetNewListBuilder for configuring the list command</returns>
  public DotNetNewListBuilder List(string? templateName = null)
  {
    return new DotNetNewListBuilder(templateName, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new search' command.
  /// </summary>
  /// <param name="templateName">The template name to search for</param>
  /// <returns>A DotNetNewSearchBuilder for configuring the search command</returns>
  public DotNetNewSearchBuilder Search(string templateName)
  {
    return new DotNetNewSearchBuilder(templateName, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new install' command.
  /// </summary>
  /// <param name="packageName">The template package to install</param>
  /// <returns>A DotNetNewInstallBuilder for configuring the install command</returns>
  public DotNetNewInstallBuilder Install(string packageName)
  {
    return new DotNetNewInstallBuilder(packageName, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new uninstall' command.
  /// </summary>
  /// <param name="packageName">The template package to uninstall</param>
  /// <returns>A DotNetNewUninstallBuilder for configuring the uninstall command</returns>
  public DotNetNewUninstallBuilder Uninstall(string packageName)
  {
    return new DotNetNewUninstallBuilder(packageName, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet new update' command.
  /// </summary>
  /// <returns>A DotNetNewUpdateBuilder for configuring the update command</returns>
  public DotNetNewUpdateBuilder Update()
  {
    return new DotNetNewUpdateBuilder(_options);
  }

  /// <summary>
  /// Builds the command arguments and executes the dotnet new command.
  /// </summary>
  /// <returns>A CommandResult for further processing</returns>
  public CommandResult Build()
  {
    var arguments = new List<string> { "new" };

    // Add template name if specified
    if (!string.IsNullOrWhiteSpace(_templateName))
    {
      arguments.Add(_templateName);
    }

    // Add template args
    arguments.AddRange(_templateArgs);

    // Add options
    if (!string.IsNullOrWhiteSpace(_output))
    {
      arguments.Add("--output");
      arguments.Add(_output);
    }

    if (!string.IsNullOrWhiteSpace(_name))
    {
      arguments.Add("--name");
      arguments.Add(_name);
    }

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_verbosity))
    {
      arguments.Add("--verbosity");
      arguments.Add(_verbosity);
    }

    // Add boolean flags
    if (_dryRun)
    {
      arguments.Add("--dry-run");
    }

    if (_force)
    {
      arguments.Add("--force");
    }

    if (_noUpdateCheck)
    {
      arguments.Add("--no-update-check");
    }

    if (_diagnostics)
    {
      arguments.Add("--diagnostics");
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  /// <summary>
  /// Executes the dotnet new command and returns the output as a string.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as a string</returns>
  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet new command and returns the output as an array of lines.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>The command output as an array of lines</returns>
  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  /// <summary>
  /// Executes the dotnet new command without capturing output.
  /// </summary>
  /// <param name="cancellationToken">Cancellation token for the operation</param>
  /// <returns>A task representing the command execution</returns>
  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet new list' commands.
/// </summary>
public class DotNetNewListBuilder
{
  private readonly string? _templateName;
  private readonly CommandOptions _options;

  public DotNetNewListBuilder(string? templateName, CommandOptions options)
  {
    _templateName = templateName;
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "new", "list" };

    if (!string.IsNullOrWhiteSpace(_templateName))
    {
      arguments.Add(_templateName);
    }

    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet new search' commands.
/// </summary>
public class DotNetNewSearchBuilder
{
  private readonly string _templateName;
  private readonly CommandOptions _options;

  public DotNetNewSearchBuilder(string templateName, CommandOptions options)
  {
    _templateName = templateName ?? throw new ArgumentNullException(nameof(templateName));
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "new", "search", _templateName };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet new install' commands.
/// </summary>
public class DotNetNewInstallBuilder
{
  private readonly string _packageName;
  private readonly CommandOptions _options;

  public DotNetNewInstallBuilder(string packageName, CommandOptions options)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "new", "install", _packageName };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet new uninstall' commands.
/// </summary>
public class DotNetNewUninstallBuilder
{
  private readonly string _packageName;
  private readonly CommandOptions _options;

  public DotNetNewUninstallBuilder(string packageName, CommandOptions options)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "new", "uninstall", _packageName };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet new update' commands.
/// </summary>
public class DotNetNewUpdateBuilder
{
  private readonly CommandOptions _options;

  public DotNetNewUpdateBuilder(CommandOptions options)
  {
    _options = options;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "new", "update" };
    return CommandExtensions.Run("dotnet", arguments.ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task<ExecutionResult> ExecuteAsync(CancellationToken cancellationToken = default)
  {
    return await Build().ExecuteAsync(cancellationToken);
  }
}