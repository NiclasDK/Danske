using Microsoft.EntityFrameworkCore;
using TaxCalculator.Models;

namespace TaxCalculator.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<TaxRecord> TaxRecords { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Municipality>().HasData(
                new Municipality
                {
                    MunicipalityCode = 1000,
                    Name = "Copenhagen",
                }
            );

            modelBuilder.Entity<TaxRecord>().HasData(
                new TaxRecord
                {
                    Id = 1,
                    MunicipalityId = 1000,
                    TaxRate = 0.1M,
                    StartDate = DateTime.Parse("2024-01-01T00:00:00Z"),
                    EndDate = DateTime.Parse("2024-12-31T23:59:59Z"),
                    TaxPrioritization = Priority.Yearly
                },
                new TaxRecord
                {
                    Id = 2,
                    MunicipalityId = 1000,
                    TaxRate = 0.2M,
                    StartDate = DateTime.Parse("2024-01-01T00:00:00Z"),
                    EndDate = DateTime.Parse("2024-01-31T23:59:59Z"),
                    TaxPrioritization = Priority.Monthly
                },
                new TaxRecord
                {
                    Id = 3,
                    MunicipalityId = 1000,
                    TaxRate = 0.3M,
                    StartDate = DateTime.Parse("2024-01-01T00:00:00Z"),
                    EndDate = DateTime.Parse("2024-12-31T23:59:59Z"),
                    TaxPrioritization = Priority.Weekly
                },
                new TaxRecord
                {
                    Id = 4,
                    MunicipalityId = 2000,
                    TaxRate = 0.4M,
                    StartDate = DateTime.Parse("2024-01-01T00:00:00Z"),
                    EndDate = DateTime.Parse("2024-12-31T23:59:59Z"),
                    TaxPrioritization = Priority.Yearly
                }
            );
        }

    }
}
