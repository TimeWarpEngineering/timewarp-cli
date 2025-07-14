#!/usr/bin/dotnet run

await RunTests<DotNetWatchCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetWatchCommandTests

{
  public static async Task TestBasicDotNetWatchBuilderCreation()
  {
    DotNetWatchBuilder watchBuilder = DotNet.Watch();
    AssertTrue(watchBuilder != null, "DotNet.Watch() should return a valid builder instance");
  }

  public static async Task TestWatchRunCommand()
  {
    CommandResult command = DotNet.Watch()
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch Run command should return a valid CommandResult instance");
  }

  public static async Task TestWatchTestCommand()
  {
    CommandResult command = DotNet.Watch()
      .Test()
      .Build();
    
    AssertTrue(command != null, "Watch Test command should return a valid CommandResult instance");
  }

  public static async Task TestWatchBuildCommand()
  {
    CommandResult command = DotNet.Watch()
      .Build()
      .Build();
    
    AssertTrue(command != null, "Watch Build command should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithProject()
  {
    CommandResult command = DotNet.Watch()
      .WithProject("MyApp.csproj")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with project should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithBasicOptions()
  {
    CommandResult command = DotNet.Watch()
      .WithQuiet()
      .WithVerbose()
      .WithList()
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with basic options should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithNoOptions()
  {
    CommandResult command = DotNet.Watch()
      .WithNoRestore()
      .WithNoLaunchProfile()
      .WithNoHotReload()
      .WithNoBuild()
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with no-options should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithIncludeExcludePatterns()
  {
    CommandResult command = DotNet.Watch()
      .WithInclude("**/*.cs")
      .WithInclude("**/*.cshtml")
      .WithExclude("**/bin/**")
      .WithExclude("**/obj/**")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with include/exclude patterns should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithBuildConfiguration()
  {
    CommandResult command = DotNet.Watch()
      .WithConfiguration("Release")
      .WithTargetFramework("net10.0")
      .WithRuntime("linux-x64")
      .WithVerbosity("detailed")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with build configuration should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithPropertiesAndLaunchProfile()
  {
    CommandResult command = DotNet.Watch()
      .WithProperty("Configuration=Debug")
      .WithProperty("Platform=x64")
      .WithLaunchProfile("Development")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with properties and launch profile should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithAdditionalArguments()
  {
    CommandResult command = DotNet.Watch()
      .WithArguments("--environment", "Development")
      .WithArgument("--port")
      .WithArgument("5000")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Watch with additional arguments should return a valid CommandResult instance");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.Watch()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development")
      .Run()
      .Build();
    
    AssertTrue(command != null, "Working directory and environment variables should return a valid CommandResult instance");
  }

  public static async Task TestWatchWithComprehensiveOptions()
  {
    CommandResult command = DotNet.Watch()
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
      .Build();
    
    AssertTrue(command != null, "Watch with comprehensive options should return a valid CommandResult instance");
  }

  public static async Task TestWatchListCommandExecution()
  {
    // This lists watched files without actually starting the watcher
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.Watch()
        .WithList()
        .Run()
        .GetStringAsync(),
      "should throw for watch list without valid project (default validation behavior)"
    );
  }
}