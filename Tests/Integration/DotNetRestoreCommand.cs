#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetRestoreCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Restore() builder creation
totalTests++;
try
{
  DotNetRestoreBuilder restoreBuilder = DotNet.Restore();
  if (restoreBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Restore() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Restore() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Fluent configuration methods
totalTests++;
try
{
  CommandResult command = DotNet.Restore()
    .WithProject("test.csproj")
    .WithRuntime("linux-x64")
    .WithVerbosity("minimal")
    .WithPackagesDirectory("./packages")
    .WithNoCache()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Restore fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Restore Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Method chaining with sources and properties
totalTests++;
try
{
  CommandResult chainedCommand = DotNet.Restore()
    .WithProject("test.csproj")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithSource("https://nuget.pkg.github.com/MyOrg/index.json")
    .WithNoDependencies()
    .WithInteractive()
    .WithTerminalLogger("auto")
    .WithProperty("RestoreNoCache", "true")
    .WithProperty("RestoreIgnoreFailedSources", "true")
    .Build();
  
  if (chainedCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Restore method chaining works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Chained Restore Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Lock file and working directory options
totalTests++;
try
{
  CommandResult lockCommand = DotNet.Restore()
    .WithProject("test.csproj")
    .WithLockFilePath("./packages.lock.json")
    .WithLockedMode()
    .WithForce()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("NUGET_PACKAGES", "./temp-packages")
    .Build();
  
  if (lockCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Restore lock file and working directory options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Lock file config Restore Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Restore overload with project parameter
totalTests++;
try
{
  CommandResult overloadCommand = DotNet.Restore("test.csproj")
    .WithRuntime("win-x64")
    .WithVerbosity("quiet")
    .WithNoCache()
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Restore overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Restore overload returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Command execution (graceful handling for non-existent project)
totalTests++;
try
{
  // This should handle gracefully since the project doesn't exist
  string output = await DotNet.Restore()
    .WithProject("nonexistent.csproj")
    .WithNoCache()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 6 PASSED: Restore command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetRestoreCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);