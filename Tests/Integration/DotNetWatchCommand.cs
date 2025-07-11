#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetWatchCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Watch() builder creation
totalTests++;
try
{
  var watchBuilder = DotNet.Watch();
  if (watchBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Watch() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Watch() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Watch Run command
totalTests++;
try
{
  var command = DotNet.Watch()
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Watch Run command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Watch Run Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Watch Test command
totalTests++;
try
{
  var command = DotNet.Watch()
    .Test()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Watch Test command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Watch Test Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Watch Build command
totalTests++;
try
{
  var command = DotNet.Watch()
    .Build()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Watch Build command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Watch Build Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Watch with project
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithProject("MyApp.csproj")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Watch with project works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Watch with project Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Watch with basic options
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithQuiet()
    .WithVerbose()
    .WithList()
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Watch with basic options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Watch with basic options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Watch with no-options
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithNoRestore()
    .WithNoLaunchProfile()
    .WithNoHotReload()
    .WithNoBuild()
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Watch with no-options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Watch with no-options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Watch with include/exclude patterns
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithInclude("**/*.cs")
    .WithInclude("**/*.cshtml")
    .WithExclude("**/bin/**")
    .WithExclude("**/obj/**")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: Watch with include/exclude patterns works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: Watch with include/exclude patterns Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Watch with build configuration
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithConfiguration("Release")
    .WithTargetFramework("net10.0")
    .WithRuntime("linux-x64")
    .WithVerbosity("detailed")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: Watch with build configuration works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: Watch with build configuration Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Watch with properties and launch profile
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithProperty("Configuration=Debug")
    .WithProperty("Platform=x64")
    .WithLaunchProfile("Development")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: Watch with properties and launch profile works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: Watch with properties and launch profile Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Watch with additional arguments
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithArguments("--environment", "Development")
    .WithArgument("--port")
    .WithArgument("5000")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: Watch with additional arguments works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: Watch with additional arguments Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: Working directory and environment variables
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 12 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 12 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Test 13: Watch with comprehensive options
totalTests++;
try
{
  var command = DotNet.Watch()
    .WithProject("MyApp.csproj")
    .WithConfiguration("Release")
    .WithTargetFramework("net10.0")
    .WithVerbosity("minimal")
    .WithInclude("**/*.cs")
    .WithExclude("**/bin/**")
    .WithProperty("DefineConstants=RELEASE")
    .WithNoRestore()
    .WithArguments("--environment", "Production")
    .Run()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 13 PASSED: Watch with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 13 FAILED: Watch with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 13 FAILED: Exception - {ex.Message}");
}

// Test 14: Command execution (list watched files - safe to test)
totalTests++;
try
{
  // This lists watched files without actually starting the watcher
  var output = await DotNet.Watch()
    .WithList()
    .Run()
    .GetStringAsync();
  
  // Should handle gracefully (may show error message about no project)
  Console.WriteLine("✅ Test 14 PASSED: Watch list command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 14 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetWatchCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);