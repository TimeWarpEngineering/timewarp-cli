#!/usr/bin/dotnet run

await RunTests<DotNetSlnCommandTests>();

internal sealed class DotNetSlnCommandTests
{
  public static async Task BasicDotNetSlnCommand()
  {
    // DotNet.Sln() alone doesn't build a valid command - needs a subcommand
    // This test verifies the builder is created
    DotNetSlnBuilder builder = DotNet.Sln();
    
    AssertTrue(
      builder != null,
      "DotNet.Sln() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task DotNetSlnWithSolutionFileParameter()
  {
    // Test that we can create a builder with solution file
    DotNetSlnBuilder builder = DotNet.Sln("MySolution.sln");
    
    AssertTrue(
      builder != null,
      "DotNet.Sln() with solution file should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task SolutionAddCommand()
  {
    string command = DotNet.Sln("MySolution.sln")
      .Add("MyApp.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln MySolution.sln add MyApp.csproj",
      $"Expected 'dotnet sln MySolution.sln add MyApp.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task SolutionAddWithMultipleProjects()
  {
    string command = DotNet.Sln("MySolution.sln")
      .Add("MyApp.csproj", "MyLibrary.csproj", "MyTests.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln MySolution.sln add MyApp.csproj MyLibrary.csproj MyTests.csproj",
      $"Expected 'dotnet sln MySolution.sln add MyApp.csproj MyLibrary.csproj MyTests.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task SolutionListCommand()
  {
    string command = DotNet.Sln("MySolution.sln")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln MySolution.sln list",
      $"Expected 'dotnet sln MySolution.sln list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task SolutionRemoveCommand()
  {
    string command = DotNet.Sln("MySolution.sln")
      .Remove("MyApp.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln MySolution.sln remove MyApp.csproj",
      $"Expected 'dotnet sln MySolution.sln remove MyApp.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task SolutionMigrateCommand()
  {
    string command = DotNet.Sln("MySolution.sln")
      .Migrate()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln MySolution.sln migrate",
      $"Expected 'dotnet sln MySolution.sln migrate', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task WorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Sln()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln list",
      $"Expected 'dotnet sln list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task CommandBuilderWithNonExistentSolution()
  {
    // Verify that the command builder creates a valid command even with non-existent solution
    string command = DotNet.Sln("nonexistent.sln")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet sln nonexistent.sln list",
      $"Expected 'dotnet sln nonexistent.sln list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}