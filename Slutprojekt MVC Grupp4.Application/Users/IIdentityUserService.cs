using Slutprojekt.Application.Dtos;

namespace Slutprojekt.Application.Users
{
    public interface IIdentityUserService
    {
        public Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);

        public Task<UserResultDto> SignInAsync(string email, string password);
    }
}