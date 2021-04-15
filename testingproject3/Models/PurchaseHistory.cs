using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace testingproject3.Models
{

    public class MyContext : DbContext
    {
        public DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public DbSet<PurchaseHistories> PurchaseHistories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PurchaseHistories>()
                .HasMany(b => b.PurchaseHistory)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseHistory>()
                .HasOne(i=>i.PurchaseHistories)
                .WithMany(i=>i.PurchaseHistory)
                .HasForeignKey(p=>p.ProductId)
                .HasConstraintName("FK_History_Histories");
        }
    }
    public class PurchaseHistory
    {
        public int Id { get; set; }
        public int GroupId  { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Number")]
        public int Quantity { get; set; }
        public PurchaseHistories PurchaseHistories { get; set; }
    }

    public class PurchaseHistories
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Created on")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }
        public List<PurchaseHistory> PurchaseHistory { get; set; }
    }
}
