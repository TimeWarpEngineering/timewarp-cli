#!/usr/bin/dotnet run
#:package TimeWarp.Cli

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetBuildCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Build() builder creation
totalTests++;
try
{
  DotNetBuildBuilder buildBuilder = DotNet.Build();
  if (buildBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Build() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Build() returned null");
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
  CommandResult command = DotNet.Build()
    .WithProject("test.csproj")
    .WithConfiguration("Debug")
    .WithFramework("net10.0")
    .WithNoRestore()
    .WithOutput("bin/Debug")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Build fluent configuration methods work correctly");
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

// Test 3: Method chaining with advanced options
totalTests++;
try
{
  CommandResult chainedCommand = DotNet.Build()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithArchitecture("x64")
    .WithOperatingSystem("linux")
    .WithNoRestore()
    .WithNoDependencies()
    .WithNoIncremental()
    .WithVerbosity("minimal")
    .WithProperty("Platform", "AnyCPU")
    .Build();
  
  if (chainedCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Build method chaining works correctly");
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
  CommandResult envCommand = DotNet.Build()
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("BUILD_ENV", "test")
    .WithNoLogo()
    .Build();
  
  if (envCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Build working directory and environment variables work correctly");
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

// Test 5: MSBuild properties including no-cache options
totalTests++;
try
{
  CommandResult propsCommand = DotNet.Build()
    .WithProject("test.csproj")
    .WithConfiguration("Debug")
    .WithProperty("RestoreNoCache", "true")
    .WithProperty("DisableImplicitNuGetFallbackFolder", "true")
    .WithProperty("RestoreIgnoreFailedSources", "true")
    .Build();
  
  if (propsCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: MSBuild properties including no-cache options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Properties Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Build overload with project parameter (NEW)
totalTests++;
try
{
  CommandResult overloadCommand = DotNet.Build("test.csproj")
    .WithConfiguration("Debug")
    .WithNoRestore()
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Build overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Build overload returned null");
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
  string output = await DotNet.Build()
    .WithProject("nonexistent.csproj")
    .WithConfiguration("Debug")
    .WithNoRestore()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: Build command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetBuildCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);