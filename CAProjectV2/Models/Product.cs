using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAProjectV2.Models
{
    public class Product
    {

        [Column("ProductId")]
        public string Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string tag { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    }
}
