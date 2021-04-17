using System.ComponentModel.DataAnnotations;

namespace CAProjectV2.Models
{
    public class LogInViewModel
    {
        public LogInViewModel(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public LogInViewModel() : this("", "") { }

        [Key]
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
