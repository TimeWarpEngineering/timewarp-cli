#!/usr/bin/dotnet run

await RunTests<DotNetPublishTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetPublishTests

{

  public static async Task TestBasicBuilderCreation()
  {
    DotNetPublishBuilder publishBuilder = DotNet.Publish();
    AssertTrue(publishBuilder != null, "DotNet.Publish() should return a non-null builder");
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithFramework("net10.0")
      .WithRuntime("win-x64")
      .WithOutput("./publish")
      .WithNoRestore()
      .Build();
    
    AssertTrue(command != null, "Publish fluent configuration should build successfully");
  }

  public static async Task TestAdvancedDeploymentOptions()
  {
    CommandResult deployCommand = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithRuntime("linux-x64")
      .WithSelfContained()
      .WithReadyToRun()
      .WithSingleFile()
      .WithTrimmed()
      .WithNoLogo()
      .Build();
    
    AssertTrue(deployCommand != null, "Advanced deployment options should work correctly");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Publish()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("PUBLISH_ENV", "production")
      .WithArchitecture("x64")
      .WithOperatingSystem("linux")
      .Build();
    
    AssertTrue(envCommand != null, "Working directory and environment variables should work correctly");
  }

  public static async Task TestMSBuildPropertiesAndPublishingConfiguration()
  {
    CommandResult propsCommand = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithProperty("PublishProfile", "Production")
      .WithProperty("EnvironmentName", "Staging")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithVerbosity("minimal")
      .Build();
    
    AssertTrue(propsCommand != null, "MSBuild properties and publishing configuration should work correctly");
  }

  public static async Task TestPublishOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.Publish("test.csproj")
      .WithConfiguration("Release")
      .WithRuntime("win-x64")
      .WithNoSelfContained()
      .WithNoBuild()
      .Build();
    
    AssertTrue(overloadCommand != null, "Publish overload with project parameter should work correctly");
  }

  public static async Task TestCommandBuilderWithNonExistentProject()
  {
    // Verify that the command builder creates a valid command even with non-existent project
    CommandResult command = DotNet.Publish()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Release")
      .WithRuntime("win-x64")
      .WithNoRestore()
      .Build();
    
    AssertTrue(command != null, "Publish command builder should create a valid command");
  }
}