using Microsoft.AspNetCore.Identity;
using Slutprojekt.Application.Dtos;
using Slutprojekt.Application.Users;
using Slutprojekt.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Infrastructure.Services;


public class IdentityUserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<ApplicationUser> roleManager) : IIdentityUserService
{
    const string RoleName = "Administrator";
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
    {
        if (!await roleManager.RoleExistsAsync(RoleName))
            await roleManager.CreateAsync(new IdentityRole(RoleName));

        var result = await userManager.CreateAsync(new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Admin = user.Admin
        }, password);

        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserResultDto> SignInAsync(string email, string password)
    {
        var result = await signInManager.PasswordSignInAsync(email, password, false, false);
        return new UserResultDto(result.Succeeded ? null : "Invalid user credentials");
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
