using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using testingproject3.Models;

namespace testingproject3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<testingproject3.Models.PurchaseHistory> PurchaseHistory { get; set; }
        public DbSet<testingproject3.Models.PurchaseHistories> PurchaseHistories { get; set; }
    }
}
