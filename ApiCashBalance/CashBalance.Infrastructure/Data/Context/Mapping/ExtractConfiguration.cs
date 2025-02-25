using CashBalance.Domain.Domain;
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
            builder.HasKey(c => c.Id);
        }
    }
}
