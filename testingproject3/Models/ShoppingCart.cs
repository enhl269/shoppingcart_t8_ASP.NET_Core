using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testingproject3.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
