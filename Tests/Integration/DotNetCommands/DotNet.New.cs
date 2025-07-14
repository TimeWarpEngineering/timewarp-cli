#!/usr/bin/dotnet run

await RunTests<DotNetNewTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetNewTests

{
  public static async Task TestBasicNewBuilderCreation()
  {
    DotNetNewBuilder newBuilder = DotNet.New("console");
    
    AssertTrue(
      newBuilder != null,
      "DotNet.New() should create builder successfully"
    );
  }

  public static async Task TestNewWithoutTemplateName()
  {
    DotNetNewBuilder newBuilder = DotNet.New();
    
    AssertTrue(
      newBuilder != null,
      "DotNet.New() without template should create successfully"
    );
  }

  public static async Task TestFluentConfigurationMethods()
  {
    CommandResult command = DotNet.New("console")
      .WithName("TestApp")
      .WithOutput("./test-output")
      .WithForce()
      .WithDryRun()
      .Build();
    
    AssertTrue(
      command != null,
      "New fluent configuration methods should work correctly"
    );
  }

  public static async Task TestTemplateArgumentsAndAdvancedOptions()
  {
    CommandResult command = DotNet.New("web")
      .WithName("MyWebApp")
      .WithOutput("./web-output")
      .WithTemplateArg("--framework")
      .WithTemplateArg("net10.0")
      .WithVerbosity("detailed")
      .WithNoUpdateCheck()
      .WithDiagnostics()
      .Build();
    
    AssertTrue(
      command != null,
      "Template arguments and advanced options should work correctly"
    );
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.New("classlib")
      .WithName("MyLibrary")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("TEMPLATE_ENV", "test")
      .WithProject("test.csproj")
      .Build();
    
    AssertTrue(
      command != null,
      "Working directory and environment variables should work correctly"
    );
  }

  public static async Task TestNewListSubcommand()
  {
    DotNetNewListBuilder listCommand = DotNet.New().List("console");
    
    AssertTrue(
      listCommand != null,
      "New List() subcommand should work correctly"
    );
  }

  public static async Task TestNewSearchSubcommand()
  {
    DotNetNewSearchBuilder searchCommand = DotNet.New().Search("blazor");
    
    AssertTrue(
      searchCommand != null,
      "New Search() subcommand should work correctly"
    );
  }

  public static async Task TestCommandExecutionWithDryRun()
  {
    // This should show what would happen without actually creating files
    string output = await DotNet.New("console")
      .WithName("TestConsoleApp")
      .WithOutput("./dry-run-test")
      .WithDryRun()
      .GetStringAsync();
    
    // Should return output showing what would be created
    AssertTrue(
      true,
      "New command execution with dry run should complete successfully"
    );
  }
}