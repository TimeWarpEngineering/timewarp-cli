#!/usr/bin/dotnet run

await TestRunner.RunTests<ErrorHandlingTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
internal sealed class ErrorHandlingTests
#pragma warning restore CA1812
{

  // Create options with no validation for graceful degradation tests
  static CommandOptions NoValidationCommandOptions = new CommandOptions().WithNoValidation();

  public static async Task TestNonExistentCommandWithNoValidation()
  {
    await AssertThrowsAsync<Exception>(
      async () => await Run("nonexistentcommand12345", Array.Empty<string>(), NoValidationCommandOptions).GetStringAsync(),
      "should have thrown for non-existent command"
    );
  }

  public static async Task TestCommandWithNonZeroExitCodeAndNoValidation()
  {
    string[] lsArgs = ["/nonexistent/path/12345"];

    string lines = await Run("ls", lsArgs, NoValidationCommandOptions).GetStringAsync();

    AssertTrue(
      string.IsNullOrEmpty(lines),
      "should return empty string for command with non-zero exit code and no validation"
    );
  }

  public static async Task TestExecuteAsyncThrowsOnNonZeroExit()
  {
    await AssertThrowsAsync<Exception>(
      async () => await Run("ls", "/nonexistent/path/12345").ExecuteAsync(),
      "should have thrown for command with non-zero exit code"
    );
  }

  public static async Task TestGetLinesAsyncWithNoValidation()
  {
    string[] lsArgs2 = ["/nonexistent/path/12345"];

    string[] lines = await Run("ls", lsArgs2, NoValidationCommandOptions).GetLinesAsync();

    AssertTrue(lines.Length == 0, "should return empty array for non-existent path with no validation");
  }

  public static async Task TestSpecialCharactersInArguments()
  {
    string result = await Run("echo", "Hello \"World\" with 'quotes' and $pecial chars!").GetStringAsync();

    AssertTrue(
      !string.IsNullOrEmpty(result),
      "should not return empty string for command with special characters"
    );
  }

  public static async Task TestEmptyCommandReturnsEmptyString()
  {
    string result = await Run("").GetStringAsync();
    AssertTrue(
      string.IsNullOrEmpty(result),
      "should return empty string for empty command"
    );
  }

  public static async Task TestWhitespaceCommandReturnsEmptyString()
  {
    string result = await Run("   ").GetStringAsync();
    AssertTrue(
      string.IsNullOrEmpty(result),
      "should return empty string for whitespace command"
    );
  }

  public static async Task TestDefaultGetStringThrowsOnError()
  {
    await AssertThrowsAsync<Exception>(
      async () => await Run("ls", "/nonexistent/path/12345").GetStringAsync(),
      "should have thrown for command with non-zero exit code"
    );
  }

  public static async Task TestDefaultGetLinesThrowsOnError()
  {
    await AssertThrowsAsync<Exception>(
      async () => await Run("ls", "/nonexistent/path/12345").GetLinesAsync(),
      "should have thrown for command with non-zero exit code"
    );
  }

  public static async Task TestExecuteAsyncWithNoValidation()
  {
    string[] lsArgs3 = ["/nonexistent/path/12345"];
    await Run("ls", lsArgs3, NoValidationCommandOptions).ExecuteAsync();
    AssertTrue(
      true,
      "should not throw for command with no validation"
    );
  }
}

internal static class TestRunner
{
  static int PassCount;
  static int TotalTests;

  public static async Task RunTests<T>() where T : class
  {
    Console.WriteLine("🧪 Testing ErrorHandling...");
    // get all public methods in the class
    MethodInfo[] testMethods = typeof(T).GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);

    // and run them as tests

    foreach (MethodInfo method in testMethods)
    {
      await RunTest(method);
    }
    // Summary
    Console.WriteLine($"\n📊 ErrorHandling Results: {PassCount}/{TotalTests} tests passed");
    Environment.Exit(PassCount == TotalTests ? 0 : 1);

  }

  private static async Task RunTest(MethodInfo method)
  {
    if (!method.IsPublic || !method.IsStatic || method.ReturnType != typeof(Task))
    {
      // Skip non-test methods
      return;
    }

    // Increment total tests and run the test

    TotalTests++;
    string testName = method.Name;
    Console.WriteLine($"Running {testName}...");

    try
    {
      var task = method.Invoke(null, null) as Task;
      if (task != null) await task;
      PassCount++;
      TestPassed(testName);
    }
    catch (Exception ex)
    {
      TestFailed(testName, ex.Message);
    }

  }
}