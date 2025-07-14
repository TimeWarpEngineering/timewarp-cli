namespace TimeWarp.Cli.Test.Helpers;

using System.Reflection;

public static class TestRunner
{
  static int PassCount;
  static int TotalTests;

  public static async Task RunTests<T>() where T : class
  {
    string testClassName = typeof(T).Name.Replace("Tests", "", StringComparison.Ordinal);
    Console.WriteLine($"🧪 Testing {testClassName}...");
    
    // get all public methods in the class
    MethodInfo[] testMethods = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Static);

    // and run them as tests
    foreach (MethodInfo method in testMethods)
    {
      await RunTest(method);
    }
    
    // Call cleanup method if it exists
    MethodInfo? cleanupMethod = typeof(T).GetMethod("CleanUp", BindingFlags.Public | BindingFlags.Static);
    if (cleanupMethod != null)
    {
      cleanupMethod.Invoke(null, null);
    }
    
    // Summary
    Console.WriteLine($"\n📊 {testClassName} Results: {PassCount}/{TotalTests} tests passed");
    Environment.Exit(PassCount == TotalTests ? 0 : 1);
  }

  private static async Task RunTest(MethodInfo method)
  {
    if (!method.IsPublic || !method.IsStatic || method.ReturnType != typeof(Task) || method.Name == "CleanUp")
    {
      // Skip non-test methods
      return;
    }

    // Increment total tests and run the test
    TotalTests++;
    string testName = method.Name;
    Console.WriteLine($"Running {testName}...");

    try
    {
      var task = method.Invoke(null, null) as Task;
      if (task != null) await task;
      PassCount++;
      TestHelpers.TestPassed(testName);
    }
    catch (Exception ex)
    {
      TestHelpers.TestFailed(testName, ex.Message);
    }
  }
}