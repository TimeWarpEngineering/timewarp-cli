#!/usr/bin/dotnet run

await RunTests<DotNetWorkloadCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetWorkloadCommandTests

{
  public static async Task TestBasicDotNetWorkloadBuilderCreation()
  {
    DotNetWorkloadBuilder workloadBuilder = DotNet.Workload();
    AssertTrue(workloadBuilder != null, "DotNet.Workload() should return a valid builder instance");
  }

  public static async Task TestWorkloadInfoCommand()
  {
    CommandResult command = DotNet.Workload()
      .Info()
      .Build();
    
    AssertTrue(command != null, "Workload Info command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadVersionCommand()
  {
    CommandResult command = DotNet.Workload()
      .Version()
      .Build();
    
    AssertTrue(command != null, "Workload Version command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadInstallCommand()
  {
    CommandResult command = DotNet.Workload()
      .Install("maui")
      .Build();
    
    AssertTrue(command != null, "Workload Install command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadInstallWithMultipleWorkloadsAndOptions()
  {
    CommandResult command = DotNet.Workload()
      .Install("maui", "android", "ios")
      .WithConfigFile("nuget.config")
      .WithIncludePreview()
      .WithSkipManifestUpdate()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithVersion("8.0.100")
      .Build();
    
    AssertTrue(command != null, "Workload Install with options should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadListCommand()
  {
    CommandResult command = DotNet.Workload()
      .List()
      .Build();
    
    AssertTrue(command != null, "Workload List command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadListWithVerbosity()
  {
    CommandResult command = DotNet.Workload()
      .List()
      .WithVerbosity("detailed")
      .Build();
    
    AssertTrue(command != null, "Workload List with verbosity should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadSearchCommand()
  {
    CommandResult command = DotNet.Workload()
      .Search()
      .Build();
    
    AssertTrue(command != null, "Workload Search command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadSearchWithSearchString()
  {
    CommandResult command = DotNet.Workload()
      .Search("maui")
      .WithVerbosity("minimal")
      .Build();
    
    AssertTrue(command != null, "Workload Search with search string should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadUninstallCommand()
  {
    CommandResult command = DotNet.Workload()
      .Uninstall("maui")
      .Build();
    
    AssertTrue(command != null, "Workload Uninstall command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadUninstallWithMultipleWorkloads()
  {
    CommandResult command = DotNet.Workload()
      .Uninstall("maui", "android", "ios")
      .Build();
    
    AssertTrue(command != null, "Workload Uninstall with multiple workloads should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadUpdateCommand()
  {
    CommandResult command = DotNet.Workload()
      .Update()
      .Build();
    
    AssertTrue(command != null, "Workload Update command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadUpdateWithComprehensiveOptions()
  {
    CommandResult command = DotNet.Workload()
      .Update()
      .WithAdvertisingManifestsOnly()
      .WithConfigFile("nuget.config")
      .WithDisableParallel()
      .WithFromPreviousSdk()
      .WithIncludePreview()
      .WithInteractive()
      .WithNoCache()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithTempDir("/tmp")
      .WithVerbosity("diagnostic")
      .Build();
    
    AssertTrue(command != null, "Workload Update with comprehensive options should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadRepairCommand()
  {
    CommandResult command = DotNet.Workload()
      .Repair()
      .Build();
    
    AssertTrue(command != null, "Workload Repair command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadRepairWithComprehensiveOptions()
  {
    CommandResult command = DotNet.Workload()
      .Repair()
      .WithConfigFile("nuget.config")
      .WithDisableParallel()
      .WithIgnoreFailedSources()
      .WithInteractive()
      .WithNoCache()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithTempDir("/tmp")
      .WithVerbosity("detailed")
      .Build();
    
    AssertTrue(command != null, "Workload Repair with comprehensive options should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadCleanCommand()
  {
    CommandResult command = DotNet.Workload()
      .Clean()
      .Build();
    
    AssertTrue(command != null, "Workload Clean command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadCleanWithAllOption()
  {
    CommandResult command = DotNet.Workload()
      .Clean()
      .WithAll()
      .Build();
    
    AssertTrue(command != null, "Workload Clean with --all option should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadRestoreCommand()
  {
    CommandResult command = DotNet.Workload()
      .Restore()
      .Build();
    
    AssertTrue(command != null, "Workload Restore command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadRestoreWithProject()
  {
    CommandResult command = DotNet.Workload()
      .Restore("MyApp.csproj")
      .Build();
    
    AssertTrue(command != null, "Workload Restore with project should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadRestoreWithComprehensiveOptions()
  {
    CommandResult command = DotNet.Workload()
      .Restore("MyApp.csproj")
      .WithConfigFile("nuget.config")
      .WithDisableParallel()
      .WithIncludePreview()
      .WithInteractive()
      .WithNoCache()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithTempDir("/tmp")
      .WithVerbosity("normal")
      .WithVersion("8.0.100")
      .Build();
    
    AssertTrue(command != null, "Workload Restore with comprehensive options should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadConfigCommand()
  {
    CommandResult command = DotNet.Workload()
      .Config()
      .Build();
    
    AssertTrue(command != null, "Workload Config command should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadConfigWithUpdateModeWorkloadSet()
  {
    CommandResult command = DotNet.Workload()
      .Config()
      .WithUpdateModeWorkloadSet()
      .Build();
    
    AssertTrue(command != null, "Workload Config with workload-set mode should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadConfigWithUpdateModeManifests()
  {
    CommandResult command = DotNet.Workload()
      .Config()
      .WithUpdateModeManifests()
      .Build();
    
    AssertTrue(command != null, "Workload Config with manifests mode should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadConfigWithCustomUpdateMode()
  {
    CommandResult command = DotNet.Workload()
      .Config()
      .WithUpdateMode("manifests")
      .Build();
    
    AssertTrue(command != null, "Workload Config with custom update mode should return a valid CommandResult instance");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.Workload()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build();
    
    AssertTrue(command != null, "Working directory and environment variables should return a valid CommandResult instance");
  }

  public static async Task TestWorkloadListCommandExecution()
  {
    // This should show installed workloads or handle gracefully
    string output = await DotNet.Workload()
      .List()
      .WithVerbosity("quiet")
      .GetStringAsync();
    
    // Should complete without errors (graceful handling)
    AssertTrue(output != null, "Workload List command should execute successfully");
  }

  public static async Task TestWorkloadSearchCommandExecution()
  {
    // This should show available workloads or handle gracefully
    string output = await DotNet.Workload()
      .Search("maui")
      .WithVerbosity("quiet")
      .GetStringAsync();
    
    // Should complete without errors (graceful handling)
    AssertTrue(output != null, "Workload Search command should execute successfully");
  }

  public static async Task TestWorkloadInfoCommandExecution()
  {
    // This should show workload information or handle gracefully
    string output = await DotNet.Workload()
      .Info()
      .GetStringAsync();
    
    // Should complete without errors (graceful handling)
    AssertTrue(output != null, "Workload Info command should execute successfully");
  }
}