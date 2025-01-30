using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RabbitMq.Adapter;

public class BaseRabbitMq
{
    
    private readonly IConfiguration _configuration;
    public BaseRabbitMq(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected  ConnectionFactory GetConnectionFactory()
    {
        
        var conf = _configuration.GetSection("ServiceBusSettings");
        string username = conf.GetSection("Username").Value ?? throw new Exception("username for queue connection not found");  
        string password = conf.GetSection("Password").Value ?? throw new Exception("password for queue connection not found");
        int port = int.Parse(conf.GetSection("Port").Value);
        string hostName = conf.GetSection("HostName").Value ?? throw new Exception("host name for queue connection not found");
        
        
        var factory = new ConnectionFactory
        {
            UserName = username,
            Password = password,
            Port = port,
            HostName = hostName,
        };
        return factory;
    }
}