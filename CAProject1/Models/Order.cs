using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAProject1.Models
{
    public class Order
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string OrderId {get;set;}
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        public string UserId { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }


    }
}
