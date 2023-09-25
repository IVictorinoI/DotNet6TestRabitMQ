using DotNet6TestRabitMQ.MessageBus;

namespace DotNet6TestRabitMQ.Api.RabbitMQSender
{
    public interface IRabbitMqMessageSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
