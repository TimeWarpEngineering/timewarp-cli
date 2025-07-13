#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing ErrorHandling...");

int passCount = 0;
int totalTests = 0;

// Create options with no validation for graceful degradation tests
CommandOptions noValidation = new CommandOptions().WithNoValidation();

// Test 1: Non-existent command with no validation should return empty string (graceful failure)
totalTests++;
try
{
    string result = await Run("nonexistentcommand12345", Array.Empty<string>(), noValidation).GetStringAsync();
    if (string.IsNullOrEmpty(result))
    {
        Console.WriteLine("✅ Test 1 PASSED: Non-existent command returned empty string with no validation");
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

// Test 2: Command with non-zero exit code should not throw when validation disabled
totalTests++;
try
{
    string result = await Run("ls", new[] { "/nonexistent/path/12345" }, noValidation).GetStringAsync();
    Console.WriteLine("✅ Test 2 PASSED: Command with non-zero exit code didn't throw when validation disabled");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Command with non-zero exit code threw exception - {ex.Message}");
}

// Test 3: ExecuteAsync with non-zero exit code SHOULD throw by default
totalTests++;
try
{
    await Run("ls", "/nonexistent/path/12345").ExecuteAsync();
    Console.WriteLine("❌ Test 3 FAILED: ExecuteAsync with non-zero exit code didn't throw (but should have)");
}
catch (Exception)
{
    Console.WriteLine("✅ Test 3 PASSED: ExecuteAsync with non-zero exit code threw exception as expected");
    passCount++;
}

// Test 4: GetLinesAsync with non-zero exit code and no validation should return empty array
totalTests++;
try
{
    string[] lines = await Run("ls", new[] { "/nonexistent/path/12345" }, noValidation).GetLinesAsync();
    if (lines.Length == 0)
    {
        Console.WriteLine("✅ Test 4 PASSED: GetLinesAsync with non-zero exit code returned empty array");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 4 FAILED: GetLinesAsync returned {lines.Length} lines instead of empty array");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: GetLinesAsync threw exception - {ex.Message}");
}

// Test 5: Command with special characters in arguments
totalTests++;
try
{
    string result = await Run("echo", "Hello & World | Test").GetStringAsync();
    if (result.Contains("Hello", StringComparison.Ordinal) && result.Contains("World", StringComparison.Ordinal) && result.Contains("Test", StringComparison.Ordinal))
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

// Test 6: Empty command should return empty string gracefully (built-in null check)
totalTests++;
try
{
    string result = await Run("").GetStringAsync();
    if (string.IsNullOrEmpty(result))
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
    string result = await Run("   ").GetStringAsync();
    if (string.IsNullOrEmpty(result))
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

// Test 8: Default behavior - GetStringAsync should throw on non-zero exit
totalTests++;
try
{
    string result = await Run("ls", "/nonexistent/path/12345").GetStringAsync();
    Console.WriteLine("❌ Test 8 FAILED: GetStringAsync didn't throw on non-zero exit (default behavior)");
}
catch (Exception)
{
    Console.WriteLine("✅ Test 8 PASSED: GetStringAsync threw exception on non-zero exit (default behavior)");
    passCount++;
}

// Test 9: Default behavior - GetLinesAsync should throw on non-zero exit
totalTests++;
try
{
    string[] lines = await Run("ls", "/nonexistent/path/12345").GetLinesAsync();
    Console.WriteLine("❌ Test 9 FAILED: GetLinesAsync didn't throw on non-zero exit (default behavior)");
}
catch (Exception)
{
    Console.WriteLine("✅ Test 9 PASSED: GetLinesAsync threw exception on non-zero exit (default behavior)");
    passCount++;
}

// Test 10: ExecuteAsync with no validation should not throw
totalTests++;
try
{
    await Run("ls", new[] { "/nonexistent/path/12345" }, noValidation).ExecuteAsync();
    Console.WriteLine("✅ Test 10 PASSED: ExecuteAsync with no validation didn't throw");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 10 FAILED: ExecuteAsync with no validation threw - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 ErrorHandling Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);