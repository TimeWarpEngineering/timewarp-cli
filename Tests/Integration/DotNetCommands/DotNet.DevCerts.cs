#!/usr/bin/dotnet run

await RunTests<DotNetDevCertsCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetDevCertsCommandTests

{
  public static async Task TestBasicDotNetDevCertsBuilderCreation()
  {
    // DotNet.DevCerts() alone doesn't build a valid command - needs a subcommand
    DotNetDevCertsBuilder devCertsBuilder = DotNet.DevCerts();
    
    AssertTrue(
      devCertsBuilder != null,
      "DotNet.DevCerts() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsCommand()
  {
    string command = DotNet.DevCerts().Https()
      .Build()
      .ToCommandString();
      
    AssertTrue(
      command == "dotnet dev-certs https",
      $"Expected 'dotnet dev-certs https', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithCheckOption()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithCheck()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --check",
      $"Expected 'dotnet dev-certs https --check', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithCleanOption()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithClean()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --clean",
      $"Expected 'dotnet dev-certs https --clean', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithExportOptions()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pfx")
      .WithPassword("testpassword")
      .WithFormat("Pfx")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --export --export-path ./localhost.pfx --password testpassword --format Pfx",
      $"Expected correct dev-certs export command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithTrustOption()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithTrust()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --trust",
      $"Expected 'dotnet dev-certs https --trust', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithNoPasswordOption()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pfx")
      .WithNoPassword()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --export --export-path ./localhost.pfx --no-password",
      $"Expected correct dev-certs no-password command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithVerboseAndQuietOptions()
  {
    string verboseCommand = DotNet.DevCerts().Https().WithVerbose().Build().ToCommandString();
    string quietCommand = DotNet.DevCerts().Https().WithQuiet().Build().ToCommandString();
    
    AssertTrue(
      verboseCommand == "dotnet dev-certs https --verbose",
      $"Expected 'dotnet dev-certs https --verbose', got '{verboseCommand}'"
    );
    
    AssertTrue(
      quietCommand == "dotnet dev-certs https --quiet",
      $"Expected 'dotnet dev-certs https --quiet', got '{quietCommand}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsHttpsWithPemFormat()
  {
    string command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pem")
      .WithFormat("Pem")
      .WithNoPassword()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --export --export-path ./localhost.pem --format Pem --no-password",
      $"Expected correct dev-certs PEM format command, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.DevCerts()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .Https()
      .WithCheck()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --check",
      $"Expected 'dotnet dev-certs https --check', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestDevCertsCheckCommandExecution()
  {
    // Test command string generation for check operation
    string command = DotNet.DevCerts()
      .Https()
      .WithCheck()
      .WithQuiet()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet dev-certs https --check --quiet",
      $"Expected 'dotnet dev-certs https --check --quiet', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}