using Microsoft.EntityFrameworkCore;
using StockClient.Model;

namespace StockClient.Database
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Stock> Stocks => Set<Stock>();
        public ApiDbContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Stocks;Trusted_Connection=True;");
        }
    }
}
