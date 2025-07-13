#!/bin/bash

echo "🧹 Clearing all TimeWarp.Cli package caches..."

# Clear from home directory NuGet cache
echo "Clearing ~/.nuget/packages/timewarp.cli/"
rm -rf ~/.nuget/packages/timewarp.cli/

# Clear from LocalNuGetCache in the repo
echo "Clearing LocalNuGetCache/timewarp.cli/"
rm -rf /home/steventcramer/repos/github.com/TimeWarpEngineering/timewarp-cli/LocalNuGetCache/timewarp.cli

# Clear from LocalNuGetFeed in the repo
echo "Clearing LocalNuGetFeed/*.nupkg"
rm -f /home/steventcramer/repos/github.com/TimeWarpEngineering/timewarp-cli/LocalNuGetFeed/TimeWarp.Cli.*.nupkg

# Clear from Tests/LocalNuGetCache
echo "Clearing Tests/LocalNuGetCache/timewarp.cli/"
rm -rf /home/steventcramer/repos/github.com/TimeWarpEngineering/timewarp-cli/Tests/LocalNuGetCache/timewarp.cli

# Clear any other common cache locations
echo "Clearing Tests/Integration/Core/LocalNuGetCache/timewarp.cli/"
rm -rf /home/steventcramer/repos/github.com/TimeWarpEngineering/timewarp-cli/Tests/Integration/Core/LocalNuGetCache/timewarp.cli

echo "✅ All TimeWarp.Cli package caches cleared!"