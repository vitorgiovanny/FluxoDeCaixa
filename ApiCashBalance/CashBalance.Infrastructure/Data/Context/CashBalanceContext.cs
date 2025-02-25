using CashBalance.Domain;
using CashBalance.Domain.Domain;
using CashBalance.Infrastructure.Data.Context.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CashBalance.Infrastructure.Data.Context
{
    public class CashBalanceContext : DbContext
    {
        public CashBalanceContext(DbContextOptions<CashBalanceContext> options) : base(options){}


        public DbSet<Cashier> Cashiers { get; set; }
        public DbSet<Extract> Extracts { get; set; }
        public DbSet<Cash> Cashes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashierConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}