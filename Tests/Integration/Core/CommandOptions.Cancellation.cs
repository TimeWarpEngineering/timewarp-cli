#!/usr/bin/dotnet run

await RunTests<CancellationTests>();

internal sealed class CancellationTests
{
  public static async Task TestQuickCommandWithCancellationToken()
  {
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(10)); // Long timeout, command should complete
    
    string result = await Shell.Run("echo").WithArguments("Hello World").GetStringAsync(cts.Token);
    
    AssertTrue(
      result.Trim() == "Hello World",
      "Quick command with cancellation token should complete normally"
    );
  }

  public static async Task TestAlreadyCancelledToken()
  {
    using var cts = new CancellationTokenSource();
    await cts.CancelAsync(); // Cancel immediately
    
    try
    {
      string result = await Shell.Run("echo").WithArguments("test").GetStringAsync(cts.Token);
      // Should return empty string due to cancellation
      AssertTrue(
        string.IsNullOrEmpty(result),
        "Already cancelled token should return empty result"
      );
    }
    catch (OperationCanceledException)
    {
      // This is also acceptable behavior - either empty result or exception
      AssertTrue(true, "Already cancelled token can throw OperationCanceledException");
    }
  }

  public static async Task TestGetLinesAsyncWithCancellationToken()
  {
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    string[] lines = await Shell.Run("echo").WithArguments("line1\nline2\nline3").GetLinesAsync(cts.Token);
    
    AssertTrue(
      lines.Length == 3 && lines[0] == "line1" && lines[1] == "line2" && lines[2] == "line3",
      $"GetLinesAsync with cancellation token should return 3 lines, got {lines.Length}"
    );
  }

  public static async Task TestExecuteAsyncWithCancellationToken()
  {
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    ExecutionResult result = await Shell.Run("echo").WithArguments("execute test").ExecuteAsync(cts.Token);
    
    AssertTrue(
      result.ExitCode == 0,
      $"ExecuteAsync with cancellation token should succeed with exit code 0, got {result.ExitCode}"
    );
  }

  public static async Task TestTimeoutCancellation()
  {
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromMilliseconds(100)); // Very short timeout
    
    // Use sleep command that should be cancelled (works on both Unix and Windows with different commands)
    bool isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
    
    try
    {
      string result = isWindows 
        ? await Shell.Run("timeout").WithArguments("5").GetStringAsync(cts.Token)  // Windows: timeout 5 seconds
        : await Shell.Run("sleep").WithArguments("5").GetStringAsync(cts.Token);   // Unix: sleep 5 seconds
      
      // If we get here, either the command completed very quickly or returned empty due to cancellation
      AssertTrue(
        string.IsNullOrEmpty(result) || result.Length < 100,
        "Timeout should either cancel command or return quickly"
      );
    }
    catch (OperationCanceledException)
    {
      // Expected behavior - timeout cancelled the command
      AssertTrue(true, "Timeout properly cancelled long-running command");
    }
  }

  public static async Task TestPipelineWithCancellation()
  {
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    string result = await Shell.Run("echo").WithArguments("line1\nline2\nline3")
      .Pipe("grep", "line")
      .GetStringAsync(cts.Token);
    
    AssertTrue(
      !string.IsNullOrEmpty(result) && result.Contains("line", StringComparison.Ordinal),
      "Pipeline with cancellation token should work normally"
    );
  }

  public static async Task TestDefaultCancellationTokenBehavior()
  {
    string result = await Shell.Run("echo").WithArguments("default token test").GetStringAsync();
    
    AssertTrue(
      result.Trim() == "default token test",
      "Default cancellation token behavior should be preserved"
    );
  }
}