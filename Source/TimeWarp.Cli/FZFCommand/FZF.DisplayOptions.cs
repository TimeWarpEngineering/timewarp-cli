namespace TimeWarp.Cli;

public partial class FZFBuilder
{
  // Display Options
  /// <summary>
  /// Enables processing of ANSI color codes.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithAnsi()
  {
    _arguments.Add("--ansi");
    return this;
  }

  /// <summary>
  /// Specifies the number of spaces for a tab character.
  /// </summary>
  /// <param name="spaces">Number of spaces per tab</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithTabstop(int spaces)
  {
    _arguments.Add($"--tabstop={spaces}");
    return this;
  }

  /// <summary>
  /// Specifies the color scheme.
  /// </summary>
  /// <param name="colors">Color specification</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithColor(string colors)
  {
    _arguments.Add($"--color={colors}");
    return this;
  }

  /// <summary>
  /// Disables bold text.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoBold()
  {
    _arguments.Add("--no-bold");
    return this;
  }
}