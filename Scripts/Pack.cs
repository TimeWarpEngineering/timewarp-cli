#!/usr/bin/dotnet run
// Pack.cs - Pack and publish TimeWarp.Cli to local NuGet feed
#pragma warning disable IDE0005 // Using directive is unnecessary
using System.Diagnostics;
#pragma warning restore IDE0005

Console.WriteLine("Packing and publishing TimeWarp.Cli to local NuGet feed...");

try
{
    // First, clean any existing packages
    var packagesPath = Path.Combine("Source", "TimeWarp.Cli", "bin", "Release");
    if (Directory.Exists(packagesPath))
    {
        var nupkgFiles = Directory.GetFiles(packagesPath, "*.nupkg", SearchOption.AllDirectories);
        foreach (var file in nupkgFiles)
        {
            File.Delete(file);
            Console.WriteLine($"Deleted existing package: {file}");
        }
    }

    // Pack the project
    var packProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = "pack Source/TimeWarp.Cli/TimeWarp.Cli.csproj --configuration Release --no-build",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        }
    };

    packProcess.Start();
    string packOutput = await packProcess.StandardOutput.ReadToEndAsync();
    string packError = await packProcess.StandardError.ReadToEndAsync();
    await packProcess.WaitForExitAsync();

    Console.WriteLine(packOutput);
    if (!string.IsNullOrEmpty(packError))
    {
        Console.WriteLine($"Pack errors: {packError}");
    }

    if (packProcess.ExitCode != 0)
    {
        Console.WriteLine($"❌ Pack failed with exit code {packProcess.ExitCode}");
        Environment.Exit(packProcess.ExitCode);
    }

    // Find the generated package
    var newNupkgFiles = Directory.GetFiles(packagesPath, "*.nupkg", SearchOption.AllDirectories);
    if (newNupkgFiles.Length == 0)
    {
        Console.WriteLine("❌ No package file found after packing");
        Environment.Exit(1);
    }

    var packagePath = newNupkgFiles[0];
    Console.WriteLine($"📦 Package created: {packagePath}");

    // Push to local feed
    var pushProcess = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"nuget push \"{packagePath}\" --source Local",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        }
    };

    pushProcess.Start();
    string pushOutput = await pushProcess.StandardOutput.ReadToEndAsync();
    string pushError = await pushProcess.StandardError.ReadToEndAsync();
    await pushProcess.WaitForExitAsync();

    Console.WriteLine(pushOutput);
    if (!string.IsNullOrEmpty(pushError))
    {
        Console.WriteLine($"Push errors: {pushError}");
    }

    if (pushProcess.ExitCode == 0)
    {
        Console.WriteLine("✅ Package successfully published to local NuGet feed!");
    }
    else
    {
        Console.WriteLine($"❌ Push failed with exit code {pushProcess.ExitCode}");
        Environment.Exit(pushProcess.ExitCode);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ An error occurred: {ex.Message}");
    Environment.Exit(1);
}