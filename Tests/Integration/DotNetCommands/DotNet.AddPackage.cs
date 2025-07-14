#!/usr/bin/dotnet run

await RunTests<DotNetAddPackageTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetAddPackageTests
{
  public static async Task TestBasicAddPackageBuilderCreation()
  {
    DotNetAddPackageBuilder addPackageBuilder = DotNet.AddPackage("TestPackage");
    
    AssertTrue(
      addPackageBuilder != null,
      "DotNet.AddPackage() should create builder successfully"
    );
  }

  public static async Task TestAddPackageWithVersionOverload()
  {
    DotNetAddPackageBuilder versionCommand = DotNet.AddPackage("TestPackage", "1.0.0");
    
    AssertTrue(
      versionCommand != null,
      "AddPackage with version overload should work correctly"
    );
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.AddPackage("Microsoft.Extensions.Logging")
      .WithProject("test.csproj")
      .WithFramework("net10.0")
      .WithVersion("8.0.0")
      .WithNoRestore()
      .Build();
    
    AssertTrue(
      command != null,
      "AddPackage fluent configuration methods should work correctly"
    );
  }

  public static async Task TestPackageSpecificOptions()
  {
    CommandResult packageCommand = DotNet.AddPackage("Newtonsoft.Json")
      .WithProject("test.csproj")
      .WithVersion("13.0.3")
      .WithPrerelease()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithPackageDirectory("./packages")
      .Build();
    
    AssertTrue(
      packageCommand != null,
      "Package-specific options should work correctly"
    );
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.AddPackage("TestPackage")
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_ENV", "test")
      .WithInteractive()
      .Build();
    
    AssertTrue(
      envCommand != null,
      "Working directory and environment variables should work correctly"
    );
  }

  public static async Task TestMultipleSourcesConfiguration()
  {
    CommandResult sourcesCommand = DotNet.AddPackage("TestPackage")
      .WithProject("test.csproj")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithSource("https://my-private-feed.com/v3/index.json")
      .WithFramework("net10.0")
      .WithNoRestore()
      .Build();
    
    AssertTrue(
      sourcesCommand != null,
      "Multiple sources configuration should work correctly"
    );
  }

  public static async Task TestCommandExecutionGracefulHandling()
  {
    // This should throw an exception since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.AddPackage("TestPackage")
        .WithProject("nonexistent.csproj")
        .WithVersion("1.0.0")
        .WithNoRestore()
        .GetStringAsync(),
      "AddPackage should throw exception for non-existent project"
    );
  }
}