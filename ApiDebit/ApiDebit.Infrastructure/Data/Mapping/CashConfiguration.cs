using ApiDebit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDebit.Infrastructure.Data.Mapping
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
        }

    }
}
