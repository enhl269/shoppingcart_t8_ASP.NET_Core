using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CAProject1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CAProject1.Data
{
    public class WebsiteContext : IdentityDbContext<ApplicationUser>
    {
        public WebsiteContext(DbContextOptions<WebsiteContext> options) : base(options)
        { }

        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderDetails>  OrderDetails {get;set;}
        public DbSet<Product> Product {get;set;}
        public DbSet<ShoppingCart> ShoppingCart {get;set;}
        public DbSet<ShoppingCartItem> ShoppingCartItem {get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1001",
                ProductName = "Course A",
                Description = "This is Course A", 
                Price = 1.1m,
                ImageUrl = "/images/A.png",
                tag ="Course A"
                
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1002",
                ProductName = "Course B",
                Description = "This is Course B",
                Price = 1.1m,
                ImageUrl = "/images/B.png",
                tag = "Course B"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1003",
                ProductName = "Course C",
                Description = "This is Course C",
                Price = 1.1m,
                ImageUrl = "/images/C.png",
                tag = "Course C"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1004",
                ProductName = "Course D",
                Description = "This is Course D",
                Price = 1.1m,
                ImageUrl = "/images/D.jpg",
                tag = "Course D"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1005",
                ProductName = "Course E",
                Description = "This is Course E",
                Price = 1.1m,
                ImageUrl = "/images/E.png",
                tag = "Course E"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1006",
                ProductName = "Course F",
                Description = "This is Course F",
                Price = 1.1m,
                ImageUrl = "/images/F.jpg",
                tag = "Course F"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1007",
                ProductName = "Course G",
                Description = "This is Course G",
                Price = 1.1m,
                ImageUrl = "/images/G.jpg",
                tag = "Course G"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1008",
                ProductName = "Course A",
                Description = "This is Course H",
                Price = 1.1m,
                ImageUrl = "/images/H.jpg",
                tag = "Course H"

            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = "1009",
                ProductName = "Course I",
                Description = "This is Course I",
                Price = 1.1m,
                ImageUrl = "/images/I.png",
                tag = "Course I"

            });

            //modelBuilder.Entity<ShoppingCartItem>().HasNoKey();
        }

        }
}
