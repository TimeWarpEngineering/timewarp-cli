#!/usr/bin/dotnet run

#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing ErrorHandling...");

int PassCount = 0;
int TotalTests = 0;

// Create options with no validation for graceful degradation tests
CommandOptions NoValidationCommandOptions = new CommandOptions().WithNoValidation();

// Test Functions
#pragma warning disable CS8321 // Local function is declared but never used

string FormatTestName(string name) => System.Text.RegularExpressions.Regex.Replace(name, "([A-Z])", " $1").Trim();
void TestPassed(string testName) => Console.WriteLine($"✅ {FormatTestName(testName)}");
void TestFailed(string testName, string reason) => Console.WriteLine($"❌ {FormatTestName(testName)}: {reason}");

async Task<bool> TestNonExistentCommandWithNoValidation()
{
    const string TestName = nameof(TestNonExistentCommandWithNoValidation);
    try
    {
        string result = await Run("nonexistentcommand12345", Array.Empty<string>(), NoValidationCommandOptions).GetStringAsync();
        TestFailed(TestName, "should have thrown for non-existent command");
        return false;
    }
    catch (Exception)
    {
        // This is expected - process start failures should always throw
        TestPassed(TestName);
        return true;
    }
}

async Task<bool> TestCommandWithNonZeroExitCodeAndNoValidation()
{
    const string TestName = nameof(TestCommandWithNonZeroExitCodeAndNoValidation);
    try
    {
        string[] lsArgs = ["/nonexistent/path/12345"];
        string result = await Run("ls", lsArgs, NoValidationCommandOptions).GetStringAsync();
        TestPassed(TestName);
        return true;
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

async Task<bool> TestExecuteAsyncThrowsOnNonZeroExit()
{
    const string TestName = nameof(TestExecuteAsyncThrowsOnNonZeroExit);
    try
    {
        await Run("ls", "/nonexistent/path/12345").ExecuteAsync();
        TestFailed(TestName, "didn't throw as expected");
        return false;
    }
    catch (Exception)
    {
        TestPassed(TestName);
        return true;
    }
}

async Task<bool> TestGetLinesAsyncWithNoValidation()
{
    const string TestName = nameof(TestGetLinesAsyncWithNoValidation);
    try
    {
        string[] lsArgs2 = ["/nonexistent/path/12345"];
        string[] lines = await Run("ls", lsArgs2, NoValidationCommandOptions).GetLinesAsync();
        if (lines.Length == 0)
        {
            TestPassed(TestName);
            return true;
        }
        else
        {
            TestFailed(TestName, $"returned {lines.Length} lines instead of empty array");
            return false;
        }
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

async Task<bool> TestSpecialCharactersInArguments()
{
    const string TestName = nameof(TestSpecialCharactersInArguments);
    try
    {
        string result = await Run("echo", "Hello \"World\" with 'quotes' and $pecial chars!").GetStringAsync();
        if (result.Contains("Hello", StringComparison.Ordinal) && result.Contains("World", StringComparison.Ordinal) && result.Contains("quotes", StringComparison.Ordinal))
        {
            TestPassed(TestName);
            return true;
        }
        else
        {
            TestFailed(TestName, $"Got: {result}");
            return false;
        }
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

async Task<bool> TestEmptyCommandReturnsEmptyString()
{
    const string TestName = nameof(TestEmptyCommandReturnsEmptyString);
    try
    {
        string result = await Run("").GetStringAsync();
        if (string.IsNullOrEmpty(result))
        {
            TestPassed(TestName);
            return true;
        }
        else
        {
            TestFailed(TestName, $"returned '{result}'");
            return false;
        }
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

async Task<bool> TestWhitespaceCommandReturnsEmptyString()
{
    const string TestName = nameof(TestWhitespaceCommandReturnsEmptyString);
    try
    {
        string result = await Run("   ").GetStringAsync();
        if (string.IsNullOrEmpty(result))
        {
            TestPassed(TestName);
            return true;
        }
        else
        {
            TestFailed(TestName, $"returned '{result}'");
            return false;
        }
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

async Task<bool> TestDefaultGetStringThrowsOnError()
{
    const string TestName = nameof(TestDefaultGetStringThrowsOnError);
    try
    {
        string result = await Run("ls", "/nonexistent/path/12345").GetStringAsync();
        TestFailed(TestName, "didn't throw as expected");
        return false;
    }
    catch (Exception)
    {
        TestPassed(TestName);
        return true;
    }
}

async Task<bool> TestDefaultGetLinesThrowsOnError()
{
    const string TestName = nameof(TestDefaultGetLinesThrowsOnError);
    try
    {
        string[] lines = await Run("ls", "/nonexistent/path/12345").GetLinesAsync();
        TestFailed(TestName, "didn't throw as expected");
        return false;
    }
    catch (Exception)
    {
        TestPassed(TestName);
        return true;
    }
}

async Task<bool> TestExecuteAsyncWithNoValidation()
{
    const string TestName = nameof(TestExecuteAsyncWithNoValidation);
    try
    {
        string[] lsArgs3 = ["/nonexistent/path/12345"];
        await Run("ls", lsArgs3, NoValidationCommandOptions).ExecuteAsync();
        TestPassed(TestName);
        return true;
    }
    catch (Exception ex)
    {
        TestFailed(TestName, ex.Message);
        return false;
    }
}

// Run all tests
TotalTests++; if (await TestDefaultGetStringThrowsOnError()) PassCount++;
TotalTests++; if (await TestNonExistentCommandWithNoValidation()) PassCount++;
TotalTests++; if (await TestCommandWithNonZeroExitCodeAndNoValidation()) PassCount++;
TotalTests++; if (await TestExecuteAsyncThrowsOnNonZeroExit()) PassCount++;
TotalTests++; if (await TestGetLinesAsyncWithNoValidation()) PassCount++;
TotalTests++; if (await TestSpecialCharactersInArguments()) PassCount++;
TotalTests++; if (await TestEmptyCommandReturnsEmptyString()) PassCount++;
TotalTests++; if (await TestWhitespaceCommandReturnsEmptyString()) PassCount++;
TotalTests++; if (await TestDefaultGetLinesThrowsOnError()) PassCount++;
TotalTests++; if (await TestExecuteAsyncWithNoValidation()) PassCount++;
#pragma warning restore CS8321

// Summary
Console.WriteLine($"\n📊 ErrorHandling Results: {PassCount}/{TotalTests} tests passed");
Environment.Exit(PassCount == TotalTests ? 0 : 1);