#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetUserSecretsCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.UserSecrets() builder creation
totalTests++;
try
{
  DotNetUserSecretsBuilder userSecretsBuilder = DotNet.UserSecrets();
  if (userSecretsBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.UserSecrets() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.UserSecrets() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: UserSecrets Init command
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .Init()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: UserSecrets Init command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: UserSecrets Init Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: UserSecrets Init with project
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .WithProject("MyApp.csproj")
    .Init()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: UserSecrets Init with project works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: UserSecrets Init with project Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: UserSecrets Init with ID
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .WithId("my-app-secrets")
    .Init()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: UserSecrets Init with ID works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: UserSecrets Init with ID Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: UserSecrets Set command
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .Set("ConnectionString", "Server=localhost;Database=MyApp;")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: UserSecrets Set command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: UserSecrets Set Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: UserSecrets Set with project
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .WithProject("MyApp.csproj")
    .Set("ApiKey", "secret-key-value")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: UserSecrets Set with project works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: UserSecrets Set with project Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: UserSecrets Remove command
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .Remove("ConnectionString")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: UserSecrets Remove command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: UserSecrets Remove Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: UserSecrets List command
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: UserSecrets List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: UserSecrets List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: UserSecrets Clear command
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .Clear()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: UserSecrets Clear command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: UserSecrets Clear Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: UserSecrets with all options
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .WithProject("MyApp.csproj")
    .WithId("my-app-secrets")
    .Set("DatabaseConnection", "Server=localhost;")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: UserSecrets with all options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: UserSecrets with all options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.UserSecrets()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("DOTNET_ENV", "test")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: Command execution (list - safe to test without project)
totalTests++;
try
{
  // This will fail gracefully without a project but tests the execution path
  string output = await DotNet.UserSecrets()
    .List()
    .GetStringAsync();
  
  // Should handle gracefully (return empty or error message)
  Console.WriteLine("✅ Test 12 PASSED: UserSecrets list command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetUserSecretsCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);