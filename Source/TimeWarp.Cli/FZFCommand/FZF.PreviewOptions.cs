namespace TimeWarp.Cli;

public partial class FZFBuilder
{
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
}