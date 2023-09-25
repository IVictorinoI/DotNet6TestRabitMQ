using DotNet6TestRabitMQ.MessageBus;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using DotNet6TestRabitMQ.Application.Dtos;

namespace DotNet6TestRabitMQ.Api.RabbitMQSender
{
    public class RabbitMqMessageSender : IRabbitMqMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private readonly int _port;
        private IConnection _connection;

        public RabbitMqMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
            _port = 5672;
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                UserName = _userName,
                Password = _password,
                Port = _port
            };
            _connection = factory.CreateConnection();

            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            byte[] body = GetMessageAsByteArray(message);
            channel.BasicPublish(
                exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<PersonDto>((PersonDto)message, options);
            var body = Encoding.UTF8.GetBytes(json);
            return body;
        }
    }
}
