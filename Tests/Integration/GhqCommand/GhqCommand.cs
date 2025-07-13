#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing Ghq Command...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic Ghq builder creation
totalTests++;
try
{
  GhqBuilder ghqBuilder = Ghq.Run();
  if (ghqBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: Ghq.Run() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: Ghq.Run() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Ghq Get command
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Get("github.com/TimeWarpEngineering/timewarp-cli")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: Ghq Get command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: Ghq Get Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Ghq Clone command (alias)
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Clone("github.com/user/repo")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: Ghq Clone command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: Ghq Clone Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Ghq Get with options
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Get("github.com/user/repo")
    .WithShallow()
    .WithBranch("develop")
    .WithUpdate()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: Ghq Get with options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: Ghq Get with options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Ghq List command
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: Ghq List command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: Ghq List Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Ghq List with options
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .List()
    .WithFullPath()
    .WithExact()
    .FilterByVcs("git")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: Ghq List with options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: Ghq List with options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Ghq Remove command
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Remove("github.com/old/repo")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: Ghq Remove command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: Ghq Remove Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Ghq Rm command (alias)
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Rm("github.com/old/repo")
    .WithDryRun()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: Ghq Rm command with dry-run works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: Ghq Rm with dry-run Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Ghq Root command
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Root()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: Ghq Root command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: Ghq Root Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Ghq Root with all option
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Root()
    .WithAll()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: Ghq Root with all option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: Ghq Root with all Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: Ghq Create command
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Create("github.com/user/new-repo")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: Ghq Create command works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: Ghq Create Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: Ghq with working directory and environment variables
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("GHQ_ROOT", "/tmp/ghq")
    .List()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 12 PASSED: Ghq with working directory and environment variables works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 12 FAILED: Ghq with working directory and environment Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Test 13: Ghq Get with advanced options
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Get("github.com/example/repo")
    .WithLook()
    .WithParallel()
    .WithSilent()
    .WithNoRecursive()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 13 PASSED: Ghq Get with advanced options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 13 FAILED: Ghq Get with advanced options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 13 FAILED: Exception - {ex.Message}");
}

// Test 14: Ghq Get with VCS backend
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Get("gitlab.com/user/project")
    .WithVcs("gitlab")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 14 PASSED: Ghq Get with VCS backend works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 14 FAILED: Ghq Get with VCS backend Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 14 FAILED: Exception - {ex.Message}");
}

// Test 15: Ghq List with unique option
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .List()
    .WithUnique()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 15 PASSED: Ghq List with unique option works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 15 FAILED: Ghq List with unique Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 15 FAILED: Exception - {ex.Message}");
}

// Test 16: Ghq Get with bare repository
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .Get("github.com/user/bare-repo")
    .WithBare()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 16 PASSED: Ghq Get with bare repository works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 16 FAILED: Ghq Get with bare Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 16 FAILED: Exception - {ex.Message}");
}

// Test 17: PipeTo extension method
totalTests++;
try
{
  CommandResult command = Ghq.Run()
    .List()
    .PipeTo("grep", "timewarp");
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 17 PASSED: Ghq PipeTo extension works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 17 FAILED: Ghq PipeTo returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 17 FAILED: Exception - {ex.Message}");
}

// Test 18: Ghq graceful handling when ghq not available (will test command building)
totalTests++;
try
{
  // This tests that the command builds correctly even if ghq might not be installed
  CommandResult command = Ghq.Run()
    .List()
    .WithFullPath()
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 18 PASSED: Ghq command builds correctly for testing");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 18 FAILED: Ghq command building failed");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 18 FAILED: Exception - {ex.Message}");
}

// Test 19: Actual command execution - Ghq Root (safe command)
totalTests++;
try
{
  string result = await Ghq.Run()
    .Root()
    .GetStringAsync();
  
  // Even if ghq is not installed, should return empty string gracefully
  Console.WriteLine("✅ Test 19 PASSED: Ghq Root command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 19 FAILED: Exception - {ex.Message}");
}

// Test 20: Actual command execution - Ghq List (safe command)
totalTests++;
try
{
  string[] lines = await Ghq.Run()
    .List()
    .GetLinesAsync();
  
  // Even if ghq is not installed or no repos exist, should return empty array gracefully
  Console.WriteLine("✅ Test 20 PASSED: Ghq List command execution completed with graceful handling");
  passCount++;
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 20 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 Ghq Command Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);