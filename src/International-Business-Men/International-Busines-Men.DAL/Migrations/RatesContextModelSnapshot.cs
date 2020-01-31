﻿// <auto-generated />
using International_Business_Men.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace International_Business_Men.DAL.Migrations
{
    [DbContext(typeof(RatesContext))]
    partial class RatesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("International_Business_Men.DAL.Models.CurrencyRate", b =>
                {
                    b.Property<string>("From")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("To")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(12, 4)");

                    b.HasKey("From", "To");

                    b.ToTable("CurrencyRates");
                });
#pragma warning restore 612, 618
        }
    }
}
