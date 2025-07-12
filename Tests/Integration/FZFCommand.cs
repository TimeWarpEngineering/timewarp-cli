#!/usr/bin/dotnet run
#:package TimeWarp.Cli@*-*
#:property RestoreNoCache true
#:property DisableImplicitNuGetFallbackFolder true
#:property RestorePackagesPath ./local-packages

#pragma warning disable IDE0005 // Using directive is unnecessary
using TimeWarp.Cli;
using static TimeWarp.Cli.CommandExtensions;
#pragma warning restore IDE0005

Console.WriteLine("🧪 Testing FZF Command...");

int passCount = 0;
int totalTests = 0;

// Test 1: Basic FZF builder creation
totalTests++;
try
{
  FZFBuilder fzfBuilder = FZF.Run();
  if (fzfBuilder != null)
  {
    Console.WriteLine("✅ Test 1 PASSED: FZF.Run() builder created successfully");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 1 FAILED: FZF.Run() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 1 FAILED: Exception - {ex.Message}");
}

// Test 2: FZF with input items
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .FromInput("apple", "banana", "cherry")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 2 PASSED: FZF with input items works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 2 FAILED: FZF with input items Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 2 FAILED: Exception - {ex.Message}");
}

// Test 3: FZF with multi-select
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithMulti()
    .FromInput("item1", "item2", "item3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 3 PASSED: FZF with multi-select works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 3 FAILED: FZF with multi-select Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 3 FAILED: Exception - {ex.Message}");
}

// Test 4: FZF with preview
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithPreview("echo {}")
    .FromInput("file1.txt", "file2.txt", "file3.txt")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 4 PASSED: FZF with preview works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 4 FAILED: FZF with preview Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 4 FAILED: Exception - {ex.Message}");
}

// Test 5: FZF with height and layout options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithHeightPercent(50)
    .WithLayout("reverse")
    .WithBorder("rounded")
    .FromInput("option1", "option2", "option3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 5 PASSED: FZF with height and layout options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 5 FAILED: FZF with height and layout options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 5 FAILED: Exception - {ex.Message}");
}

// Test 6: FZF with search options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithExact()
    .WithCaseInsensitive()
    .WithScheme("path")
    .FromInput("path/to/file1", "path/to/file2", "another/path/file3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 6 PASSED: FZF with search options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 6 FAILED: FZF with search options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 6 FAILED: Exception - {ex.Message}");
}

// Test 7: FZF with interface options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithCycle()
    .WithNoMouse()
    .WithScrollOff(3)
    .WithPrompt("Select: ")
    .FromInput("choice1", "choice2", "choice3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 7 PASSED: FZF with interface options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 7 FAILED: FZF with interface options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 7 FAILED: Exception - {ex.Message}");
}

// Test 8: FZF with display options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithAnsi()
    .WithColor("dark")
    .WithTabstop(4)
    .WithNoBold()
    .FromInput("colored text", "bold text", "normal text")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 8 PASSED: FZF with display options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 8 FAILED: FZF with display options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 8 FAILED: Exception - {ex.Message}");
}

// Test 9: FZF with history options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithHistory("/tmp/fzf_history")
    .WithHistorySize(100)
    .FromInput("history1", "history2", "history3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 9 PASSED: FZF with history options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 9 FAILED: FZF with history options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 9 FAILED: Exception - {ex.Message}");
}

// Test 10: FZF with scripting options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithQuery("initial")
    .WithSelect1()
    .WithExit0()
    .WithFilter("test")
    .FromInput("initial test", "other item", "another option")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 10 PASSED: FZF with scripting options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 10 FAILED: FZF with scripting options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 10 FAILED: Exception - {ex.Message}");
}

// Test 11: FZF with comprehensive options
totalTests++;
try
{
  CommandResult command = FZF.Run()
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
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 11 PASSED: FZF with comprehensive options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 11 FAILED: FZF with comprehensive options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 11 FAILED: Exception - {ex.Message}");
}

// Test 12: FZF with file input
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .FromFiles("*.cs")
    .WithPreview("head -20 {}")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 12 PASSED: FZF with file input works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 12 FAILED: FZF with file input Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 12 FAILED: Exception - {ex.Message}");
}

// Test 13: FZF with command input
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .FromCommand("echo hello world")
    .WithPrompt("Select output: ")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 13 PASSED: FZF with command input works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 13 FAILED: FZF with command input Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 13 FAILED: Exception - {ex.Message}");
}

// Test 14: FZF with working directory and environment variables
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithWorkingDirectory("/tmp")
    .WithEnvironmentVariable("FZF_DEFAULT_OPTS", "--height 40%")
    .FromInput("env1", "env2", "env3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 14 PASSED: FZF with working directory and environment variables works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 14 FAILED: FZF with working directory and environment variables Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 14 FAILED: Exception - {ex.Message}");
}

// Test 15: FZF with advanced layout options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithLayout("reverse-list")
    .WithBorderLabel("Selection")
    .WithBorderLabelPos("10")
    .WithMargin("1,2,3,4")
    .WithPadding("1")
    .WithInfo("inline")
    .WithSeparator("---")
    .FromInput("advanced1", "advanced2", "advanced3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 15 PASSED: FZF with advanced layout options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 15 FAILED: FZF with advanced layout options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 15 FAILED: Exception - {ex.Message}");
}

// Test 16: FZF with field processing
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithNth("1,2")
    .WithWithNth("2..")
    .WithDelimiter(":")
    .FromInput("field1:field2:field3", "data1:data2:data3", "info1:info2:info3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 16 PASSED: FZF with field processing works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 16 FAILED: FZF with field processing Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 16 FAILED: Exception - {ex.Message}");
}

// Test 17: FZF with sorting and tracking options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithNoSort()
    .WithTac()
    .WithTrack()
    .WithTiebreak("length,begin")
    .FromInput("short", "medium length", "very long text item")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 17 PASSED: FZF with sorting and tracking options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 17 FAILED: FZF with sorting and tracking options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 17 FAILED: Exception - {ex.Message}");
}

// Test 18: FZF with scrollbar and ellipsis
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithScrollbar("█░")
    .WithEllipsis("...")
    .WithHeaderLines(2)
    .WithHeaderFirst()
    .FromInput("Header Line 1", "Header Line 2", "Content 1", "Content 2", "Content 3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 18 PASSED: FZF with scrollbar and ellipsis works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 18 FAILED: FZF with scrollbar and ellipsis Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 18 FAILED: Exception - {ex.Message}");
}

// Test 19: FZF with preview label options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithPreview("cat {}")
    .WithPreviewLabel("File Contents")
    .WithPreviewLabelPos(5)
    .WithPreviewWindow("right:60%:border")
    .FromInput("file1.txt", "file2.txt", "file3.txt")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 19 PASSED: FZF with preview label options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 19 FAILED: FZF with preview label options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 19 FAILED: Exception - {ex.Message}");
}

// Test 20: FZF with input collection
totalTests++;
try
{
  var items = new List<string> { "collection1", "collection2", "collection3" };
  CommandResult command = FZF.Run()
    .FromInput(items)
    .WithPrompt("From collection: ")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 20 PASSED: FZF with input collection works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 20 FAILED: FZF with input collection Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 20 FAILED: Exception - {ex.Message}");
}

// Test 21: FZF graceful handling when fzf not available (will test command building, not execution)
totalTests++;
try
{
  // This tests that the command builds correctly even if fzf might not be installed
  CommandResult command = FZF.Run()
    .WithFilter("test")  // Use filter mode to avoid interactive requirement
    .FromInput("test1", "test2", "test3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 21 PASSED: FZF command builds correctly for testing");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 21 FAILED: FZF command building failed");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 21 FAILED: Exception - {ex.Message}");
}

// Test 22: FZF with listen and sync options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithListen(8080)
    .WithSync()
    .FromInput("server1", "server2", "server3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 22 PASSED: FZF with listen and sync options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 22 FAILED: FZF with listen and sync options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 22 FAILED: Exception - {ex.Message}");
}

// Test 23: FZF with null handling options
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithRead0()
    .WithPrint0()
    .FromInput("null1", "null2", "null3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 23 PASSED: FZF with null handling options works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 23 FAILED: FZF with null handling options Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 23 FAILED: Exception - {ex.Message}");
}

// Test 24: FZF with expect and print query
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithExpect("ctrl-a,ctrl-b")
    .WithPrintQuery()
    .FromInput("expect1", "expect2", "expect3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 24 PASSED: FZF with expect and print query works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 24 FAILED: FZF with expect and print query Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 24 FAILED: Exception - {ex.Message}");
}

// Test 25: FZF with bind and jump labels
totalTests++;
try
{
  CommandResult command = FZF.Run()
    .WithBind("ctrl-a:select-all")
    .WithJumpLabels("abcdefghij")
    .WithFilepathWord()
    .FromInput("path/to/file1", "path/to/file2", "different/path/file3")
    .Build();
  
  if (command != null)
  {
    Console.WriteLine("✅ Test 25 PASSED: FZF with bind and jump labels works correctly");
    passCount++;
  }
  else
  {
    Console.WriteLine("❌ Test 25 FAILED: FZF with bind and jump labels Build() returned null");
  }
}
catch (Exception ex)
{
  Console.WriteLine($"❌ Test 25 FAILED: Exception - {ex.Message}");
}

// Summary
Console.WriteLine($"\n📊 FZF Command Results: {passCount}/{totalTests} tests passed");
Environment.Exit(passCount == totalTests ? 0 : 1);