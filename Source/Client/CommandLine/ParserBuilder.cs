namespace TimeWarpCli.Client.CommandLine
{
  //using TimeWarpCli.Client.Commands.Sample;
  using FluentValidation;
  using MediatR;
  using Microsoft.Extensions.DependencyInjection;
  using System;
  using System.Collections.Generic;
  using System.CommandLine;
  using System.CommandLine.Builder;
  using System.Linq;
  using System.Reflection;

  internal class ParserBuilder : CommandLineBuilder
  {
    private static IServiceProvider ServiceProvider { get; set; }

    private IServiceCollection ServiceCollection { get; set; }

    private XmlDocReader XmlDocReader { get; }

    public ParserBuilder
    (
      IServiceProvider aServiceProvider,
      IServiceCollection aServiceCollection
    ) : base(new RootCommand())
    {
      Console.WriteLine("Yo Yo");
      Console.WriteLine("ParserBuilder ctor");
      ServiceProvider = aServiceProvider;
      Console.WriteLine("ParserBuilder ctor 2");
      ServiceCollection = aServiceCollection;
      Console.WriteLine("ParserBuilder ctor 3");
      string xmlPath = Assembly.GetAssembly(typeof(ParserBuilder)).Location.Replace(".dll", ".xml");
      Console.WriteLine("ParserBuilder ctor 4");
      XmlDocReader = new XmlDocReader(xmlPath);
      Console.WriteLine("ParserBuilder ctor 5");

      ConfigureServices(ServiceCollection);
      Console.WriteLine("ParserBuilder ctor 6");

      Configure();
      Console.WriteLine("ParserBuilder ctor 7");

    }

    public void ConfigureServices(IServiceCollection aServiceCollection)
    {
      aServiceCollection.AddMediatR(typeof(Program).Assembly);
      //aServiceCollection.AddScoped<IValidator<SampleRequest>, SampleValidator>();
      UseMediatorCommands();
    }

    private void BuildCommandForRequest(Type aType)
    {
      var handler = new ConsoleCommandHandler(aType, ServiceProvider.GetService<IMediator>());

      var command = new Command
      (
        name: aType.Name.Replace("Request", ""),
        description: XmlDocReader.GetDescriptionForType(aType)
      )
      {
        Handler = handler
      };

      // Add command Options from properties
      foreach (PropertyInfo propertyInfo in aType.GetProperties())
      {
        var option = new Option(
          aliases: new string[] { $"--{propertyInfo.Name}" },
          description: XmlDocReader.GetDescriptionForPropertyInfo(propertyInfo))
        {
          ArgumentHelpName = CreateGenericArgument(propertyInfo.PropertyType),
          IsHidden = false
        };
        command.AddOption(option);
      }

      this.Command.AddCommand(command);
    }

    private void Configure()
    {
      this.UseVersionOption()
      // middle-ware
      .UseHelp()
      .UseParseDirective()
      .UseDebugDirective()
      .UseSuggestDirective()
      .RegisterWithDotnetSuggest()
      .UseParseErrorReporting()
      .UseExceptionHandler();
    }

    private string CreateGenericArgument(Type aPropertyType)
    {
      Type argumentType = typeof(Argument<>);
      Type genericArgumentType = argumentType.MakeGenericType(aPropertyType);
      return Activator.CreateInstance(genericArgumentType) as string;
    }

    private ParserBuilder UseMediatorCommands()
    {
      // Get all serviceDescriptors that implement IRequestHandler
      string iRequestHandlerName = typeof(IRequestHandler<>).Name;
      const char GenericBackTic = '`';
      iRequestHandlerName = iRequestHandlerName.Substring(0, iRequestHandlerName.IndexOf(GenericBackTic));

      IEnumerable<ServiceDescriptor> serviceDescriptors = ServiceCollection.Where
      (
        aServiceDescriptor =>
        aServiceDescriptor.ServiceType.IsConstructedGenericType &&
        aServiceDescriptor.Lifetime == ServiceLifetime.Transient &&
        aServiceDescriptor.ServiceType.Name.Contains(iRequestHandlerName)
      );

      // Add Command for each IRequest Handled
      foreach (ServiceDescriptor serviceDescriptor in serviceDescriptors)
      {
        BuildCommandForRequest(serviceDescriptor.ServiceType.GenericTypeArguments[0]);
      }
      return this;
    }
  }
}
