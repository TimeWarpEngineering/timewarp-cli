namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - NuGet command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget' command.
  /// </summary>
  /// <returns>A DotNetNuGetBuilder for configuring the dotnet nuget command</returns>
  public static DotNetNuGetBuilder NuGet()
  {
    return new DotNetNuGetBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet nuget' commands.
/// </summary>
public class DotNetNuGetBuilder
{
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetBuilder WithWorkingDirectory(string directory)
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
  public DotNetNuGetBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget push' command.
  /// </summary>
  /// <param name="packagePath">The path to the package to push</param>
  /// <returns>A DotNetNuGetPushBuilder for configuring the push command</returns>
  public DotNetNuGetPushBuilder Push(string packagePath)
  {
    return new DotNetNuGetPushBuilder(packagePath, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget delete' command.
  /// </summary>
  /// <param name="packageName">The name of the package to delete</param>
  /// <param name="version">The version of the package to delete</param>
  /// <returns>A DotNetNuGetDeleteBuilder for configuring the delete command</returns>
  public DotNetNuGetDeleteBuilder Delete(string packageName, string version)
  {
    return new DotNetNuGetDeleteBuilder(packageName, version, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget list source' command.
  /// </summary>
  /// <returns>A DotNetNuGetListSourceBuilder for configuring the list source command</returns>
  public DotNetNuGetListSourceBuilder ListSources()
  {
    return new DotNetNuGetListSourceBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget add source' command.
  /// </summary>
  /// <param name="source">The package source URL or path</param>
  /// <returns>A DotNetNuGetAddSourceBuilder for configuring the add source command</returns>
  public DotNetNuGetAddSourceBuilder AddSource(string source)
  {
    return new DotNetNuGetAddSourceBuilder(source, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget remove source' command.
  /// </summary>
  /// <param name="name">The name of the source to remove</param>
  /// <returns>A DotNetNuGetRemoveSourceBuilder for configuring the remove source command</returns>
  public DotNetNuGetRemoveSourceBuilder RemoveSource(string name)
  {
    return new DotNetNuGetRemoveSourceBuilder(name, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget update source' command.
  /// </summary>
  /// <param name="name">The name of the source to update</param>
  /// <returns>A DotNetNuGetUpdateSourceBuilder for configuring the update source command</returns>
  public DotNetNuGetUpdateSourceBuilder UpdateSource(string name)
  {
    return new DotNetNuGetUpdateSourceBuilder(name, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget enable source' command.
  /// </summary>
  /// <param name="name">The name of the source to enable</param>
  /// <returns>A DotNetNuGetEnableSourceBuilder for configuring the enable source command</returns>
  public DotNetNuGetEnableSourceBuilder EnableSource(string name)
  {
    return new DotNetNuGetEnableSourceBuilder(name, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget disable source' command.
  /// </summary>
  /// <param name="name">The name of the source to disable</param>
  /// <returns>A DotNetNuGetDisableSourceBuilder for configuring the disable source command</returns>
  public DotNetNuGetDisableSourceBuilder DisableSource(string name)
  {
    return new DotNetNuGetDisableSourceBuilder(name, _options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget locals' command.
  /// </summary>
  /// <returns>A DotNetNuGetLocalsBuilder for configuring the locals command</returns>
  public DotNetNuGetLocalsBuilder Locals()
  {
    return new DotNetNuGetLocalsBuilder(_options);
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet nuget why' command.
  /// </summary>
  /// <param name="packageName">The name of the package to analyze</param>
  /// <returns>A DotNetNuGetWhyBuilder for configuring the why command</returns>
  public DotNetNuGetWhyBuilder Why(string packageName)
  {
    return new DotNetNuGetWhyBuilder(packageName, _options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget push' commands.
/// </summary>
public class DotNetNuGetPushBuilder
{
  private readonly string _packagePath;
  private readonly CommandOptions _options;
  private string? _source;
  private string? _symbolSource;
  private int? _timeout;
  private string? _apiKey;
  private string? _symbolApiKey;
  private bool _disableBuffering;
  private bool _noSymbols;
  private bool _noServiceEndpoint;
  private bool _interactive;
  private bool _skipDuplicate;
  private string? _configFile;

  public DotNetNuGetPushBuilder(string packagePath, CommandOptions options)
  {
    _packagePath = packagePath ?? throw new ArgumentNullException(nameof(packagePath));
    _options = options;
  }

  /// <summary>
  /// Specifies the package source to use.
  /// </summary>
  /// <param name="source">Package source (URL, UNC/folder path or package source name)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithSource(string source)
  {
    _source = source;
    return this;
  }

  /// <summary>
  /// Specifies the symbol server URL to use.
  /// </summary>
  /// <param name="symbolSource">Symbol server URL</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithSymbolSource(string symbolSource)
  {
    _symbolSource = symbolSource;
    return this;
  }

  /// <summary>
  /// Specifies the timeout for pushing to a server in seconds.
  /// </summary>
  /// <param name="timeout">Timeout in seconds (default is 300)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithTimeout(int timeout)
  {
    _timeout = timeout;
    return this;
  }

  /// <summary>
  /// Specifies the API key for the server.
  /// </summary>
  /// <param name="apiKey">The API key</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithApiKey(string apiKey)
  {
    _apiKey = apiKey;
    return this;
  }

  /// <summary>
  /// Specifies the API key for the symbol server.
  /// </summary>
  /// <param name="symbolApiKey">The symbol API key</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithSymbolApiKey(string symbolApiKey)
  {
    _symbolApiKey = symbolApiKey;
    return this;
  }

  /// <summary>
  /// Disables buffering when pushing to an HTTP(S) server to decrease memory usage.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithDisableBuffering()
  {
    _disableBuffering = true;
    return this;
  }

  /// <summary>
  /// If a symbols package exists, it will not be pushed to a symbols server.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithNoSymbols()
  {
    _noSymbols = true;
    return this;
  }

  /// <summary>
  /// Does not append "api/v2/package" to the source URL.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithNoServiceEndpoint()
  {
    _noServiceEndpoint = true;
    return this;
  }

  /// <summary>
  /// Allows the command to block and require manual action for operations like authentication.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// If a package and version already exists, skip it and continue with the next package in the push.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithSkipDuplicate()
  {
    _skipDuplicate = true;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetPushBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "push", _packagePath };

    if (!string.IsNullOrWhiteSpace(_source))
    {
      arguments.Add("--source");
      arguments.Add(_source);
    }

    if (!string.IsNullOrWhiteSpace(_symbolSource))
    {
      arguments.Add("--symbol-source");
      arguments.Add(_symbolSource);
    }

    if (_timeout.HasValue)
    {
      arguments.Add("--timeout");
      arguments.Add(_timeout.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    if (!string.IsNullOrWhiteSpace(_apiKey))
    {
      arguments.Add("--api-key");
      arguments.Add(_apiKey);
    }

    if (!string.IsNullOrWhiteSpace(_symbolApiKey))
    {
      arguments.Add("--symbol-api-key");
      arguments.Add(_symbolApiKey);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_disableBuffering)
    {
      arguments.Add("--disable-buffering");
    }

    if (_noSymbols)
    {
      arguments.Add("--no-symbols");
    }

    if (_noServiceEndpoint)
    {
      arguments.Add("--no-service-endpoint");
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
    }

    if (_skipDuplicate)
    {
      arguments.Add("--skip-duplicate");
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget delete' commands.
/// </summary>
public class DotNetNuGetDeleteBuilder
{
  private readonly string _packageName;
  private readonly string _version;
  private readonly CommandOptions _options;
  private string? _source;
  private string? _apiKey;
  private bool _interactive;
  private string? _configFile;

  public DotNetNuGetDeleteBuilder(string packageName, string version, CommandOptions options)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
    _version = version ?? throw new ArgumentNullException(nameof(version));
    _options = options;
  }

  /// <summary>
  /// Specifies the package source to use.
  /// </summary>
  /// <param name="source">Package source (URL, UNC/folder path or package source name)</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetDeleteBuilder WithSource(string source)
  {
    _source = source;
    return this;
  }

  /// <summary>
  /// Specifies the API key for the server.
  /// </summary>
  /// <param name="apiKey">The API key</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetDeleteBuilder WithApiKey(string apiKey)
  {
    _apiKey = apiKey;
    return this;
  }

  /// <summary>
  /// Allows the command to block and require manual action for operations like authentication.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetDeleteBuilder WithInteractive()
  {
    _interactive = true;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetDeleteBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "delete", _packageName, _version };

    if (!string.IsNullOrWhiteSpace(_source))
    {
      arguments.Add("--source");
      arguments.Add(_source);
    }

    if (!string.IsNullOrWhiteSpace(_apiKey))
    {
      arguments.Add("--api-key");
      arguments.Add(_apiKey);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
    }

    if (_interactive)
    {
      arguments.Add("--interactive");
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget list source' commands.
/// </summary>
public class DotNetNuGetListSourceBuilder
{
  private readonly CommandOptions _options;
  private string? _format;
  private string? _configFile;

  public DotNetNuGetListSourceBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Specifies the format of the list command output.
  /// </summary>
  /// <param name="format">The format: "Detailed" (default) or "Short"</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetListSourceBuilder WithFormat(string format)
  {
    _format = format;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetListSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "list", "source" };

    if (!string.IsNullOrWhiteSpace(_format))
    {
      arguments.Add("--format");
      arguments.Add(_format);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget add source' commands.
/// </summary>
public class DotNetNuGetAddSourceBuilder
{
  private readonly string _source;
  private readonly CommandOptions _options;
  private string? _name;
  private string? _username;
  private string? _password;
  private string? _configFile;

  public DotNetNuGetAddSourceBuilder(string source, CommandOptions options)
  {
    _source = source ?? throw new ArgumentNullException(nameof(source));
    _options = options;
  }

  /// <summary>
  /// Specifies the name of the source.
  /// </summary>
  /// <param name="name">The name of the source</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetAddSourceBuilder WithName(string name)
  {
    _name = name;
    return this;
  }

  /// <summary>
  /// Specifies the username for the source.
  /// </summary>
  /// <param name="username">The username</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetAddSourceBuilder WithUsername(string username)
  {
    _username = username;
    return this;
  }

  /// <summary>
  /// Specifies the password for the source.
  /// </summary>
  /// <param name="password">The password</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetAddSourceBuilder WithPassword(string password)
  {
    _password = password;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetAddSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "add", "source", _source };

    if (!string.IsNullOrWhiteSpace(_name))
    {
      arguments.Add("--name");
      arguments.Add(_name);
    }

    if (!string.IsNullOrWhiteSpace(_username))
    {
      arguments.Add("--username");
      arguments.Add(_username);
    }

    if (!string.IsNullOrWhiteSpace(_password))
    {
      arguments.Add("--password");
      arguments.Add(_password);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget remove source' commands.
/// </summary>
public class DotNetNuGetRemoveSourceBuilder
{
  private readonly string _name;
  private readonly CommandOptions _options;
  private string? _configFile;

  public DotNetNuGetRemoveSourceBuilder(string name, CommandOptions options)
  {
    _name = name ?? throw new ArgumentNullException(nameof(name));
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetRemoveSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "remove", "source", _name };

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget update source' commands.
/// </summary>
public class DotNetNuGetUpdateSourceBuilder
{
  private readonly string _name;
  private readonly CommandOptions _options;
  private string? _source;
  private string? _username;
  private string? _password;
  private string? _configFile;

  public DotNetNuGetUpdateSourceBuilder(string name, CommandOptions options)
  {
    _name = name ?? throw new ArgumentNullException(nameof(name));
    _options = options;
  }

  /// <summary>
  /// Specifies the new source URL.
  /// </summary>
  /// <param name="source">The new source URL</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetUpdateSourceBuilder WithSource(string source)
  {
    _source = source;
    return this;
  }

  /// <summary>
  /// Specifies the username for the source.
  /// </summary>
  /// <param name="username">The username</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetUpdateSourceBuilder WithUsername(string username)
  {
    _username = username;
    return this;
  }

  /// <summary>
  /// Specifies the password for the source.
  /// </summary>
  /// <param name="password">The password</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetUpdateSourceBuilder WithPassword(string password)
  {
    _password = password;
    return this;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetUpdateSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "update", "source", _name };

    if (!string.IsNullOrWhiteSpace(_source))
    {
      arguments.Add("--source");
      arguments.Add(_source);
    }

    if (!string.IsNullOrWhiteSpace(_username))
    {
      arguments.Add("--username");
      arguments.Add(_username);
    }

    if (!string.IsNullOrWhiteSpace(_password))
    {
      arguments.Add("--password");
      arguments.Add(_password);
    }

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget enable source' commands.
/// </summary>
public class DotNetNuGetEnableSourceBuilder
{
  private readonly string _name;
  private readonly CommandOptions _options;
  private string? _configFile;

  public DotNetNuGetEnableSourceBuilder(string name, CommandOptions options)
  {
    _name = name ?? throw new ArgumentNullException(nameof(name));
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetEnableSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "enable", "source", _name };

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget disable source' commands.
/// </summary>
public class DotNetNuGetDisableSourceBuilder
{
  private readonly string _name;
  private readonly CommandOptions _options;
  private string? _configFile;

  public DotNetNuGetDisableSourceBuilder(string name, CommandOptions options)
  {
    _name = name ?? throw new ArgumentNullException(nameof(name));
    _options = options;
  }

  /// <summary>
  /// Specifies the NuGet configuration file to use.
  /// </summary>
  /// <param name="configFile">The NuGet configuration file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetDisableSourceBuilder WithConfigFile(string configFile)
  {
    _configFile = configFile;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "disable", "source", _name };

    if (!string.IsNullOrWhiteSpace(_configFile))
    {
      arguments.Add("--configfile");
      arguments.Add(_configFile);
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget locals' commands.
/// </summary>
public class DotNetNuGetLocalsBuilder
{
  private readonly CommandOptions _options;
  private string? _clear;
  private string? _list;

  public DotNetNuGetLocalsBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Clears the specified local cache.
  /// </summary>
  /// <param name="cache">The cache to clear (e.g., "all", "http-cache", "global-packages")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetLocalsBuilder Clear(string cache)
  {
    _clear = cache;
    return this;
  }

  /// <summary>
  /// Lists the specified local cache.
  /// </summary>
  /// <param name="cache">The cache to list (e.g., "all", "http-cache", "global-packages")</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetLocalsBuilder List(string cache)
  {
    _list = cache;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "locals" };

    if (!string.IsNullOrWhiteSpace(_clear))
    {
      arguments.Add(_clear);
      arguments.Add("--clear");
    }
    else if (!string.IsNullOrWhiteSpace(_list))
    {
      arguments.Add(_list);
      arguments.Add("--list");
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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}

/// <summary>
/// Fluent builder for 'dotnet nuget why' commands.
/// </summary>
public class DotNetNuGetWhyBuilder
{
  private readonly string _packageName;
  private readonly CommandOptions _options;
  private string? _project;
  private string? _framework;

  public DotNetNuGetWhyBuilder(string packageName, CommandOptions options)
  {
    _packageName = packageName ?? throw new ArgumentNullException(nameof(packageName));
    _options = options;
  }

  /// <summary>
  /// Specifies the project file to analyze.
  /// </summary>
  /// <param name="project">The project file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetWhyBuilder WithProject(string project)
  {
    _project = project;
    return this;
  }

  /// <summary>
  /// Specifies the target framework to analyze.
  /// </summary>
  /// <param name="framework">The target framework moniker</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetNuGetWhyBuilder WithFramework(string framework)
  {
    _framework = framework;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "nuget", "why" };

    if (!string.IsNullOrWhiteSpace(_project))
    {
      arguments.Add("--project");
      arguments.Add(_project);
    }

    if (!string.IsNullOrWhiteSpace(_framework))
    {
      arguments.Add("--framework");
      arguments.Add(_framework);
    }

    arguments.Add(_packageName);

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

  public async Task ExecuteAsync(CancellationToken cancellationToken = default)
  {
    await Build().ExecuteAsync(cancellationToken);
  }
}