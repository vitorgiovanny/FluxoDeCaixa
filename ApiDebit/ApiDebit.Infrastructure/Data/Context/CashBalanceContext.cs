using ApiDebit.Domain.Entities;
using ApiDebit.Infrastructure.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Infrastructure.Data.Context
{
    public class CashBalanceContext : DbContext
    {
        public DbSet<Cash> Cashes { get; set; }

        public CashBalanceContext(DbContextOptions<CashBalanceContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashConfiguration());
            base.OnModelCreating(modelBuilder);
        }

    }
}
