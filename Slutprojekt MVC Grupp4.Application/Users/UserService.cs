using Slutprojekt.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Application.Users;


public class UserService(IIdentityUserService identityUserService) : IUserService
{
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password) =>
        await identityUserService.CreateUserAsync(user, password);

    public async Task<UserProfileDto[]> GetAllUsersAsync()
    {
        return await identityUserService.GetAllUsersAsync();
    }

    public async Task<UserResultDto> SignInAsync(string email, string password) =>
        await identityUserService.SignInAsync(email, password);
    public async Task SignOutAsync() =>
        await identityUserService.SignOutAsync();
}

