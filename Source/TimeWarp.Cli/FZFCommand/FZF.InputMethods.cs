namespace TimeWarp.Cli;

public partial class FZFBuilder
{
  // Input Methods
  /// <summary>
  /// Specifies input items as a string array.
  /// </summary>
  /// <param name="items">Items to select from</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder FromInput(params string[] items)
  {
    _inputItems.AddRange(items);
    return this;
  }

  /// <summary>
  /// Specifies input items as a collection.
  /// </summary>
  /// <param name="items">Items to select from</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder FromInput(IEnumerable<string> items)
  {
    _inputItems.AddRange(items);
    return this;
  }

  /// <summary>
  /// Specifies input from file glob patterns.
  /// </summary>
  /// <param name="pattern">File glob pattern</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder FromFiles(string pattern)
  {
    _inputGlob = pattern;
    return this;
  }

  /// <summary>
  /// Specifies input from a command.
  /// </summary>
  /// <param name="command">Command to execute for input</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder FromCommand(string command)
  {
    _inputCommand = command;
    return this;
  }

  /// <summary>
  /// Uses standard input as the input source.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder FromStdin()
  {
    _useStdin = true;
    return this;
  }
}