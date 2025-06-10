using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [InlineData(4, 99, "", "lejonlik", false)]
        public async Task InsertBreedWithParamsExpectCorrectViewModelAsync(int id, int breedType, string breedName, string description, bool expected)
        {
            //Arrange
            var viewModel = new InsertBreedVM { Id = id, BreedType = breedType, BreedName = breedName, Description = description };
            //var breedController = GetBreedsController();
            var o = GetBreedsController();
            var breedController = o.Item1;
            var breedServiceMock = o.Item2;
            var breedTypeServiceMock = o.Item3;


            // Act
            if (!expected)
            {
                breedController.ModelState.AddModelError("BreedName", "BreedName är obligatoriskt");
            }

            var result = await breedController.InsertBreed(viewModel);

            // Assert
            if (expected)
            {
                var redirect = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal(nameof(breedController.Index), redirect.ActionName);
                breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Once);
            }
            else
            {
                var viewResult = Assert.IsType<ViewResult>(result);
                Assert.IsType<InsertBreedVM>(viewResult.Model);
                breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Never);
            }
        }
        public (BreedsController, Mock<IBreedService>, Mock<IBreedTypeService>) GetBreedsController()
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


//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Slutprojekt.Application.Breeds.Interfaces;
//using Slutprojekt.Domain.Entities;
//using Slutprojekt.Web.Controllers;
//using Slutprojekt.Web.Views.Breeds;
//using Xunit;
//using System.Threading.Tasks;
//using System.Linq;

//namespace Slutprojekt.Web.Tests
//{
//    public class BreedsControllerTests
//    {
//        [Fact]
//        public async Task Index_ReturnsViewWithBreeds()
//        {
//            // Arrange
//            var controller = GetBreedsController().controller;

//            // Act
//            var result = await controller.Index();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsType<IndexVM>(viewResult.Model);
//            Assert.Equal(2, model.Raser.Length);
//        }

//        [Fact]
//        public async Task Admin_ReturnsViewWithBreeds()
//        {
//            // Arrange
//            var controller = GetBreedsController().controller;

//            // Act
//            var result = await controller.Admin();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsType<IndexVM>(viewResult.Model);
//            Assert.Equal(2, model.Raser.Length);
//        }

//        [Fact]
//        public async Task DisplayBreed_ValidId_ReturnsViewWithCorrectBreed()
//        {
//            // Arrange
//            var breedServiceMock = new Mock<IBreedService>();
//            var breedTypeServiceMock = new Mock<IBreedTypeService>();

//            breedServiceMock.Setup(x => x.GetBreedByIdAsync(1))
//                .ReturnsAsync(new Breed
//                {
//                    Id = 1,
//                    BreedName = "Schäfer",
//                    BreedType = 1,
//                    Description = "Tysk hund"
//                });

//            breedTypeServiceMock.Setup(x => x.GetBreedTypeByIdAsync(1))
//                .ReturnsAsync(new BreedType
//                {
//                    Id = 1,
//                    BreedTypeName = "Grupp 1"
//                });

//            var controller = new BreedsController(breedServiceMock.Object, breedTypeServiceMock.Object);

//            // Act
//            var result = await controller.DisplayBreed(1);

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            var model = Assert.IsType<DisplayBreedVM>(viewResult.Model);
//            Assert.Equal("Schäfer", model.BreedName);
//            Assert.Equal("Grupp 1", model.BreedTypeInfo);
//            Assert.Equal("Tysk hund", model.Description);
//        }

//        [Fact]
//        public async Task InsertBreed_GetRequest_ReturnsInsertBreedVM()
//        {
//            // Arrange
//            var controller = GetBreedsController().controller;

//            // Act
//            var result = await controller.InsertBreed();

//            // Assert
//            var viewResult = Assert.IsType<ViewResult>(result);
//            Assert.IsType<InsertBreedVM>(viewResult.Model);
//        }

//        [Fact]
//        public async Task InsertBreed_PostValidModel_RedirectsToIndexAndAddsBreed()
//        {
//            // Arrange
//            var (controller, breedServiceMock) = GetBreedsController();
//            var model = new InsertBreedVM
//            {
//                Id = 3,
//                BreedType = 1,
//                BreedName = "Schäfer",
//                Description = "tysk hund"
//            };

//            // Act
//            var result = await controller.InsertBreed(model);

//            // Assert
//            var redirect = Assert.IsType<RedirectToActionResult>(result);
//            Assert.Equal("Index", redirect.ActionName);
//            breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Once);
//        }

//        [Fact]
//        public async Task InsertBreed_PostInvalidModel_ReturnsViewAndDoesNotAddBreed()
//        {
//            // Arrange
//            var (controller, breedServiceMock) = GetBreedsController();
//            var model = new InsertBreedVM
//            {
//                Id = 4,
//                BreedType = 3,
//                BreedName = "", // ogiltigt
//                Description = "lejonlik"
//            };

//            controller.ModelState.AddModelError("BreedName", "BreedName är obligatoriskt");

//            // Act
//            var result = await controller.InsertBreed(model);

//            // Assert
//            var view = Assert.IsType<ViewResult>(result);
//            Assert.Null(view.ViewName);
//            breedServiceMock.Verify(s => s.AddBreedAsync(It.IsAny<Breed>()), Times.Never);
//        }

//        private (BreedsController controller, Mock<IBreedService> breedServiceMock) GetBreedsController()
//        {
//            var breedServiceMock = new Mock<IBreedService>();
//            var breedTypeServiceMock = new Mock<IBreedTypeService>();

//            breedServiceMock.Setup(service => service.GetAllBreedsAsync())
//                .ReturnsAsync(new[]
//                {
//                    new Breed { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" },
//                    new Breed { Id = 2, BreedType = 2, BreedName = "Tax", Description = "betalar hundskatt" }
//                });

//            breedTypeServiceMock.Setup(service => service.GetAllBreedTypesAsync())
//                .ReturnsAsync(new[]
//                {
//                    new BreedType { Id = 1, BreedTypeName = "Grupp 1" },
//                    new BreedType { Id = 2, BreedTypeName = "Grupp 2" }
//                });

//            return (new BreedsController(breedServiceMock.Object, breedTypeServiceMock.Object), breedServiceMock);
//        }
//    }
//}
