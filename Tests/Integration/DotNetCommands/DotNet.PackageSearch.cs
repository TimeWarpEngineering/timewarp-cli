#!/usr/bin/dotnet run
#:package TimeWarp.Cli

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetPackageSearchCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.PackageSearch() builder creation
totalTests++;
try
{
  DotNetPackageSearchBuilder searchBuilder = DotNet.PackageSearch("TimeWarp.Cli");
  if (searchBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.PackageSearch() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.PackageSearch() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: DotNet.PackageSearch() without search term
totalTests++;
try
{
  DotNetPackageSearchBuilder searchBuilder = DotNet.PackageSearch();
  if (searchBuilder != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: DotNet.PackageSearch() without search term created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: DotNet.PackageSearch() without search term returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Fluent configuration methods
totalTests++;
try
{
  CommandResult command = DotNet.PackageSearch("Microsoft.Extensions.Logging")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithTake(5)
    .WithSkip(0)
    .WithFormat("table")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: PackageSearch fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Advanced search options
totalTests++;
try
{
  CommandResult command = DotNet.PackageSearch("Newtonsoft.Json")
    .WithExactMatch()
    .WithPrerelease()
    .WithFormat("json")
    .WithVerbosity("detailed")
    .WithInteractive()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Advanced search options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Advanced options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Multiple sources configuration
totalTests++;
try
{
  CommandResult command = DotNet.PackageSearch("TestPackage")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithSource("https://pkgs.dev.azure.com/example/feed")
    .WithTake(10)
    .WithConfigFile("nuget.config")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Multiple sources configuration works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Multiple sources Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.PackageSearch("TestPackage")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("NUGET_ENV", "test")
    .WithTake(1)
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Command execution (search for a well-known package)
totalTests++;
try
{
  // Search for a well-known package that should exist
  string output = await DotNet.PackageSearch("Microsoft.Extensions.Logging")
    .WithTake(1)
    .WithFormat("table")
    .GetStringAsync();
  
  // Should return search results
  Console.WriteLine("✅ Test 7 PASSED: PackageSearch command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Exact match search
totalTests++;
try
{
  // Search for TimeWarp.Cli with exact match and prerelease
  string output = await DotNet.PackageSearch("TimeWarp.Cli")
    .WithExactMatch()
    .WithPrerelease()
    .GetStringAsync();
  
  // Should return search results or handle gracefully
  Console.WriteLine("✅ Test 8 PASSED: Exact match search completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetPackageSearchCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);