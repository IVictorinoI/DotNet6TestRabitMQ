using DotNet6TestRabitMQ.Domain;

namespace DotNet6TestRabitMQ.Application
{
    public interface IPersonApplication
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> CreateAsync();
        Task<Person> UpdateAsync(int id);
        Task DeleteAsync(int id);
    }
}
