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
            BreedsController breedController = new BreedsController(breedService.Object, breedTypeService.Object);
         //   InsertBreedVM newBreed = new() { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" };

            //Act
            var result = await breedController.InsertBreed();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<InsertBreedVM>(viewResult.Model);
            
        }
    }
}
