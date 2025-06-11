using System.ComponentModel.DataAnnotations;

namespace Slutprojekt.Web.Views.Account
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
    }
}
