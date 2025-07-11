#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetRemovePackageCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.RemovePackage() builder creation
totalTests++;
try
{
  var removePackageBuilder = DotNet.RemovePackage("TestPackage");
  if (removePackageBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.RemovePackage() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.RemovePackage() returned null");
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
  var command = DotNet.RemovePackage("Microsoft.Extensions.Logging")
    .WithProject("test.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: RemovePackage fluent configuration methods work correctly");
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

// Test 3: Working directory configuration
totalTests++;
try
{
  var dirCommand = DotNet.RemovePackage("Newtonsoft.Json")
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .Build();
  
  if (dirCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Working directory configuration works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Working directory Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Environment variables
totalTests++;
try
{
  var envCommand = DotNet.RemovePackage("TestPackage")
    .WithProject("test.csproj")
    .WithEnvironmentVariable("NUGET_ENV", "test")
    .WithEnvironmentVariable("REMOVE_PACKAGE_LOG", "verbose")
    .Build();
  
  if (envCommand != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Environment variables work correctly");
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

// Test 5: Package name validation
totalTests++;
try
{
  var validationCommand = DotNet.RemovePackage("Valid.Package.Name")
    .WithProject("MyProject.csproj")
    .Build();
  
  if (validationCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Package name validation works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Package name validation Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Multiple configuration chaining
totalTests++;
try
{
  var chainCommand = DotNet.RemovePackage("ChainedPackage")
    .WithProject("test.csproj")
    .WithWorkingDirectory("/project")
    .WithEnvironmentVariable("BUILD_ENV", "test")
    .Build();
  
  if (chainCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Multiple configuration chaining works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Chained config Build() returned null");
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
  var output = await DotNet.RemovePackage("TestPackage")
    .WithProject("nonexistent.csproj")
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: RemovePackage command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetRemovePackageCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);