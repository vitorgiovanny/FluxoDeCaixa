using CashBalance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBalance.Infrastructure.Data.Context.Mapping
{
    public class ExtractConfiguration : IEntityTypeConfiguration<Extract>
    {
        public void Configure(EntityTypeBuilder<Extract> builder)
        {
            builder.ToTable("Extracts");
            builder.HasKey(c => c.Id);

            builder.HasOne(p => p.Cashier)
                .WithMany(p => p.Extracts)
                .HasForeignKey(p => p.IdCashier)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cash)
                .WithMany(p => p.Extracts)
                .HasForeignKey(p => p.IdCash)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
