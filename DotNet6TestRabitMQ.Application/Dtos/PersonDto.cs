using DotNet6TestRabitMQ.MessageBus;

namespace DotNet6TestRabitMQ.Application.Dtos
{
    public class PersonDto : BaseMessage
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
