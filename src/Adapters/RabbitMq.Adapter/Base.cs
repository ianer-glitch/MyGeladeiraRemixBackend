using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RabbitMq.Adapter;

public class Base
{
    
    private readonly IConfiguration _configuration;
    public Base(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected  ConnectionFactory GetConnectionFactory()
    {
        
        
        string username = _configuration.GetSection("Username").Value ?? throw new Exception("username for queue connection not found");  
        string password = _configuration.GetSection("Password").Value ?? throw new Exception("password for queue connection not found");
        int port = int.Parse(_configuration.GetSection("Port").Value);
        string hostName = _configuration.GetSection("HostName").Value ?? throw new Exception("host name for queue connection not found");
        
        
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