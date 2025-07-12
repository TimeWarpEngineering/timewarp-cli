namespace TimeWarp.Cli;

/// <summary>
/// Fluent API for FZF (fuzzy finder) integration.
/// </summary>
public static class FZF
{
  /// <summary>
  /// Creates a fluent builder for the 'fzf' command.
  /// </summary>
  /// <returns>A FZFBuilder for configuring the fzf command</returns>
  public static FZFBuilder Run()
  {
    return new FZFBuilder();
  }
}

/// <summary>
/// Fluent builder for configuring 'fzf' commands.
/// </summary>
public class FZFBuilder
{
  private CommandOptions _options = new();
  private List<string> _arguments = new();
  private List<string> _inputItems = new();
  private string? _inputCommand;
  private string? _inputGlob;
  private bool _useStdin;

  /// <summary>
  /// Specifies the working directory for the command.
  /// </summary>
  /// <param name="directory">The working directory path</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithWorkingDirectory(string directory)
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
  public FZFBuilder WithEnvironmentVariable(string key, string? value)
  {
    _options = _options.WithEnvironmentVariable(key, value);
    return this;
  }

  // Search Options
  /// <summary>
  /// Enables extended search mode (enabled by default).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithExtended()
  {
    _arguments.Add("--extended");
    return this;
  }

  /// <summary>
  /// Disables extended search mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoExtended()
  {
    _arguments.Add("--no-extended");
    return this;
  }

  /// <summary>
  /// Enables exact match mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithExact()
  {
    _arguments.Add("--exact");
    return this;
  }

  /// <summary>
  /// Enables case-insensitive matching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithCaseInsensitive()
  {
    _arguments.Add("-i");
    return this;
  }

  /// <summary>
  /// Enables case-sensitive matching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithCaseSensitive()
  {
    _arguments.Add("+i");
    return this;
  }

  /// <summary>
  /// Specifies the scoring scheme.
  /// </summary>
  /// <param name="scheme">The scoring scheme (default, path, history)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithScheme(string scheme)
  {
    _arguments.Add($"--scheme={scheme}");
    return this;
  }

  /// <summary>
  /// Enables literal matching (do not normalize latin script letters).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithLiteral()
  {
    _arguments.Add("--literal");
    return this;
  }

  /// <summary>
  /// Specifies field index expressions for limiting search scope.
  /// </summary>
  /// <param name="nth">Comma-separated list of field indices</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNth(string nth)
  {
    _arguments.Add($"--nth={nth}");
    return this;
  }

  /// <summary>
  /// Transforms the presentation of each line using field index expressions.
  /// </summary>
  /// <param name="withNth">Field index expressions for presentation</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithWithNth(string withNth)
  {
    _arguments.Add($"--with-nth={withNth}");
    return this;
  }

  /// <summary>
  /// Specifies the field delimiter regex.
  /// </summary>
  /// <param name="delimiter">The delimiter regex</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithDelimiter(string delimiter)
  {
    _arguments.Add($"--delimiter={delimiter}");
    return this;
  }

  /// <summary>
  /// Disables sorting of results.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithNoSort()
  {
    _arguments.Add("--no-sort");
    return this;
  }

  /// <summary>
  /// Enables tracking of current selection when results are updated.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithTrack()
  {
    _arguments.Add("--track");
    return this;
  }

  /// <summary>
  /// Reverses the order of input.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithTac()
  {
    _arguments.Add("--tac");
    return this;
  }

  /// <summary>
  /// Disables search functionality.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithDisabled()
  {
    _arguments.Add("--disabled");
    return this;
  }

  /// <summary>
  /// Specifies tie-breaking criteria for equal scores.
  /// </summary>
  /// <param name="tiebreak">Comma-separated list of criteria (length, chunk, begin, end, index)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithTiebreak(string tiebreak)
  {
    _arguments.Add($"--tiebreak={tiebreak}");
    return this;
  }

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

  // Preview Options
  /// <summary>
  /// Specifies the preview command.
  /// </summary>
  /// <param name="command">Command to preview highlighted line</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPreview(string command)
  {
    _arguments.Add($"--preview={command}");
    return this;
  }

  /// <summary>
  /// Specifies the preview window options.
  /// </summary>
  /// <param name="options">Preview window layout options</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPreviewWindow(string options)
  {
    _arguments.Add($"--preview-window={options}");
    return this;
  }

  /// <summary>
  /// Specifies the preview window label.
  /// </summary>
  /// <param name="label">Preview window label</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPreviewLabel(string label)
  {
    _arguments.Add($"--preview-label={label}");
    return this;
  }

  /// <summary>
  /// Specifies the preview window label position.
  /// </summary>
  /// <param name="position">Label position</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPreviewLabelPos(int position)
  {
    _arguments.Add($"--preview-label-pos={position}");
    return this;
  }

  // Scripting Options
  /// <summary>
  /// Specifies the initial query string.
  /// </summary>
  /// <param name="query">Initial query</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithQuery(string query)
  {
    _arguments.Add($"--query={query}");
    return this;
  }

  /// <summary>
  /// Automatically selects the only match.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithSelect1()
  {
    _arguments.Add("--select-1");
    return this;
  }

  /// <summary>
  /// Exits immediately when there's no match.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithExit0()
  {
    _arguments.Add("--exit-0");
    return this;
  }

  /// <summary>
  /// Enables filter mode (non-interactive).
  /// </summary>
  /// <param name="filter">Filter string</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithFilter(string filter)
  {
    _arguments.Add($"--filter={filter}");
    return this;
  }

  /// <summary>
  /// Prints the query as the first line of output.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPrintQuery()
  {
    _arguments.Add("--print-query");
    return this;
  }

  /// <summary>
  /// Specifies keys that complete fzf.
  /// </summary>
  /// <param name="keys">Comma-separated list of keys</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithExpect(string keys)
  {
    _arguments.Add($"--expect={keys}");
    return this;
  }

  /// <summary>
  /// Reads input delimited by ASCII NUL characters.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithRead0()
  {
    _arguments.Add("--read0");
    return this;
  }

  /// <summary>
  /// Prints output delimited by ASCII NUL characters.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithPrint0()
  {
    _arguments.Add("--print0");
    return this;
  }

  /// <summary>
  /// Enables synchronous search for multi-staged filtering.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithSync()
  {
    _arguments.Add("--sync");
    return this;
  }

  /// <summary>
  /// Starts HTTP server on default port.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithListen()
  {
    _arguments.Add("--listen");
    return this;
  }

  /// <summary>
  /// Starts HTTP server on specified port.
  /// </summary>
  /// <param name="port">Port number</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithListen(int port)
  {
    _arguments.Add($"--listen={port}");
    return this;
  }

  /// <summary>
  /// Starts HTTP server on specified address and port.
  /// </summary>
  /// <param name="address">Address and port</param>
  /// <returns>The builder instance for method chaining</returns>
  public FZFBuilder WithListen(string address)
  {
    _arguments.Add($"--listen={address}");
    return this;
  }

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

  public CommandResult Build()
  {
    var arguments = new List<string> { "fzf" };
    arguments.AddRange(_arguments);

    CommandResult command = CommandExtensions.Run("fzf", arguments.Skip(1).ToArray(), _options);

    // Handle input sources
    if (_inputItems.Count > 0)
    {
      // Create a pipeline with echo for the input items
      string input = string.Join("\n", _inputItems);
      command = CommandExtensions.Run("echo", new[] { input }, _options).Pipe("fzf", arguments.Skip(1).ToArray());
    }
    else if (!string.IsNullOrEmpty(_inputGlob))
    {
      // Use find command for file glob
      command = CommandExtensions.Run("find", new[] { ".", "-name", _inputGlob }, _options).Pipe("fzf", arguments.Skip(1).ToArray());
    }
    else if (!string.IsNullOrEmpty(_inputCommand))
    {
      // Parse and execute the input command
      string[] parts = _inputCommand.Split(' ', StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length > 0)
      {
        string exe = parts[0];
        string[] args = parts.Skip(1).ToArray();
        command = CommandExtensions.Run(exe, args, _options).Pipe("fzf", arguments.Skip(1).ToArray());
      }
    }
    else if (_useStdin)
    {
      // Use fzf directly with stdin (no pipeline needed)
      command = CommandExtensions.Run("fzf", arguments.Skip(1).ToArray(), _options);
    }

    return command;
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
/// Extension methods for integrating FZF with CommandResult.
/// </summary>
public static class FZFExtensions
{
  /// <summary>
  /// Pipes the command output to FZF for interactive selection.
  /// </summary>
  /// <param name="command">The command to pipe from</param>
  /// <param name="configure">Optional FZF configuration</param>
  /// <returns>A CommandResult with FZF selection</returns>
  public static CommandResult SelectWithFzf(this CommandResult command, Action<FZFBuilder>? configure = null)
  {
    var fzfBuilder = new FZFBuilder();
    configure?.Invoke(fzfBuilder);
    
    CommandResult fzfArguments = fzfBuilder.Build();
    return command.Pipe("fzf", ExtractFzfArguments(fzfArguments));
  }

  private static string[] ExtractFzfArguments(CommandResult fzfCommand)
  {
    // This is a simplified implementation
    // In a real implementation, you'd extract the arguments from the FZFBuilder
    return Array.Empty<string>();
  }
}