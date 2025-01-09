using Grpc.Net.Client;
using Identity.Domain.Ports;


namespace Grpc.Adapter;

public class Connection : IIdentityGrpcConnection
{
    public TClient GetGrpcClient<TClient>(string clientUrl) where TClient : class
    {
        try
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            var channel = GrpcChannel.ForAddress(clientUrl, new GrpcChannelOptions { HttpHandler = handler });
            
            var client = (TClient)Activator.CreateInstance(typeof(TClient),channel)!;
            
            ArgumentNullException.ThrowIfNull(client,"Client Grpc Not Found");
            
            return client;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
