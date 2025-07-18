#!/usr/bin/dotnet run

await RunTests<DotNetWatchCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetWatchCommandTests

{
  public static async Task TestBasicDotNetWatchBuilderCreation()
  {
    // DotNet.Watch() alone doesn't build a valid command - needs a subcommand
    DotNetWatchBuilder watchBuilder = DotNet.Watch();
    
    AssertTrue(
      watchBuilder != null,
      "DotNet.Watch() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchRunCommand()
  {
    string command = DotNet.Watch()
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch run",
      $"Expected 'dotnet watch run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchTestCommand()
  {
    string command = DotNet.Watch()
      .Test()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch test",
      $"Expected 'dotnet watch test', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchBuildCommand()
  {
    string command = DotNet.Watch()
      .Build()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch build",
      $"Expected 'dotnet watch build', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithProject()
  {
    string command = DotNet.Watch()
      .WithProject("MyApp.csproj")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --project MyApp.csproj run",
      $"Expected 'dotnet watch --project MyApp.csproj run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithBasicOptions()
  {
    string command = DotNet.Watch()
      .WithQuiet()
      .WithVerbose()
      .WithList()
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --quiet --verbose --list run",
      $"Expected 'dotnet watch --quiet --verbose --list run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithNoOptions()
  {
    string command = DotNet.Watch()
      .WithNoRestore()
      .WithNoLaunchProfile()
      .WithNoHotReload()
      .WithNoBuild()
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --no-restore --no-launch-profile --no-hot-reload --no-build run",
      $"Expected correct watch command with no-options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithIncludeExcludePatterns()
  {
    string command = DotNet.Watch()
      .WithInclude("**/*.cs")
      .WithInclude("**/*.cshtml")
      .WithExclude("**/bin/**")
      .WithExclude("**/obj/**")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --include **/*.cs --include **/*.cshtml --exclude **/bin/** --exclude **/obj/** run",
      $"Expected correct watch command with patterns, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithBuildConfiguration()
  {
    string command = DotNet.Watch()
      .WithConfiguration("Release")
      .WithTargetFramework("net10.0")
      .WithRuntime("linux-x64")
      .WithVerbosity("detailed")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --framework net10.0 --configuration Release --runtime linux-x64 --verbosity detailed run",
      $"Expected correct watch command with build configuration, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithPropertiesAndLaunchProfile()
  {
    string command = DotNet.Watch()
      .WithProperty("Configuration=Debug")
      .WithProperty("Platform=x64")
      .WithLaunchProfile("Development")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --property Configuration=Debug --property Platform=x64 --launch-profile Development run",
      $"Expected correct watch command with properties, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithAdditionalArguments()
  {
    string command = DotNet.Watch()
      .WithArguments("--environment", "Development")
      .WithArgument("--port")
      .WithArgument("5000")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch run --environment Development --port 5000",
      $"Expected correct watch command with arguments, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Watch()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch run",
      $"Expected 'dotnet watch run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchWithComprehensiveOptions()
  {
    string command = DotNet.Watch()
      .WithProject("MyApp.csproj")
      .WithConfiguration("Release")
      .WithTargetFramework("net10.0")
      .WithVerbosity("minimal")
      .WithInclude("**/*.cs")
      .WithExclude("**/bin/**")
      .WithProperty("DefineConstants=RELEASE")
      .WithNoRestore()
      .WithArguments("--environment", "Production")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --project MyApp.csproj --no-restore --include **/*.cs --exclude **/bin/** --property DefineConstants=RELEASE --framework net10.0 --configuration Release --verbosity minimal run --environment Production",
      $"Expected correct comprehensive watch command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWatchListCommandExecution()
  {
    // Test command string generation for list operation
    string command = DotNet.Watch()
      .WithList()
      .WithProject("test.csproj")
      .Run()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet watch --project test.csproj --list run",
      $"Expected 'dotnet watch --project test.csproj --list run', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}