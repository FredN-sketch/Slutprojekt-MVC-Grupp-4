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
    public async Task GetAllBreedsAsync_ReturnsArrayOfBreed()
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
   
    public async Task GetBreedByIdAsync_WithProperId_ReturnsCorrectBreed(int id, string name)
    {
        Mock<IUnitOfWork> unitOfWork = GetUnitOfWorkID( id,  name);
        var breedService = new BreedService(unitOfWork.Object);

        var result = await breedService.GetBreedByIdAsync(id);

        Assert.Equal(id, result.Id);
        Assert.Equal(name, result.BreedName);
    }
    [Fact]
    public async Task AddBreedAsync_ExpectAddBreed()
    {
        Breed nyBreed = new Breed { Id = 5, BreedType = 1, BreedName = "Labrador" };

        
        Mock<IBreedsRepository> breedsRepository = new Mock<IBreedsRepository>();
        breedsRepository.Setup(u => u.AddBreedAsync(nyBreed)).Returns(Task.CompletedTask);

        Mock<IUnitOfWork> unitOfWork = GetUnitOfWork();
        unitOfWork.Setup(u => u.BreedsRepository).Returns(breedsRepository.Object);

        //Mock<IBreedTypeRepository> breedTypeRepository = new Mock<IBreedTypeRepository>();

        var breedService = new BreedService(unitOfWork.Object);

        
        await breedService.AddBreedAsync(nyBreed);

        var result = await breedService.GetAllBreedsAsync();
        
        breedsRepository.Verify(u => u.AddBreedAsync(nyBreed), Times.Once);
    }

    private static Mock<IUnitOfWork> GetUnitOfWork()
    {
        var unitOfWork = new Mock<IUnitOfWork>();
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

}
