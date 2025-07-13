#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing BasicCommands...");

int passCount = 0;
int totalTests = 0;

// Test 1: Simple echo command
totalTests++;
try
{
    string result = await Run("echo", "Hello World").GetStringAsync();
    if (result.Trim() == "Hello World")
    {
        Console.WriteLine("✅ Test 1 PASSED: Echo command works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 1 FAILED: Expected 'Hello World', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Command with multiple arguments
totalTests++;
try
{
    string result = await Run("echo", "arg1", "arg2", "arg3").GetStringAsync();
    if (result.Trim() == "arg1 arg2 arg3")
    {
        Console.WriteLine("✅ Test 2 PASSED: Multiple arguments work");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 2 FAILED: Expected 'arg1 arg2 arg3', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: ExecuteAsync doesn't throw
totalTests++;
try
{
    await Run("echo", "test").ExecuteAsync();
    Console.WriteLine("✅ Test 3 PASSED: ExecuteAsync works without throwing");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: ExecuteAsync threw - {ex.Message}");
}

// Test 4: Date command (cross-platform)
totalTests++;
try
{
    string result = await Run("date").GetStringAsync();
    if (!string.IsNullOrEmpty(result.Trim()))
    {
        Console.WriteLine("✅ Test 4 PASSED: Date command returns non-empty result");
        passCount++;
    }
    else
    {
        Console.WriteLine("❌ Test 4 FAILED: Date command returned empty result");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 BasicCommands Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);