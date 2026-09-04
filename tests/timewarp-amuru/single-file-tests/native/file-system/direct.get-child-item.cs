#!/usr/bin/dotnet --

#region Purpose
// Tests for Direct.GetChildItem - validates directory listing via Direct API
#endregion

#region Design
// Naming convention: SUT_Action_Given_Should_Result
// SUT: Direct (the static class providing direct file operations with IAsyncEnumerable)
// Action: GetChildItem (the method to list directory contents)
// Tests verify cancellation via WithCancellation
#endregion

#if !JARIBU_MULTI
return await RunAllTests();
#endif

namespace Direct_
{
  [TestTag("Native")]
  public class GetChildItem_Given_
  {
    [ModuleInitializer]
    internal static void Register() => RegisterTests<GetChildItem_Given_>();

    public static async Task Cancellation_Should_ThrowOperationCanceledException()
    {
      string testDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
      Directory.CreateDirectory(testDir);

      try
      {
        for (int i = 1; i <= 5; i++)
        {
          await File.WriteAllTextAsync(Path.Combine(testDir, $"file{i}.txt"), $"content{i}");
        }

        using CancellationTokenSource cts = new();
        List<FileSystemInfo> readEntries = [];

        await Should.ThrowAsync<OperationCanceledException>(async () =>
        {
          await foreach (FileSystemInfo entry in Direct.GetChildItem(testDir).WithCancellation(cts.Token))
          {
            readEntries.Add(entry);
            if (readEntries.Count == 1)
            {
              await cts.CancelAsync();
            }
          }
        });

        readEntries.Count.ShouldBeGreaterThanOrEqualTo(1);
      }
      finally
      {
        Directory.Delete(testDir, recursive: true);
      }
    }
  }
}
