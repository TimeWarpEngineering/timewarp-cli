#region Purpose
// Distinguishes file vs directory for NewItem and TestPath.
#endregion

namespace TimeWarp.Amuru.Native.FileSystem;

/// <summary>
/// Kind of file-system item to create or test.
/// </summary>
public enum ItemType
{
  /// <summary>
  /// A regular file.
  /// </summary>
  File = 0,

  /// <summary>
  /// A directory.
  /// </summary>
  Directory = 1
}
