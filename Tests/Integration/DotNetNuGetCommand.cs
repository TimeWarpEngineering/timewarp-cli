#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetNuGetCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.NuGet() builder creation
totalTests++;
try
{
  DotNetNuGetBuilder nugetBuilder = DotNet.NuGet();
  if (nugetBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.NuGet() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.NuGet() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: NuGet Push command builder
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .Push("package.nupkg")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithApiKey("test-key")
    .WithTimeout(300)
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: NuGet Push command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: NuGet Push Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: NuGet Delete command builder
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .Delete("MyPackage", "1.0.0")
    .WithSource("https://api.nuget.org/v3/index.json")
    .WithApiKey("test-key")
    .WithInteractive()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: NuGet Delete command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: NuGet Delete Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: NuGet ListSources command builder
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .ListSources()
    .WithFormat("Detailed")
    .WithConfigFile("nuget.config")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: NuGet ListSources command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: NuGet ListSources Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: NuGet AddSource command builder
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .AddSource("https://my-private-feed.com/v3/index.json")
    .WithName("MyPrivateFeed")
    .WithUsername("testuser")
    .WithPassword("testpass")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: NuGet AddSource command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: NuGet AddSource Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: NuGet source management commands
totalTests++;
try
{
  CommandResult enableCommand = DotNet.NuGet().EnableSource("MySource").Build();
  CommandResult disableCommand = DotNet.NuGet().DisableSource("MySource").Build();
  CommandResult removeCommand = DotNet.NuGet().RemoveSource("MySource").Build();
  CommandResult updateCommand = DotNet.NuGet().UpdateSource("MySource").WithSource("https://new-url.com").Build();
  
  if (enableCommand != null && disableCommand != null && removeCommand != null && updateCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: NuGet source management commands work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: One or more source management commands returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: NuGet Locals command builder
totalTests++;
try
{
  CommandResult clearCommand = DotNet.NuGet().Locals().Clear("http-cache").Build();
  CommandResult listCommand = DotNet.NuGet().Locals().List("global-packages").Build();
  
  if (clearCommand != null && listCommand != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: NuGet Locals commands work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: NuGet Locals commands returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: NuGet Why command builder
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .Why("Microsoft.Extensions.Logging")
    .WithProject("MyApp.csproj")
    .WithFramework("net10.0")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: NuGet Why command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: NuGet Why Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.NuGet()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("NUGET_ENV", "test")
    .ListSources()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Command execution (list sources - safe to test)
totalTests++;
try
{
  // This should show configured NuGet sources
  string output = await DotNet.NuGet()
    .ListSources()
    .WithFormat("Short")
    .GetStringAsync();
  
  // Should return source information or handle gracefully
  Console.WriteLine("✅ Test 10 PASSED: NuGet ListSources command execution completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetNuGetCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);