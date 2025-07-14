#!/usr/bin/dotnet run

await RunTests<DotNetDevCertsCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetDevCertsCommandTests

{
  public static async Task TestBasicDotNetDevCertsBuilderCreation()
  {
    DotNetDevCertsBuilder devCertsBuilder = DotNet.DevCerts();
    AssertTrue(devCertsBuilder != null, "DotNet.DevCerts() should return a valid builder instance");
  }

  public static async Task TestDevCertsHttpsBuilderCreation()
  {
    DotNetDevCertsHttpsBuilder httpsBuilder = DotNet.DevCerts().Https();
    AssertTrue(httpsBuilder != null, "DotNet.DevCerts().Https() should return a valid builder instance");
  }

  public static async Task TestDevCertsHttpsWithCheckOption()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithCheck()
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with Check option should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsHttpsWithCleanOption()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithClean()
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with Clean option should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsHttpsWithExportOptions()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pfx")
      .WithPassword("testpassword")
      .WithFormat("Pfx")
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with Export options should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsHttpsWithTrustOption()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithTrust()
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with Trust option should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsHttpsWithNoPasswordOption()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pfx")
      .WithNoPassword()
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with NoPassword option should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsHttpsWithVerboseAndQuietOptions()
  {
    CommandResult verboseCommand = DotNet.DevCerts().Https().WithVerbose().Build();
    CommandResult quietCommand = DotNet.DevCerts().Https().WithQuiet().Build();
    
    AssertTrue(verboseCommand != null && quietCommand != null, 
      "DevCerts Https with Verbose and Quiet options should return valid CommandResult instances");
  }

  public static async Task TestDevCertsHttpsWithPemFormat()
  {
    CommandResult command = DotNet.DevCerts()
      .Https()
      .WithExport()
      .WithExportPath("./localhost.pem")
      .WithFormat("Pem")
      .WithNoPassword()
      .Build();
    
    AssertTrue(command != null, "DevCerts Https with PEM format should return a valid CommandResult instance");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.DevCerts()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .Https()
      .WithCheck()
      .Build();
    
    AssertTrue(command != null, "Working directory and environment variables should return a valid CommandResult instance");
  }

  public static async Task TestDevCertsCheckCommandExecution()
  {
    // This checks if a certificate exists without making changes
    string output = await DotNet.DevCerts()
      .Https()
      .WithCheck()
      .WithQuiet()
      .GetStringAsync();
    
    // Should complete without errors (graceful handling)
    AssertTrue(output != null, "DevCerts check command should execute successfully");
  }
}