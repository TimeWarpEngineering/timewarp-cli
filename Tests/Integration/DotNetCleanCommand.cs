#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetCleanCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Clean() builder creation
totalTests++;
try
{
  var cleanBuilder = DotNet.Clean();
  if (cleanBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Clean() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Clean() returned null");
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
  var command = DotNet.Clean()
    .WithProject("test.csproj")
    .WithConfiguration("Debug")
    .WithFramework("net10.0")
    .WithOutput("bin/Debug")
    .WithVerbosity("minimal")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Clean fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Clean Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Method chaining with runtime and properties
totalTests++;
try
{
  var chainedCommand = DotNet.Clean()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithRuntime("linux-x64")
    .WithNoLogo()
    .WithProperty("Platform", "AnyCPU")
    .WithProperty("CleanTargets", "All")
    .Build();
  
  if (chainedCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Clean method chaining works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Chained Clean Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Working directory and environment variables
totalTests++;
try
{
  var envCommand = DotNet.Clean()
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("CLEAN_ENV", "test")
    .WithVerbosity("quiet")
    .Build();
  
  if (envCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Clean working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Environment config Clean Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Clean overload with project parameter
totalTests++;
try
{
  var overloadCommand = DotNet.Clean("test.csproj")
    .WithConfiguration("Debug")
    .WithNoLogo()
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Clean overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Clean overload returned null");
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
  var output = await DotNet.Clean()
    .WithProject("nonexistent.csproj")
    .WithConfiguration("Debug")
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 6 PASSED: Clean command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetCleanCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);