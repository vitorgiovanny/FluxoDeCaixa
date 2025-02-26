using ApiCredit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCredit.Infrastructure.Data.Mapping
{
    public class CashConfiguration : IEntityTypeConfiguration<Cash>
    {
        public void Configure(EntityTypeBuilder<Cash> builder)
        {
            builder.ToTable("Cash");
            builder.HasKey(x => x.Id);
            builder.OwnsOne(c => c.Amount, money =>
            {
                money.Property(m => m.Value)
                    .HasColumnName("Amount")
                    .IsRequired();
            });

            builder.HasOne(c => c.Cashier)
                .WithMany()
                .HasForeignKey(c => c.CashierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
