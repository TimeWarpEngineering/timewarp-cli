namespace TimeWarp.Cli.Test.Helpers;

public static class Asserts
{
  public static void AssertTrue(bool condition, string failureMessage)
  {
    if (!condition) throw new InvalidOperationException(failureMessage);
  }

  public static void AssertFalse(bool condition, string failureMessage)
  {
    if (condition) throw new InvalidOperationException(failureMessage);
  }

  public static async Task AssertThrowsAsync<TException>(Func<Task> action, string failureMessage) where TException : Exception
  {
    ArgumentNullException.ThrowIfNull(action);
    
    try
    {
      await action();
      throw new InvalidOperationException(failureMessage); // Didn't throw as expected
    }
    catch (TException) { /* Expected */ }
    catch (Exception ex)
    {
      throw new InvalidOperationException($"Unexpected exception: {ex.Message}", ex);
    }
  }
}