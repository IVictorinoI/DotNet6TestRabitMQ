using DotNet6TestRabitMQ.Domain;

namespace DotNet6TestRabitMQ.Repository
{
    public static class MemoryDatabase
    {
        public static List<Person> Persons { get; set; } = new List<Person>();
    }
}
