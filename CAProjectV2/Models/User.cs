using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { set; get; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Username")]
        public string UserName { set; get; }
        [Required]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { set; get; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { set; get; }

        public string PhoneNumber { set; get; }

        public virtual ICollection<Order> Order { get; set; }
       

    }
}
