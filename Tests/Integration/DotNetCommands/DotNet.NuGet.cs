#!/usr/bin/dotnet run

await RunTests<DotNetNuGetTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetNuGetTests

{
  public static async Task BasicDotNetNuGetBuilderCreation()
  {
    // DotNet.NuGet() alone doesn't build a valid command - needs a subcommand
    DotNetNuGetBuilder nugetBuilder = DotNet.NuGet();
    
    AssertTrue(
      nugetBuilder != null,
      "DotNet.NuGet() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetPushCommandBuilder()
  {
    string command = DotNet.NuGet()
      .Push("package.nupkg")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithApiKey("test-key")
      .WithTimeout(300)
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget push package.nupkg --source https://api.nuget.org/v3/index.json --timeout 300 --api-key test-key",
      $"Expected correct nuget push command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetDeleteCommandBuilder()
  {
    string command = DotNet.NuGet()
      .Delete("MyPackage", "1.0.0")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithApiKey("test-key")
      .WithInteractive()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget delete MyPackage 1.0.0 --source https://api.nuget.org/v3/index.json --api-key test-key --interactive",
      $"Expected correct nuget delete command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetListSourcesCommandBuilder()
  {
    string command = DotNet.NuGet()
      .ListSources()
      .WithFormat("Detailed")
      .WithConfigFile("nuget.config")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget list source --format Detailed --configfile nuget.config",
      $"Expected correct nuget list sources command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetAddSourceCommandBuilder()
  {
    string command = DotNet.NuGet()
      .AddSource("https://my-private-feed.com/v3/index.json")
      .WithName("MyPrivateFeed")
      .WithUsername("testuser")
      .WithPassword("testpass")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget add source https://my-private-feed.com/v3/index.json --name MyPrivateFeed --username testuser --password testpass",
      $"Expected correct nuget add source command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetSourceManagementCommands()
  {
    string enableCommand = DotNet.NuGet().EnableSource("MySource").Build().ToCommandString();
    string disableCommand = DotNet.NuGet().DisableSource("MySource").Build().ToCommandString();
    string removeCommand = DotNet.NuGet().RemoveSource("MySource").Build().ToCommandString();
    string updateCommand = DotNet.NuGet().UpdateSource("MySource").WithSource("https://new-url.com").Build().ToCommandString();
    
    AssertTrue(
      enableCommand == "dotnet nuget enable source MySource",
      $"Expected 'dotnet nuget enable source MySource', got '{enableCommand}'"
    );
    
    AssertTrue(
      disableCommand == "dotnet nuget disable source MySource",
      $"Expected 'dotnet nuget disable source MySource', got '{disableCommand}'"
    );
    
    AssertTrue(
      removeCommand == "dotnet nuget remove source MySource",
      $"Expected 'dotnet nuget remove source MySource', got '{removeCommand}'"
    );
    
    AssertTrue(
      updateCommand == "dotnet nuget update source MySource --source https://new-url.com",
      $"Expected 'dotnet nuget update source MySource --source https://new-url.com', got '{updateCommand}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetLocalsCommandBuilder()
  {
    string clearCommand = DotNet.NuGet().Locals().Clear(NuGetCacheType.HttpCache).Build().ToCommandString();
    string listCommand = DotNet.NuGet().Locals().List(NuGetCacheType.GlobalPackages).Build().ToCommandString();
    
    AssertTrue(
      clearCommand == "dotnet nuget locals http-cache --clear",
      $"Expected 'dotnet nuget locals http-cache --clear', got '{clearCommand}'"
    );
    
    AssertTrue(
      listCommand == "dotnet nuget locals global-packages --list",
      $"Expected 'dotnet nuget locals global-packages --list', got '{listCommand}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task NuGetWhyCommandBuilder()
  {
    string command = DotNet.NuGet()
      .Why("Microsoft.Extensions.Logging")
      .WithProject("MyApp.csproj")
      .WithFramework("net10.0")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget why --project MyApp.csproj --framework net10.0 Microsoft.Extensions.Logging",
      $"Expected correct nuget why command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task WorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.NuGet()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_ENV", "test")
      .ListSources()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget list source",
      $"Expected 'dotnet nuget list source', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task CommandExecutionListSources()
  {
    // Test command string generation for list sources
    string command = DotNet.NuGet()
      .ListSources()
      .WithFormat("Short")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet nuget list source --format Short",
      $"Expected 'dotnet nuget list source --format Short', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}