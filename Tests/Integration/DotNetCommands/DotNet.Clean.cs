#!/usr/bin/dotnet run

await RunTests<DotNetCleanCommandTests>();

internal sealed class DotNetCleanCommandTests
{
  public static async Task TestBasicDotNetCleanBuilderCreation()
  {
    DotNetCleanBuilder cleanBuilder = DotNet.Clean();
    
    AssertTrue(
      cleanBuilder != null,
      "DotNet.Clean() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCleanFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Clean()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithOutput("bin/Debug")
      .WithVerbosity("minimal")
      .Build();
    
    AssertTrue(
      command != null,
      "Clean fluent configuration methods should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCleanMethodChainingWithRuntimeAndProperties()
  {
    CommandResult chainedCommand = DotNet.Clean()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithRuntime("linux-x64")
      .WithNoLogo()
      .WithProperty("Platform", "AnyCPU")
      .WithProperty("CleanTargets", "All")
      .Build();
    
    AssertTrue(
      chainedCommand != null,
      "Clean method chaining should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCleanWithWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Clean()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("CLEAN_ENV", "test")
      .WithVerbosity("quiet")
      .Build();
    
    AssertTrue(
      envCommand != null,
      "Clean with working directory and environment variables should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCleanOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.Clean("test.csproj")
      .WithConfiguration("Debug")
      .WithNoLogo()
      .Build();
    
    AssertTrue(
      overloadCommand != null,
      "Clean overload with project parameter should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCleanCommandExecutionGracefulHandling()
  {
    // Test that the builder creates a valid command even for non-existent projects
    CommandResult command = DotNet.Clean()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Debug")
      .Build();
    
    // The command should be created successfully
    AssertTrue(
      command != null,
      "Clean command should be created even for non-existent projects"
    );
    
    await Task.CompletedTask;
  }
}