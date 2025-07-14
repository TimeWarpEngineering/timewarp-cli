#!/usr/bin/dotnet run

await RunTests<BasicCommandTests>();

internal sealed class BasicCommandTests
{
  public static async Task TestSimpleEchoCommand()
  {
    string result = await Run("echo", "Hello World").GetStringAsync();
    
    AssertTrue(
      result.Trim() == "Hello World",
      "Echo command should return 'Hello World'"
    );
  }

  public static async Task TestCommandWithMultipleArguments()
  {
    string result = await Run("echo", "arg1", "arg2", "arg3").GetStringAsync();
    
    AssertTrue(
      result.Trim() == "arg1 arg2 arg3",
      $"Multiple arguments should work correctly, got '{result.Trim()}'"
    );
  }

  public static async Task TestExecuteAsyncDoesNotThrow()
  {
    await Run("echo", "test").ExecuteAsync();
    
    // Test passes if no exception is thrown
    AssertTrue(true, "ExecuteAsync should work without throwing");
  }

  public static async Task TestDateCommand()
  {
    string result = await Run("date").GetStringAsync();
    
    AssertTrue(
      !string.IsNullOrEmpty(result.Trim()),
      "Date command should return non-empty result"
    );
  }
}