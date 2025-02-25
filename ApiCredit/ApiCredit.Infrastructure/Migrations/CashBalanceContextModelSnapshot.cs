﻿// <auto-generated />
using System;
using ApiCredit.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiCredit.Infrastructure.Migrations
{
    [DbContext(typeof(CashBalanceContext))]
    partial class CashBalanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiCredit.Domain.Entities.Cash", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AtLastUpdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AtRegister")
                        .HasColumnType("datetime2");

                    b.Property<double>("Cashs")
                        .HasColumnType("float");

                    b.Property<Guid>("IdCashed")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Cashes");
                });
#pragma warning restore 612, 618
        }
    }
}
