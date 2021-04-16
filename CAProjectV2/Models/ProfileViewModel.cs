using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CAProjectV2.Models
{
    public class ProfileViewModel
    {
        public ProfileViewModel(string userImageUrl, string firstName, string lastName, string userName, string email, string phoneNumber, string newPassword, string oldPassword)
        {
            UserImageUrl = userImageUrl;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }

        public ProfileViewModel() : this("", "", "", "", "", "", "", "") { } 

        
        public string UserImageUrl { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { set; get; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { set; get; }

        [Key]
        [Required]
        [Display(Name = "Username")]
        public string UserName { set; get; }
        [Required]
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

        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { set; get; }
        [Required]
        [Display(Name = "Current Password")]
        [Compare("Password", ErrorMessage = "The password entered does not match the current password.")]
        public string OldPassword { set; get; }
    }
}
