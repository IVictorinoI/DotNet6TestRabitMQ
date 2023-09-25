using DotNet6TestRabitMQ.Api.RabbitMQSender;
using DotNet6TestRabitMQ.Application;
using DotNet6TestRabitMQ.Application.Dtos;
using DotNet6TestRabitMQ.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotNet6TestRabitMQ.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonApplication _personApplication;
        private readonly IRabbitMqMessageSender _rabbitMqMessageSender;
        public PersonController(IPersonApplication personApplication, IRabbitMqMessageSender rabbitMqMessageSender)
        {
            _personApplication = personApplication;
            _rabbitMqMessageSender = rabbitMqMessageSender;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var record = await _personApplication.GetByIdAsync(id);
                return Ok(record);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet]
        public async Task<IEnumerable<Person>> GetAll()
        {
            var records = await _personApplication.GetAllAsync();
            return records!;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonDto dto)
        {
            try
            {
                _rabbitMqMessageSender.SendMessage(dto, "person");

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update([FromQuery] int id)
        {
            try
            {
                await _personApplication.UpdateAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _personApplication.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
