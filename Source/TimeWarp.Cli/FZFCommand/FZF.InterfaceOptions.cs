namespace TimeWarp.Cli;

public partial class FZFBuilder
{
  // Interface Options
  /// <summary>
  /// Enables multi-select mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithMulti()
  {
    _arguments.Add("--multi");
    return this;
  }

  /// <summary>
  /// Enables multi-select mode with maximum selection limit.
  /// </summary>
  /// <param name="max">Maximum number of selections</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithMulti(int max)
  {
    _arguments.Add($"--multi={max}");
    return this;
  }

  /// <summary>
  /// Disables mouse support.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoMouse()
  {
    _arguments.Add("--no-mouse");
    return this;
  }

  /// <summary>
  /// Specifies custom key bindings.
  /// </summary>
  /// <param name="bindings">Key binding specifications</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithBind(string bindings)
  {
    _arguments.Add($"--bind={bindings}");
    return this;
  }

  /// <summary>
  /// Enables cyclic scrolling.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithCycle()
  {
    _arguments.Add("--cycle");
    return this;
  }

  /// <summary>
  /// Keeps the right end of the line visible on overflow.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithKeepRight()
  {
    _arguments.Add("--keep-right");
    return this;
  }

  /// <summary>
  /// Specifies the number of screen lines to keep when scrolling.
  /// </summary>
  /// <param name="lines">Number of lines to keep</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithScrollOff(int lines)
  {
    _arguments.Add($"--scroll-off={lines}");
    return this;
  }

  /// <summary>
  /// Disables horizontal scrolling.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoHScroll()
  {
    _arguments.Add("--no-hscroll");
    return this;
  }

  /// <summary>
  /// Specifies the number of screen columns to keep to the right.
  /// </summary>
  /// <param name="cols">Number of columns to keep</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHScrollOff(int cols)
  {
    _arguments.Add($"--hscroll-off={cols}");
    return this;
  }

  /// <summary>
  /// Makes word-wise movements respect path separators.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithFilepathWord()
  {
    _arguments.Add("--filepath-word");
    return this;
  }

  /// <summary>
  /// Specifies label characters for jump operations.
  /// </summary>
  /// <param name="chars">Characters to use for jump labels</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithJumpLabels(string chars)
  {
    _arguments.Add($"--jump-labels={chars}");
    return this;
  }
}