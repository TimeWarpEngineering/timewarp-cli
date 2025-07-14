#!/usr/bin/dotnet run

await RunTests<DotNetRestoreCommandTests>();

internal sealed class DotNetRestoreCommandTests
{
  public static async Task TestBasicRestoreBuilderCreation()
  {
    DotNetRestoreBuilder restoreBuilder = DotNet.Restore();
    
    AssertTrue(
      restoreBuilder != null,
      "DotNet.Restore() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Restore()
      .WithProject("test.csproj")
      .WithRuntime("linux-x64")
      .WithVerbosity("minimal")
      .WithPackagesDirectory("./packages")
      .WithNoCache()
      .Build();
    
    AssertTrue(
      command != null,
      "Restore fluent configuration methods should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreMethodChainingWithSourcesAndProperties()
  {
    CommandResult chainedCommand = DotNet.Restore()
      .WithProject("test.csproj")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithSource("https://nuget.pkg.github.com/MyOrg/index.json")
      .WithNoDependencies()
      .WithInteractive()
      .WithTerminalLogger("auto")
      .WithProperty("RestoreNoCache", "true")
      .WithProperty("RestoreIgnoreFailedSources", "true")
      .Build();
    
    AssertTrue(
      chainedCommand != null,
      "Restore method chaining should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreLockFileAndWorkingDirectoryOptions()
  {
    CommandResult lockCommand = DotNet.Restore()
      .WithProject("test.csproj")
      .WithLockFilePath("./packages.lock.json")
      .WithLockedMode()
      .WithForce()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_PACKAGES", "./temp-packages")
      .Build();
    
    AssertTrue(
      lockCommand != null,
      "Restore lock file and working directory options should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.Restore("test.csproj")
      .WithRuntime("win-x64")
      .WithVerbosity("quiet")
      .WithNoCache()
      .Build();
    
    AssertTrue(
      overloadCommand != null,
      "Restore overload with project parameter should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreCommandExecutionGracefulHandling()
  {
    // Test that the builder creates a valid command even for non-existent projects
    CommandResult command = DotNet.Restore()
      .WithProject("nonexistent.csproj")
      .WithNoCache()
      .Build();
    
    // The command should be created successfully
    AssertTrue(
      command != null,
      "Restore command should be created even for non-existent projects"
    );
    
    await Task.CompletedTask;
  }
}