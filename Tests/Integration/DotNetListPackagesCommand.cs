#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetListPackagesCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.ListPackages() builder creation
totalTests++;
try
{
  DotNetListPackagesBuilder listPackagesBuilder = DotNet.ListPackages();
  if (listPackagesBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.ListPackages() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.ListPackages() returned null");
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
  CommandResult command = DotNet.ListPackages()
    .WithProject("test.csproj")
    .WithFramework("net10.0")
    .WithVerbosity("minimal")
    .WithFormat("console")
    .Outdated()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: ListPackages fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: ListPackages Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Method chaining with transitive and vulnerable options
totalTests++;
try
{
  CommandResult chainedCommand = DotNet.ListPackages()
    .WithProject("test.csproj")
    .IncludeTransitive()
    .Vulnerable()
    .Deprecated()
    .WithInteractive()
    .WithSource("https://api.nuget.org/v3/index.json")
    .IncludePrerelease()
    .Build();
  
  if (chainedCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: ListPackages method chaining works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Chained ListPackages Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: JSON format and highest version options
totalTests++;
try
{
  CommandResult jsonCommand = DotNet.ListPackages()
    .WithProject("test.csproj")
    .WithFormat("json")
    .WithOutputVersion("1")
    .WithConfig("nuget.config")
    .Outdated()
    .HighestMinor()
    .HighestPatch()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("NUGET_PACKAGES", "./temp-packages")
    .Build();
  
  if (jsonCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: ListPackages JSON format and version options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: JSON format config ListPackages Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: ListPackages overload with project parameter
totalTests++;
try
{
  CommandResult overloadCommand = DotNet.ListPackages("test.csproj")
    .WithFramework("net8.0")
    .IncludeTransitive()
    .WithVerbosity("quiet")
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: ListPackages overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: ListPackages overload returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: ToListAsync method (Overview.md compatibility)
totalTests++;
try
{
  string[] packages = await DotNet.ListPackages()
    .WithProject("nonexistent.csproj")
    .IncludeTransitive()
    .Outdated()
    .ToListAsync();
  
  // Should return empty array for non-existent project (graceful degradation)
  if (packages != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: ToListAsync method works correctly (Overview.md compatibility)");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: ToListAsync returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Command execution (graceful handling for non-existent project)
totalTests++;
try
{
  // This should handle gracefully since the project doesn't exist
  string output = await DotNet.ListPackages()
    .WithProject("nonexistent.csproj")
    .Outdated()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: ListPackages command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetListPackagesCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);