using DotNet6TestRabitMQ.Api.RabbitMQConsumer;
using DotNet6TestRabitMQ.Api.RabbitMQSender;
using DotNet6TestRabitMQ.Application;
using DotNet6TestRabitMQ.Domain;
using DotNet6TestRabitMQ.Repository;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepPerson, RepPerson>();
builder.Services.AddSingleton<IPersonService, PersonService>();
builder.Services.AddSingleton<IRabbitMqMessageSender, RabbitMqMessageSender>();
builder.Services.AddSingleton<IPersonApplication, PersonApplication>();
builder.Services.AddHostedService<RabbitMqMessageConsumer>();

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
