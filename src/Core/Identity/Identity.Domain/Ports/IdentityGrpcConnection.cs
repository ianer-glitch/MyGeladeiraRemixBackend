namespace Identity.Domain.Ports;

public interface IIdentityGrpcConnection
{
    public TClient GetGrpcClient<TClient>(string clientUrl) where TClient : class;
}