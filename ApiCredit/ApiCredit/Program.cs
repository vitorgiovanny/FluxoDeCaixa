using ApiCredit.Application.Services;
using ApiCredit.Domain.Entities;
using ApiCredit.Domain.Interfaces;
using ApiCredit.Domain.RabbitMq;
using ApiCredit.Infrastructure.Data.Context;
using ApiCredit.Infrastructure.RabbitMq.Pushed;
using ApiCredit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddDbContext<CashBalanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHostedService<Publisher>();

builder.Services.AddScoped<ICashServices, CashServices>();
builder.Services.AddScoped<IRepository<Cash>, Repository<Cash>>();
builder.Services.AddSingleton<IRabbitMqPublisher, Publisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run("http://localhost:7001");
