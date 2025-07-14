#!/usr/bin/dotnet run

await RunTests<GhqCommandTests>();

internal sealed class GhqCommandTests
{
  public static async Task TestBasicGhqBuilderCreation()
  {
    GhqBuilder ghqBuilder = Ghq.Run();
    
    AssertTrue(
      ghqBuilder != null,
      "Ghq.Run() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqGetCommand()
  {
    CommandResult command = Ghq.Run()
      .Get("github.com/TimeWarpEngineering/timewarp-cli")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Get command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqCloneCommand()
  {
    CommandResult command = Ghq.Run()
      .Clone("github.com/user/repo")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Clone command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqGetWithOptions()
  {
    CommandResult command = Ghq.Run()
      .Get("github.com/user/repo")
      .WithShallow()
      .WithBranch("develop")
      .WithUpdate()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Get with options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqListCommand()
  {
    CommandResult command = Ghq.Run()
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq List command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqListWithOptions()
  {
    CommandResult command = Ghq.Run()
      .List()
      .WithFullPath()
      .WithExact()
      .FilterByVcs("git")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq List with options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqRemoveCommand()
  {
    CommandResult command = Ghq.Run()
      .Remove("github.com/old/repo")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Remove command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqRmCommandWithDryRun()
  {
    CommandResult command = Ghq.Run()
      .Rm("github.com/old/repo")
      .WithDryRun()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Rm command with dry-run should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqRootCommand()
  {
    CommandResult command = Ghq.Run()
      .Root()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Root command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqRootWithAllOption()
  {
    CommandResult command = Ghq.Run()
      .Root()
      .WithAll()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Root with all option should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqCreateCommand()
  {
    CommandResult command = Ghq.Run()
      .Create("github.com/user/new-repo")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Create command should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqWithWorkingDirectoryAndEnvironment()
  {
    CommandResult command = Ghq.Run()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("GHQ_ROOT", "/tmp/ghq")
      .List()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq with working directory and environment variables should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqGetWithAdvancedOptions()
  {
    CommandResult command = Ghq.Run()
      .Get("github.com/example/repo")
      .WithLook()
      .WithParallel()
      .WithSilent()
      .WithNoRecursive()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Get with advanced options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqGetWithVcsBackend()
  {
    CommandResult command = Ghq.Run()
      .Get("gitlab.com/user/project")
      .WithVcs("gitlab")
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Get with VCS backend should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqListWithUniqueOption()
  {
    CommandResult command = Ghq.Run()
      .List()
      .WithUnique()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq List with unique option should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqGetWithBareRepository()
  {
    CommandResult command = Ghq.Run()
      .Get("github.com/user/bare-repo")
      .WithBare()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq Get with bare repository should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqPipeToExtension()
  {
    CommandResult command = Ghq.Run()
      .List()
      .PipeTo("grep", "timewarp");
    
    AssertTrue(
      command != null,
      "Ghq PipeTo extension should work correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqCommandBuilds()
  {
    // This tests that the command builds correctly even if ghq might not be installed
    CommandResult command = Ghq.Run()
      .List()
      .WithFullPath()
      .Build();
    
    AssertTrue(
      command != null,
      "Ghq command should build correctly for testing"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestGhqRootExecution()
  {
    string result = await Ghq.Run()
      .Root()
      .GetStringAsync();
    
    // Even if ghq is not installed, should return empty string gracefully
    AssertTrue(
      result != null,
      "Ghq Root command execution should complete with graceful handling"
    );
  }

  public static async Task TestGhqListExecution()
  {
    string[] lines = await Ghq.Run()
      .List()
      .GetLinesAsync();
    
    // Even if ghq is not installed or no repos exist, should return empty array gracefully
    AssertTrue(
      lines != null,
      "Ghq List command execution should complete with graceful handling"
    );
  }
}