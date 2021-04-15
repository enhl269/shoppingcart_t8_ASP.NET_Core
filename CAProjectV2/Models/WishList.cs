using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAProjectV2.Models
{
    public class WishList
    {
        [Column("WishId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }

        public virtual ICollection<Product> Product { get; set; }
        public virtual ICollection<User> User { get; set; }


    }
}
