#!/usr/bin/dotnet run

await RunTests<GwqCommandTests>();

internal sealed class GwqCommandTests
{
  public static async Task TestBasicGwqBuilderCreation()
  {
    GwqBuilder gwqBuilder = Gwq.Run();
    
    AssertTrue(
      gwqBuilder != null,
      "Gwq.Run() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddCommand()
  {
    string command = Gwq.Run()
      .Add("feature/new-branch")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq add feature/new-branch",
      $"Expected 'gwq add feature/new-branch', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddWithPath()
  {
    string command = Gwq.Run()
      .Add("feature/branch", "~/worktrees/feature")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq add ~/worktrees/feature feature/branch",
      $"Expected 'gwq add ~/worktrees/feature feature/branch', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddInteractive()
  {
    string command = Gwq.Run()
      .AddInteractive()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq add -i",
      $"Expected 'gwq add -i', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddWithNewBranch()
  {
    string command = Gwq.Run()
      .Add("feature/new")
      .WithNewBranch()
      .WithForce()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq add -b -f feature/new",
      $"Expected 'gwq add -b -f feature/new', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqListCommand()
  {
    string command = Gwq.Run()
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq list",
      $"Expected 'gwq list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqListWithOptions()
  {
    string command = Gwq.Run()
      .List()
      .WithGlobal()
      .WithVerbose()
      .WithJson()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq list -g -v --json",
      $"Expected 'gwq list -g -v --json', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRemoveCommand()
  {
    string command = Gwq.Run()
      .Remove()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq remove",
      $"Expected 'gwq remove', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRemoveWithPattern()
  {
    string command = Gwq.Run()
      .Remove("feature/old")
      .WithDeleteBranch()
      .WithDryRun()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq remove -b -d feature/old",
      $"Expected 'gwq remove -b -d feature/old', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRmAlias()
  {
    string command = Gwq.Run()
      .Rm("feature/completed")
      .WithForceDeleteBranch()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq remove --force-delete-branch feature/completed",
      $"Expected 'gwq remove --force-delete-branch feature/completed', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusCommand()
  {
    string command = Gwq.Run()
      .Status()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq status",
      $"Expected 'gwq status', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusWithComprehensiveOptions()
  {
    string command = Gwq.Run()
      .Status()
      .WithGlobal()
      .WithJson()
      .WithFilter("changed")
      .WithSort("modified")
      .WithShowProcesses()
      .WithStaleDays(7)
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq status -g --json -f changed -s modified --show-processes --stale-days 7",
      $"Expected 'gwq status -g --json -f changed -s modified --show-processes --stale-days 7', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusWithWatchMode()
  {
    string command = Gwq.Run()
      .Status()
      .WithWatch()
      .WithInterval(10)
      .WithNoFetch()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq status -w -i 10s --no-fetch",
      $"Expected 'gwq status -w -i 10s --no-fetch', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqGetCommand()
  {
    string command = Gwq.Run()
      .Get("feature")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq get feature",
      $"Expected 'gwq get feature', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqGetWithNullTermination()
  {
    string command = Gwq.Run()
      .Get("main")
      .WithGlobal()
      .WithNull()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq get -g -0 main",
      $"Expected 'gwq get -g -0 main', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqExecCommand()
  {
    string command = Gwq.Run()
      .Exec("feature")
      .WithCommand("npm", "test")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq exec feature -- npm test",
      $"Expected 'gwq exec feature -- npm test', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqExecWithStayOption()
  {
    string command = Gwq.Run()
      .Exec("main")
      .WithCommand("git", "pull")
      .WithStay()
      .WithGlobal()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq exec -s -g main -- git pull",
      $"Expected 'gwq exec -s -g main -- git pull', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqConfigList()
  {
    string command = Gwq.Run()
      .ConfigList()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq config list",
      $"Expected 'gwq config list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqConfigGetAndSet()
  {
    string getCommand = Gwq.Run()
      .ConfigGet("worktree.basedir")
      .Build()
      .ToCommandString();
    
    string setCommand = Gwq.Run()
      .ConfigSet("worktree.basedir", "~/worktrees")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      getCommand == "gwq config get worktree.basedir",
      $"Expected 'gwq config get worktree.basedir', got '{getCommand}'"
    );
    
    AssertTrue(
      setCommand == "gwq config set worktree.basedir ~/worktrees",
      $"Expected 'gwq config set worktree.basedir ~/worktrees', got '{setCommand}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqPruneCommand()
  {
    string command = Gwq.Run()
      .Prune()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq prune",
      $"Expected 'gwq prune', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqVersionCommand()
  {
    string command = Gwq.Run()
      .Version()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq version",
      $"Expected 'gwq version', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqWithWorkingDirectoryAndEnvironment()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = Gwq.Run()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("GIT_DIR", "/tmp/.git")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq list",
      $"Expected 'gwq list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqPipeToExtension()
  {
    string command = Gwq.Run()
      .List()
      .PipeTo("grep", "feature")
      .ToCommandString();
    
    AssertTrue(
      command == "grep feature",
      $"Expected 'grep feature', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqCommandBuilds()
  {
    string command = Gwq.Run()
      .List()
      .WithGlobal()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq list -g",
      $"Expected 'gwq list -g', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqVersionExecution()
  {
    // Test command string generation for version command
    string command = Gwq.Run()
      .Version()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "gwq version",
      $"Expected 'gwq version', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}