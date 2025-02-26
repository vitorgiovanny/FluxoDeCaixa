using CashBalance.Domain;
using CashBalance.Domain.Entities;
using CashBalance.Infrastructure.Data.Context.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CashBalance.Infrastructure.Data.Context
{
    public class CashBalanceContext : DbContext
    {
        public CashBalanceContext(DbContextOptions<CashBalanceContext> options) : base(options){}


        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Extract> Extracts { get; set; }
        public DbSet<Cash> Cash { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashierConfiguration());
            modelBuilder.ApplyConfiguration(new CashConfiguration());
            modelBuilder.ApplyConfiguration(new ExtractConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}