using Microsoft.EntityFrameworkCore;

using Restaurant.Models;

namespace Restaurant.Repositories
{
    public class RestaurantDatabaseContext : DbContext
    {
        public DbSet<Table> Tables { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=RestaurantDatabase.db");
        }
    }
}
