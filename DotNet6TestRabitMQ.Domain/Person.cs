namespace DotNet6TestRabitMQ.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
