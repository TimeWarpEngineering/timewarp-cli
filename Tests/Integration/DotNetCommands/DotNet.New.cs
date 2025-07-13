#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing DotNetNewCommand...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic DotNet.New() builder creation
totalTests++;
try
{
  DotNetNewBuilder newBuilder = DotNet.New("console");
  if (newBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: DotNet.New() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: DotNet.New() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: DotNet.New() without template name
totalTests++;
try
{
  DotNetNewBuilder newBuilder = DotNet.New();
  if (newBuilder != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: DotNet.New() without template created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: DotNet.New() without template returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Fluent configuration methods
totalTests++;
try
{
  CommandResult command = DotNet.New("console")
    .WithName("TestApp")
    .WithOutput("./test-output")
    .WithForce()
    .WithDryRun()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: New fluent configuration methods work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Template arguments and advanced options
totalTests++;
try
{
  CommandResult command = DotNet.New("web")
    .WithName("MyWebApp")
    .WithOutput("./web-output")
    .WithTemplateArg("--framework")
    .WithTemplateArg("net10.0")
    .WithVerbosity("detailed")
    .WithNoUpdateCheck()
    .WithDiagnostics()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Template arguments and advanced options work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Advanced options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Working directory and environment variables
totalTests++;
try
{
  CommandResult command = DotNet.New("classlib")
    .WithName("MyLibrary")
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("TEMPLATE_ENV", "test")
    .WithProject("test.csproj")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Working directory and environment variables work correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Environment config Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Subcommands - List
totalTests++;
try
{
  DotNetNewListBuilder listCommand = DotNet.New().List("console");
  if (listCommand != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: New List() subcommand works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: List() subcommand returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Subcommands - Search
totalTests++;
try
{
  DotNetNewSearchBuilder searchCommand = DotNet.New().Search("blazor");
  if (searchCommand != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: New Search() subcommand works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Search() subcommand returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Command execution with dry run (safe to test)
totalTests++;
try
{
  // This should show what would happen without actually creating files
  string output = await DotNet.New("console")
    .WithName("TestConsoleApp")
    .WithOutput("./dry-run-test")
    .WithDryRun()
    .GetStringAsync();
  
  // Should return output showing what would be created
  Console.WriteLine("✅ Test 8 PASSED: New command execution with dry run completed successfully");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 DotNetNewCommand Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);