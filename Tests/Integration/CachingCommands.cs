#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing CachingCommands...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic caching - command executed only once
totalTests++;
try
{
    int executionCount = 0;
    string testFile = Path.GetTempFileName();
    
    try
    {
        // Use a command that appends to a file to track executions
        CommandResult cachedCmd = Run("bash", "-c", $"echo 'execution' >> {testFile}").Cached();
        
        // Execute multiple times
        await cachedCmd.ExecuteAsync();
        await cachedCmd.ExecuteAsync();
        await cachedCmd.ExecuteAsync();
        
        // Count lines in file (should be 1 if cached properly)
        string[] lines = await File.ReadAllLinesAsync(testFile);
        executionCount = lines.Length;
        
        if (executionCount == 1)
        {
            Console.WriteLine("✅ Test 1 PASSED: Cached command executed only once");
            passCount++;
        }
        else
        {
            Console.WriteLine($"❌ Test 1 FAILED: Expected 1 execution, got {executionCount}");
        }
    }
    finally
    {
        if (File.Exists(testFile)) File.Delete(testFile);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: GetStringAsync caching
totalTests++;
try
{
    // Use a command that returns current time
    CommandResult cachedCmd = Run("date", "+%N").Cached(); // Nanoseconds for precision
    
    string result1 = await cachedCmd.GetStringAsync();
    await Task.Delay(10); // Small delay to ensure time would change
    string result2 = await cachedCmd.GetStringAsync();
    string result3 = await cachedCmd.GetStringAsync();
    
    if (result1 == result2 && result2 == result3 && !string.IsNullOrEmpty(result1))
    {
        Console.WriteLine("✅ Test 2 PASSED: GetStringAsync returns cached result");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 2 FAILED: Results differ - R1: {result1.Trim()}, R2: {result2.Trim()}, R3: {result3.Trim()}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: GetLinesAsync caching
totalTests++;
try
{
    CommandResult cachedCmd = Run("echo", "line1\nline2\nline3").Cached();
    
    string[] lines1 = await cachedCmd.GetLinesAsync();
    string[] lines2 = await cachedCmd.GetLinesAsync();
    string[] lines3 = await cachedCmd.GetLinesAsync();
    
    if (lines1.Length == 3 && 
        lines1.SequenceEqual(lines2) && 
        lines2.SequenceEqual(lines3))
    {
        Console.WriteLine("✅ Test 3 PASSED: GetLinesAsync returns cached result");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 3 FAILED: Line arrays differ in content or length");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: Non-cached commands execute fresh each time
totalTests++;
try
{
    CommandResult nonCachedCmd = Run("date", "+%N"); // No .Cached() call
    
    string result1 = await nonCachedCmd.GetStringAsync();
    await Task.Delay(10); // Small delay
    string result2 = await nonCachedCmd.GetStringAsync();
    
    if (result1 != result2)
    {
        Console.WriteLine("✅ Test 4 PASSED: Non-cached commands execute fresh each time");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 4 FAILED: Non-cached results are identical: {result1.Trim()}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: Pipeline caching - cache entire pipeline result
totalTests++;
try
{
    string testData = "apple\nbanana\napricot\navocado";
    CommandResult cachedPipeline = Run("echo", testData)
        .Pipe("grep", "a")
        .Pipe("sort")
        .Cached();
    
    string[] result1 = await cachedPipeline.GetLinesAsync();
    string[] result2 = await cachedPipeline.GetLinesAsync();
    
    if (result1.Length == 4 && result1.SequenceEqual(result2))
    {
        Console.WriteLine("✅ Test 5 PASSED: Pipeline caching works correctly");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 5 FAILED: Pipeline results differ or wrong count");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: Intermediate pipeline caching
totalTests++;
try
{
    // Cache an expensive first operation
    CommandResult cachedFind = Run("echo", "file1.cs\nfile2.txt\nfile3.cs\nfile4.md").Cached();
    
    // Different filters on the same cached base
    string[] csFiles = await cachedFind.Pipe("grep", ".cs").GetLinesAsync();
    string[] txtFiles = await cachedFind.Pipe("grep", ".txt").GetLinesAsync();
    string[] allFiles = await cachedFind.GetLinesAsync();
    
    if (csFiles.Length == 2 && txtFiles.Length == 1 && allFiles.Length == 4)
    {
        Console.WriteLine("✅ Test 6 PASSED: Intermediate pipeline caching works");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 6 FAILED: Expected 2 cs, 1 txt, 4 total. Got {csFiles.Length}, {txtFiles.Length}, {allFiles.Length}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: Cache isolation - each instance has its own cache
totalTests++;
try
{
    CommandResult cmd1 = Run("echo", Guid.NewGuid().ToString()).Cached();
    CommandResult cmd2 = Run("echo", Guid.NewGuid().ToString()).Cached();
    
    string result1 = await cmd1.GetStringAsync();
    string result2 = await cmd2.GetStringAsync();
    
    if (result1 != result2)
    {
        Console.WriteLine("✅ Test 7 PASSED: Each CommandResult instance has isolated cache");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 7 FAILED: Different cached instances returned same result");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: Mixed caching on same command
totalTests++;
try
{
    CommandResult baseCmd = Run("echo", "test_data");
    
    // First call cached
    string cachedResult1 = await baseCmd.Cached().GetStringAsync();
    string cachedResult2 = await baseCmd.Cached().GetStringAsync();
    
    // These should be equal (both cached)
    if (cachedResult1 == cachedResult2)
    {
        // Now call without caching - should still work
        string freshResult = await baseCmd.GetStringAsync();
        
        if (freshResult == cachedResult1) // Same output expected
        {
            Console.WriteLine("✅ Test 8 PASSED: Mixed caching usage works correctly");
            passCount++;
        }
        else
        {
            Console.WriteLine($"❌ Test 8 FAILED: Fresh result differs from cached");
        }
    }
    else
    {
        Console.WriteLine($"❌ Test 8 FAILED: Cached results differ");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: Caching with cancellation token
totalTests++;
try
{
    using var cts = new CancellationTokenSource();
    CommandResult cachedCmd = Run("echo", "cancellation_test").Cached();
    
    string result1 = await cachedCmd.GetStringAsync(cts.Token);
    string result2 = await cachedCmd.GetStringAsync(cts.Token);
    
    if (result1 == result2 && result1.Trim() == "cancellation_test")
    {
        Console.WriteLine("✅ Test 9 PASSED: Caching works with cancellation tokens");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 9 FAILED: Results differ or incorrect");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: Caching failed commands
totalTests++;
try
{
    CommandResult cachedCmd = Run("nonexistentcommand123").Cached();
    
    string result1 = await cachedCmd.GetStringAsync();
    string result2 = await cachedCmd.GetStringAsync();
    string[] lines1 = await cachedCmd.GetLinesAsync();
    
    if (string.IsNullOrEmpty(result1) && string.IsNullOrEmpty(result2) && lines1.Length == 0)
    {
        Console.WriteLine("✅ Test 10 PASSED: Failed commands cache empty results");
        passCount++;
    }
    else
    {
        Console.WriteLine($"❌ Test 10 FAILED: Expected empty results for failed command");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 CachingCommands Results: {passCount}/{totalTests} tests passed");

if (passCount == totalTests)
{
    Console.WriteLine("🎉 All caching tests passed!");
}
else
{
    Console.WriteLine($"⚠️  {totalTests - passCount} test(s) failed. Caching support may need review.");
}

Environment.Exit(passCount == totalTests ? 0 : 1);