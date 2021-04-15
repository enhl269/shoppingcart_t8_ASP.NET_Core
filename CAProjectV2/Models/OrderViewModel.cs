using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Models
{
    public class OrderViewModel
    {
        public string ProductId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ActCode { get; set; }
        public string ImageUrl { get; set; }
    }
}
