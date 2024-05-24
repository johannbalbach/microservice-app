using RabbitMQ.Client;
using Shared.Interfaces;
using Shared.RabbitMq;
using System.Text;
using System.Text.Json;

namespace Common.ServiceBus
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqService()
        {
            _factory = new ConnectionFactory() { HostName = RabbitMqConfigure.HostName };
        }

        public void SendMessage<T>(T message)
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            SendMessage(serializedMessage);
        }

        private void SendMessage(string message)
        {
            using (var connection = _factory.CreateConnection())
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
