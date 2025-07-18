#!/usr/bin/dotnet run

await RunTests<DotNetFluentApiTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetFluentApiTests

{
  public static async Task TestBasicDotNetRunBuilderCreation()
  {
    string command = DotNet.Run()
      .Build()
      .ToCommandString();
      
    AssertTrue(
      command == "dotnet run",
      $"Expected 'dotnet run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFluentConfigurationMethods()
  {
    string command = DotNet.Run()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithNoRestore()
      .WithArguments("--help")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet run --project test.csproj --configuration Debug --framework net10.0 --no-restore -- --help",
      $"Expected correct run command with configuration, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestMethodChaining()
  {
    string command = DotNet.Run()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithNoRestore()
      .WithNoBuild()
      .WithVerbosity("minimal")
      .WithArguments("arg1", "arg2")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet run --project test.csproj --configuration Release --verbosity minimal --no-restore --no-build -- arg1 arg2",
      $"Expected correct chained run command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Run()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEST_VAR", "test_value")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet run --project test.csproj",
      $"Expected 'dotnet run --project test.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestExtendedOptions()
  {
    string command = DotNet.Run()
      .WithProject("test.csproj")
      .WithArchitecture("x64")
      .WithOperatingSystem("linux")
      .WithLaunchProfile("Development")
      .WithForce()
      .WithInteractive()
      .WithTerminalLogger("auto")
      .WithProperty("Configuration", "Debug")
      .WithProperty("Platform", "AnyCPU")
      .WithProcessEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development")
      .WithNoLaunchProfile()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet run --project test.csproj --arch x64 --os linux --launch-profile Development --tl auto --no-launch-profile --force --interactive --property:Configuration=Debug --property:Platform=AnyCPU -e ASPNETCORE_ENVIRONMENT=Development",
      $"Expected correct extended options command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCommandExecutionWithGracefulHandling()
  {
    // Test command string generation for non-existent project
    string command = DotNet.Run()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Debug")
      .WithNoRestore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet run --project nonexistent.csproj --configuration Debug --no-restore",
      $"Expected correct command string even for non-existent projects, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}