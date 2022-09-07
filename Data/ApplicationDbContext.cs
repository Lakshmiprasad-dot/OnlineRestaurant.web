using OnlineRestaurant.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OnlineRestaurant.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }

        public DbSet<Order> Orders { get; set; }
      
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
      
        public DbSet<OnlineRestaurant.Web.Models.OrderStatus> OrderStatus { get; set; }
    }
}
