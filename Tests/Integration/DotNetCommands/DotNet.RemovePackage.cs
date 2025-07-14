#!/usr/bin/dotnet run

await RunTests<DotNetRemovePackageTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetRemovePackageTests
{
  public static async Task TestBasicRemovePackageBuilderCreation()
  {
    DotNetRemovePackageBuilder removePackageBuilder = DotNet.RemovePackage("TestPackage");
    
    AssertTrue(
      removePackageBuilder != null,
      "DotNet.RemovePackage() should create builder successfully"
    );
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.RemovePackage("Microsoft.Extensions.Logging")
      .WithProject("test.csproj")
      .Build();
    
    AssertTrue(
      command != null,
      "RemovePackage fluent configuration methods should work correctly"
    );
  }

  public static async Task TestWorkingDirectoryConfiguration()
  {
    CommandResult dirCommand = DotNet.RemovePackage("Newtonsoft.Json")
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .Build();
    
    AssertTrue(
      dirCommand != null,
      "Working directory configuration should work correctly"
    );
  }

  public static async Task TestEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.RemovePackage("TestPackage")
      .WithProject("test.csproj")
      .WithEnvironmentVariable("NUGET_ENV", "test")
      .WithEnvironmentVariable("REMOVE_PACKAGE_LOG", "verbose")
      .Build();
    
    AssertTrue(
      envCommand != null,
      "Environment variables should work correctly"
    );
  }

  public static async Task TestPackageNameValidation()
  {
    CommandResult validationCommand = DotNet.RemovePackage("Valid.Package.Name")
      .WithProject("MyProject.csproj")
      .Build();
    
    AssertTrue(
      validationCommand != null,
      "Package name validation should work correctly"
    );
  }

  public static async Task TestMultipleConfigurationChaining()
  {
    CommandResult chainCommand = DotNet.RemovePackage("ChainedPackage")
      .WithProject("test.csproj")
      .WithWorkingDirectory("/project")
      .WithEnvironmentVariable("BUILD_ENV", "test")
      .Build();
    
    AssertTrue(
      chainCommand != null,
      "Multiple configuration chaining should work correctly"
    );
  }

  public static async Task TestCommandExecutionGracefulHandling()
  {
    // This should throw an exception since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.RemovePackage("TestPackage")
        .WithProject("nonexistent.csproj")
        .GetStringAsync(),
      "RemovePackage should throw exception for non-existent project"
    );
  }
}