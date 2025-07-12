namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for ghq (GitHub repository manager) integration.
/// </summary>
public static class Ghq
{
  /// <summary>
  /// Creates a fluent builder for the 'ghq' command.
  /// </summary>
  /// <returns>A GhqBuilder for configuring the ghq command</returns>
  public static GhqBuilder Run()
  {
    return new GhqBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'ghq' commands.
/// </summary>
public class GhqBuilder
{
  private CommandOptions _options = new();
  private List<string> _arguments = new();
  private string? _subCommand;
  private string? _repository;
  private List<string> _subCommandArguments = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithWorkingDirectory(string directory)
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
  public GhqBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  // Get/Clone Command
  /// <summary>
  /// Clone/sync with a remote repository.
  /// </summary>
  /// <param name="repository">Repository to clone (e.g., 'github.com/user/repo')</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Get(string repository)
  {
    _subCommand = "get";
    _repository = repository;
    return this;
  }

  /// <summary>
  /// Clone/sync with a remote repository (alias for Get).
  /// </summary>
  /// <param name="repository">Repository to clone</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Clone(string repository)
  {
    return Get(repository);
  }

  /// <summary>
  /// Look for existing clones and fetch them.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithLook()
  {
    _subCommandArguments.Add("--look");
    return this;
  }

  /// <summary>
  /// Update existing clones.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithUpdate()
  {
    _subCommandArguments.Add("--update");
    return this;
  }

  /// <summary>
  /// Clone with shallow history.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithShallow()
  {
    _subCommandArguments.Add("--shallow");
    return this;
  }

  /// <summary>
  /// Clone only the specified branch.
  /// </summary>
  /// <param name="branch">Branch name to clone</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithBranch(string branch)
  {
    _subCommandArguments.Add("--branch");
    _subCommandArguments.Add(branch);
    return this;
  }

  /// <summary>
  /// Clone as a bare repository.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithBare()
  {
    _subCommandArguments.Add("--bare");
    return this;
  }

  /// <summary>
  /// Skip recursive fetching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithNoRecursive()
  {
    _subCommandArguments.Add("--no-recursive");
    return this;
  }

  /// <summary>
  /// Silence git output.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithSilent()
  {
    _subCommandArguments.Add("--silent");
    return this;
  }

  /// <summary>
  /// Enable parallel fetching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithParallel()
  {
    _subCommandArguments.Add("--parallel");
    return this;
  }

  /// <summary>
  /// Use specified VCS backend (git, github, gitlab, etc.).
  /// </summary>
  /// <param name="vcs">VCS backend name</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithVcs(string vcs)
  {
    _subCommandArguments.Add("--vcs");
    _subCommandArguments.Add(vcs);
    return this;
  }

  // List Command
  /// <summary>
  /// List local repositories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder List()
  {
    _subCommand = "list";
    return this;
  }

  /// <summary>
  /// Show exact matches only.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithExact()
  {
    _subCommandArguments.Add("--exact");
    return this;
  }

  /// <summary>
  /// Filter by VCS backend.
  /// </summary>
  /// <param name="vcs">VCS backend to filter by</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder FilterByVcs(string vcs)
  {
    _subCommandArguments.Add("--vcs");
    _subCommandArguments.Add(vcs);
    return this;
  }

  /// <summary>
  /// Print full paths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithFullPath()
  {
    _subCommandArguments.Add("--full-path");
    return this;
  }

  /// <summary>
  /// Print unique subpaths.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithUnique()
  {
    _subCommandArguments.Add("--unique");
    return this;
  }

  // Remove Command
  /// <summary>
  /// Remove local repository.
  /// </summary>
  /// <param name="repository">Repository to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Remove(string repository)
  {
    _subCommand = "rm";
    _repository = repository;
    return this;
  }

  /// <summary>
  /// Remove local repository (alias for Remove).
  /// </summary>
  /// <param name="repository">Repository to remove</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Rm(string repository)
  {
    return Remove(repository);
  }

  /// <summary>
  /// Dry run mode for remove.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithDryRun()
  {
    _subCommandArguments.Add("--dry-run");
    return this;
  }

  // Root Command
  /// <summary>
  /// Show repositories' root.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Root()
  {
    _subCommand = "root";
    return this;
  }

  /// <summary>
  /// Show all root directories.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder WithAll()
  {
    _subCommandArguments.Add("--all");
    return this;
  }

  // Create Command
  /// <summary>
  /// Create a new repository.
  /// </summary>
  /// <param name="repository">Repository to create</param>
  /// <returns>The builder instance for method chaining</returns>
  public GhqBuilder Create(string repository)
  {
    _subCommand = "create";
    _repository = repository;
    return this;
  }

  // Build methods
  public CommandResult Build()
  {
    var arguments = new List<string> { "ghq" };
    
    // Add subcommand
    if (!string.IsNullOrEmpty(_subCommand))
    {
      arguments.Add(_subCommand);
    }
    
    // Add subcommand arguments
    arguments.AddRange(_subCommandArguments);
    
    // Add repository if specified
    if (!string.IsNullOrEmpty(_repository))
    {
      arguments.Add(_repository);
    }
    
    // Add global arguments
    arguments.AddRange(_arguments);

    return CommandExtensions.Run("ghq", arguments.Skip(1).ToArray(), _options);
  }

  public async Task<string> GetStringAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetStringAsync(cancellationToken);
  }

  public async Task<string[]> GetLinesAsync(CancellationToken cancellationToken = default)
  {
    return await Build().GetLinesAsync(cancellationToken);
  }

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Extension methods for integrating ghq with CommandResult.
/// </summary>
public static class GhqExtensions
{
  /// <summary>
  /// Lists repositories and pipes to another command.
  /// </summary>
  /// <param name="builder">The ghq builder</param>
  /// <param name="command">Command to pipe to</param>
  /// <param name="arguments">Arguments for the piped command</param>
  /// <returns>A CommandResult for the piped command</returns>
  public static CommandResult PipeTo(this GhqBuilder builder, string command, params string[] arguments)
  {
    return builder.Build().Pipe(command, arguments);
  }

  /// <summary>
  /// Lists repositories and selects one with FZF.
  /// </summary>
  /// <param name="builder">The ghq builder configured for list</param>
  /// <param name="configureFzf">Optional FZF configuration</param>
  /// <returns>A CommandResult with the selected repository</returns>
  public static CommandResult SelectWithFzf(this GhqBuilder builder, Action<FZFBuilder>? configureFzf = null)
  {
    FZFBuilder fzfBuilder = FZF.Run();
    configureFzf?.Invoke(fzfBuilder);
    
    return builder.Build().Pipe("fzf", ExtractFzfArguments(fzfBuilder));
  }

  private static string[] ExtractFzfArguments(FZFBuilder fzfBuilder)
  {
    // This would need to be implemented to extract arguments from FZFBuilder
    // For now, return empty array
    return Array.Empty<string>();
  }
}