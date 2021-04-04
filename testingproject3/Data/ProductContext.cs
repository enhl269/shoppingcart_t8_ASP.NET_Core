using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using testingproject3.Models;

namespace testingproject3.Data
{
    public class ProductContext: DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }

        
    }
}
