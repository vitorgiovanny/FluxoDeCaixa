using ApiDebit.Application.Services;
using ApiDebit.Domain.Entities;
using ApiDebit.Domain.Interfaces;
using ApiDebit.Domain.Model.Push;
using ApiDebit.Infrastructure.Data.Context;
using ApiDebit.Infrastructure.RabbitMq;
using ApiDebit.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddDbContext<CashBalanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHostedService<Published>();
builder.Services.AddScoped<ICashServices, CashServices>();
builder.Services.AddSingleton<IRabbitMqPublisher, Published>();
builder.Services.AddScoped<IRepository<Cash>, Repository<Cash>>();

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

app.Run("http://localhost:7002");
