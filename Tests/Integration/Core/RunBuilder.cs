#!/usr/bin/dotnet run

await RunTests<RunBuilderTests>();

internal sealed class RunBuilderTests
{
  public static async Task TestBasicRunBuilder()
  {
    string result = await Shell.Run("echo")
      .WithArguments("Hello", "World")
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == "Hello World",
      "RunBuilder should work with basic arguments"
    );
  }

  public static async Task TestRunBuilderWithMultipleWithArguments()
  {
    string result = await Shell.Run("echo")
      .WithArguments("arg1")
      .WithArguments("arg2", "arg3")
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == "arg1 arg2 arg3",
      $"Multiple WithArguments calls should accumulate, got '{result.Trim()}'"
    );
  }

  public static async Task TestRunBuilderWithEnvironmentVariable()
  {
    string result = await Shell.Run("printenv")
      .WithEnvironmentVariable("TEST_VAR", "test_value")
      .WithArguments("TEST_VAR")
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == "test_value",
      $"Environment variable should be set correctly, got '{result.Trim()}'"
    );
  }

  public static async Task TestRunBuilderWithNoValidation()
  {
    // This would normally throw because 'false' exits with code 1
    ExecutionResult result = await Shell.Run("false")
      .WithNoValidation()
      .ExecuteAsync();
    
    AssertTrue(
      result.ExitCode == 1,
      "WithNoValidation should allow non-zero exit codes"
    );
  }

  public static async Task TestRunBuilderGetLinesAsync()
  {
    string[] lines = await Shell.Run("printf")
      .WithArguments("line1\\nline2\\nline3")
      .GetLinesAsync();
    
    AssertTrue(
      lines.Length == 3,
      $"Should return 3 lines, got {lines.Length}"
    );
    
    AssertTrue(
      lines[0] == "line1" && lines[1] == "line2" && lines[2] == "line3",
      "Lines should match expected values"
    );
  }

  public static async Task TestRunBuilderWithWorkingDirectory()
  {
    string tempDir = Path.GetTempPath();
    string result = await Shell.Run("pwd")
      .WithWorkingDirectory(tempDir)
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == tempDir.TrimEnd('/'),
      $"Working directory should be set to {tempDir}, got {result.Trim()}"
    );
  }

  public static async Task TestRunBuilderPipeline()
  {
    string result = await Shell.Run("echo")
      .WithArguments("Hello\nWorld\nTest")
      .Build()
      .Pipe("grep", "World")
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == "World",
      $"Pipeline should filter for 'World', got '{result.Trim()}'"
    );
  }

  public static async Task TestRunBuilderExecuteAsync()
  {
    ExecutionResult result = await Shell.Run("echo")
      .WithArguments("test output")
      .ExecuteAsync();
    
    AssertTrue(
      result.IsSuccess,
      "Command should execute successfully"
    );
    
    AssertTrue(
      result.StandardOutput.Trim() == "test output",
      $"Output should match, got '{result.StandardOutput.Trim()}'"
    );
  }

  public static async Task TestRunBuilderChaining()
  {
    // Test that all methods can be chained fluently
    string result = await Shell.Run("bash")
      .WithArguments("-c", "echo $TEST1 $TEST2")
      .WithEnvironmentVariable("TEST1", "Hello")
      .WithEnvironmentVariable("TEST2", "World")
      .WithNoValidation()
      .GetStringAsync();
    
    AssertTrue(
      result.Trim() == "Hello World",
      $"Chained configuration should work correctly, got '{result.Trim()}'"
    );
  }
}