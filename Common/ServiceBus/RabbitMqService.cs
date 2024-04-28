using RabbitMQ.Client;
using Shared.Interfaces;
using Shared.RabbitMq;
using System.Text;
using System.Text.Json;

namespace Common.ServiceBus
{
    public class RabbitMqService : IRabbitMqService
    {
        public void SendMessage(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            SendMessage(message);
        }

        public void SendMessage(string message)
        {
            // Не забудьте вынести значения "localhost" и "MyQueue"
            // в файл конфигурации
            var factory = new ConnectionFactory() { HostName = RabbitMqConfigure.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: RabbitMqConfigure.queueName,
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                               routingKey: RabbitMqConfigure.queueName,
                               basicProperties: null,
                               body: body);
            }
        }
    }
}
