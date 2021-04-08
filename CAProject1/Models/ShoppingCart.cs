using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAProject1.Models
{
    public class ShoppingCart
    {
        [Column("ShoppingCartId")]
        public string Id { get; set; }

        public string UserId{get;set;}
        

        //public ShoppingCartItem ShoppingCartItem { get; set; }
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
       
    }
}
