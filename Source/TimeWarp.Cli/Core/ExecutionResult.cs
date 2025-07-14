namespace TimeWarp.Cli;

public class ExecutionResult
{
  public CliWrap.CommandResult Result { get; }
  public string StandardOutput { get; }
  public string StandardError { get; }
  
  public ExecutionResult(CliWrap.CommandResult result, string standardOutput, string standardError)
  {
    Result = result;
    StandardOutput = standardOutput;
    StandardError = standardError;
  }
  
  public int ExitCode => Result.ExitCode;
  public bool IsSuccess => Result.IsSuccess;
  public DateTimeOffset StartTime => Result.StartTime;
  public DateTimeOffset ExitTime => Result.ExitTime;
  public TimeSpan RunTime => Result.RunTime;
}