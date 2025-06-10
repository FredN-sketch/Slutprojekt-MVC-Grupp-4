using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Web.Controllers;
using Slutprojekt.Web.Views.Breeds;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Slutprojekt.Web.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task InsertBreedNoParamsExpectCorrectViewModelAsync()
        {
            //Arrange
            var breedController = GetBreedsController().Item1;

            //Act
            var result = await breedController.InsertBreed();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<InsertBreedVM>(viewResult.Model);
        }

        [Theory]
        //   [InlineData(new InsertBreedVM { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" }, true)]
        [InlineData(3, 1, "Schäfer", "tysk hund", true)]
        [InlineData(4, 99, "Tax", "", false)]
        [InlineData(5, 2, null, "", false)]
        [InlineData(6, null, "Sankt Bernard", "", false)]

        public async Task InsertBreedWithParamsExpectCorrectViewModelAsync(int id, int breedType, string breedName, string description, bool expected)
        {
            //Arrange
            var viewModel = new InsertBreedVM { Id = id, BreedType = breedType, BreedName = breedName, Description = description };

            var o = GetBreedsController();
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
                    switch (viewModel.Id) 
                    {
                        case 6:
                    //        Assert.Equal("Breedtype is required", item.ErrorMessage);
                    //        break;
                        case 4: Assert.Equal("Breedtype 1-10", item.ErrorMessage);
                            break;
                        case 5: Assert.Equal("Breedname is required", item.ErrorMessage);
                            break;
                        default: break;
                    }
                    breedController.ModelState.AddModelError("TEST",item.ErrorMessage);
                }
            }

            // Act
            //if (!expected)
            //{
            //    breedController.ModelState.AddModelError("BreedName", "BreedName är obligatoriskt");
            //}

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
        private (BreedsController, Mock<IBreedService>, Mock<IBreedTypeService>) GetBreedsController()
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
            return(new BreedsController(breedService.Object, breedTypeService.Object), breedService, breedTypeService);

        }
    }
}
