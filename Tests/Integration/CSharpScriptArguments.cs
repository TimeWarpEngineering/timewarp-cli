#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing C# Script Arguments Handling...");

int passCount = 0;
int totalTests = 0;

// Create a test script that echoes its arguments
string testScript = Path.GetTempFileName() + ".cs";
string scriptContent = @"#!/usr/bin/dotnet run
Console.WriteLine($""Args: {string.Join("", "", args)}"");
";
await File.WriteAllTextAsync(testScript, scriptContent);

// Make it executable on Unix-like systems
if (!OperatingSystem.IsWindows())
{
  await Run("chmod", "+x", testScript).ExecuteAsync();
}

// Test 1: Arguments starting with dash should be passed to script
totalTests++;
try
{
  string result = await Run(testScript, "-v", "pattern").GetStringAsync();
  if (result.Contains("Args: -v, pattern", StringComparison.Ordinal))
  {
    Console.WriteLine("✅ Test 1 PASSED: Arguments with dash passed correctly to script");
    passCount++;
  }
  else
  {
    Console.WriteLine($"❌ Test 1 FAILED: Expected 'Args: -v, pattern', got '{result.Trim()}'");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Multiple dashed arguments
totalTests++;
try
{
  string result = await Run(testScript, "--verbose", "-c", "config.json", "--help").GetStringAsync();
  if (result.Contains("Args: --verbose, -c, config.json, --help", StringComparison.Ordinal))
  {
    Console.WriteLine("✅ Test 2 PASSED: Multiple dashed arguments passed correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine($"❌ Test 2 FAILED: Arguments not passed correctly - got '{result.Trim()}'");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Mixed arguments (with and without dashes)
totalTests++;
try
{
  string result = await Run(testScript, "command", "-f", "file.txt", "--", "extra").GetStringAsync();
  if (result.Contains("Args: command, -f, file.txt, --, extra", StringComparison.Ordinal))
  {
    Console.WriteLine("✅ Test 3 PASSED: Mixed arguments passed correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine($"❌ Test 3 FAILED: Mixed arguments not passed correctly - got '{result.Trim()}'");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: No arguments
totalTests++;
try
{
  string result = await Run(testScript).GetStringAsync();
  if (result.Trim() == "Args:")
  {
    Console.WriteLine("✅ Test 4 PASSED: No arguments handled correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine($"❌ Test 4 FAILED: No arguments case failed - got '{result.Trim()}'");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Regular executable (non-.cs) should not be affected
totalTests++;
try
{
  string result = await Run("echo", "-n", "test").GetStringAsync();
  if (result.Trim() == "test")
  {
    Console.WriteLine("✅ Test 5 PASSED: Non-.cs executables unaffected");
    passCount++;
  }
  else
  {
    Console.WriteLine($"❌ Test 5 FAILED: Non-.cs executable affected - got '{result}'");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Clean up
try
{
  File.Delete(testScript);
}
catch
{
  // Ignore cleanup errors
}

// Summary
Console.WriteLine($"\n📊 C# Script Arguments Test Results: {passCount}/{totalTests} tests passed");

if (passCount < totalTests)
{
  Console.WriteLine("⚠️  Some tests failed. The .cs script argument handling may need review.");
  Environment.Exit(1);
}
else
{
  Console.WriteLine("🎉 All C# script argument tests passed!");
}