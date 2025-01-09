namespace Identity.Domain.Ports;

public interface IIdentityGrpcConnection
{
    public TClient GetGrpcClient<TClient>() where TClient : class;
}