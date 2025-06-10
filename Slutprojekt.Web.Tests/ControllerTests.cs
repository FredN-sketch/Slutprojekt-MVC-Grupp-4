using Microsoft.AspNetCore.Mvc;
using Moq;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Web.Controllers;
using Slutprojekt.Web.Views.Breeds;

namespace Slutprojekt.Web.Tests
{
    public class ControllerTests
    {
        [Fact]
        public async Task InsertBreedNoParamsExpectCorrectViewModelAsync()
        {
            //Arrange
            var breedController = GetBreedsController();
       
            //Act
            var result = await breedController.InsertBreed();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<InsertBreedVM>(viewResult.Model);   
        }
        [Theory]
        //   [InlineData(new InsertBreedVM { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" }, true)]
        [InlineData( 3, 1,  "Schäfer",  "tysk hund", true)]
        [InlineData( 4, 3, "Leonberger", "lejonlik", false)]
        public async Task InsertBreedWithParamsExpectCorrectViewModelAsync(int id, int breedType, string breedName, string description, bool expected)
        {
            //Arrange
            var viewModel = new InsertBreedVM { Id = id, BreedType = breedType, BreedName = breedName, Description = description };
            var breedController = GetBreedsController();

            // Act
            var result = await breedController.InsertBreed(viewModel);

            // Assert
            if (expected)
            {
                var redirect = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(nameof(breedController.Index), redirect.ActionName);
                
            }
            else
            {
                var view = Assert.IsType<ViewResult>(result);
                Assert.Null(view.ViewName);
               
            }
        }
        public BreedsController GetBreedsController()
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
            return new BreedsController(breedService.Object, breedTypeService.Object);

        }
       
    }
}
