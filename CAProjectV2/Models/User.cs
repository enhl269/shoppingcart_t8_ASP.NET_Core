using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Models
{
    public class User
    {
        
        [Column("UserId")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string sessionId { get; set; }
        public string UserImageUrl { get; set; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public string ConfirmPassword { set; get; }

        public string Email { set; get; }

        public string PhoneNumber { set; get; }

        public virtual ICollection<Order> Order { get; set; }
       

    }
}
