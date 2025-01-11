using Identity.Domain.Ports;
using Microsoft.Extensions.Configuration;

namespace Identity.Application.Helpers;

public class ConnectionHelper(IIdentityGrpcConnection con,IConfiguration conf) : IConnectionHelper
{
    private readonly IIdentityGrpcConnection _con = con;
    private readonly IConfiguration _conf = conf;    
    
    public T GetUserConnection<T>() where T : class
    {
        var userServiceUrl = _conf.GetSection("ConnectionStrings").GetSection("UserService").Value ?? string.Empty;
        ArgumentException.ThrowIfNullOrEmpty(userServiceUrl);
        return _con.GetGrpcClient<T>(userServiceUrl);   
    }
    
    public T GetPlanConnection<T>() where T : class
    {
        throw new NotImplementedException();
        var userServiceUrl = _conf.GetSection("ConnectionStrings").GetSection("UserService").Value ?? string.Empty;
        ArgumentException.ThrowIfNullOrEmpty(userServiceUrl);
        return _con.GetGrpcClient<T>(userServiceUrl);   
    }
}