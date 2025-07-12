namespace TimeWarp.Cli;

public partial class FZFBuilder
{
  // History Options
  /// <summary>
  /// Specifies the history file.
  /// </summary>
  /// <param name="file">History file path</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHistory(string file)
  {
    _arguments.Add($"--history={file}");
    return this;
  }

  /// <summary>
  /// Specifies the maximum number of history entries.
  /// </summary>
  /// <param name="size">Maximum history size</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHistorySize(int size)
  {
    _arguments.Add($"--history-size={size}");
    return this;
  }
}