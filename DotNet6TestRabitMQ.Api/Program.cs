using DotNet6TestRabitMQ.Api.RabbitMQSender;
using DotNet6TestRabitMQ.Application;
using DotNet6TestRabitMQ.Domain;
using DotNet6TestRabitMQ.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepPerson, RepPerson>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonApplication, PersonApplication>();
builder.Services.AddSingleton<IRabbitMqMessageSender, RabbitMqMessageSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
