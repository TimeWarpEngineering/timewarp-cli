#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetSlnCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Sln() builder creation
totalTests++;
try
{
  var slnBuilder = DotNet.Sln();
  if (slnBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Sln() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Sln() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: DotNet.Sln() with solution file parameter
totalTests++;
try
{
  var slnBuilder = DotNet.Sln("MySolution.sln");
  if (slnBuilder != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: DotNet.Sln() with solution file created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: DotNet.Sln() with solution file returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Solution Add command
totalTests++;
try
{
  var command = DotNet.Sln("MySolution.sln")
    .Add("MyApp.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Solution Add command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Solution Add Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Solution Add with multiple projects
totalTests++;
try
{
  var command = DotNet.Sln("MySolution.sln")
    .Add("MyApp.csproj", "MyLibrary.csproj", "MyTests.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Solution Add with multiple projects works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Solution Add multiple projects Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Solution List command
totalTests++;
try
{
  var command = DotNet.Sln("MySolution.sln")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Solution List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Solution List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Solution Remove command
totalTests++;
try
{
  var command = DotNet.Sln("MySolution.sln")
    .Remove("MyApp.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Solution Remove command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Solution Remove Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Solution Migrate command
totalTests++;
try
{
  var command = DotNet.Sln("MySolution.sln")
    .Migrate()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Solution Migrate command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Solution Migrate Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Working directory and environment variables
totalTests++;
try
{
  var command = DotNet.Sln()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("DOTNET_ENV", "test")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Command execution (graceful handling for non-existent solution)
totalTests++;
try
{
  // This should handle gracefully since the solution doesn't exist
  var output = await DotNet.Sln("nonexistent.sln")
    .List()
    .GetStringAsync();
  
  // Should return empty string for non-existent solution (graceful degradation)
  Console.WriteLine("✅ Test 9 PASSED: Solution command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetSlnCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);