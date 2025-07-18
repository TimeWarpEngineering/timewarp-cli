#!/usr/bin/dotnet run

await RunTests<DotNetNewTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetNewTests

{
  public static async Task TestBasicNewBuilderCreation()
  {
    string command = DotNet.New("console")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new console",
      $"Expected 'dotnet new console', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestNewWithoutTemplateName()
  {
    string command = DotNet.New()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new",
      $"Expected 'dotnet new', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFluentConfigurationMethods()
  {
    string command = DotNet.New("console")
      .WithName("TestApp")
      .WithOutput("./test-output")
      .WithForce()
      .WithDryRun()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new console --output ./test-output --name TestApp --dry-run --force",
      $"Expected correct new command with configuration, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestTemplateArgumentsAndAdvancedOptions()
  {
    string command = DotNet.New("web")
      .WithName("MyWebApp")
      .WithOutput("./web-output")
      .WithTemplateArg("--framework")
      .WithTemplateArg("net10.0")
      .WithVerbosity("detailed")
      .WithNoUpdateCheck()
      .WithDiagnostics()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new web --framework net10.0 --output ./web-output --name MyWebApp --verbosity detailed --no-update-check --diagnostics",
      $"Expected correct new command with template arguments, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.New("classlib")
      .WithName("MyLibrary")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEMPLATE_ENV", "test")
      .WithProject("test.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new classlib --name MyLibrary --project test.csproj",
      $"Expected correct new command with project option, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestNewListSubcommand()
  {
    string command = DotNet.New().List("console")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new list console",
      $"Expected 'dotnet new list console', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestNewSearchSubcommand()
  {
    string command = DotNet.New().Search("blazor")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new search blazor",
      $"Expected 'dotnet new search blazor', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestCommandExecutionWithDryRun()
  {
    // Test command string generation for dry run
    string command = DotNet.New("console")
      .WithName("TestConsoleApp")
      .WithOutput("./dry-run-test")
      .WithDryRun()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet new console --output ./dry-run-test --name TestConsoleApp --dry-run",
      $"Expected correct new command with dry run, got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}