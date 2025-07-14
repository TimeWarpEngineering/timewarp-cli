#!/usr/bin/dotnet run

await RunTests<DotNetFluentApiTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetFluentApiTests

{
  public static async Task TestBasicDotNetRunBuilderCreation()
  {
    DotNetRunBuilder runBuilder = DotNet.Run();
    AssertTrue(runBuilder != null, "DotNet.Run() should return a valid builder instance");
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Run()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithNoRestore()
      .WithArguments("--help")
      .Build();
    
    AssertTrue(command != null, "Build() should return a valid CommandResult instance");
  }

  public static async Task TestMethodChaining()
  {
    CommandResult chainedCommand = DotNet.Run()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithNoRestore()
      .WithNoBuild()
      .WithVerbosity("minimal")
      .WithArguments("arg1", "arg2")
      .Build();
    
    AssertTrue(chainedCommand != null, "Chained Build() should return a valid CommandResult instance");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Run()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEST_VAR", "test_value")
      .Build();
    
    AssertTrue(envCommand != null, "Environment config Build() should return a valid CommandResult instance");
  }

  public static async Task TestExtendedOptions()
  {
    CommandResult extendedCommand = DotNet.Run()
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
      .Build();
    
    AssertTrue(extendedCommand != null, "Extended options Build() should return a valid CommandResult instance");
  }

  public static async Task TestCommandExecutionWithGracefulHandling()
  {
    // This should handle gracefully since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.Run()
        .WithProject("nonexistent.csproj")
        .WithConfiguration("Debug")
        .WithNoRestore()
        .GetStringAsync(),
      "should throw for non-existent project (default validation behavior)"
    );
  }
}