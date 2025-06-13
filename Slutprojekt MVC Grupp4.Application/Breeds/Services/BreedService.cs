
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Services;

public class BreedService(IUnitOfWork unitOfWork): IBreedService
{
    //static int nextId = 71;

    public async Task<Breed[]> GetAllBreedsAsync()
    {
        return [.. (await unitOfWork.BreedsRepository.GetAllBreedsAsync()).OrderBy(o => o.BreedName)];
    }

    public async Task<Breed?> GetBreedByIdAsync(int id)
    {
        return await unitOfWork.BreedsRepository.GetBreedByIdAsync(id);
    }

    public async Task AddBreedAsync(Breed breed)
    {
        unitOfWork.BreedsRepository.AddBreed(breed);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteBreedAsync(int id)
    {
        var breed = await unitOfWork.BreedsRepository.GetBreedByIdAsync(id);
        if (breed != null)
        {
            unitOfWork.BreedsRepository.DeleteBreed(breed);
            await unitOfWork.SaveChangesAsync();
        }
    }

    public async Task UpdateBreedAsync(Breed breed)
    {
        await unitOfWork.UpdateBreedAsync(breed);
    }
}
