namespace TimeWarp.Cli.Test.Helpers;

public static class TestHelpers
{
  public static string FormatTestName(string name) => 
    System.Text.RegularExpressions.Regex.Replace(name, "([A-Z])", " $1").Trim();
  
  public static void TestPassed(string testName) => 
    Console.WriteLine($"✅ {FormatTestName(testName)}");
  
  public static void TestFailed(string testName, string reason) => 
    Console.WriteLine($"❌ {FormatTestName(testName)}: {reason}");
  
  /// <summary>
  /// Test wrapper that handles common try-catch pattern for test execution.
  /// Any unhandled exceptions will cause the test to fail with the exception message.
  /// </summary>
  public static async Task<bool> TestWrapper(Func<string, Task> testAction, string testName)
  {
    ArgumentNullException.ThrowIfNull(testAction);
    
    try
    {
      await testAction(testName);
      return true;
    }
    catch (Exception ex)
    {
      TestFailed(testName, ex.Message);
      return false;
    }
  }
}