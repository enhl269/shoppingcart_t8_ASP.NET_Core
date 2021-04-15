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

        public string FirstName { set; get; }

        public string LastName { set; get; }

        [Key]
        public string UserName { set; get; }

        public string Password { set; get; }

        public string ConfirmPassword { set; get; }

        public string Email { set; get; }

        public string PhoneNumber { set; get; }

        public string NewPassword { set; get; }

        public string OldPassword { set; get; }
    }
}
