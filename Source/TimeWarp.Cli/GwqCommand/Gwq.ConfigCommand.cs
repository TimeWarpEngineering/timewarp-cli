namespace TimeWarp.Cli;

public partial class GwqBuilder
{
  // Config Command
  /// <summary>
  /// Configuration management.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Config()
  {
    _subCommand = "config";
    return this;
  }

  /// <summary>
  /// List all configuration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigList()
  {
    _subCommand = "config";
    _subCommandArguments.Add("list");
    return this;
  }

  /// <summary>
  /// Get configuration value.
  /// </summary>
  /// <param name="key">Configuration key</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigGet(string key)
  {
    _subCommand = "config";
    _subCommandArguments.Add("get");
    _subCommandArguments.Add(key);
    return this;
  }

  /// <summary>
  /// Set configuration value.
  /// </summary>
  /// <param name="key">Configuration key</param>
  /// <param name="value">Configuration value</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigSet(string key, string value)
  {
    _subCommand = "config";
    _subCommandArguments.Add("set");
    _subCommandArguments.Add(key);
    _subCommandArguments.Add(value);
    return this;
  }
}