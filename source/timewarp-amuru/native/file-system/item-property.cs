#region Purpose
// Snapshot of file-system metadata returned by GetItemProperty (stat).
#endregion

#region Design
// A snapshot, not a live FileSystemInfo: callers can hold it without Refresh() races.
// LinkTarget is null when the path is not a reparse point / symbolic link.
#endregion

namespace TimeWarp.Amuru.Native.FileSystem;

/// <summary>
/// Metadata for a file or directory, captured at the time of the call.
/// </summary>
public sealed class ItemProperty
{
  /// <summary>
  /// Gets the fully qualified path.
  /// </summary>
  public required string FullName { get; init; }

  /// <summary>
  /// Gets the file or directory name.
  /// </summary>
  public required string Name { get; init; }

  /// <summary>
  /// Gets the length in bytes. Zero for directories.
  /// </summary>
  public long Length { get; init; }

  /// <summary>
  /// Gets the creation timestamp.
  /// </summary>
  public DateTimeOffset CreationTime { get; init; }

  /// <summary>
  /// Gets the last-write timestamp.
  /// </summary>
  public DateTimeOffset LastWriteTime { get; init; }

  /// <summary>
  /// Gets the last-access timestamp.
  /// </summary>
  public DateTimeOffset LastAccessTime { get; init; }

  /// <summary>
  /// Gets the file attributes.
  /// </summary>
  public FileAttributes Attributes { get; init; }

  /// <summary>
  /// Gets a value indicating whether the path exists.
  /// </summary>
  public bool Exists { get; init; }

  /// <summary>
  /// Gets a value indicating whether the path is a file.
  /// </summary>
  public bool IsFile { get; init; }

  /// <summary>
  /// Gets a value indicating whether the path is a directory.
  /// </summary>
  public bool IsDirectory { get; init; }

  /// <summary>
  /// Gets a value indicating whether the path is a reparse point (symlink, junction).
  /// </summary>
  public bool IsReparsePoint { get; init; }

  /// <summary>
  /// Gets the symbolic-link target, or <see langword="null"/> when the path is not a link.
  /// </summary>
  public string? LinkTarget { get; init; }
}
