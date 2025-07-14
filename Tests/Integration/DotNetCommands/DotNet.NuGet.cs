#!/usr/bin/dotnet run

await RunTests<DotNetNuGetTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetNuGetTests

{
  public static async Task BasicDotNetNuGetBuilderCreation()
  {
    DotNetNuGetBuilder nugetBuilder = DotNet.NuGet();
    AssertTrue(nugetBuilder != null, "DotNet.NuGet() returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetPushCommandBuilder()
  {
    CommandResult command = DotNet.NuGet()
      .Push("package.nupkg")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithApiKey("test-key")
      .WithTimeout(300)
      .Build();
    
    AssertTrue(command != null, "NuGet Push Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetDeleteCommandBuilder()
  {
    CommandResult command = DotNet.NuGet()
      .Delete("MyPackage", "1.0.0")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithApiKey("test-key")
      .WithInteractive()
      .Build();
    
    AssertTrue(command != null, "NuGet Delete Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetListSourcesCommandBuilder()
  {
    CommandResult command = DotNet.NuGet()
      .ListSources()
      .WithFormat("Detailed")
      .WithConfigFile("nuget.config")
      .Build();
    
    AssertTrue(command != null, "NuGet ListSources Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetAddSourceCommandBuilder()
  {
    CommandResult command = DotNet.NuGet()
      .AddSource("https://my-private-feed.com/v3/index.json")
      .WithName("MyPrivateFeed")
      .WithUsername("testuser")
      .WithPassword("testpass")
      .Build();
    
    AssertTrue(command != null, "NuGet AddSource Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetSourceManagementCommands()
  {
    CommandResult enableCommand = DotNet.NuGet().EnableSource("MySource").Build();
    CommandResult disableCommand = DotNet.NuGet().DisableSource("MySource").Build();
    CommandResult removeCommand = DotNet.NuGet().RemoveSource("MySource").Build();
    CommandResult updateCommand = DotNet.NuGet().UpdateSource("MySource").WithSource("https://new-url.com").Build();
    
    AssertTrue(
      enableCommand != null && disableCommand != null && removeCommand != null && updateCommand != null,
      "One or more source management commands returned null"
    );
    await Task.CompletedTask;
  }

  public static async Task NuGetLocalsCommandBuilder()
  {
    CommandResult clearCommand = DotNet.NuGet().Locals().Clear("http-cache").Build();
    CommandResult listCommand = DotNet.NuGet().Locals().List("global-packages").Build();
    
    AssertTrue(clearCommand != null && listCommand != null, "NuGet Locals commands returned null");
    await Task.CompletedTask;
  }

  public static async Task NuGetWhyCommandBuilder()
  {
    CommandResult command = DotNet.NuGet()
      .Why("Microsoft.Extensions.Logging")
      .WithProject("MyApp.csproj")
      .WithFramework("net10.0")
      .Build();
    
    AssertTrue(command != null, "NuGet Why Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task WorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.NuGet()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_ENV", "test")
      .ListSources()
      .Build();
    
    AssertTrue(command != null, "Environment config Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task CommandExecutionListSources()
  {
    // This should show configured NuGet sources
    string output = await DotNet.NuGet()
      .ListSources()
      .WithFormat("Short")
      .GetStringAsync();
    
    // Should return source information or handle gracefully
    AssertTrue(true, "NuGet ListSources command execution should not throw");
  }
}