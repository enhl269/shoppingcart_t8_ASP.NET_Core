using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testingproject3.Models;
using Microsoft.EntityFrameworkCore;

namespace testingproject3.Data
{
    public class ShoppingCartContext: DbContext
    {
        public ShoppingCartContext(DbContextOptions<ShoppingCartContext> options) : base(options)
        { }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCart");
        }
    }
}
