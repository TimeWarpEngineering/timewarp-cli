#region Purpose
// Direct API for listing directory entries as an async enumerable of FileSystemInfo.
#endregion

#region Design
// Enumerates via DirectoryInfo and yields between entries (Task.Yield) for cooperative
// scheduling. [EnumeratorCancellation] lets callers cancel via WithCancellation.
// Async method suffix is deferred: the public name shipped in 1.0.0 stays GetChildItem.
#endregion

namespace TimeWarp.Amuru.Native.FileSystem;

public static partial class Direct
{
  /// <summary>
  /// Lists directory contents as an async stream of file system entries.
  /// </summary>
  /// <param name="path">Path to the directory to list</param>
  /// <param name="cancellationToken">Token used to cancel the enumeration</param>
  /// <returns>Async enumerable of file system entries</returns>
  /// <exception cref="DirectoryNotFoundException">When the directory doesn't exist</exception>
  /// <exception cref="IOException">When there's an I/O error reading the directory</exception>
  /// <exception cref="OperationCanceledException">When <paramref name="cancellationToken"/> is canceled</exception>
  public static async IAsyncEnumerable<FileSystemInfo> GetChildItem(
    string path = ".",
    [EnumeratorCancellation] CancellationToken cancellationToken = default)
  {
    cancellationToken.ThrowIfCancellationRequested();
    DirectoryInfo directory = new(path);

    // This will throw DirectoryNotFoundException if path doesn't exist
    foreach (FileSystemInfo entry in directory.EnumerateFileSystemInfos())
    {
      cancellationToken.ThrowIfCancellationRequested();
      yield return entry;
      await Task.Yield(); // Allow other async operations to run
    }
  }

  /// <summary>
  /// Bash-style alias for GetChildItem.
  /// </summary>
  public static IAsyncEnumerable<FileSystemInfo> Ls(string path = ".") => GetChildItem(path);

  /// <summary>
  /// DOS-style alias for GetChildItem.
  /// </summary>
  public static IAsyncEnumerable<FileSystemInfo> Dir(string path = ".") => GetChildItem(path);
}
