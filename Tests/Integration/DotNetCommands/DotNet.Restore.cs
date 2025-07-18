#!/usr/bin/dotnet run

await RunTests<DotNetRestoreCommandTests>();

internal sealed class DotNetRestoreCommandTests
{
  public static async Task TestBasicRestoreCommand()
  {
    string command = DotNet.Restore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore",
      $"Expected 'dotnet restore', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreFluentConfigurationMethods()
  {
    string command = DotNet.Restore()
      .WithProject("test.csproj")
      .WithRuntime("linux-x64")
      .WithVerbosity("minimal")
      .WithPackagesDirectory("./packages")
      .WithNoCache()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore test.csproj --runtime linux-x64 --verbosity minimal --packages ./packages --no-cache",
      $"Expected correct restore command with configuration, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreMethodChainingWithSourcesAndProperties()
  {
    string command = DotNet.Restore()
      .WithProject("test.csproj")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithSource("https://nuget.pkg.github.com/MyOrg/index.json")
      .WithNoDependencies()
      .WithInteractive()
      .WithTerminalLogger("auto")
      .WithProperty("RestoreNoCache", "true")
      .WithProperty("RestoreIgnoreFailedSources", "true")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore test.csproj --tl auto --source https://api.nuget.org/v3/index.json --source https://nuget.pkg.github.com/MyOrg/index.json --no-dependencies --interactive --property:RestoreNoCache=true --property:RestoreIgnoreFailedSources=true",
      $"Expected correct restore command with sources and properties, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreLockFileAndWorkingDirectoryOptions()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Restore()
      .WithProject("test.csproj")
      .WithLockFilePath("./packages.lock.json")
      .WithLockedMode()
      .WithForce()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_PACKAGES", "./temp-packages")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore test.csproj --lock-file-path ./packages.lock.json --locked-mode --force",
      $"Expected correct restore command with lock file options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreOverloadWithProjectParameter()
  {
    string command = DotNet.Restore("test.csproj")
      .WithRuntime("win-x64")
      .WithVerbosity("quiet")
      .WithNoCache()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore test.csproj --runtime win-x64 --verbosity quiet --no-cache",
      $"Expected correct restore command with overload, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestRestoreCommandExecutionGracefulHandling()
  {
    // Test that command string is built correctly even for non-existent project
    string command = DotNet.Restore()
      .WithProject("nonexistent.csproj")
      .WithNoCache()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet restore nonexistent.csproj --no-cache",
      $"Expected correct command string even for non-existent projects, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}