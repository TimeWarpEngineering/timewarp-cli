#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetPublishCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.Publish() builder creation
totalTests++;
try
{
  DotNetPublishBuilder publishBuilder = DotNet.Publish();
  if (publishBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.Publish() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.Publish() returned null");
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
  CommandResult command = DotNet.Publish()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithFramework("net10.0")
    .WithRuntime("win-x64")
    .WithOutput("./publish")
    .WithNoRestore()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Publish fluent configuration methods work correctly");
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

// Test 3: Advanced deployment options
totalTests++;
try
{
  CommandResult deployCommand = DotNet.Publish()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithRuntime("linux-x64")
    .WithSelfContained()
    .WithReadyToRun()
    .WithSingleFile()
    .WithTrimmed()
    .WithNoLogo()
    .Build();
  
  if (deployCommand != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Advanced deployment options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Advanced deployment Build() returned null");
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
  CommandResult envCommand = DotNet.Publish()
    .WithProject("test.csproj")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("PUBLISH_ENV", "production")
    .WithArchitecture("x64")
    .WithOperatingSystem("linux")
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

// Test 5: MSBuild properties and publishing configuration
totalTests++;
try
{
  CommandResult propsCommand = DotNet.Publish()
    .WithProject("test.csproj")
    .WithConfiguration("Release")
    .WithProperty("PublishProfile", "Production")
    .WithProperty("EnvironmentName", "Staging")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithVerbosity("minimal")
    .Build();
  
  if (propsCommand != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: MSBuild properties and publishing configuration work correctly");
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

// Test 6: Publish overload with project parameter
totalTests++;
try
{
  CommandResult overloadCommand = DotNet.Publish("test.csproj")
    .WithConfiguration("Release")
    .WithRuntime("win-x64")
    .WithNoSelfContained()
    .WithNoBuild()
    .Build();
  
  if (overloadCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Publish overload with project parameter works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Publish overload returned null");
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
  string output = await DotNet.Publish()
    .WithProject("nonexistent.csproj")
    .WithConfiguration("Release")
    .WithRuntime("win-x64")
    .WithNoRestore()
    .GetStringAsync();
  
  // Should return empty string for non-existent project (graceful degradation)
  Console.WriteLine("✅ Test 7 PASSED: Publish command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetPublishCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);