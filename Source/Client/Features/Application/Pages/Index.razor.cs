namespace TimeWarpCli.Pages
{
  using MediatR;
  using Microsoft.AspNetCore.Components;
  using ProtoBuf.Grpc.Client;
  using System.Net.Http;
  using System.Threading.Tasks;
  using TimeWarpCli.Features.Bases;

  public partial class Index : BaseComponent
  {
    private const string RouteTemplate = "/";


    public static string GetRoute() => RouteTemplate;
  }
}
