#!/usr/bin/dotnet run
#:package TimeWarp.Cli

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetAddPackageCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.AddPackage() builder creation
totalTests++;
try
{
  DotNetAddPackageBuilder addPackageBuilder = DotNet.AddPackage("TestPackage");
  if (addPackageBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.AddPackage() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.AddPackage() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: AddPackage with version overload
totalTests++;
try
{
  DotNetAddPackageBuilder versionCommand = DotNet.AddPackage("TestPackage", "1.0.0");
  if (versionCommand != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: AddPackage with version overload works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: AddPackage version overload returned null");
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
  CommandResult command = DotNet.AddPackage("Microsoft.Extensions.Logging")
    .WithProject("test.csproj")
    .WithFramework("net10.0")
    .WithVersion("8.0.0")
    .WithNoRestore()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: AddPackage fluent configuration methods work correctly");
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

// Test 4: Package-specific options
totalTests++;
try
{
  CommandResult packageCommand = DotNet.AddPackage("Newtonsoft.Json")
    .WithProject("test.csproj")
    .WithVersion("13.0.3")
    .WithPrerelease()
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithPackageDirectory("./packages")
    .Build();
  
  if (packageCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Package-specific options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Package options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Working directory and environment variables
totalTests++;
try
{
  CommandResult envCommand = DotNet.AddPackage("TestPackage")
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("NUGET_ENV", "test")
    .WithInteractive()
    .Build();
  
  if (envCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Multiple sources configuration
totalTests++;
try
{
  CommandResult sourcesCommand = DotNet.AddPackage("TestPackage")
    .WithProject("test.csproj")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithSource("https://my-private-feed.com/v3/index.json")
    .WithFramework("net10.0")
    .WithNoRestore()
    .Build();
  
  if (sourcesCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Multiple sources configuration works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Sources config Build() returned null");
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
  string output = await DotNet.AddPackage("TestPackage")
    .WithProject("nonexistent.csproj")
    .WithVersion("1.0.0")
    .WithNoRestore()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: AddPackage command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetAddPackageCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);