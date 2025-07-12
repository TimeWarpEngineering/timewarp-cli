namespace TimeWarp.Cli;

public partial class FZFBuilder
{
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
}