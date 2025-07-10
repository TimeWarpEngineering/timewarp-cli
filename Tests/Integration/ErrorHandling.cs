#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true

using TimeWarp.Cli;

Console.WriteLine("🧪 Testing ErrorHandling...");

int passCount = 0;
int totalTests = 0;

// Test 1: Non-existent command should return empty string (graceful failure)
totalTests++;
try
{
    var result = await CommandExtensions.Run("nonexistentcommand12345").GetStringAsync();
    if (result == string.Empty)
    {
        Console.WriteLine("✅ Test 1 PASSED: Non-existent command returned empty string");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 1 FAILED: Non-existent command returned '{result}' instead of empty string");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Non-existent command threw exception - {ex.Message}");
}

// Test 2: Command with non-zero exit code should not throw
totalTests++;
try
{
    var result = await CommandExtensions.Run("ls", "/nonexistent/path/12345").GetStringAsync();
    Console.WriteLine("✅ Test 2 PASSED: Command with non-zero exit code didn't throw");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Command with non-zero exit code threw exception - {ex.Message}");
}

// Test 3: ExecuteAsync with non-zero exit code should not throw
totalTests++;
try
{
    await CommandExtensions.Run("ls", "/nonexistent/path/12345").ExecuteAsync();
    Console.WriteLine("✅ Test 3 PASSED: ExecuteAsync with non-zero exit code didn't throw");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: ExecuteAsync with non-zero exit code threw exception - {ex.Message}");
}

// Test 4: GetLinesAsync with non-zero exit code should not throw
totalTests++;
try
{
    var lines = await CommandExtensions.Run("ls", "/nonexistent/path/12345").GetLinesAsync();
    Console.WriteLine("✅ Test 4 PASSED: GetLinesAsync with non-zero exit code didn't throw");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: GetLinesAsync with non-zero exit code threw exception - {ex.Message}");
}

// Test 5: Command with special characters in arguments
totalTests++;
try
{
    var result = await CommandExtensions.Run("echo", "Hello & World | Test").GetStringAsync();
    if (result.Contains("Hello") && result.Contains("World") && result.Contains("Test"))
    {
        Console.WriteLine("✅ Test 5 PASSED: Special characters in arguments handled correctly");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 5 FAILED: Special characters not handled correctly, got: '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 5 FAILED: Special characters caused exception - {ex.Message}");
}

// Test 6: Empty command should return empty string gracefully
totalTests++;
try
{
    var result = await CommandExtensions.Run("").GetStringAsync();
    if (result == string.Empty)
    {
        Console.WriteLine("✅ Test 6 PASSED: Empty command returned empty string gracefully");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 6 FAILED: Empty command returned '{result}' instead of empty string");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 6 FAILED: Empty command threw exception - {ex.Message}");
}

// Test 7: Command with only spaces should return empty string gracefully
totalTests++;
try
{
    var result = await CommandExtensions.Run("   ").GetStringAsync();
    if (result == string.Empty)
    {
        Console.WriteLine("✅ Test 7 PASSED: Whitespace-only command returned empty string gracefully");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 7 FAILED: Whitespace-only command returned '{result}' instead of empty string");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 7 FAILED: Whitespace-only command threw exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 ErrorHandling Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);