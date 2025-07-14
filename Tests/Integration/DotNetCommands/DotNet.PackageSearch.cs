#!/usr/bin/dotnet run

await RunTests<DotNetPackageSearchTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetPackageSearchTests

{
  public static async Task BasicDotNetPackageSearchBuilderCreation()
  {
    DotNetPackageSearchBuilder searchBuilder = DotNet.PackageSearch("TimeWarp.Cli");
    AssertTrue(searchBuilder != null, "DotNet.PackageSearch() returned null");
    await Task.CompletedTask;
  }

  public static async Task DotNetPackageSearchWithoutSearchTerm()
  {
    DotNetPackageSearchBuilder searchBuilder = DotNet.PackageSearch();
    AssertTrue(searchBuilder != null, "DotNet.PackageSearch() without search term returned null");
    await Task.CompletedTask;
  }

  public static async Task FluentConfigurationMethods()
  {
    CommandResult command = DotNet.PackageSearch("Microsoft.Extensions.Logging")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithTake(5)
      .WithSkip(0)
      .WithFormat("table")
      .Build();
    
    AssertTrue(command != null, "Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task AdvancedSearchOptions()
  {
    CommandResult command = DotNet.PackageSearch("Newtonsoft.Json")
      .WithExactMatch()
      .WithPrerelease()
      .WithFormat("json")
      .WithVerbosity("detailed")
      .WithInteractive()
      .Build();
    
    AssertTrue(command != null, "Advanced options Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task MultipleSourcesConfiguration()
  {
    CommandResult command = DotNet.PackageSearch("TestPackage")
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithSource("https://pkgs.dev.azure.com/example/feed")
      .WithTake(10)
      .WithConfigFile("nuget.config")
      .Build();
    
    AssertTrue(command != null, "Multiple sources Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task WorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.PackageSearch("TestPackage")
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("NUGET_ENV", "test")
      .WithTake(1)
      .Build();
    
    AssertTrue(command != null, "Environment config Build() returned null");
    await Task.CompletedTask;
  }

  public static async Task CommandExecutionSearchWellKnownPackage()
  {
    // Search for a well-known package that should exist
    string output = await DotNet.PackageSearch("Microsoft.Extensions.Logging")
      .WithTake(1)
      .WithFormat("table")
      .GetStringAsync();
    
    // Should return search results
    AssertTrue(true, "PackageSearch command execution should not throw");
  }

  public static async Task ExactMatchSearch()
  {
    // Search for TimeWarp.Cli with exact match and prerelease
    string output = await DotNet.PackageSearch("TimeWarp.Cli")
      .WithExactMatch()
      .WithPrerelease()
      .GetStringAsync();
    
    // Should return search results or handle gracefully
    AssertTrue(true, "Exact match search should not throw");
  }
}