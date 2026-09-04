#region Purpose
// Optional filters for FindItem (name glob, size, date, attributes).
#endregion

#region Design
// All properties are optional; a null criteria matches every walked entry.
// Name is a glob against the file name, or against the relative path when it contains '/' or '**'.
#endregion

namespace TimeWarp.Amuru.Native.FileSystem;

/// <summary>
/// Filters applied by <see cref="Direct.FindItem"/> and <see cref="Commands.FindItem"/>.
/// </summary>
public sealed class FindCriteria
{
  /// <summary>
  /// Gets a glob matched against the entry name, or against the relative path when the
  /// pattern contains a directory separator or <c>**</c>.
  /// </summary>
  public string? Name { get; init; }

  /// <summary>
  /// Gets the inclusive minimum file size in bytes. Directories are excluded when set.
  /// </summary>
  public long? MinSize { get; init; }

  /// <summary>
  /// Gets the inclusive maximum file size in bytes. Directories are excluded when set.
  /// </summary>
  public long? MaxSize { get; init; }

  /// <summary>
  /// Gets the inclusive lower bound for last-write time.
  /// </summary>
  public DateTimeOffset? ModifiedAfter { get; init; }

  /// <summary>
  /// Gets the inclusive upper bound for last-write time.
  /// </summary>
  public DateTimeOffset? ModifiedBefore { get; init; }

  /// <summary>
  /// Gets attribute flags that the entry must have (all bits required).
  /// </summary>
  public FileAttributes? Attributes { get; init; }
}
