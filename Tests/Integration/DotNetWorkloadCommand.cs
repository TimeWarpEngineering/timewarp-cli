#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetWorkloadCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Workload() builder creation
totalTests++;
try
{
  DotNetWorkloadBuilder workloadBuilder = DotNet.Workload();
  if (workloadBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Workload() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Workload() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Workload Info command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Info()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Workload Info command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Workload Info Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Workload Version command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Version()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Workload Version command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Workload Version Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Workload Install command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Install("maui")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Workload Install command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Workload Install Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Workload Install with multiple workloads and options
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Install("maui", "android", "ios")
    .WithConfigFile("nuget.config")
    .WithIncludePreview()
    .WithSkipManifestUpdate()
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithVersion("8.0.100")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Workload Install with options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Workload Install with options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Workload List command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Workload List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Workload List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Workload List with verbosity
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .List()
    .WithVerbosity("detailed")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Workload List with verbosity works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Workload List with verbosity Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Workload Search command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Search()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: Workload Search command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: Workload Search Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Workload Search with search string
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Search("maui")
    .WithVerbosity("minimal")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: Workload Search with search string works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: Workload Search with search string Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Workload Uninstall command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Uninstall("maui")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: Workload Uninstall command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: Workload Uninstall Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Workload Uninstall with multiple workloads
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Uninstall("maui", "android", "ios")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: Workload Uninstall with multiple workloads works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: Workload Uninstall with multiple workloads Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: Workload Update command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Update()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 12 PASSED: Workload Update command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 12 FAILED: Workload Update Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Test 13: Workload Update with comprehensive options
totalTests++;
try
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
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 13 PASSED: Workload Update with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 13 FAILED: Workload Update with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 13 FAILED: Exception - {ex.Message}");
}

// Test 14: Workload Repair command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Repair()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 14 PASSED: Workload Repair command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 14 FAILED: Workload Repair Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 14 FAILED: Exception - {ex.Message}");
}

// Test 15: Workload Repair with comprehensive options
totalTests++;
try
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
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 15 PASSED: Workload Repair with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 15 FAILED: Workload Repair with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 15 FAILED: Exception - {ex.Message}");
}

// Test 16: Workload Clean command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Clean()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 16 PASSED: Workload Clean command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 16 FAILED: Workload Clean Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 16 FAILED: Exception - {ex.Message}");
}

// Test 17: Workload Clean with --all option
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Clean()
    .WithAll()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 17 PASSED: Workload Clean with --all option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 17 FAILED: Workload Clean with --all option Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 17 FAILED: Exception - {ex.Message}");
}

// Test 18: Workload Restore command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Restore()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 18 PASSED: Workload Restore command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 18 FAILED: Workload Restore Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 18 FAILED: Exception - {ex.Message}");
}

// Test 19: Workload Restore with project
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Restore("MyApp.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 19 PASSED: Workload Restore with project works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 19 FAILED: Workload Restore with project Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 19 FAILED: Exception - {ex.Message}");
}

// Test 20: Workload Restore with comprehensive options
totalTests++;
try
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
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 20 PASSED: Workload Restore with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 20 FAILED: Workload Restore with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 20 FAILED: Exception - {ex.Message}");
}

// Test 21: Workload Config command
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Config()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 21 PASSED: Workload Config command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 21 FAILED: Workload Config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 21 FAILED: Exception - {ex.Message}");
}

// Test 22: Workload Config with update mode workload-set
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Config()
    .WithUpdateModeWorkloadSet()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 22 PASSED: Workload Config with workload-set mode works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 22 FAILED: Workload Config with workload-set mode Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 22 FAILED: Exception - {ex.Message}");
}

// Test 23: Workload Config with update mode manifests
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Config()
    .WithUpdateModeManifests()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 23 PASSED: Workload Config with manifests mode works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 23 FAILED: Workload Config with manifests mode Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 23 FAILED: Exception - {ex.Message}");
}

// Test 24: Workload Config with custom update mode
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .Config()
    .WithUpdateMode("manifests")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 24 PASSED: Workload Config with custom update mode works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 24 FAILED: Workload Config with custom update mode Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 24 FAILED: Exception - {ex.Message}");
}

// Test 25: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.Workload()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("DOTNET_ENV", "test")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 25 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 25 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 25 FAILED: Exception - {ex.Message}");
}

// Test 26: Command execution (list workloads - safe to test)
totalTests++;
try
{
  // This should show installed workloads or handle gracefully
  string output = await DotNet.Workload()
    .List()
    .WithVerbosity("quiet")
    .GetStringAsync();
  
  // Should complete without errors (graceful handling)
  Console.WriteLine("✅ Test 26 PASSED: Workload List command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 26 FAILED: Exception - {ex.Message}");
}

// Test 27: Command execution (search workloads - safe to test)
totalTests++;
try
{
  // This should show available workloads or handle gracefully
  string output = await DotNet.Workload()
    .Search("maui")
    .WithVerbosity("quiet")
    .GetStringAsync();
  
  // Should complete without errors (graceful handling)
  Console.WriteLine("✅ Test 27 PASSED: Workload Search command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 27 FAILED: Exception - {ex.Message}");
}

// Test 28: Command execution (workload info - safe to test)
totalTests++;
try
{
  // This should show workload information or handle gracefully
  string output = await DotNet.Workload()
    .Info()
    .GetStringAsync();
  
  // Should complete without errors (graceful handling)
  Console.WriteLine("✅ Test 28 PASSED: Workload Info command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 28 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetWorkloadCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);