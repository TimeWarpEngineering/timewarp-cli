#!/usr/bin/dotnet run

// MANUAL TEST: This test requires interactive terminal and cannot run in CI
// Run manually to verify interactive functionality works correctly

await RunTests<CommandResultInteractiveTests>();

internal sealed class CommandResultInteractiveTests
{
  public static async Task TestGetStringInteractiveAsyncWithMockFzf()
  {
    // Since we can't run interactive commands in CI/automated tests,
    // we'll use a mock that simulates FZF behavior
    string mockFzfPath = CreateMockFzf();
    
    try
    {
      // Configure to use our mock
      CliConfiguration.SetCommandPath("fzf", mockFzfPath);
      
      // Test simple selection
      string result = await Fzf.Run()
        .FromInput("option1", "option2", "option3")
        .WithPrompt("Select: ")
        .GetStringInteractiveAsync();
      
      AssertEquals(
        "option1",
        result,
        "Mock FZF should return first option"
      );
    }
    finally
    {
      CliConfiguration.Reset();
      File.Delete(mockFzfPath);
    }
  }
  
  public static async Task TestPipelineWithInteractiveSelection()
  {
    string mockFzfPath = CreateMockFzf();
    
    try
    {
      CliConfiguration.SetCommandPath("fzf", mockFzfPath);
      
      // Test pipeline: echo | fzf
      string result = await Shell.Run("echo")
        .WithArguments("red\ngreen\nblue")
        .Pipe("fzf", "--prompt", "Select color: ")
        .GetStringInteractiveAsync();
      
      AssertEquals(
        "red",
        result,
        "Pipeline with mock FZF should return first line"
      );
    }
    finally
    {
      CliConfiguration.Reset();
      File.Delete(mockFzfPath);
    }
  }
  
  public static async Task TestExecuteInteractiveAsync()
  {
    // Test with a simple echo command (non-interactive but safe)
    ExecutionResult result = await Shell.Run("echo")
      .WithArguments("Hello from interactive mode")
      .ExecuteInteractiveAsync();
    
    AssertEquals(
      0,
      result.ExitCode,
      "Echo command should succeed"
    );
    
    // Output strings should be empty since output went to console
    AssertEquals(
      string.Empty,
      result.StandardOutput,
      "ExecuteInteractiveAsync should not capture stdout"
    );
  }
  
  public static async Task TestInteractiveMethodsWithNullCommand()
  {
    // Test graceful degradation with null command
    CommandResult nullCommand = CommandResult.NullCommandResult;
    
    string stringResult = await nullCommand.GetStringInteractiveAsync();
    AssertEquals(
      string.Empty,
      stringResult,
      "GetStringInteractiveAsync with null command should return empty string"
    );
    
    ExecutionResult execResult = await nullCommand.ExecuteInteractiveAsync();
    AssertEquals(
      0,
      execResult.ExitCode,
      "ExecuteInteractiveAsync with null command should return exit code 0"
    );
  }
  
  private static string CreateMockFzf()
  {
    // Create a simple script that acts like FZF but just returns the first line
    string mockPath = Path.GetTempFileName();
    File.Delete(mockPath); // Delete the file so we can recreate it with .sh extension
    mockPath = mockPath + ".sh";
    
    string mockScript = @"#!/bin/bash
# Mock FZF - just output the first line of input
head -n 1
";
    
    File.WriteAllText(mockPath, mockScript);
    
    // Make it executable
    if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
    {
      using Process chmod = Process.Start(new ProcessStartInfo
      {
        FileName = "chmod",
        Arguments = $"+x \"{mockPath}\"",
        RedirectStandardOutput = true,
        RedirectStandardError = true
      })!;
      chmod.WaitForExit();
    }
    
    return mockPath;
  }
}