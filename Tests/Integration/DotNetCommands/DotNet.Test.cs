#!/usr/bin/dotnet run

await RunTests<DotNetTestTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetTestTests
{
  public static async Task TestBasicDotNetTestCommand()
  {
    string command = DotNet.Test()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test",
      $"Expected 'dotnet test', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
  
  public static async Task TestTestWithProjectOnly()
  {
    string command = DotNet.Test()
      .WithProject("MyApp.Tests.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test MyApp.Tests.csproj",
      $"Expected 'dotnet test MyApp.Tests.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFluentConfigurationMethods()
  {
    string command = DotNet.Test()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithNoRestore()
      .WithFilter("Category=Unit")
      .WithLogger("console")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test test.csproj --configuration Debug --framework net10.0 --filter Category=Unit --logger console --no-restore",
      $"Expected correct test command with configuration options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestMethodChainingWithAdvancedOptions()
  {
    string command = DotNet.Test()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithArchitecture("x64")
      .WithOperatingSystem("linux")
      .WithNoRestore()
      .WithNoBuild()
      .WithVerbosity("minimal")
      .WithFilter("TestCategory=Integration")
      .WithLogger("trx")
      .WithLogger("html")
      .WithBlame()
      .WithCollect()
      .WithResultsDirectory("TestResults")
      .WithProperty("Platform", "AnyCPU")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test test.csproj --configuration Release --arch x64 --os linux --verbosity minimal --filter TestCategory=Integration --results-directory TestResults --logger trx --logger html --no-restore --no-build --blame --collect --property:Platform=AnyCPU",
      $"Expected correct test command with advanced options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Test()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEST_ENV", "integration")
      .WithNoLogo()
      .WithSettings("test.runsettings")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test test.csproj --settings test.runsettings --nologo",
      $"Expected 'dotnet test test.csproj --settings test.runsettings --nologo', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCommandBuilderWithNonExistentProject()
  {
    // Verify that the command builder creates a valid command even with non-existent project
    string command = DotNet.Test()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Debug")
      .WithNoRestore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet test nonexistent.csproj --configuration Debug --no-restore",
      $"Expected correct command string even for non-existent projects, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}