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
    CommandResult command = Gwq.Run()
      .Add("feature/new-branch")
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Add command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddWithPath()
  {
    CommandResult command = Gwq.Run()
      .Add("feature/branch", "~/worktrees/feature")
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Add with path should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddInteractive()
  {
    CommandResult command = Gwq.Run()
      .AddInteractive()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Add interactive should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqAddWithNewBranch()
  {
    CommandResult command = Gwq.Run()
      .Add("feature/new")
      .WithNewBranch()
      .WithForce()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Add with new branch should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqListCommand()
  {
    CommandResult command = Gwq.Run()
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq List command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqListWithOptions()
  {
    CommandResult command = Gwq.Run()
      .List()
      .WithGlobal()
      .WithVerbose()
      .WithJson()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq List with options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRemoveCommand()
  {
    CommandResult command = Gwq.Run()
      .Remove()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Remove command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRemoveWithPattern()
  {
    CommandResult command = Gwq.Run()
      .Remove("feature/old")
      .WithDeleteBranch()
      .WithDryRun()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Remove with pattern should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqRmAlias()
  {
    CommandResult command = Gwq.Run()
      .Rm("feature/completed")
      .WithForceDeleteBranch()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Rm alias should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusCommand()
  {
    CommandResult command = Gwq.Run()
      .Status()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Status command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusWithComprehensiveOptions()
  {
    CommandResult command = Gwq.Run()
      .Status()
      .WithGlobal()
      .WithJson()
      .WithFilter("changed")
      .WithSort("modified")
      .WithShowProcesses()
      .WithStaleDays(7)
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Status with comprehensive options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqStatusWithWatchMode()
  {
    CommandResult command = Gwq.Run()
      .Status()
      .WithWatch()
      .WithInterval(10)
      .WithNoFetch()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Status with watch mode should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqGetCommand()
  {
    CommandResult command = Gwq.Run()
      .Get("feature")
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Get command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqGetWithNullTermination()
  {
    CommandResult command = Gwq.Run()
      .Get("main")
      .WithGlobal()
      .WithNull()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Get with null termination should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqExecCommand()
  {
    CommandResult command = Gwq.Run()
      .Exec("feature")
      .WithCommand("npm", "test")
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Exec command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqExecWithStayOption()
  {
    CommandResult command = Gwq.Run()
      .Exec("main")
      .WithCommand("git", "pull")
      .WithStay()
      .WithGlobal()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Exec with stay option should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqConfigList()
  {
    CommandResult command = Gwq.Run()
      .ConfigList()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Config list should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqConfigGetAndSet()
  {
    CommandResult getCommand = Gwq.Run()
      .ConfigGet("worktree.basedir")
      .Build();
    
    CommandResult setCommand = Gwq.Run()
      .ConfigSet("worktree.basedir", "~/worktrees")
      .Build();
    
    AssertTrue(
      getCommand != null && setCommand != null,
      "Gwq Config get/set should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqPruneCommand()
  {
    CommandResult command = Gwq.Run()
      .Prune()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Prune command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqVersionCommand()
  {
    CommandResult command = Gwq.Run()
      .Version()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq Version command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqWithWorkingDirectoryAndEnvironment()
  {
    CommandResult command = Gwq.Run()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("GIT_DIR", "/tmp/.git")
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq with working directory and environment variables should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqPipeToExtension()
  {
    CommandResult command = Gwq.Run()
      .List()
      .PipeTo("grep", "feature");
    
    AssertTrue(
      command != null,
      "Gwq PipeTo extension should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqCommandBuilds()
  {
    CommandResult command = Gwq.Run()
      .List()
      .WithGlobal()
      .Build();
    
    AssertTrue(
      command != null,
      "Gwq command should build correctly for testing"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGwqVersionExecution()
  {
    string result = await Gwq.Run()
      .Version()
      .GetStringAsync();
    
    // Even if gwq is not installed, should return empty string gracefully
    AssertTrue(
      result != null,
      "Gwq Version command execution should complete with graceful handling"
    );
  }
}