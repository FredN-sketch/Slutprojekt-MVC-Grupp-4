using Slutprojekt.Application.Dtos;

namespace Slutprojekt.Web.Views.Account
{
    public class AdminVM
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
