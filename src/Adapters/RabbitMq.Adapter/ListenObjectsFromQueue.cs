using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ports;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.Adapter;

public class ListenObjectsFromQueue : BaseRabbitMq, IListenObjectsFromQueue
{
    
    public ListenObjectsFromQueue(IConfiguration configuration, IServiceProvider serviceProvider) : base(configuration)
    {
    }

    public Task ExecuteAsync<TIn, TOut>(Func<TIn, Task<TOut>> functionToRun, CancellationToken cancelToken,
        EQueue queue,IServiceProvider serviceProvider)
    {

        ConnectionFactory factory = GetConnectionFactory();
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: queue.ToString(),
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);


        var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                
                var body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                
                using (var scope = serviceProvider.CreateScope())
                {
                    var type =  scope.ServiceProvider.GetRequiredService<TIn>().GetType();
                    var objectFromQueue = Newtonsoft.Json.JsonConvert.DeserializeObject(message,type);
                    await functionToRun((TIn)objectFromQueue);
                    
                }
            };

        
        channel.BasicConsume(queue: queue.ToString(),
            autoAck: true,
            consumer: consumer);
        return Task.CompletedTask;

    }

}