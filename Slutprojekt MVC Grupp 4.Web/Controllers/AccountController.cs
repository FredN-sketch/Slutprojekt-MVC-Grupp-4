using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slutprojekt.Application.Dtos;
using Slutprojekt.Application.Users;
using Slutprojekt.Web.Views.Account;
using Slutprojekt.Web.Controllers;

namespace Slutprojekt.Web.Controllers
{
    public class AccountController(IUserService userService) : Controller
    {
        [Authorize(Roles = "Administrator")]
        [HttpGet("admin")]
        public async Task<IActionResult> Admin()
        {
            var viewModel = (await userService.GetAllUsersAsync())
                .Select(u => new AdminVM
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsAdmin = u.Admin
                })
                .ToArray();
            return View(viewModel);
        }


        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterVM viewModel)
        {
            
            if (!ModelState.IsValid)
                return View();

            // Try to register user
            var userDto = new UserProfileDto(viewModel.Email, viewModel.FirstName, viewModel.LastName, viewModel.Admin);
            var result = await userService.CreateUserAsync(userDto, viewModel.Password);
            if (!result.Succeeded)
            {
                // Show error
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
                return View();
            }
            await userService.SignInAsync(viewModel.Email, viewModel.Password);
            // Redirect user
            return RedirectToAction(nameof(Index), "Breeds");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginVM viewModel)
        {
            if (!ModelState.IsValid)
                return View();

            // Check if credentials is valid (and set auth cookie)
            var result = await userService.SignInAsync(viewModel.Username, viewModel.Password);
            if (!result.Succeeded)
            {
                // Show error
                ModelState.AddModelError(string.Empty, result.ErrorMessage!);
                return View();
            }


            // Redirect user
            
            return RedirectToAction(nameof(Index), "Breeds");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

    }
}
