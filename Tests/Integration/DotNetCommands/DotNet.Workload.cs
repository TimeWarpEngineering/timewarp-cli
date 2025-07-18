#!/usr/bin/dotnet run

await RunTests<DotNetWorkloadCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetWorkloadCommandTests

{
  public static async Task TestBasicDotNetWorkloadBuilderCreation()
  {
    // DotNet.Workload() alone doesn't build a valid command - needs a subcommand
    DotNetWorkloadBuilder workloadBuilder = DotNet.Workload();
    
    AssertTrue(
      workloadBuilder != null,
      "DotNet.Workload() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadInfoCommand()
  {
    string command = DotNet.Workload()
      .Info()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload --info",
      $"Expected 'dotnet workload --info', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadVersionCommand()
  {
    string command = DotNet.Workload()
      .Version()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload --version",
      $"Expected 'dotnet workload --version', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadInstallCommand()
  {
    string command = DotNet.Workload()
      .Install("maui")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload install maui",
      $"Expected 'dotnet workload install maui', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadInstallWithMultipleWorkloadsAndOptions()
  {
    string command = DotNet.Workload()
      .Install("maui", "android", "ios")
      .WithConfigFile("nuget.config")
      .WithIncludePreview()
      .WithSkipManifestUpdate()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithVersion("8.0.100")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload install maui android ios --configfile nuget.config --include-previews --skip-manifest-update --source https://api.nuget.org/v3/index.json --version 8.0.100",
      $"Expected correct workload install command with options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadListCommand()
  {
    string command = DotNet.Workload()
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload list",
      $"Expected 'dotnet workload list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadListWithVerbosity()
  {
    string command = DotNet.Workload()
      .List()
      .WithVerbosity("detailed")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload list --verbosity detailed",
      $"Expected 'dotnet workload list --verbosity detailed', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadSearchCommand()
  {
    string command = DotNet.Workload()
      .Search()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload search",
      $"Expected 'dotnet workload search', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadSearchWithSearchString()
  {
    string command = DotNet.Workload()
      .Search("maui")
      .WithVerbosity("minimal")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload search maui --verbosity minimal",
      $"Expected 'dotnet workload search maui --verbosity minimal', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadUninstallCommand()
  {
    string command = DotNet.Workload()
      .Uninstall("maui")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload uninstall maui",
      $"Expected 'dotnet workload uninstall maui', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadUninstallWithMultipleWorkloads()
  {
    string command = DotNet.Workload()
      .Uninstall("maui", "android", "ios")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload uninstall maui android ios",
      $"Expected 'dotnet workload uninstall maui android ios', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadUpdateCommand()
  {
    string command = DotNet.Workload()
      .Update()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload update",
      $"Expected 'dotnet workload update', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadUpdateWithComprehensiveOptions()
  {
    string command = DotNet.Workload()
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
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload update --advertising-manifests-only --configfile nuget.config --disable-parallel --from-previous-sdk --include-previews --interactive --no-cache --source https://api.nuget.org/v3/index.json --temp-dir /tmp --verbosity diagnostic",
      $"Expected correct workload update command with options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadRepairCommand()
  {
    string command = DotNet.Workload()
      .Repair()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload repair",
      $"Expected 'dotnet workload repair', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadRepairWithComprehensiveOptions()
  {
    string command = DotNet.Workload()
      .Repair()
      .WithConfigFile("nuget.config")
      .WithDisableParallel()
      .WithIgnoreFailedSources()
      .WithInteractive()
      .WithNoCache()
      .WithSource("https://api.nuget.org/v3/index.json")
      .WithTempDir("/tmp")
      .WithVerbosity("detailed")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload repair --configfile nuget.config --disable-parallel --ignore-failed-sources --interactive --no-cache --source https://api.nuget.org/v3/index.json --temp-dir /tmp --verbosity detailed",
      $"Expected correct workload repair command with options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadCleanCommand()
  {
    string command = DotNet.Workload()
      .Clean()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload clean",
      $"Expected 'dotnet workload clean', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadCleanWithAllOption()
  {
    string command = DotNet.Workload()
      .Clean()
      .WithAll()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload clean --all",
      $"Expected 'dotnet workload clean --all', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadRestoreCommand()
  {
    string command = DotNet.Workload()
      .Restore()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload restore",
      $"Expected 'dotnet workload restore', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadRestoreWithProject()
  {
    string command = DotNet.Workload()
      .Restore("MyApp.csproj")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload restore MyApp.csproj",
      $"Expected 'dotnet workload restore MyApp.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadRestoreWithComprehensiveOptions()
  {
    string command = DotNet.Workload()
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
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload restore MyApp.csproj --configfile nuget.config --disable-parallel --include-previews --interactive --no-cache --source https://api.nuget.org/v3/index.json --temp-dir /tmp --verbosity normal --version 8.0.100",
      $"Expected correct workload restore command with options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadConfigCommand()
  {
    string command = DotNet.Workload()
      .Config()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload config",
      $"Expected 'dotnet workload config', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadConfigWithUpdateModeWorkloadSet()
  {
    string command = DotNet.Workload()
      .Config()
      .WithUpdateModeWorkloadSet()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload config --update-mode workload-set",
      $"Expected 'dotnet workload config --update-mode workload-set', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadConfigWithUpdateModeManifests()
  {
    string command = DotNet.Workload()
      .Config()
      .WithUpdateModeManifests()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload config --update-mode manifests",
      $"Expected 'dotnet workload config --update-mode manifests', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadConfigWithCustomUpdateMode()
  {
    string command = DotNet.Workload()
      .Config()
      .WithUpdateMode("manifests")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload config --update-mode manifests",
      $"Expected 'dotnet workload config --update-mode manifests', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.Workload()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload list",
      $"Expected 'dotnet workload list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadListCommandExecution()
  {
    // Test command string generation for list operation
    string command = DotNet.Workload()
      .List()
      .WithVerbosity("quiet")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload list --verbosity quiet",
      $"Expected 'dotnet workload list --verbosity quiet', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadSearchCommandExecution()
  {
    // Test command string generation for search operation
    string command = DotNet.Workload()
      .Search("maui")
      .WithVerbosity("quiet")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload search maui --verbosity quiet",
      $"Expected 'dotnet workload search maui --verbosity quiet', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkloadInfoCommandExecution()
  {
    // Test command string generation for info operation
    string command = DotNet.Workload()
      .Info()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet workload --info",
      $"Expected 'dotnet workload --info', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}