#!/usr/bin/dotnet run

await RunTests<DotNetListPackagesTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetListPackagesTests
{
  public static async Task TestBasicListPackagesBuilderCreation()
  {
    DotNetListPackagesBuilder listPackagesBuilder = DotNet.ListPackages();
    
    AssertTrue(
      listPackagesBuilder != null,
      "DotNet.ListPackages() should create builder successfully"
    );
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.ListPackages()
      .WithProject("test.csproj")
      .WithFramework("net10.0")
      .WithVerbosity("minimal")
      .WithFormat("console")
      .Outdated()
      .Build();
    
    AssertTrue(
      command != null,
      "ListPackages fluent configuration methods should work correctly"
    );
  }

  public static async Task TestMethodChainingWithTransitiveAndVulnerableOptions()
  {
    CommandResult chainedCommand = DotNet.ListPackages()
      .WithProject("test.csproj")
      .IncludeTransitive()
      .Vulnerable()
      .Deprecated()
      .WithInteractive()
      .WithSource("https://api.nuget.org/v3/index.json")
      .IncludePrerelease()
      .Build();
    
    AssertTrue(
      chainedCommand != null,
      "ListPackages method chaining should work correctly"
    );
  }

  public static async Task TestJsonFormatAndHighestVersionOptions()
  {
    CommandResult jsonCommand = DotNet.ListPackages()
      .WithProject("test.csproj")
      .WithFormat("json")
      .WithOutputVersion("1")
      .WithConfig("nuget.config")
      .Outdated()
      .HighestMinor()
      .HighestPatch()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_PACKAGES", "./temp-packages")
      .Build();
    
    AssertTrue(
      jsonCommand != null,
      "ListPackages JSON format and version options should work correctly"
    );
  }

  public static async Task TestListPackagesOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.ListPackages("test.csproj")
      .WithFramework("net8.0")
      .IncludeTransitive()
      .WithVerbosity("quiet")
      .Build();
    
    AssertTrue(
      overloadCommand != null,
      "ListPackages overload with project parameter should work correctly"
    );
  }

  public static async Task TestToListAsyncMethod()
  {
    // This should throw an exception since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.ListPackages()
        .WithProject("nonexistent.csproj")
        .IncludeTransitive()
        .Outdated()
        .ToListAsync(),
      "ToListAsync should throw exception for non-existent project"
    );
  }

  public static async Task TestCommandExecutionGracefulHandling()
  {
    // This should throw an exception since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.ListPackages()
        .WithProject("nonexistent.csproj")
        .Outdated()
        .GetStringAsync(),
      "ListPackages should throw exception for non-existent project"
    );
  }
}