#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetDevCertsCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.DevCerts() builder creation
totalTests++;
try
{
  DotNetDevCertsBuilder devCertsBuilder = DotNet.DevCerts();
  if (devCertsBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.DevCerts() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.DevCerts() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: DevCerts.Https() builder creation
totalTests++;
try
{
  DotNetDevCertsHttpsBuilder httpsBuilder = DotNet.DevCerts().Https();
  if (httpsBuilder != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: DotNet.DevCerts().Https() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: DotNet.DevCerts().Https() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: DevCerts Https with Check option
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithCheck()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: DevCerts Https with Check option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: DevCerts Https Check Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: DevCerts Https with Clean option
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithClean()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: DevCerts Https with Clean option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: DevCerts Https Clean Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: DevCerts Https with Export options
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithExport()
    .WithExportPath("./localhost.pfx")
    .WithPassword("testpassword")
    .WithFormat("Pfx")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: DevCerts Https with Export options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: DevCerts Https Export Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: DevCerts Https with Trust option
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithTrust()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: DevCerts Https with Trust option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: DevCerts Https Trust Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: DevCerts Https with NoPassword option
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithExport()
    .WithExportPath("./localhost.pfx")
    .WithNoPassword()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: DevCerts Https with NoPassword option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: DevCerts Https NoPassword Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: DevCerts Https with Verbose and Quiet options
totalTests++;
try
{
  CommandResult verboseCommand = DotNet.DevCerts().Https().WithVerbose().Build();
  CommandResult quietCommand = DotNet.DevCerts().Https().WithQuiet().Build();
  
  if (verboseCommand != null && quietCommand != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: DevCerts Https with Verbose and Quiet options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: DevCerts Https Verbose/Quiet Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: DevCerts Https with PEM format
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .Https()
    .WithExport()
    .WithExportPath("./localhost.pem")
    .WithFormat("Pem")
    .WithNoPassword()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: DevCerts Https with PEM format works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: DevCerts Https PEM Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.DevCerts()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("DOTNET_ENV", "test")
    .Https()
    .WithCheck()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Command execution (check for existing certificate - safe to test)
totalTests++;
try
{
  // This checks if a certificate exists without making changes
  string output = await DotNet.DevCerts()
    .Https()
    .WithCheck()
    .WithQuiet()
    .GetStringAsync();
  
  // Should complete without errors (graceful handling)
  Console.WriteLine("✅ Test 11 PASSED: DevCerts check command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetDevCertsCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);