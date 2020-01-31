using International_Business_Men.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace International_Business_Men.DAL.Context
{
    public class RatesContext: DbContext
    {
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public RatesContext(DbContextOptions<RatesContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RatesDB;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrencyRate>()
                .HasKey(o => new { o.From, o.To });
            modelBuilder.Entity<CurrencyRate>()
                .Property(b => b.Rate)
                .HasColumnType("decimal(12, 4)");
        }
    }
}
