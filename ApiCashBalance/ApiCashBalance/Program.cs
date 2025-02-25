using CashBalance.Application.Services;
using CashBalance.Domain;
using CashBalance.Domain.Interfaces;
using CashBalance.Domain.RabbitMq;
using CashBalance.Infrastructure.Data.Consumers;
using CashBalance.Infrastructure.Data.Context;
using CashBalance.Infrastructure.Repository;
using CashBalance.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<RabbitMqConsumer>();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CashBalanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICashierServices, CashierServices>();
builder.Services.AddScoped<IExtractServices, ExtractServices>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped<IRepository<Cashier>, Repository<Cashier>>();

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
