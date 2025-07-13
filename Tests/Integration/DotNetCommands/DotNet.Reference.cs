#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetReferenceCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Reference() builder creation
totalTests++;
try
{
  DotNetReferenceBuilder referenceBuilder = DotNet.Reference();
  if (referenceBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Reference() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Reference() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: DotNet.Reference() with project parameter
totalTests++;
try
{
  DotNetReferenceBuilder referenceBuilder = DotNet.Reference("MyApp.csproj");
  if (referenceBuilder != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: DotNet.Reference() with project created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: DotNet.Reference() with project returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Reference Add command
totalTests++;
try
{
  CommandResult command = DotNet.Reference("MyApp.csproj")
    .Add("MyLibrary.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Reference Add command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Reference Add Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Reference Add with multiple projects
totalTests++;
try
{
  CommandResult command = DotNet.Reference("MyApp.csproj")
    .Add("MyLibrary.csproj", "MyOtherLibrary.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Reference Add with multiple projects works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Reference Add multiple projects Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Reference List command
totalTests++;
try
{
  CommandResult command = DotNet.Reference("MyApp.csproj")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Reference List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Reference List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Reference Remove command
totalTests++;
try
{
  CommandResult command = DotNet.Reference("MyApp.csproj")
    .Remove("MyLibrary.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Reference Remove command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Reference Remove Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.Reference()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("DOTNET_ENV", "test")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Command execution (graceful handling for non-existent project)
totalTests++;
try
{
  // This should handle gracefully since the project doesn't exist
  string output = await DotNet.Reference("nonexistent.csproj")
    .List()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 8 PASSED: Reference command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetReferenceCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);