using BurgerShop.Models;
using Microsoft.EntityFrameworkCore;
namespace BurgerShop.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }
        // Create tables in database
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<User>().HasData(
                new User() { MobileNo = "1234567890", Password = "Sumeet" },
                new User() { MobileNo = "8484964558", Password = "Sumeet@123" }
                );

            modelBuilder.Entity<Order>().HasData(
                new Order() { Id = 9, UserId = 1, BurgerType = "Crispy Burger", BurgerCategory = "Veg", Quantity = 2, Price = 100, TotalPrice = 200 },
                new Order() { Id = 8, UserId = 1, BurgerType = "Tandoor Grill", BurgerCategory = "Non-Veg", Quantity = 1, Price = 200, TotalPrice = 200 },
                new Order() { Id = 7, UserId = 2, BurgerType = "Whopper", BurgerCategory = "Veg", Quantity = 1, Price = 100, TotalPrice = 100 }
                );*/

            modelBuilder.Entity<Menu>().HasData(
        new Menu() { Id = 1, BurgerName = "Crispy Supreme", ImageUrl = "~/images/burger_image.png",BasePrice=100, Description = "A serving of the Crispy Chicken Supreme Burger from Betty’s Burgers clocks in at a hearty 865 calories. That’s quite a culinary adventure! 🌟" },
        new Menu() { Id = 2, BurgerName = "Surprise", ImageUrl = "~/images/burger_image.png", BasePrice = 100, Description = "The legendary surprise burger—a delightful concoction that combines nostalgia, creativity, and a touch of culinary magic! 🍔✨" },
        new Menu() { Id = 3, BurgerName = "Whopper", ImageUrl = "~/images/burger_image.png", BasePrice = 100, Description = "The Whopper—a legendary burger that has graced fast-food menus since 1957. 🍔" },
        new Menu() { Id = 4, BurgerName = "Chilli Cheese", ImageUrl = "~/images/burger_image.png", BasePrice = 100, Description = "The world of Chili Cheese Burgers—a delightful fusion of juicy burgers, melty cheese, and a kick of chili goodness. 🍔🌶️" },
        new Menu() { Id = 5, BurgerName = "Tandoor Grill", ImageUrl = "~/images/burger_image.png", BasePrice = 100, Description = "The flavorful world of Tandoori Chicken Burgers—a delightful twist on traditional burgers that brings together bold spices, juicy chicken, and a touch of South Asian flair. 🍔🌶️" }
    );
        }
    }
}
