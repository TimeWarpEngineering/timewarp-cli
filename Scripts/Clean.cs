#!/usr/bin/dotnet run
// Clean.cs - Clean build artifacts and local NuGet packages
using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

Console.WriteLine("Cleaning build artifacts and local NuGet packages...");

try
{
    // Clean the project
    var cleanProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "clean Source/TimeWarp.Cli/TimeWarp.Cli.csproj",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        }
    };

    cleanProcess.Start();
    string cleanOutput = await cleanProcess.StandardOutput.ReadToEndAsync();
    string cleanError = await cleanProcess.StandardError.ReadToEndAsync();
    await cleanProcess.WaitForExitAsync();

    Console.WriteLine(cleanOutput);
    if (!string.IsNullOrEmpty(cleanError))
    {
        Console.WriteLine($"Clean errors: {cleanError}");
    }

    if (cleanProcess.ExitCode == 0)
    {
        Console.WriteLine("✅ Project cleaned successfully!");
    }
    else
    {
        Console.WriteLine($"❌ Clean failed with exit code {cleanProcess.ExitCode}");
    }

    // Clean local NuGet feed
    var localFeedPath = "LocalNuGetFeed";
    if (Directory.Exists(localFeedPath))
    {
        var timeWarpPackages = Directory.GetDirectories(localFeedPath, "timewarp.cli", SearchOption.AllDirectories);
        foreach (var packageDir in timeWarpPackages)
        {
            Directory.Delete(packageDir, true);
            Console.WriteLine($"🗑️  Removed: {packageDir}");
        }

        var nupkgFiles = Directory.GetFiles(localFeedPath, "TimeWarp.Cli.*.nupkg", SearchOption.AllDirectories);
        foreach (var file in nupkgFiles)
        {
            File.Delete(file);
            Console.WriteLine($"🗑️  Removed: {file}");
        }
    }

    // Clean NuGet cache for this package
    var cacheCleanProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "nuget locals all --clear",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        }
    };

    cacheCleanProcess.Start();
    string cacheOutput = await cacheCleanProcess.StandardOutput.ReadToEndAsync();
    string cacheError = await cacheCleanProcess.StandardError.ReadToEndAsync();
    await cacheCleanProcess.WaitForExitAsync();

    Console.WriteLine(cacheOutput);
    if (!string.IsNullOrEmpty(cacheError))
    {
        Console.WriteLine($"Cache clean errors: {cacheError}");
    }

    Console.WriteLine("✅ Cleanup completed!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ An error occurred: {ex.Message}");
    Environment.Exit(1);
}