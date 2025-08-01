namespace TimeWarp.Amuru;

public partial class GwqBuilder
{
  // Config Command
  /// <summary>
  /// Configuration management.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder Config()
  {
    SubCommand = "config";
    return this;
  }

  /// <summary>
  /// List all configuration.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigList()
  {
    SubCommand = "config";
    SubCommandArguments.Add("list");
    return this;
  }

  /// <summary>
  /// Get configuration value.
  /// </summary>
  /// <param name="key">Configuration key</param>
  /// <returns>The builder instance for method chaining</returns>
  public GwqBuilder ConfigGet(string key)
  {
    SubCommand = "config";
    SubCommandArguments.Add("get");
    SubCommandArguments.Add(key);
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
    SubCommand = "config";
    SubCommandArguments.Add("set");
    SubCommandArguments.Add(key);
    SubCommandArguments.Add(value);
    return this;
  }
}