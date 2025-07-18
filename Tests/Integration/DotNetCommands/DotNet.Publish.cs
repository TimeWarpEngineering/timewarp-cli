#!/usr/bin/dotnet run

await RunTests<DotNetPublishTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetPublishTests

{

  public static async Task TestBasicDotNetPublishCommand()
  {
    string command = DotNet.Publish()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish",
      $"Expected 'dotnet publish', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
  
  public static async Task TestPublishWithProjectOnly()
  {
    string command = DotNet.Publish()
      .WithProject("MyApp.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish MyApp.csproj",
      $"Expected 'dotnet publish MyApp.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFluentConfigurationMethods()
  {
    string command = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithFramework("net10.0")
      .WithRuntime("win-x64")
      .WithOutput("./publish")
      .WithNoRestore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish test.csproj --configuration Release --framework net10.0 --runtime win-x64 --output ./publish --no-restore",
      $"Expected correct publish command with configuration options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestAdvancedDeploymentOptions()
  {
    string command = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithRuntime("linux-x64")
      .WithSelfContained()
      .WithReadyToRun()
      .WithSingleFile()
      .WithTrimmed()
      .WithNoLogo()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish test.csproj --configuration Release --runtime linux-x64 --nologo --self-contained --property:PublishReadyToRun=true --property:PublishSingleFile=true --property:PublishTrimmed=true",
      $"Expected correct publish command with advanced deployment options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Publish()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("PUBLISH_ENV", "production")
      .WithArchitecture("x64")
      .WithOperatingSystem("linux")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish test.csproj --arch x64 --os linux",
      $"Expected 'dotnet publish test.csproj --arch x64 --os linux', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestMSBuildPropertiesAndPublishingConfiguration()
  {
    string command = DotNet.Publish()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithProperty("PublishProfile", "Production")
      .WithProperty("EnvironmentName", "Staging")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithVerbosity("minimal")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish test.csproj --configuration Release --verbosity minimal --source https://api.nuget.org/v3/index.json --property:PublishProfile=Production --property:EnvironmentName=Staging",
      $"Expected correct publish command with MSBuild properties, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestPublishOverloadWithProjectParameter()
  {
    string command = DotNet.Publish("test.csproj")
      .WithConfiguration("Release")
      .WithRuntime("win-x64")
      .WithNoSelfContained()
      .WithNoBuild()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish test.csproj --configuration Release --runtime win-x64 --no-build --no-self-contained",
      $"Expected correct publish command with overload, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCommandBuilderWithNonExistentProject()
  {
    // Verify that the command builder creates a valid command even with non-existent project
    string command = DotNet.Publish()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Release")
      .WithRuntime("win-x64")
      .WithNoRestore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet publish nonexistent.csproj --configuration Release --runtime win-x64 --no-restore",
      $"Expected correct command string even for non-existent projects, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}