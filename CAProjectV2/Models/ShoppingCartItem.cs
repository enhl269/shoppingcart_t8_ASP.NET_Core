using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace CAProjectV2.Models
{
    public class ShoppingCartItem
    {
        //Maybe we can use the sessionId as 
        [Column("ShoppingCartItemId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        
        public string Id { get; set; }
        public string ShoppingCartId { get; set; }
        public string ShoppingCartItemEachProductId { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }



        public virtual Product Product {get;set;}
    }
}
