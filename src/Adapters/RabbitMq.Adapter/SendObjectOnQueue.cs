using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RabbitMq.Adapter;

public class SendObjectOnQueue(IConfiguration configuration) : Base(configuration)
{
    public void  Execute(object request, string queueName)
    {
        var factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        string requestMessage = JsonSerializer.Serialize(request);
        var body = Encoding.UTF8.GetBytes(requestMessage);
            
        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            basicProperties: null,
            body: body);
    }
}