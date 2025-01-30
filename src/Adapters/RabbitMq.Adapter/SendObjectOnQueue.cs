using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Ports;
using RabbitMQ.Client;

namespace RabbitMq.Adapter;

public class SendObjectOnQueue:Base,ISendObjectOnQueue
{

    public SendObjectOnQueue(IConfiguration configuration) : base(configuration)
    {
        
    }
    
    public void  Execute(object request, EQueue queue)
    {
        var factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: queue.ToString(),
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        string requestMessage = JsonSerializer.Serialize(request);
        var body = Encoding.UTF8.GetBytes(requestMessage);
            
        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queue.ToString(),
            basicProperties: null,
            body: body);
    }
}