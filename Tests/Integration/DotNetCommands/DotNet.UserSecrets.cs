#!/usr/bin/dotnet run

await RunTests<DotNetUserSecretsCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetUserSecretsCommandTests

{
  public static async Task TestBasicDotNetUserSecretsBuilderCreation()
  {
    // DotNet.UserSecrets() alone doesn't build a valid command - needs a subcommand
    DotNetUserSecretsBuilder userSecretsBuilder = DotNet.UserSecrets();
    
    AssertTrue(
      userSecretsBuilder != null,
      "DotNet.UserSecrets() should create a valid builder"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsInitCommand()
  {
    string command = DotNet.UserSecrets()
      .Init()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets init",
      $"Expected 'dotnet user-secrets init', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsInitWithProject()
  {
    string command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .Init()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets init --project MyApp.csproj",
      $"Expected 'dotnet user-secrets init --project MyApp.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsInitWithId()
  {
    string command = DotNet.UserSecrets()
      .WithId("my-app-secrets")
      .Init()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets init --id my-app-secrets",
      $"Expected 'dotnet user-secrets init --id my-app-secrets', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsSetCommand()
  {
    string command = DotNet.UserSecrets()
      .Set("ConnectionString", "Server=localhost;Database=MyApp;")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets set ConnectionString Server=localhost;Database=MyApp;",
      $"Expected 'dotnet user-secrets set ConnectionString Server=localhost;Database=MyApp;', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsSetWithProject()
  {
    string command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .Set("ApiKey", "secret-key-value")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets set ApiKey secret-key-value --project MyApp.csproj",
      $"Expected correct user-secrets set command with project, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsRemoveCommand()
  {
    string command = DotNet.UserSecrets()
      .Remove("ConnectionString")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets remove ConnectionString",
      $"Expected 'dotnet user-secrets remove ConnectionString', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsListCommand()
  {
    string command = DotNet.UserSecrets()
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets list",
      $"Expected 'dotnet user-secrets list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsClearCommand()
  {
    string command = DotNet.UserSecrets()
      .Clear()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets clear",
      $"Expected 'dotnet user-secrets clear', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsWithAllOptions()
  {
    string command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .WithId("my-app-secrets")
      .Set("DatabaseConnection", "Server=localhost;")
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets set DatabaseConnection Server=localhost; --project MyApp.csproj --id my-app-secrets",
      $"Expected correct user-secrets set command with all options, got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    // Note: Working directory and environment variables don't appear in ToCommandString()
    string command = DotNet.UserSecrets()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets list",
      $"Expected 'dotnet user-secrets list', got '{command}'"
    );
    
    await Task.CompletedTask;
  }

  public static async Task TestUserSecretsListCommandExecution()
  {
    // Test command string generation for list operation
    string command = DotNet.UserSecrets()
      .WithProject("test.csproj")
      .List()
      .Build()
      .ToCommandString();
    
    AssertTrue(
      command == "dotnet user-secrets list --project test.csproj",
      $"Expected 'dotnet user-secrets list --project test.csproj', got '{command}'"
    );
    
    await Task.CompletedTask;
  }
}