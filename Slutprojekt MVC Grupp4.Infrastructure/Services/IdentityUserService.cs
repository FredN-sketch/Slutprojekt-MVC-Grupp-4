﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slutprojekt.Application.Dtos;
using Slutprojekt.Application.Users;
using Slutprojekt.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Infrastructure.Services;


public class IdentityUserService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    RoleManager<IdentityRole> roleManager) : IIdentityUserService
{
    const string RoleName = "Administrator";
    public async Task<UserResultDto> CreateUserAsync(UserProfileDto user, string password)
    {
        if (!await roleManager.RoleExistsAsync(RoleName))
            await roleManager.CreateAsync(new IdentityRole(RoleName));

        var appuser = new ApplicationUser
        {
            UserName = user.Email,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Admin = user.Admin
        };
        var result = await userManager.CreateAsync(appuser, password);
        // Lägg till en användare till en roll
        if (user.Admin)
            await userManager.AddToRoleAsync(appuser, RoleName);

        if (result.Succeeded) await userManager.AddClaimAsync(appuser, new Claim("FirstName", user.FirstName));

        return new UserResultDto(result.Errors.FirstOrDefault()?.Description);
    }

    public async Task<UserProfileDto[]> GetAllUsersAsync()
    {
        return await userManager.Users
            .Select(u => new UserProfileDto(u.Email!, u.FirstName, u.LastName, u.Admin))
            .ToArrayAsync();
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
