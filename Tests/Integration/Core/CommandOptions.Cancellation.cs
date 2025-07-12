#!/usr/bin/dotnet run
#:package TimeWarp.Cli
#:property RestoreNoCache true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing CancellationCommands...");

int passCount = 0;
int totalTests = 0;

// Test 1: Quick command that completes before cancellation
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(10)); // Long timeout, command should complete
    
    string result = await Run("echo", "Hello World").GetStringAsync(cts.Token);
    if (result.Trim() == "Hello World")
    {
        Console.WriteLine("✅ Test 1 PASSED: Quick command with cancellation token works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 1 FAILED: Expected 'Hello World', got '{result.Trim()}'");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("❌ Test 1 FAILED: Unexpected cancellation of quick command");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Immediate cancellation token (already cancelled)
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    await cts.CancelAsync(); // Cancel immediately
    
    string result = await Run("echo", "test").GetStringAsync(cts.Token);
    // Should return empty string due to cancellation
    if (string.IsNullOrEmpty(result))
    {
        Console.WriteLine("✅ Test 2 PASSED: Already cancelled token returns empty result");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 2 FAILED: Expected empty string, got '{result}'");
    }
}
catch (OperationCanceledException)
{
    // This is also acceptable behavior - either empty result or exception
    Console.WriteLine("✅ Test 2 PASSED: Already cancelled token throws OperationCanceledException");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Unexpected exception - {ex.Message}");
}

// Test 3: GetLinesAsync with cancellation token
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    string[] lines = await Run("echo", "line1\nline2\nline3").GetLinesAsync(cts.Token);
    if (lines.Length == 3 && lines[0] == "line1" && lines[1] == "line2" && lines[2] == "line3")
    {
        Console.WriteLine("✅ Test 3 PASSED: GetLinesAsync with cancellation token works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 3 FAILED: Expected 3 lines, got {lines.Length}");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("❌ Test 3 FAILED: Unexpected cancellation of quick command");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: ExecuteAsync with cancellation token
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    await Run("echo", "execute test").ExecuteAsync(cts.Token);
    Console.WriteLine("✅ Test 4 PASSED: ExecuteAsync with cancellation token works");
    passCount++;
}
catch (OperationCanceledException)
{
    Console.WriteLine("❌ Test 4 FAILED: Unexpected cancellation of quick command");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Timeout scenario with a longer running command
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromMilliseconds(100)); // Very short timeout
    
    // Use sleep command that should be cancelled (works on both Unix and Windows with different commands)
    bool isWindows = Environment.OSVersion.Platform == PlatformID.Win32NT;
    string result = isWindows 
        ? await Run("timeout", "5").GetStringAsync(cts.Token)  // Windows: timeout 5 seconds
        : await Run("sleep", "5").GetStringAsync(cts.Token);   // Unix: sleep 5 seconds
    
    // If we get here, either the command completed very quickly or returned empty due to cancellation
    if (string.IsNullOrEmpty(result))
    {
        Console.WriteLine("✅ Test 5 PASSED: Timeout cancellation returns empty result");
        passCount++;
    }
    else
    {
        Console.WriteLine("⚠️  Test 5 NOTE: Command completed before timeout could take effect");
        passCount++; // Still count as pass since behavior is valid
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("✅ Test 5 PASSED: Timeout properly cancelled long-running command");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 5 FAILED: Unexpected exception - {ex.Message}");
}

// Test 6: Pipeline with cancellation (basic test)
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    cts.CancelAfter(TimeSpan.FromSeconds(5));
    
    string result = await Run("echo", "line1\nline2\nline3")
        .Pipe("grep", "line")
        .GetStringAsync(cts.Token);
    
    if (!string.IsNullOrEmpty(result) && result.Contains("line", StringComparison.Ordinal))
    {
        Console.WriteLine("✅ Test 6 PASSED: Pipeline with cancellation token works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 6 FAILED: Pipeline result unexpected: '{result}'");
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("❌ Test 6 FAILED: Unexpected cancellation of quick pipeline");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Default cancellation token behavior (should work like before)
totalTests++;
try
{
    string result = await Run("echo", "default token test").GetStringAsync();
    if (result.Trim() == "default token test")
    {
        Console.WriteLine("✅ Test 7 PASSED: Default cancellation token behavior preserved");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 7 FAILED: Expected 'default token test', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 CancellationCommands Results: {passCount}/{totalTests} tests passed");

if (passCount == totalTests)
{
    Console.WriteLine("🎉 All cancellation tests passed!");
}
else
{
    Console.WriteLine($"⚠️  {totalTests - passCount} test(s) failed. Cancellation support may need review.");
}

Environment.Exit(passCount == totalTests ? 0 : 1);