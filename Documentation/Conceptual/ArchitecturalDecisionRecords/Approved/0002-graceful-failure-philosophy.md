# Graceful Failure Philosophy

* Status: accepted
* Architect: Steven T. Cramer
* Date: 2025-01-10

Technical Story: Design error handling strategy that aligns with shell scripting expectations

## Context and Problem Statement

Shell command execution can fail for various reasons (non-existent commands, permission issues, network problems). The library needs a consistent error handling strategy that provides predictable behavior for scripting scenarios.

## Decision Drivers

* Prevent scripts from crashing on command failures
* Provide consistent, predictable return values
* Align with shell scripting expectations
* Enable graceful degradation in automation scenarios
* Minimize exception handling boilerplate in scripts

## Considered Options

* Graceful failure with empty results
* Exception throwing on failures
* Result pattern with success/failure indication
* Optional pattern with null returns

## Decision Outcome

Chosen option: "Graceful failure with empty results", because it provides the most predictable scripting experience and aligns with shell behavior where failed commands don't crash the shell.

### Positive Consequences

* Scripts never crash due to command failures
* Consistent return types regardless of command success
* Natural shell-like behavior
* Minimal error handling boilerplate required
* Graceful degradation in automation scenarios

### Negative Consequences

* Silent failures can mask important errors
* No built-in way to detect command failure
* Debugging can be more difficult
* May not align with traditional .NET error handling patterns

## Pros and Cons of the Options

### Graceful failure with empty results

* Good, because prevents script crashes
* Good, because provides consistent return types
* Good, because aligns with shell behavior
* Good, because requires minimal error handling
* Good, because enables graceful degradation
* Bad, because masks command failures
* Bad, because no built-in failure detection

### Exception throwing on failures

* Good, because explicit error indication
* Good, because follows .NET conventions
* Good, because enables traditional error handling
* Good, because debugging is straightforward
* Bad, because scripts crash on command failures
* Bad, because requires extensive try-catch blocks
* Bad, because doesn't align with shell expectations

### Result pattern with success/failure indication

* Good, because explicit success/failure indication
* Good, because maintains return value on success
* Good, because enables conditional logic
* Good, because follows modern functional patterns
* Bad, because requires checking result status
* Bad, because adds complexity to simple scenarios
* Bad, because changes API surface significantly

### Optional pattern with null returns

* Good, because indicates failure with null
* Good, because enables null-conditional operations
* Good, because follows C# nullable patterns
* Bad, because requires null checking
* Bad, because inconsistent with collection returns
* Bad, because doesn't work well with arrays

## Implementation Details

All command execution methods follow the same pattern:

```csharp
// GetStringAsync returns empty string on failure
public async Task<string> GetStringAsync()
{
    if (Command == null) return string.Empty;
    
    try
    {
        var result = await Command.ExecuteBufferedAsync();
        return result.StandardOutput;
    }
    catch
    {
        return string.Empty;
    }
}

// GetLinesAsync returns empty array on failure
public async Task<string[]> GetLinesAsync()
{
    if (Command == null) return Array.Empty<string>();
    
    try
    {
        var result = await Command.ExecuteBufferedAsync();
        return result.StandardOutput.Split(/*...*/);
    }
    catch
    {
        return Array.Empty<string>();
    }
}
```

## Links

* Related to [ADR-0001](0001-use-static-api-entry-point.md) - Use Static API Entry Point
* Related to [ADR-0008](0008-integration-testing-strategy.md) - Integration Testing Strategy