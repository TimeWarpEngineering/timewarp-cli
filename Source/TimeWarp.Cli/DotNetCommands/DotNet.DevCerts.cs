namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for .NET CLI commands - Dev-certs command implementation.
/// </summary>
public static partial class DotNet
{
  /// <summary>
  /// Creates a fluent builder for the 'dotnet dev-certs' command.
  /// </summary>
  /// <returns>A DotNetDevCertsBuilder for configuring the dotnet dev-certs command</returns>
  public static DotNetDevCertsBuilder DevCerts()
  {
    return new DotNetDevCertsBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'dotnet dev-certs' commands.
/// </summary>
public class DotNetDevCertsBuilder
{
  private CommandOptions _options = new();

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsBuilder WithWorkingDirectory(string directory)
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
  public DotNetDevCertsBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  /// <summary>
  /// Creates a fluent builder for the 'dotnet dev-certs https' command.
  /// </summary>
  /// <returns>A DotNetDevCertsHttpsBuilder for configuring the https command</returns>
  public DotNetDevCertsHttpsBuilder Https()
  {
    return new DotNetDevCertsHttpsBuilder(_options);
  }
}

/// <summary>
/// Fluent builder for 'dotnet dev-certs https' commands.
/// </summary>
public class DotNetDevCertsHttpsBuilder
{
  private readonly CommandOptions _options;
  private bool _check;
  private bool _clean;
  private bool _export;
  private bool _trust;
  private string? _exportPath;
  private string? _password;
  private string? _format;
  private bool _noPassword;
  private bool _verbose;
  private bool _quiet;

  public DotNetDevCertsHttpsBuilder(CommandOptions options)
  {
    _options = options;
  }

  /// <summary>
  /// Checks if a valid certificate exists.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithCheck()
  {
    _check = true;
    return this;
  }

  /// <summary>
  /// Cleans up existing certificates.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithClean()
  {
    _clean = true;
    return this;
  }

  /// <summary>
  /// Exports the certificate to a file.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithExport()
  {
    _export = true;
    return this;
  }

  /// <summary>
  /// Trusts the certificate on the local machine.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithTrust()
  {
    _trust = true;
    return this;
  }

  /// <summary>
  /// Specifies the path to export the certificate to.
  /// </summary>
  /// <param name="exportPath">The path to export the certificate to</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithExportPath(string exportPath)
  {
    _exportPath = exportPath;
    return this;
  }

  /// <summary>
  /// Specifies the password for the certificate.
  /// </summary>
  /// <param name="password">The password for the certificate</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithPassword(string password)
  {
    _password = password;
    return this;
  }

  /// <summary>
  /// Specifies the format of the certificate (Pfx, Pem).
  /// </summary>
  /// <param name="format">The certificate format</param>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithFormat(string format)
  {
    _format = format;
    return this;
  }

  /// <summary>
  /// Exports the certificate without a password.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithNoPassword()
  {
    _noPassword = true;
    return this;
  }

  /// <summary>
  /// Enables verbose logging.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithVerbose()
  {
    _verbose = true;
    return this;
  }

  /// <summary>
  /// Enables quiet mode (suppresses output).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public DotNetDevCertsHttpsBuilder WithQuiet()
  {
    _quiet = true;
    return this;
  }

  public CommandResult Build()
  {
    var arguments = new List<string> { "dev-certs", "https" };

    if (_check)
    {
      arguments.Add("--check");
    }

    if (_clean)
    {
      arguments.Add("--clean");
    }

    if (_export)
    {
      arguments.Add("--export");
    }

    if (_trust)
    {
      arguments.Add("--trust");
    }

    if (!string.IsNullOrWhiteSpace(_exportPath))
    {
      arguments.Add("--export-path");
      arguments.Add(_exportPath);
    }

    if (!string.IsNullOrWhiteSpace(_password))
    {
      arguments.Add("--password");
      arguments.Add(_password);
    }

    if (!string.IsNullOrWhiteSpace(_format))
    {
      arguments.Add("--format");
      arguments.Add(_format);
    }

    if (_noPassword)
    {
      arguments.Add("--no-password");
    }

    if (_verbose)
    {
      arguments.Add("--verbose");
    }

    if (_quiet)
    {
      arguments.Add("--quiet");
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