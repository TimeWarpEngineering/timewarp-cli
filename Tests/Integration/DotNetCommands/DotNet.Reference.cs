#!/usr/bin/dotnet run

await RunTests<DotNetReferenceTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetReferenceTests
{
  public static async Task TestBasicReferenceBuilderCreation()
  {
    // DotNet.Reference() alone doesn't build a valid command - needs a subcommand
    DotNetReferenceBuilder builder = DotNet.Reference();
    
    AssertTrue(
      builder != null,
      "DotNet.Reference() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestReferenceWithProjectParameter()
  {
    // Test that we can create a builder with project file
    DotNetReferenceBuilder builder = DotNet.Reference("MyApp.csproj");
    
    AssertTrue(
      builder != null,
      "DotNet.Reference() with project should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestReferenceAddCommand()
  {
    string command = DotNet.Reference("MyApp.csproj")
      .Add("MyLibrary.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference --project MyApp.csproj add MyLibrary.csproj",
      $"Expected 'dotnet reference --project MyApp.csproj add MyLibrary.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestReferenceAddWithMultipleProjects()
  {
    string command = DotNet.Reference("MyApp.csproj")
      .Add("MyLibrary.csproj", "MyOtherLibrary.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference --project MyApp.csproj add MyLibrary.csproj MyOtherLibrary.csproj",
      $"Expected 'dotnet reference --project MyApp.csproj add MyLibrary.csproj MyOtherLibrary.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestReferenceListCommand()
  {
    string command = DotNet.Reference("MyApp.csproj")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference --project MyApp.csproj list",
      $"Expected 'dotnet reference --project MyApp.csproj list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestReferenceRemoveCommand()
  {
    string command = DotNet.Reference("MyApp.csproj")
      .Remove("MyLibrary.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference --project MyApp.csproj remove MyLibrary.csproj",
      $"Expected 'dotnet reference --project MyApp.csproj remove MyLibrary.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Reference()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference list",
      $"Expected 'dotnet reference list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCommandExecutionGracefulHandling()
  {
    // Test that command string is built correctly even for non-existent project
    string command = DotNet.Reference("nonexistent.csproj")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet reference --project nonexistent.csproj list",
      $"Expected correct command string even for non-existent projects, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}