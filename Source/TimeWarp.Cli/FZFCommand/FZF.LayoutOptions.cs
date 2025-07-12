namespace TimeWarp.Cli;

public partial class FZFBuilder
{
  // Layout Options
  /// <summary>
  /// Specifies the display height.
  /// </summary>
  /// <param name="height">Height in lines or percentage</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHeight(int height)
  {
    _arguments.Add($"--height={height}");
    return this;
  }

  /// <summary>
  /// Specifies the display height as percentage.
  /// </summary>
  /// <param name="heightPercent">Height as percentage</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHeightPercent(int heightPercent)
  {
    _arguments.Add($"--height={heightPercent}%");
    return this;
  }

  /// <summary>
  /// Specifies the minimum height when using percentage.
  /// </summary>
  /// <param name="minHeight">Minimum height in lines</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithMinHeight(int minHeight)
  {
    _arguments.Add($"--min-height={minHeight}");
    return this;
  }

  /// <summary>
  /// Specifies the layout style.
  /// </summary>
  /// <param name="layout">Layout style (default, reverse, reverse-list)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithLayout(string layout)
  {
    _arguments.Add($"--layout={layout}");
    return this;
  }

  /// <summary>
  /// Enables border with default style.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithBorder()
  {
    _arguments.Add("--border");
    return this;
  }

  /// <summary>
  /// Enables border with specified style.
  /// </summary>
  /// <param name="style">Border style (rounded, sharp, bold, etc.)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithBorder(string style)
  {
    _arguments.Add($"--border={style}");
    return this;
  }

  /// <summary>
  /// Specifies the border label.
  /// </summary>
  /// <param name="label">Label text for the border</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithBorderLabel(string label)
  {
    _arguments.Add($"--border-label={label}");
    return this;
  }

  /// <summary>
  /// Specifies the border label position.
  /// </summary>
  /// <param name="position">Position of the border label</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithBorderLabelPos(string position)
  {
    _arguments.Add($"--border-label-pos={position}");
    return this;
  }

  /// <summary>
  /// Specifies screen margins.
  /// </summary>
  /// <param name="margin">Margin specification (TRBL format)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithMargin(string margin)
  {
    _arguments.Add($"--margin={margin}");
    return this;
  }

  /// <summary>
  /// Specifies padding inside border.
  /// </summary>
  /// <param name="padding">Padding specification (TRBL format)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPadding(string padding)
  {
    _arguments.Add($"--padding={padding}");
    return this;
  }

  /// <summary>
  /// Specifies the finder info style.
  /// </summary>
  /// <param name="style">Info style (default, right, hidden, inline, etc.)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithInfo(string style)
  {
    _arguments.Add($"--info={style}");
    return this;
  }

  /// <summary>
  /// Specifies the horizontal separator string.
  /// </summary>
  /// <param name="separator">Separator string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithSeparator(string separator)
  {
    _arguments.Add($"--separator={separator}");
    return this;
  }

  /// <summary>
  /// Hides the info line separator.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoSeparator()
  {
    _arguments.Add("--no-separator");
    return this;
  }

  /// <summary>
  /// Enables scrollbar with default characters.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithScrollbar()
  {
    _arguments.Add("--scrollbar");
    return this;
  }

  /// <summary>
  /// Enables scrollbar with custom characters.
  /// </summary>
  /// <param name="chars">Characters for scrollbar</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithScrollbar(string chars)
  {
    _arguments.Add($"--scrollbar={chars}");
    return this;
  }

  /// <summary>
  /// Hides the scrollbar.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoScrollbar()
  {
    _arguments.Add("--no-scrollbar");
    return this;
  }

  /// <summary>
  /// Specifies the input prompt.
  /// </summary>
  /// <param name="prompt">Prompt string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPrompt(string prompt)
  {
    _arguments.Add($"--prompt={prompt}");
    return this;
  }

  /// <summary>
  /// Specifies the pointer to the current line.
  /// </summary>
  /// <param name="pointerString">Pointer string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPointer(string pointerString)
  {
    _arguments.Add($"--pointer={pointerString}");
    return this;
  }

  /// <summary>
  /// Specifies the multi-select marker.
  /// </summary>
  /// <param name="marker">Marker string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithMarker(string marker)
  {
    _arguments.Add($"--marker={marker}");
    return this;
  }

  /// <summary>
  /// Specifies the header string.
  /// </summary>
  /// <param name="header">Header string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHeader(string header)
  {
    _arguments.Add($"--header={header}");
    return this;
  }

  /// <summary>
  /// Specifies the number of header lines from input.
  /// </summary>
  /// <param name="lines">Number of header lines</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHeaderLines(int lines)
  {
    _arguments.Add($"--header-lines={lines}");
    return this;
  }

  /// <summary>
  /// Prints header before the prompt line.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithHeaderFirst()
  {
    _arguments.Add("--header-first");
    return this;
  }

  /// <summary>
  /// Specifies the ellipsis string for truncated lines.
  /// </summary>
  /// <param name="ellipsis">Ellipsis string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithEllipsis(string ellipsis)
  {
    _arguments.Add($"--ellipsis={ellipsis}");
    return this;
  }
}