using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace foodTrackerApi.Models
{
    public class FoodContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodEntry> FoodEntries { get; set; }
        public FoodContext(DbContextOptions<FoodContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }
    }
}