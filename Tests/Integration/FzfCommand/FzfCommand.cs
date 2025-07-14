#!/usr/bin/dotnet run

await RunTests<FzfCommandTests>();

internal sealed class FzfCommandTests
{
  public static async Task TestBasicFzfBuilderCreation()
  {
    FzfBuilder fzfBuilder = Fzf.Run();
    
    AssertTrue(
      fzfBuilder != null,
      "Fzf.Run() should create builder successfully"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithInputItems()
  {
    CommandResult command = Fzf.Run()
      .FromInput("apple", "banana", "cherry")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with input items should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithMultiSelect()
  {
    CommandResult command = Fzf.Run()
      .WithMulti()
      .FromInput("item1", "item2", "item3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with multi-select should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithPreview()
  {
    CommandResult command = Fzf.Run()
      .WithPreview("echo {}")
      .FromInput("file1.txt", "file2.txt", "file3.txt")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with preview should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithHeightAndLayoutOptions()
  {
    CommandResult command = Fzf.Run()
      .WithHeightPercent(50)
      .WithLayout("reverse")
      .WithBorder("rounded")
      .FromInput("option1", "option2", "option3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with height and layout options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithSearchOptions()
  {
    CommandResult command = Fzf.Run()
      .WithExact()
      .WithCaseInsensitive()
      .WithScheme("path")
      .FromInput("path/to/file1", "path/to/file2", "another/path/file3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with search options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithInterfaceOptions()
  {
    CommandResult command = Fzf.Run()
      .WithCycle()
      .WithNoMouse()
      .WithScrollOff(3)
      .WithPrompt("Select: ")
      .FromInput("choice1", "choice2", "choice3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with interface options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithDisplayOptions()
  {
    CommandResult command = Fzf.Run()
      .WithAnsi()
      .WithColor("dark")
      .WithTabstop(4)
      .WithNoBold()
      .FromInput("colored text", "bold text", "normal text")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with display options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithHistoryOptions()
  {
    CommandResult command = Fzf.Run()
      .WithHistory("/tmp/fzf_history")
      .WithHistorySize(100)
      .FromInput("history1", "history2", "history3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with history options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithScriptingOptions()
  {
    CommandResult command = Fzf.Run()
      .WithQuery("initial")
      .WithSelect1()
      .WithExit0()
      .WithFilter("test")
      .FromInput("initial test", "other item", "another option")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with scripting options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithComprehensiveOptions()
  {
    CommandResult command = Fzf.Run()
      .WithMulti(5)
      .WithPreview("echo 'Preview: {}'")
      .WithPreviewWindow("right:50%")
      .WithHeightPercent(60)
      .WithBorder("sharp")
      .WithPrompt("Choose: ")
      .WithMarker("*")
      .WithPointer("=>")
      .WithHeader("Select multiple items")
      .FromInput("item1", "item2", "item3", "item4", "item5", "item6")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with comprehensive options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithFileInput()
  {
    CommandResult command = Fzf.Run()
      .FromFiles("*.cs")
      .WithPreview("head -20 {}")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with file input should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithCommandInput()
  {
    CommandResult command = Fzf.Run()
      .FromCommand("echo hello world")
      .WithPrompt("Select output: ")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with command input should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithWorkingDirectoryAndEnvironment()
  {
    CommandResult command = Fzf.Run()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("FZF_DEFAULT_OPTS", "--height 40%")
      .FromInput("env1", "env2", "env3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with working directory and environment variables should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithAdvancedLayoutOptions()
  {
    CommandResult command = Fzf.Run()
      .WithLayout("reverse-list")
      .WithBorderLabel("Selection")
      .WithBorderLabelPos("10")
      .WithMargin("1,2,3,4")
      .WithPadding("1")
      .WithInfo("inline")
      .WithSeparator("---")
      .FromInput("advanced1", "advanced2", "advanced3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with advanced layout options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithFieldProcessing()
  {
    CommandResult command = Fzf.Run()
      .WithNth("1,2")
      .WithWithNth("2..")
      .WithDelimiter(":")
      .FromInput("field1:field2:field3", "data1:data2:data3", "info1:info2:info3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with field processing should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithSortingAndTrackingOptions()
  {
    CommandResult command = Fzf.Run()
      .WithNoSort()
      .WithTac()
      .WithTrack()
      .WithTiebreak("length,begin")
      .FromInput("short", "medium length", "very long text item")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with sorting and tracking options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithScrollbarAndEllipsis()
  {
    CommandResult command = Fzf.Run()
      .WithScrollbar("█░")
      .WithEllipsis("...")
      .WithHeaderLines(2)
      .WithHeaderFirst()
      .FromInput("Header Line 1", "Header Line 2", "Content 1", "Content 2", "Content 3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with scrollbar and ellipsis should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithPreviewLabelOptions()
  {
    CommandResult command = Fzf.Run()
      .WithPreview("cat {}")
      .WithPreviewLabel("File Contents")
      .WithPreviewLabelPos(5)
      .WithPreviewWindow("right:60%:border")
      .FromInput("file1.txt", "file2.txt", "file3.txt")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with preview label options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithInputCollection()
  {
    var items = new List<string> { "collection1", "collection2", "collection3" };
    CommandResult command = Fzf.Run()
      .FromInput(items)
      .WithPrompt("From collection: ")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with input collection should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfFilterMode()
  {
    // This tests that the command builds correctly even if fzf might not be installed
    CommandResult command = Fzf.Run()
      .WithFilter("test")  // Use filter mode to avoid interactive requirement
      .FromInput("test1", "test2", "test3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf command should build correctly for filter mode"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithListenAndSyncOptions()
  {
    CommandResult command = Fzf.Run()
      .WithListen(8080)
      .WithSync()
      .FromInput("server1", "server2", "server3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with listen and sync options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithNullHandlingOptions()
  {
    CommandResult command = Fzf.Run()
      .WithRead0()
      .WithPrint0()
      .FromInput("null1", "null2", "null3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with null handling options should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithExpectAndPrintQuery()
  {
    CommandResult command = Fzf.Run()
      .WithExpect("ctrl-a,ctrl-b")
      .WithPrintQuery()
      .FromInput("expect1", "expect2", "expect3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with expect and print query should build correctly"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestFzfWithBindAndJumpLabels()
  {
    CommandResult command = Fzf.Run()
      .WithBind("ctrl-a:select-all")
      .WithJumpLabels("abcdefghij")
      .WithFilepathWord()
      .FromInput("path/to/file1", "path/to/file2", "different/path/file3")
      .Build();
    
    AssertTrue(
      command != null,
      "Fzf with bind and jump labels should build correctly"
    );
    
    await Task.CompletedTask;
  }
}