namespace TimeWarp.Cli;

public partial class FzfBuilder
{
  // Search Options
  /// <summary>
  /// Enables extended search mode (enabled by default).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithExtended()
  {
    _arguments.Add("--extended");
    return this;
  }

  /// <summary>
  /// Disables extended search mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithNoExtended()
  {
    _arguments.Add("--no-extended");
    return this;
  }

  /// <summary>
  /// Enables exact match mode.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithExact()
  {
    _arguments.Add("--exact");
    return this;
  }

  /// <summary>
  /// Enables case-insensitive matching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithCaseInsensitive()
  {
    _arguments.Add("-i");
    return this;
  }

  /// <summary>
  /// Enables case-sensitive matching.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithCaseSensitive()
  {
    _arguments.Add("+i");
    return this;
  }

  /// <summary>
  /// Specifies the scoring scheme.
  /// </summary>
  /// <param name="scheme">The scoring scheme (default, path, history)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithScheme(string scheme)
  {
    _arguments.Add($"--scheme={scheme}");
    return this;
  }

  /// <summary>
  /// Enables literal matching (do not normalize latin script letters).
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithLiteral()
  {
    _arguments.Add("--literal");
    return this;
  }

  /// <summary>
  /// Specifies field index expressions for limiting search scope.
  /// </summary>
  /// <param name="nth">Field index expression string. Examples: "1" (first field), "1,3" (first and third), 
  /// "2..4" (fields 2-4), "-1" (last field), "2.." (all from second), "..-2" (all except last 2)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithNth(string nth)
  {
    _arguments.Add($"--nth={nth}");
    return this;
  }

  /// <summary>
  /// Transforms the presentation of each line using field index expressions.
  /// </summary>
  /// <param name="withNth">Field index expression string for display. Same format as WithNth.
  /// Controls which fields are shown while searching all fields</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithWithNth(string withNth)
  {
    _arguments.Add($"--with-nth={withNth}");
    return this;
  }

  /// <summary>
  /// Specifies the field delimiter regex.
  /// </summary>
  /// <param name="delimiter">Field delimiter regex. Examples: "\t" (tab), ":" (colon), 
  /// "\s+" (whitespace), "[,:]" (comma or colon). Default is AWK-style (whitespace)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithDelimiter(string delimiter)
  {
    _arguments.Add($"--delimiter={delimiter}");
    return this;
  }

  /// <summary>
  /// Disables sorting of results.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithNoSort()
  {
    _arguments.Add("--no-sort");
    return this;
  }

  /// <summary>
  /// Enables tracking of current selection when results are updated.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithTrack()
  {
    _arguments.Add("--track");
    return this;
  }

  /// <summary>
  /// Reverses the order of input.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithTac()
  {
    _arguments.Add("--tac");
    return this;
  }

  /// <summary>
  /// Disables search functionality.
  /// </summary>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithDisabled()
  {
    _arguments.Add("--disabled");
    return this;
  }

  /// <summary>
  /// Specifies tie-breaking criteria for equal scores.
  /// </summary>
  /// <param name="tiebreak">Comma-separated list of criteria (length, chunk, begin, end, index)</param>
  /// <returns>The builder instance for method chaining</returns>
  public FzfBuilder WithTiebreak(string tiebreak)
  {
    _arguments.Add($"--tiebreak={tiebreak}");
    return this;
  }
}