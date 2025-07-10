#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing OutputFormats...");

int passCount = 0;
int totalTests = 0;

// Test 1: GetStringAsync returns raw output
totalTests++;
try
{
    var result = await Run("echo", "line1\nline2\nline3").GetStringAsync();
    if (result.Contains("line1") && result.Contains("line2") && result.Contains("line3"))
    {
        Console.WriteLine("✅ Test 1 PASSED: GetStringAsync returns raw output with newlines");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 1 FAILED: Expected multi-line output, got '{result}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: GetLinesAsync splits lines correctly
totalTests++;
try
{
    // Use printf to ensure consistent cross-platform newlines
    var lines = await Run("printf", "line1\nline2\nline3").GetLinesAsync();
    if (lines.Length == 3 && lines[0] == "line1" && lines[1] == "line2" && lines[2] == "line3")
    {
        Console.WriteLine("✅ Test 2 PASSED: GetLinesAsync splits lines correctly");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 2 FAILED: Expected 3 lines [line1, line2, line3], got {lines.Length} lines: [{string.Join(", ", lines)}]");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: GetLinesAsync removes empty lines
totalTests++;
try
{
    var lines = await Run("printf", "line1\n\nline2\n\n").GetLinesAsync();
    if (lines.Length == 2 && lines[0] == "line1" && lines[1] == "line2")
    {
        Console.WriteLine("✅ Test 3 PASSED: GetLinesAsync removes empty lines");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 3 FAILED: Expected 2 lines [line1, line2], got {lines.Length} lines: [{string.Join(", ", lines)}]");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Empty output handling
totalTests++;
try
{
    var result = await Run("echo", "").GetStringAsync();
    var lines = await Run("echo", "").GetLinesAsync();
    
    if (result.Length <= 2 && lines.Length == 0) // Allow for just newline character
    {
        Console.WriteLine("✅ Test 4 PASSED: Empty output handled correctly");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 4 FAILED: Empty output not handled correctly - string length: {result.Length}, lines count: {lines.Length}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Test with ls command (real-world scenario)
totalTests++;
try
{
    var files = await Run("ls", "-1").GetLinesAsync();
    if (files.Length > 0)
    {
        Console.WriteLine($"✅ Test 5 PASSED: ls command returns {files.Length} files/directories");
        passCount++;
    }
    else
    {
        Console.WriteLine("❌ Test 5 FAILED: ls command returned no files");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 OutputFormats Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);