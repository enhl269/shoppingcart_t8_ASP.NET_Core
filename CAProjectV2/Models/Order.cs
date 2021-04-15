using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAProjectV2.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        
        [Column("OrderId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id {get;set;}

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public decimal TotalAmount { get; set; }
        public string OrderDetailsId { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
        public virtual User User { get; set; }


    }
}
