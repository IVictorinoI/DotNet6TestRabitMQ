using DotNet6TestRabitMQ.Domain;
using System.Text.Json;

namespace DotNet6TestRabitMQ.Application
{
    public class PersonApplication : IPersonApplication
    {
        private readonly IPersonService _personService;

        public PersonApplication(IPersonService personService)
        {
            _personService = personService;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var record =  await _personService.GetByIdAsync(id);
            return record;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            var records = await _personService.GetAllAsync();
            return records;
        }

        public async Task<Person> CreateAsync()
        {
            var record = await _personService.CreateAsync();
            return record;
        }

        public async Task<Person> UpdateAsync(int id)
        {
            var record = await _personService.UpdateAsync(id);
            return record;
        }

        public async Task DeleteAsync(int id)
        {
            await _personService.DeleteAsync(id);
        }
    }
}
