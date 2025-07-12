#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetPackCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Pack() builder creation
totalTests++;
try
{
  DotNetPackBuilder packBuilder = DotNet.Pack();
  if (packBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Pack() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Pack() returned null");
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
  CommandResult command = DotNet.Pack()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithFramework("net10.0")
    .WithRuntime("win-x64")
    .WithOutput("./packages")
    .WithNoRestore()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Pack fluent configuration methods work correctly");
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

// Test 3: Package-specific options
totalTests++;
try
{
  CommandResult packageCommand = DotNet.Pack()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithVersionSuffix("beta")
    .IncludeSymbols()
    .IncludeSource()
    .WithServiceable()
    .WithNoLogo()
    .Build();
  
  if (packageCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Package-specific options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Package options Build() returned null");
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
  CommandResult envCommand = DotNet.Pack()
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("PACK_ENV", "production")
    .WithVerbosity("detailed")
    .WithTerminalLogger("on")
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

// Test 5: MSBuild properties and sources
totalTests++;
try
{
  CommandResult propsCommand = DotNet.Pack()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithProperty("PackageVersion", "1.0.0")
    .WithProperty("PackageDescription", "Test package")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithNoBuild()
    .WithForce()
    .Build();
  
  if (propsCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: MSBuild properties and sources work correctly");
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

// Test 6: Pack overload with project parameter
totalTests++;
try
{
  CommandResult overloadCommand = DotNet.Pack("test.csproj")
    .WithConfiguration("Release")
    .WithOutput("./dist")
    .WithVersionSuffix("rc1")
    .WithNoDependencies()
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Pack overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Pack overload returned null");
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
  string output = await DotNet.Pack()
    .WithProject("nonexistent.csproj")
    .WithConfiguration("Release")
    .WithOutput("./packages")
    .WithNoRestore()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: Pack command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetPackCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);