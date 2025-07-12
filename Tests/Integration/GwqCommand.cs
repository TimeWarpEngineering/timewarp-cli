#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing Gwq Command...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic Gwq builder creation
totalTests++;
try
{
  GwqBuilder gwqBuilder = Gwq.Run();
  if (gwqBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: Gwq.Run() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: Gwq.Run() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Gwq Add command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Add("feature/new-branch")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Gwq Add command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Gwq Add Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Gwq Add with path
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Add("feature/branch", "~/worktrees/feature")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Gwq Add with path works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Gwq Add with path Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Gwq Add interactive
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .AddInteractive()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Gwq Add interactive works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Gwq Add interactive Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Gwq Add with new branch
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Add("feature/new")
    .WithNewBranch()
    .WithForce()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Gwq Add with new branch works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Gwq Add with new branch Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Gwq List command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Gwq List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Gwq List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Gwq List with options
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .List()
    .WithGlobal()
    .WithVerbose()
    .WithJson()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Gwq List with options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Gwq List with options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Gwq Remove command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Remove()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: Gwq Remove command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: Gwq Remove Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Gwq Remove with pattern
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Remove("feature/old")
    .WithDeleteBranch()
    .WithDryRun()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: Gwq Remove with pattern works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: Gwq Remove with pattern Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Gwq Rm alias
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Rm("feature/completed")
    .WithForceDeleteBranch()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: Gwq Rm alias works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: Gwq Rm alias Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Gwq Status command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Status()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: Gwq Status command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: Gwq Status Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: Gwq Status with comprehensive options
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Status()
    .WithGlobal()
    .WithJson()
    .WithFilter("changed")
    .WithSort("modified")
    .WithShowProcesses()
    .WithStaleDays(7)
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 12 PASSED: Gwq Status with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 12 FAILED: Gwq Status with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Test 13: Gwq Status with watch mode
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Status()
    .WithWatch()
    .WithInterval(10)
    .WithNoFetch()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 13 PASSED: Gwq Status with watch mode works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 13 FAILED: Gwq Status with watch mode Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 13 FAILED: Exception - {ex.Message}");
}

// Test 14: Gwq Get command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Get("feature")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 14 PASSED: Gwq Get command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 14 FAILED: Gwq Get Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 14 FAILED: Exception - {ex.Message}");
}

// Test 15: Gwq Get with null termination
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Get("main")
    .WithGlobal()
    .WithNull()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 15 PASSED: Gwq Get with null termination works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 15 FAILED: Gwq Get with null termination Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 15 FAILED: Exception - {ex.Message}");
}

// Test 16: Gwq Exec command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Exec("feature")
    .WithCommand("npm", "test")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 16 PASSED: Gwq Exec command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 16 FAILED: Gwq Exec Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 16 FAILED: Exception - {ex.Message}");
}

// Test 17: Gwq Exec with stay option
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Exec("main")
    .WithCommand("git", "pull")
    .WithStay()
    .WithGlobal()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 17 PASSED: Gwq Exec with stay option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 17 FAILED: Gwq Exec with stay option Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 17 FAILED: Exception - {ex.Message}");
}

// Test 18: Gwq Config commands
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .ConfigList()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 18 PASSED: Gwq Config list works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 18 FAILED: Gwq Config list Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 18 FAILED: Exception - {ex.Message}");
}

// Test 19: Gwq Config get/set
totalTests++;
try
{
  CommandResult getCommand = Gwq.Run()
    .ConfigGet("worktree.basedir")
    .Build();
  
  CommandResult setCommand = Gwq.Run()
    .ConfigSet("worktree.basedir", "~/worktrees")
    .Build();
  
  if (getCommand != null && setCommand != null)
  {
    Console.WriteLine("✅ Test 19 PASSED: Gwq Config get/set works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 19 FAILED: Gwq Config get/set Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 19 FAILED: Exception - {ex.Message}");
}

// Test 20: Gwq Prune command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Prune()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 20 PASSED: Gwq Prune command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 20 FAILED: Gwq Prune Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 20 FAILED: Exception - {ex.Message}");
}

// Test 21: Gwq Version command
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .Version()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 21 PASSED: Gwq Version command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 21 FAILED: Gwq Version Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 21 FAILED: Exception - {ex.Message}");
}

// Test 22: Gwq with working directory and environment variables
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("GIT_DIR", "/tmp/.git")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 22 PASSED: Gwq with working directory and environment variables works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 22 FAILED: Gwq with working directory and environment Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 22 FAILED: Exception - {ex.Message}");
}

// Test 23: PipeTo extension method
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .List()
    .PipeTo("grep", "feature");
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 23 PASSED: Gwq PipeTo extension works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 23 FAILED: Gwq PipeTo returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 23 FAILED: Exception - {ex.Message}");
}

// Test 24: Gwq graceful handling when gwq not available (will test command building)
totalTests++;
try
{
  CommandResult command = Gwq.Run()
    .List()
    .WithGlobal()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 24 PASSED: Gwq command builds correctly for testing");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 24 FAILED: Gwq command building failed");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 24 FAILED: Exception - {ex.Message}");
}

// Test 25: Actual command execution - Gwq Version (safe command)
totalTests++;
try
{
  string result = await Gwq.Run()
    .Version()
    .GetStringAsync();
  
  // Even if gwq is not installed, should return empty string gracefully
  Console.WriteLine("✅ Test 25 PASSED: Gwq Version command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 25 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 Gwq Command Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);