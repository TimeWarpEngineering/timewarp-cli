#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetFluentApi...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Run() builder creation
totalTests++;
try
{
  DotNetRunBuilder runBuilder = DotNet.Run();
  if (runBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Run() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Run() returned null");
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
  CommandResult command = DotNet.Run()
    .WithProject("test.csproj")
    .WithConfiguration("Debug")
    .WithFramework("net10.0")
    .WithNoRestore()
    .WithArguments("--help")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Method chaining
totalTests++;
try
{
  CommandResult chainedCommand = DotNet.Run()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithNoRestore()
    .WithNoBuild()
    .WithVerbosity("minimal")
    .WithArguments("arg1", "arg2")
    .Build();
  
  if (chainedCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Method chaining works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Chained Build() returned null");
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
  CommandResult envCommand = DotNet.Run()
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("TEST_VAR", "test_value")
    .Build();
  
  if (envCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 6: New extended options
totalTests++;
try
{
  CommandResult extendedCommand = DotNet.Run()
    .WithProject("test.csproj")
    .WithArchitecture("x64")
    .WithOperatingSystem("linux")
    .WithLaunchProfile("Development")
    .WithForce()
    .WithInteractive()
    .WithTerminalLogger("auto")
    .WithProperty("Configuration", "Debug")
    .WithProperty("Platform", "AnyCPU")
    .WithProcessEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development")
    .WithNoLaunchProfile()
    .Build();
  
  if (extendedCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Extended options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Extended options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 5: Command execution (graceful handling for non-existent project)
totalTests++;
try
{
  // This should handle gracefully since the project doesn't exist
  string output = await DotNet.Run()
    .WithProject("nonexistent.csproj")
    .WithConfiguration("Debug")
    .WithNoRestore()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 5 PASSED: Command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetFluentApi Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);