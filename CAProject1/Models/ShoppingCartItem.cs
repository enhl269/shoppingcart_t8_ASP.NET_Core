using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProject1.Models
{
    public class ShoppingCartItem
    {
        public string Id { get; set; }
        public string ShoppingCartId { get; set; }
        public string ShoppingCartItemEachProductId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }

        //public virtual Product Product {get;set;}
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
