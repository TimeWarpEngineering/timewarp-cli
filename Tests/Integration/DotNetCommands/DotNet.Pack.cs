#!/usr/bin/dotnet run

await RunTests<DotNetPackTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
internal sealed class DotNetPackTests
{
  public static async Task TestBasicBuilderCreation()
  {
    DotNetPackBuilder packBuilder = DotNet.Pack();
    AssertTrue(packBuilder != null, "DotNet.Pack() should return a non-null builder");
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.Pack()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithFramework("net10.0")
      .WithRuntime("win-x64")
      .WithOutput("./packages")
      .WithNoRestore()
      .Build();
    
    AssertTrue(command != null, "Pack fluent configuration should build successfully");
  }

  public static async Task TestPackageSpecificOptions()
  {
    CommandResult packageCommand = DotNet.Pack()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithVersionSuffix("beta")
      .IncludeSymbols()
      .IncludeSource()
      .WithServiceable()
      .WithNoLogo()
      .Build();
    
    AssertTrue(packageCommand != null, "Package-specific options should work correctly");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult envCommand = DotNet.Pack()
      .WithProject("test.csproj")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("PACK_ENV", "production")
      .WithVerbosity("detailed")
      .WithTerminalLogger("on")
      .Build();
    
    AssertTrue(envCommand != null, "Working directory and environment variables should work correctly");
  }

  public static async Task TestMSBuildPropertiesAndSources()
  {
    CommandResult propsCommand = DotNet.Pack()
      .WithProject("test.csproj")
      .WithConfiguration("Release")
      .WithProperty("PackageVersion", "1.0.0")
      .WithProperty("PackageDescription", "Test package")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithNoBuild()
      .WithForce()
      .Build();
    
    AssertTrue(propsCommand != null, "MSBuild properties and sources should work correctly");
  }

  public static async Task TestPackOverloadWithProjectParameter()
  {
    CommandResult overloadCommand = DotNet.Pack("test.csproj")
      .WithConfiguration("Release")
      .WithOutput("./dist")
      .WithVersionSuffix("rc1")
      .WithNoDependencies()
      .Build();
    
    AssertTrue(overloadCommand != null, "Pack overload with project parameter should work correctly");
  }

  public static async Task TestCommandBuilderWithNonExistentProject()
  {
    // Verify that the command builder creates a valid command even with non-existent project
    CommandResult command = DotNet.Pack()
      .WithProject("nonexistent.csproj")
      .WithConfiguration("Release")
      .WithOutput("./packages")
      .WithNoRestore()
      .Build();
    
    AssertTrue(command != null, "Pack command builder should create a valid command");
  }
}