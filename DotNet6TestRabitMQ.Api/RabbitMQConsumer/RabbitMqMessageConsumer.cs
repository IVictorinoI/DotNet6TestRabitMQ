using System.Text;
using System.Text.Json;
using DotNet6TestRabitMQ.Application;
using DotNet6TestRabitMQ.Application.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DotNet6TestRabitMQ.Api.RabbitMQConsumer
{
    public class RabbitMqMessageConsumer : BackgroundService, IRabbitMqMessageConsumer
    {
        private IConnection _connection;
        private IModel _channel;
        private readonly IPersonApplication _personApplication;
        public RabbitMqMessageConsumer(IPersonApplication personApplication)
        {
            _personApplication = personApplication;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "person", false, false, false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (chanel, evt) =>
            {
                var content = Encoding.UTF8.GetString(evt.Body.ToArray());
                var dto = JsonSerializer.Deserialize<PersonDto>(content);
                ProcessPerson(dto!).GetAwaiter().GetResult();
                _channel.BasicAck(evt.DeliveryTag, false);
            };
            _channel.BasicConsume("person", false, consumer);
            return Task.CompletedTask;
        }

        public async Task ProcessPerson(PersonDto dto)
        {
            await _personApplication.CreateAsync();
        }
    }
}
