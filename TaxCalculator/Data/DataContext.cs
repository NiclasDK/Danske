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

    }
}
