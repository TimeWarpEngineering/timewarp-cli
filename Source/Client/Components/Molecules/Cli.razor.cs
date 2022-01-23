namespace TimeWarpCli.Components
{
  using Microsoft.AspNetCore.Components;
  using Microsoft.AspNetCore.Components.Web;
  using System;
  using System.Collections.Generic;
  using System.CommandLine;
  using System.CommandLine.Invocation;
  using System.CommandLine.Parsing;
  using System.Linq;
  using System.Threading.Tasks;
  using TimeWarpCli.Features.Bases;

  public partial class Cli : BaseComponent
  {
    protected readonly string TabKey = "Tab";
    protected readonly string EnterKey = "Enter";
    [Inject] public Parser Parser { get; set; }

    protected string Input { get; set; }

    protected List<string> Output { get; set; }

    //protected string Text => string.Join("<br/>", Output);

    private ParseResult ParseResult { get; set; }

    private bool KeyPressPreventDefault { get; set; }
    private bool KeyDownPreventDefault { get; set; }
    private int SuggestedIndex { get; set; }

    private string Suggestion => Suggestions.Count > 0 ? Suggestions[SuggestedIndex] : null;

    private List<string> Suggestions { get; set; }

    public Cli()
    {
      Output = new List<string>();
      Suggestions = new List<string>();
    }

    private void HandleKeyDown(KeyboardEventArgs aKeyboardEventArgs)
    {
      Console.WriteLine($"HandleKeyDown Key:{aKeyboardEventArgs.Key}");

      if (aKeyboardEventArgs.Key == TabKey)
      {
        KeyDownPreventDefault = true;
        HandleTabKeyPress(aKeyboardEventArgs);
      }
      else
      {
        KeyDownPreventDefault = false;
        Suggestions.Clear();
      }
    }

    protected async Task HandleKeyPress(KeyboardEventArgs aKeyboardEventArgs)
    {
      Console.WriteLine($"HandleKeyPress Key:{aKeyboardEventArgs.Key} Input:{Input}");
      if (aKeyboardEventArgs.Key == EnterKey)
      {
        WriteLine("You pressed Enter");
        try
        {
          string[] args = Input?.Split(' ');
          _ = await Parser.InvokeAsync(args);
        }
        catch (Exception e)
        {
          Console.WriteLine($"Error: {e.Message}");
          //Logger.LogError(e, "Error", args);
        }
        // Process the command
      }

    }

    protected override Task OnInitializedAsync()
    {
      Output.Add("Line 1");
      Output.Add("Welcome to the Timewarp CLI");
      Input = null;
      return base.OnInitializedAsync();
    }

    //protected string ReadLine() => "something";

    protected void WriteLine(string aString) => Output.Add(aString);

    private void HandleTabKeyPress(KeyboardEventArgs aKeyboardEventArgs)
    {
      ;
      Console.WriteLine($"HandleTabKeyPress ShiftKey:{aKeyboardEventArgs.ShiftKey}");
      if (Suggestions.Count > 0)
      {
        SuggestedIndex = aKeyboardEventArgs.ShiftKey ? MovePrevious() : MoveNext();
      }
      else
      {
        Console.WriteLine($"HandleTabKeyPress Parsing Input:{Input}");
        ParseResult = Parser.Parse(Input);
        Suggestions = ParseResult.GetCompletions().Select(x => x.Label).ToList();
        SuggestedIndex = 0;
      }
      WriteLine(Suggestion);
      Console.WriteLine($"HandleTabKeyPress Suggestion:{Suggestion}");
      IEnumerable<Token> matchedTokens = ParseResult.Tokens.TakeWhile(aToken => !ParseResult.UnmatchedTokens.Contains(aToken.Value));

      string newInput = string.Join(" ", matchedTokens.Select(t => t.Value));
      Console.WriteLine($"HandleTabKeyPress matchedTokens Joined:{newInput}");
      newInput = $"{newInput} {Suggestion}";

      Input = newInput ?? Input;

      Console.WriteLine($"HandleTabKeyPress Input:{Input}");
      StateHasChanged();
    }

    private int MoveNext()
    {
      //WriteLine("You pressed Tab");
      return (SuggestedIndex == Suggestions.Count - 1) ? 0 : SuggestedIndex + 1;
      ;
    }

    private int MovePrevious()
    {
      //WriteLine("You pressed Shft-Tab");
      return (SuggestedIndex == 0) ? Suggestions.Count - 1 : SuggestedIndex - 1;
      ;
    }
  }
}
