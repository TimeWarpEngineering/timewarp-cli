#!/usr/bin/dotnet run

await RunTests<DotNetSlnCommandTests>();

internal sealed class DotNetSlnCommandTests
{
  public static async Task BasicDotNetSlnBuilderCreation()
  {
    DotNetSlnBuilder slnBuilder = DotNet.Sln();
    AssertTrue(slnBuilder != null, "DotNet.Sln() should create a valid builder");
  }

  public static async Task DotNetSlnWithSolutionFileParameter()
  {
    DotNetSlnBuilder slnBuilder = DotNet.Sln("MySolution.sln");
    AssertTrue(slnBuilder != null, "DotNet.Sln() with solution file should create a valid builder");
  }

  public static async Task SolutionAddCommand()
  {
    CommandResult command = DotNet.Sln("MySolution.sln")
      .Add("MyApp.csproj")
      .Build();
    
    AssertTrue(command != null, "Solution Add command should build successfully");
  }

  public static async Task SolutionAddWithMultipleProjects()
  {
    CommandResult command = DotNet.Sln("MySolution.sln")
      .Add("MyApp.csproj", "MyLibrary.csproj", "MyTests.csproj")
      .Build();
    
    AssertTrue(command != null, "Solution Add with multiple projects should build successfully");
  }

  public static async Task SolutionListCommand()
  {
    CommandResult command = DotNet.Sln("MySolution.sln")
      .List()
      .Build();
    
    AssertTrue(command != null, "Solution List command should build successfully");
  }

  public static async Task SolutionRemoveCommand()
  {
    CommandResult command = DotNet.Sln("MySolution.sln")
      .Remove("MyApp.csproj")
      .Build();
    
    AssertTrue(command != null, "Solution Remove command should build successfully");
  }

  public static async Task SolutionMigrateCommand()
  {
    CommandResult command = DotNet.Sln("MySolution.sln")
      .Migrate()
      .Build();
    
    AssertTrue(command != null, "Solution Migrate command should build successfully");
  }

  public static async Task WorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.Sln()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build();
    
    AssertTrue(command != null, "Working directory and environment variables should work correctly");
  }

  public static async Task CommandBuilderWithNonExistentSolution()
  {
    // Verify that the command builder creates a valid command even with non-existent solution
    CommandResult command = DotNet.Sln("nonexistent.sln")
      .List()
      .Build();
    
    AssertTrue(command != null, "Solution command builder should create a valid command");
  }
}