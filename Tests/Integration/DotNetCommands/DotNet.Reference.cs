#!/usr/bin/dotnet run

await RunTests<DotNetReferenceTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetReferenceTests
{
  public static async Task TestBasicReferenceBuilderCreation()
  {
    DotNetReferenceBuilder referenceBuilder = DotNet.Reference();
    
    AssertTrue(
      referenceBuilder != null,
      "DotNet.Reference() should create builder successfully"
    );
  }

  public static async Task TestReferenceWithProjectParameter()
  {
    DotNetReferenceBuilder referenceBuilder = DotNet.Reference("MyApp.csproj");
    
    AssertTrue(
      referenceBuilder != null,
      "DotNet.Reference() with project should create successfully"
    );
  }

  public static async Task TestReferenceAddCommand()
  {
    CommandResult command = DotNet.Reference("MyApp.csproj")
      .Add("MyLibrary.csproj")
      .Build();
    
    AssertTrue(
      command != null,
      "Reference Add command should work correctly"
    );
  }

  public static async Task TestReferenceAddWithMultipleProjects()
  {
    CommandResult command = DotNet.Reference("MyApp.csproj")
      .Add("MyLibrary.csproj", "MyOtherLibrary.csproj")
      .Build();
    
    AssertTrue(
      command != null,
      "Reference Add with multiple projects should work correctly"
    );
  }

  public static async Task TestReferenceListCommand()
  {
    CommandResult command = DotNet.Reference("MyApp.csproj")
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Reference List command should work correctly"
    );
  }

  public static async Task TestReferenceRemoveCommand()
  {
    CommandResult command = DotNet.Reference("MyApp.csproj")
      .Remove("MyLibrary.csproj")
      .Build();
    
    AssertTrue(
      command != null,
      "Reference Remove command should work correctly"
    );
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.Reference()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Working directory and environment variables should work correctly"
    );
  }

  public static async Task TestCommandExecutionGracefulHandling()
  {
    // This should throw an exception since the project doesn't exist
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.Reference("nonexistent.csproj")
        .List()
        .GetStringAsync(),
      "Reference should throw exception for non-existent project"
    );
  }
}