#!/usr/bin/dotnet run
// Pack.cs - Pack and publish TimeWarp.Cli to local NuGet feed
#pragma warning disable IDE0005 // Using directive is unnecessary
#pragma warning restore IDE0005

// Get script directory using CallerFilePath (C# equivalent of PowerShell's $PSScriptRoot)
static string GetScriptDirectory([CallerFilePath] string scriptPath = "")
{
  return Path.GetDirectoryName(scriptPath) ?? "";
}

// Push current directory, change to script directory for relative paths
string originalDirectory = Directory.GetCurrentDirectory();
string scriptDir = GetScriptDirectory();
Directory.SetCurrentDirectory(scriptDir);

Console.WriteLine("Packing and publishing TimeWarp.Cli to local NuGet feed...");
Console.WriteLine($"Script directory: {scriptDir}");
Console.WriteLine($"Working from: {Directory.GetCurrentDirectory()}");

try
{
    try
    {
        // First, clean any existing packages
    string packagesPath = "../Source/TimeWarp.Cli/bin/Release";
    if (Directory.Exists(packagesPath))
    {
        string[] nupkgFiles = Directory.GetFiles(packagesPath, "*.nupkg", SearchOption.AllDirectories);
        foreach (string file in nupkgFiles)
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
            Arguments = "pack ../Source/TimeWarp.Cli/TimeWarp.Cli.csproj --configuration Release --no-build",
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
    string[] newNupkgFiles = Directory.GetFiles(packagesPath, "*.nupkg", SearchOption.AllDirectories);
    if (newNupkgFiles.Length == 0)
    {
        Console.WriteLine("❌ No package file found after packing");
        Environment.Exit(1);
    }

    string packagePath = newNupkgFiles[0];
    Console.WriteLine($"📦 Package created: {packagePath}");

    // Clean LocalNuGetFeed directory to ensure only one version at a time
    string localFeedPath = "../LocalNuGetFeed";
    if (Directory.Exists(localFeedPath))
    {
        string[] existingPackages = Directory.GetFiles(localFeedPath, "*.nupkg");
        foreach (string existingPackage in existingPackages)
        {
            File.Delete(existingPackage);
            Console.WriteLine($"🗑️  Deleted existing package from local feed: {Path.GetFileName(existingPackage)}");
        }
    }

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
}
finally
{
    // Pop - restore original working directory
    Directory.SetCurrentDirectory(originalDirectory);
}