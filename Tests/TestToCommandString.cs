#!/usr/bin/dotnet run

// Test the new ToCommandString() method

Console.WriteLine("=== Testing ToCommandString() ===\n");

// Test with Gwq
string gwqCommand = Gwq.Run().List().WithGlobal().Build().ToCommandString();
Console.WriteLine($"Gwq command: {gwqCommand}");

// Test with Shell.Run
string shellCommand = Shell.Run("git").WithArguments("status", "--short").Build().ToCommandString();
Console.WriteLine($"Shell command: {shellCommand}");

// Test with pipeline
string pipelineCommand = Shell.Run("find")
    .WithArguments(".", "-name", "*.cs")
    .Pipe("grep", "TODO")
    .Pipe("wc", "-l")
    .ToCommandString();
Console.WriteLine($"Pipeline command: {pipelineCommand}");

// Test with DotNet
string dotnetCommand = DotNet.Build()
    .WithProject("MyProject.csproj")
    .WithConfiguration("Release")
    .Build()
    .ToCommandString();
Console.WriteLine($"DotNet command: {dotnetCommand}");