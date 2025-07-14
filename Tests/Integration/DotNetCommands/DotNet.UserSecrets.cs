#!/usr/bin/dotnet run

await RunTests<DotNetUserSecretsCommandTests>();

// Define a class to hold the test methods (NOT static so it can be used as generic parameter)
 
internal sealed class DotNetUserSecretsCommandTests

{
  public static async Task TestBasicDotNetUserSecretsBuilderCreation()
  {
    DotNetUserSecretsBuilder userSecretsBuilder = DotNet.UserSecrets();
    AssertTrue(userSecretsBuilder != null, "DotNet.UserSecrets() should return a valid builder instance");
  }

  public static async Task TestUserSecretsInitCommand()
  {
    CommandResult command = DotNet.UserSecrets()
      .Init()
      .Build();
    
    AssertTrue(command != null, "UserSecrets Init command should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsInitWithProject()
  {
    CommandResult command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .Init()
      .Build();
    
    AssertTrue(command != null, "UserSecrets Init with project should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsInitWithId()
  {
    CommandResult command = DotNet.UserSecrets()
      .WithId("my-app-secrets")
      .Init()
      .Build();
    
    AssertTrue(command != null, "UserSecrets Init with ID should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsSetCommand()
  {
    CommandResult command = DotNet.UserSecrets()
      .Set("ConnectionString", "Server=localhost;Database=MyApp;")
      .Build();
    
    AssertTrue(command != null, "UserSecrets Set command should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsSetWithProject()
  {
    CommandResult command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .Set("ApiKey", "secret-key-value")
      .Build();
    
    AssertTrue(command != null, "UserSecrets Set with project should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsRemoveCommand()
  {
    CommandResult command = DotNet.UserSecrets()
      .Remove("ConnectionString")
      .Build();
    
    AssertTrue(command != null, "UserSecrets Remove command should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsListCommand()
  {
    CommandResult command = DotNet.UserSecrets()
      .List()
      .Build();
    
    AssertTrue(command != null, "UserSecrets List command should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsClearCommand()
  {
    CommandResult command = DotNet.UserSecrets()
      .Clear()
      .Build();
    
    AssertTrue(command != null, "UserSecrets Clear command should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsWithAllOptions()
  {
    CommandResult command = DotNet.UserSecrets()
      .WithProject("MyApp.csproj")
      .WithId("my-app-secrets")
      .Set("DatabaseConnection", "Server=localhost;")
      .Build();
    
    AssertTrue(command != null, "UserSecrets with all options should return a valid CommandResult instance");
  }

  public static async Task TestWorkingDirectoryAndEnvironmentVariables()
  {
    CommandResult command = DotNet.UserSecrets()
      .WithWorkingDirectory("/tmp")
      .WithEnvironmentVariable("DOTNET_ENV", "test")
      .List()
      .Build();
    
    AssertTrue(command != null, "Working directory and environment variables should return a valid CommandResult instance");
  }

  public static async Task TestUserSecretsListCommandExecution()
  {
    // This will fail gracefully without a project but tests the execution path
    await AssertThrowsAsync<Exception>(
      async () => await DotNet.UserSecrets()
        .List()
        .GetStringAsync(),
      "should throw for user secrets list without valid project (default validation behavior)"
    );
  }
}