namespace DotNet6TestRabitMQ.Domain
{
    public interface IRepPerson
    {
        Task<Person?> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> CreateAsync(Person value);
        Task<Person> UpdateAsync(Person value);
        Task DeleteAsync(Person value);
    }
}
