using Microsoft.EntityFrameworkCore;
using StockClient.Model;

namespace StockClient.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Stock> Stocks => Set<Stock>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Stocks;Trusted_Connection=True;");
        }
    }
}
