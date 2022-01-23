namespace TimeWarpCli.Client.CommandLine
{
  using MediatR;
  using System;
  using System.CommandLine;
  using System.CommandLine.Invocation;
  using System.CommandLine.Parsing;
  using System.Threading.Tasks;

  internal class ConsoleCommandHandler : ICommandHandler
  {
    private IMediator Mediator { get; }
    private Type Type { get; }

    public ConsoleCommandHandler(Type aType, IMediator aMediator)
    {
      Type = aType;
      Mediator = aMediator;
    }

    public async Task<int> InvokeAsync(InvocationContext aInvocationContext)
    {
      try
      {
        object request = Activator.CreateInstance(Type);
        foreach (SymbolResult symbolResult in aInvocationContext.ParseResult.CommandResult.Children)
        {
          var optionResult = symbolResult as OptionResult;
          object value = optionResult.GetValueOrDefault();
          Type.GetProperty(symbolResult.Symbol.Name).SetValue(request, value);
        }

        Type requestType = Type.GetInterface(typeof(IRequest).Name);
        if (requestType != null)
        {
          _ = await Mediator.Send((IRequest)request);
        }
        else
        {
          Type genericRequestType = Type.GetInterface(typeof(IRequest<>).Name);
          Type genericTypeArguments = genericRequestType.GenericTypeArguments[0];
          var task = (Task)Mediator
            .GetType()
            .GetMethod(nameof(Mediator.Send))
            .MakeGenericMethod(genericTypeArguments)
            .Invoke(Mediator, new object[] { request, null });
          await task;
        }

        return 0;
      }
      catch (Exception aException)
      {
        Console.WriteLine($"Error: {aException.Message}");
        return 1;
      }
    }
  }
}
