using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAProject1.Models
{
    public class OrderDetails
    {
        [Column("OrderId")]
        public string Id { get; set; }

        public string ActivationCode { get; set; }
        public string ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18, 2)")]
        public decimal IndivProductPrice { get; set; }

        public virtual Order Order { get; set; }
    }
}
