#!/usr/bin/dotnet run

await RunTests<DotNetBuildCommandTests>();

internal sealed class DotNetBuildCommandTests
{
  public static async Task TestBasicDotNetBuildBuilderCreation()
  {
    DotNetBuildBuilder buildBuilder = DotNet.Build();
    
    AssertTrue(
      buildBuilder != null,
      "DotNet.Build() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Build()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithFramework("net10.0")
      .WithNoRestore()
      .WithOutput("bin/Debug")
      .Build();
    
    AssertTrue(
      command != null,
      "Build fluent configuration methods should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildMethodChainingWithAdvancedOptions()
  {
    CommandResult chainedCommand = DotNet.Build()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithArchitecture("x64")
      .WithOperatingSystem("linux")
      .WithNoRestore()
      .WithNoDependencies()
      .WithNoIncremental()
      .WithVerbosity("minimal")
      .WithProperty("Platform", "AnyCPU")
      .Build();
    
    AssertTrue(
      chainedCommand != null,
      "Build method chaining should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildWithWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Build()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("BUILD_ENV", "test")
      .WithNoLogo()
      .Build();
    
    AssertTrue(
      envCommand != null,
      "Build with working directory and environment variables should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildWithMSBuildPropertiesIncludingNoCacheOptions()
  {
    CommandResult propsCommand = DotNet.Build()
      .WithProject("test.csproj")
      .WithConfiguration("Debug")
      .WithProperty("RestoreNoCache", "true")
      .WithProperty("DisableImplicitNuGetFallbackFolder", "true")
      .WithProperty("RestoreIgnoreFailedSources", "true")
      .Build();
    
    AssertTrue(
      propsCommand != null,
      "MSBuild properties including no-cache options should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.Build("test.csproj")
      .WithConfiguration("Debug")
      .WithNoRestore()
      .Build();
    
    AssertTrue(
      overloadCommand != null,
      "Build overload with project parameter should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestBuildCommandExecutionGracefulHandling()
  {
    // Test that the builder creates a valid command even for non-existent projects
    CommandResult command = DotNet.Build()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Debug")
      .WithNoRestore()
      .Build();
    
    // The command should be created successfully
    AssertTrue(
      command != null,
      "Build command should be created even for non-existent projects"
    );
    
    await Task.CompletedTask;
  }
}