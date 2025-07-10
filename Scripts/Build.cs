#!/usr/bin/dotnet run
// Build.cs - Build the TimeWarp.Cli library
using System;
using System.Threading.Tasks;
using System.Diagnostics;

Console.WriteLine("Building TimeWarp.Cli library...");

try
{
    // Build the project
    var buildProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "build Source/TimeWarp.Cli/TimeWarp.Cli.csproj --configuration Release",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        }
    };

    buildProcess.Start();
    string output = await buildProcess.StandardOutput.ReadToEndAsync();
    string error = await buildProcess.StandardError.ReadToEndAsync();
    await buildProcess.WaitForExitAsync();

    Console.WriteLine(output);
    if (!string.IsNullOrEmpty(error))
    {
        Console.WriteLine($"Errors: {error}");
    }

    if (buildProcess.ExitCode == 0)
    {
        Console.WriteLine("✅ Build completed successfully!");
    }
    else
    {
        Console.WriteLine($"❌ Build failed with exit code {buildProcess.ExitCode}");
        Environment.Exit(buildProcess.ExitCode);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ An error occurred: {ex.Message}");
    Environment.Exit(1);
}