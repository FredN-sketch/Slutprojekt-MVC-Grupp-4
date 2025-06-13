
namespace Slutprojekt.Web.Views.Account
{
    public class AdminVM
    {
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
    }
}
