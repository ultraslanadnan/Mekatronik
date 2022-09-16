using Mekatronik.Models;
using Microsoft.EntityFrameworkCore;

namespace Mekatronik.Data
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=LAPTOP-GF4JK3JF;database=Mekatronik;integrated security=true;");

        }
        public DbSet<Product> Products { get; set; } = null!;
    }
}
