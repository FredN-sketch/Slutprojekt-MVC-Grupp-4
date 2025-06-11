using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Application.Dtos;
using Slutprojekt.Application.Users;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Web.Controllers;
using Slutprojekt.Web.Views.Account;
using Slutprojekt.Web.Views.Breeds;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Slutprojekt.Web.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task InsertBreed_NoParams_ExpectCorrectViewModel()
        {
            //Arrange
            var breedController = BreedsController.Item1;

            //Act
            var result = await breedController.InsertBreed();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<InsertBreedVM>(viewResult.Model);
        }

        [Theory]
        //   [InlineData(new InsertBreedVM { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" }, true)]
        [InlineData(3, 1, "Schäfer", "tysk hund", "", true)]
        [InlineData(4, 99, "Tax", "", "Breedtype 1-10", false)]
        [InlineData(5, 2, null, "", "Breedname is required", false)]
        [InlineData(6, null, "Sankt Bernard", "", "Breedtype 1-10", false)]

        public async Task InsertBreed_WithParams_ExpectCorrectViewModel(int id, int breedType, string breedName, string description, string msg, bool expected)
        {
            //Arrange
            var viewModel = new InsertBreedVM { Id = id, BreedType = breedType, BreedName = breedName, Description = description };

            var o = BreedsController;
            var breedController = o.Item1;
            var breedServiceMock = o.Item2;
            var breedTypeServiceMock = o.Item3;

            //Testa Validering för modelbinding
            var context = new ValidationContext(viewModel);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(viewModel, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var item in results)
                {
                    Assert.Equal(msg, item.ErrorMessage);
                    breedController.ModelState.AddModelError("TEST", item.ErrorMessage!);
                }
            }

            var result = await breedController.InsertBreed(viewModel);

            // Assert
            if (expected)
            {
                Assert.True(isValid);
                var redirect = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(nameof(breedController.Index), redirect.ActionName);
                breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Once);
            }
            else
            {
                Assert.False(isValid);
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<InsertBreedVM>(viewResult.Model);
                breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Never);
            }
        }
        [Fact]
        public async Task Admin_ReturnsView_WithExpectedUsers_WhenUserIsAdmin()
        {
            // Arrange
            var mockUserService = new Mock<IUserService>();
            mockUserService.Setup(s => s.GetAllUsersAsync())
                .Returns(Task.FromResult(new UserProfileDto[]
                {
            new ("admin@example.com", "Admin", "User", true),
            new ("user@example.com", "User", "User", false) }));

            var controller = new AccountController(mockUserService.Object);

            var adminUser = new ClaimsPrincipal(new ClaimsIdentity(
            [
        new Claim(ClaimTypes.Name, "admin@example.com"),
        new Claim(ClaimTypes.Role, "Administrator")
            ], "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = adminUser }
            };

            // Act
            var result = await controller.Admin();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<AdminVM[]>(viewResult.Model, exactMatch: false);

            Assert.Equal(2, model.Length);
            Assert.Equal("admin@example.com", model[0].Email);
            Assert.True(model[0].IsAdmin);
        }
        private (BreedsController, Mock<IBreedService>, Mock<IBreedTypeService>) BreedsController
        {
            get
            {
                var breedService = new Mock<IBreedService>();
                var breedTypeService = new Mock<IBreedTypeService>();
                breedTypeService.Setup(service => service.GetAllBreedTypesAsync())
                    .Returns(Task.FromResult(new BreedType[]
                    {
                new() { Id = 1, BreedTypeName = "Grupp 1" },
                new() { Id = 2, BreedTypeName = "Grupp 2" }
                    }));
                breedService.Setup(service => service.GetAllBreedsAsync())
                   .Returns(Task.FromResult(new Breed[]
                   {
                new() { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" },
                new() { Id = 2, BreedType = 2, BreedName = "Tax", Description = "betalar hundskatt" }
                   }));
                return (new BreedsController(breedService.Object, breedTypeService.Object), breedService, breedTypeService);

            }
        }
    }
}
