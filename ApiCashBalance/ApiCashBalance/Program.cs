using CashBalance.Application.Services;
using CashBalance.Domain;
using CashBalance.Domain.Interfaces;
using CashBalance.Infrastructure.Data.Context;
using CashBalance.Infrastructure.Repository;
using CashBalance.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<CashBalanceContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
//    b => b.MigrationsAssembly(typeof(CashBalanceContext).Assembly.FullName)));


builder.Services.AddDbContext<CashBalanceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICashierServices, CashierServices>();
builder.Services.AddScoped<IRepository<Cashier>, Repository<Cashier>>();

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
