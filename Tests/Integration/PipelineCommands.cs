#!/usr/bin/dotnet run
#:package TimeWarp.Cli@0.2.0
#:property RestoreNoCache true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing PipelineCommands...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic pipeline - echo | grep
totalTests++;
try
{
    var result = await Run("echo", "hello\nworld\ntest")
        .Pipe("grep", "world")
        .GetStringAsync();
    
    if (result.Trim() == "world")
    {
        Console.WriteLine("✅ Test 1 PASSED: Basic pipeline works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 1 FAILED: Expected 'world', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: Multi-stage pipeline - echo | grep | wc
totalTests++;
try
{
    var result = await Run("echo", "line1\nline2\nline3\nline4")
        .Pipe("grep", "line")
        .Pipe("wc", "-l")
        .GetStringAsync();
    
    if (result.Trim() == "4")
    {
        Console.WriteLine("✅ Test 2 PASSED: Multi-stage pipeline works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 2 FAILED: Expected '4', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: Pipeline with GetLinesAsync
totalTests++;
try
{
    var lines = await Run("echo", "apple\nbanana\ncherry")
        .Pipe("grep", "a")
        .GetLinesAsync();
    
    if (lines.Length == 2 && lines[0] == "apple" && lines[1] == "banana")
    {
        Console.WriteLine("✅ Test 3 PASSED: Pipeline with GetLinesAsync works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 3 FAILED: Expected 2 lines [apple, banana], got {lines.Length} lines: [{string.Join(", ", lines)}]");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Pipeline with ExecuteAsync (should not throw)
totalTests++;
try
{
    await Run("echo", "test")
        .Pipe("grep", "test")
        .ExecuteAsync();
    
    Console.WriteLine("✅ Test 4 PASSED: Pipeline with ExecuteAsync works");
    passCount++;
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Pipeline with failed first command (graceful degradation)
totalTests++;
try
{
    var result = await Run("nonexistentcommand12345")
        .Pipe("grep", "anything")
        .GetStringAsync();
    
    if (result == string.Empty)
    {
        Console.WriteLine("✅ Test 5 PASSED: Pipeline with failed first command returns empty string");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 5 FAILED: Expected empty string, got '{result}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Pipeline with failed second command (graceful degradation)
totalTests++;
try
{
    var result = await Run("echo", "test")
        .Pipe("nonexistentcommand12345")
        .GetStringAsync();
    
    if (result == string.Empty)
    {
        Console.WriteLine("✅ Test 6 PASSED: Pipeline with failed second command returns empty string");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 6 FAILED: Expected empty string, got '{result}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Real-world scenario - find files and filter
totalTests++;
try
{
    var files = await Run("find", ".", "-name", "*.cs", "-type", "f")
        .Pipe("head", "-5")
        .GetLinesAsync();
    
    if (files.Length <= 5 && files.All(f => f.EndsWith(".cs")))
    {
        Console.WriteLine($"✅ Test 7 PASSED: Real-world pipeline found {files.Length} .cs files");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 7 FAILED: Expected .cs files, got: [{string.Join(", ", files)}]");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Pipeline chaining multiple operations
totalTests++;
try
{
    var result = await Run("echo", "The quick brown fox jumps over the lazy dog")
        .Pipe("tr", " ", "\n")
        .Pipe("grep", "o")
        .Pipe("wc", "-l")
        .GetStringAsync();
    
    if (result.Trim() == "4")  // "brown", "fox", "over", "dog"
    {
        Console.WriteLine("✅ Test 8 PASSED: Complex pipeline chaining works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 8 FAILED: Expected '4', got '{result.Trim()}'");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 PipelineCommands Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);