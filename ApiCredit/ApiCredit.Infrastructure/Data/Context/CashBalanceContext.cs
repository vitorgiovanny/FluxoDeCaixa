using ApiCredit.Domain.Entities;
using ApiCredit.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Infrastructure.Data.Context
{
    public class CashBalanceContext : DbContext
    {
        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Cashier> Cashiers { get; set; }

        public CashBalanceContext(DbContextOptions<CashBalanceContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
