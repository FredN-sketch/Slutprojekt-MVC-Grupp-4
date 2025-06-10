using System.Threading;
using System.Xml.Linq;
using Moq;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Tests;

public class ServiceTests
{
    [Fact]
    public async Task GetAllBreedsAsyncReturnsArrayOfBreed()
    {
        Mock<IUnitOfWork> unitOfWork = GetUnitOfWork();

        var breedService = new BreedService(unitOfWork.Object);

        var result = await breedService.GetAllBreedsAsync();

        Assert.NotEmpty(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("Schäfer", result[0].BreedName);
        Assert.Equal("Tax", result[1].BreedName);


    }

    [Theory]
    [InlineData(1, "Schäfer")]
    [InlineData(2,"Tax")]
   
    public async Task GetBreedByIdAsyncReturnsCorrectBreed(int id, string name)
    {
        Mock<IUnitOfWork> unitOfWork = GetUnitOfWorkID( id,  name);
        var breedService = new BreedService(unitOfWork.Object);

        var result = await breedService.GetBreedByIdAsync(id);

        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.BreedName);
    }
    [Fact]
    public async Task AddBreedAsyncAdds()
    {
        Mock<IUnitOfWork> unitOfWork = GetUnitOfWork();

        var breedService = new BreedService(unitOfWork.Object);

        Breed nyBreed = new Breed { Id=5,BreedType=1,BreedName="Labrador"};
        await breedService.AddBreedAsync(nyBreed);

        var result = await breedService.GetAllBreedsAsync();
        Assert.Equal(3, result.Length);

    }

    private static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
    //    unitOfWork.Setup(m => m.AddResponse(It.IsAny<GuestResponse>())).Returns(true);
        unitOfWork.Setup(u => u.BreedsRepository.GetAllBreedsAsync())
            .Returns(Task.FromResult(new Breed[]
            {
                new() { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" },
                new() { Id = 2, BreedType = 2, BreedName = "Tax", Description = "betalar hundskatt" }
            }
            ));
        return unitOfWork;
    }
    private static Mock<IUnitOfWork> GetUnitOfWorkID(int id, string name)
    {
        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(u => u.BreedsRepository.GetBreedByIdAsync(id))
            .Returns(Task.FromResult(new Breed
            
                 { Id = id, BreedType = 1, BreedName = name, Description = "tysk hund" }
            
            ));
        return unitOfWork;
    }

    //public (BreedsController, Mock<IBreedService>, Mock<IBreedTypeService>) GetBreedsController()
    //{
    //    var breedService = new Mock<IBreedService>();
    //    var breedTypeService = new Mock<IBreedTypeService>();
    //    breedTypeService.Setup(service => service.GetAllBreedTypesAsync())
    //        .Returns(Task.FromResult(new BreedType[]
    //        {
    //            new() { Id = 1, BreedTypeName = "Grupp 1" },
    //            new() { Id = 2, BreedTypeName = "Grupp 2" }
    //        }));
    //    breedService.Setup(service => service.GetAllBreedsAsync())
    //       .Returns(Task.FromResult(new Breed[]
    //       {
    //            new() { Id = 1, BreedType = 1, BreedName = "Schäfer", Description = "tysk hund" },
    //            new() { Id = 2, BreedType = 2, BreedName = "Tax", Description = "betalar hundskatt" }
    //       }));
    //    return (new BreedsController(breedService.Object, breedTypeService.Object), breedService, breedTypeService);

    //}

}
