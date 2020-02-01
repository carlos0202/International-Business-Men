using International_Business_Men.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace International_Business_Men.DAL.Context
{
    public class LocalDbContext: DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RatesDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping primary key and money holder column for CurrencyRate Entity
            modelBuilder.Entity<CurrencyRate>()
                .HasKey(o => new { o.From, o.To });
            modelBuilder.Entity<CurrencyRate>()
                .Property(b => b.Rate)
                .HasColumnType("decimal(12, 4)");

            // Mapping primary key and money holder column for Transaction Entity
            modelBuilder.Entity<Transaction>()
                .HasKey(o => o.SKU);
            modelBuilder.Entity<Transaction>()
                .Property(b => b.Amount)
                .HasColumnType("decimal(16, 4)");
        }
    }
}
