#!/usr/bin/dotnet run

await RunTests<DotNetTestTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetTestTests
{
  public static async Task TestBasicBuilderCreation()
  {
    DotNetTestBuilder testBuilder = DotNet.Test();
    AssertTrue(testBuilder != null, "DotNet.Test() should return a non-null builder");
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Test()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithNoRestore()
      .WithFilter("Category=Unit")
      .WithLogger("console")
      .Build();
    
    AssertTrue(command != null, "Test fluent configuration should build successfully");
  }

  public static async Task TestMethodChainingWithAdvancedOptions()
  {
    CommandResult chainedCommand = DotNet.Test()
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
      .Build();
    
    AssertTrue(chainedCommand != null, "Test method chaining should work correctly");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Test()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEST_ENV", "integration")
      .WithNoLogo()
      .WithSettings("test.runsettings")
      .Build();
    
    AssertTrue(envCommand != null, "Test working directory and environment variables should work correctly");
  }

  public static async Task TestCommandBuilderWithNonExistentProject()
  {
    // Verify that the command builder creates a valid command even with non-existent project
    CommandResult command = DotNet.Test()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Debug")
      .WithNoRestore()
      .Build();
    
    AssertTrue(command != null, "Test command builder should create a valid command");
  }
}