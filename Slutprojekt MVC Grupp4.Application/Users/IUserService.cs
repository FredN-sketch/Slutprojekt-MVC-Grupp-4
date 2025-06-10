using Slutprojekt.Application.Dtos;

namespace Slutprojekt.Application.Users
{
    public interface IUserService
    {
        Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password);
        Task<UserProfileDto[]> GetAllUsersAsync();
        Task<UserResultDto> SignInAsync(string email, string password);
        Task SignOutAsync();
    }
}