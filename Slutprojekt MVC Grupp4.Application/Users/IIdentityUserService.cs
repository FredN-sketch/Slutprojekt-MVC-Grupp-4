using Slutprojekt.Application.Dtos;

namespace Slutprojekt.Application.Users
{
    public interface IIdentityUserService
    {
        public Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
        Task<UserProfileDto[]> GetAllUsersAsync();
        public Task<UserResultDto> SignInAsync(string email, string password);

        public Task SignOutAsync();
   
    }
}