﻿// <auto-generated />
using System;
using CashBalance.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CashBalance.Infrastructure.Migrations
{
    [DbContext(typeof(CashBalanceContext))]
    [Migration("20250225143424_AddPropertyCashAndCashier")]
    partial class AddPropertyCashAndCashier
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CashBalance.Domain.Cashier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");
                });

            modelBuilder.Entity("CashBalance.Domain.Domain.Extract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("AtDelete")
                        .HasColumnType("datetime2");

                    b.Property<double>("Cash")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdCash")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdCashier")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Register")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Extracts");
                });
#pragma warning restore 612, 618
        }
    }
}
