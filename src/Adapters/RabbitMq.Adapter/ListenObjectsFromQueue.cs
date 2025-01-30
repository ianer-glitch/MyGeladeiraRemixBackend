using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.Adapter;

public class ListenObjectsFromQueue : Base
{
    public ListenObjectsFromQueue(IConfiguration configuration) : base(configuration)
    {
    }
    
    public  void Execute<T>(Action<T> functionToRun, CancellationToken cancelToken, string queueName)
    {
        
        ConnectionFactory factory = GetConnectionFactory();
        using var connection =  factory.CreateConnection();
        using var channel =  connection.CreateModel();

        channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

    
        while (!cancelToken.IsCancellationRequested)
        {
            var consumer = new EventingBasicConsumer(channel);
            
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                var objectFromQueue = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(message);
                functionToRun(objectFromQueue);
            };

            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);
        }
        
    }
}