using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProjectV2.Models;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CAProjectV2.Data
{
    public class WebsiteContext : DbContext
    {
        protected IConfiguration configuration;
        public WebsiteContext(DbContextOptions<WebsiteContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Product { get; set; }
      
        public DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<WishList> WishList { get; set; }
        public DbSet<LogInViewModel> LogInViewModel { get; set; }

        public DbSet<ProfileViewModel> ProfileViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1001",
                ProductName = ".NET Charts",
                Description = "Brings powerful charting capabilities to your .NET applications.",
                Price = 99m,
                ImageUrl = "/images/chart.png",
                tag = "Course A"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1002",
                ProductName = ".NET PayPal",
                Description = "Integrate your .NET apps with PayPal the easy way!",
                Price = 69m,
                ImageUrl = "/images/paypal.png",
                tag = "Course B"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1003",
                ProductName = ".NET Numerics",
                Description = "Powerful numerical methods for your .NET aimulations.",
                Price = 199m,
                ImageUrl = "/images/numerics.png",
                tag = "Course C"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1004",
                ProductName = ".NET Monitoring",
                Description = "Add user metrics and monitor performance of your .NET apps",
                Price = 219m,
                ImageUrl = "/images/monitoring.png",
                tag = "Course D"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1005",
                ProductName = ".NET ML",
                Description = "Supercharged .NET machine learning libraries.",
                Price = 1.1m,
                ImageUrl = "/images/ml.png",
                tag = "Course E"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1006",
                ProductName = ".NET Security ",
                Description = "Protect and secure your .NET apps",
                Price = 399m,
                ImageUrl = "/images/security.png",
                tag = "Course F"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1007",
                ProductName = ".NET Location",
                Description = "Add accurate location awareness to your .NET apps",
                Price = 249m,
                ImageUrl = "/images/location.png",
                tag = "Course G"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1008",
                ProductName = ".NET Logger",
                Description = "Logs and aggregates events easily in your .NET apps.",
                Price = 49m,
                ImageUrl = "/images/logger.png",
                tag = "Course H"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1009",
                ProductName = ".NET Analytics",
                Description = "Performs data mining and analytics easily in .NET.",
                Price = 299m,
                ImageUrl = "/images/analytics.png",
                tag = "Course I"

            });

            //modelBuilder.Entity<ShoppingCartItem>().HasNoKey();
        }

    }
}
