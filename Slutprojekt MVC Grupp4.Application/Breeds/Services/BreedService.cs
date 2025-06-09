
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Services;

public class BreedService(IBreedsRepository breedsRepository): IBreedService
{
    static int nextId = 71;

    public async Task<Breed[]> GetAllBreedsAsync()
    {
        return [.. (await breedsRepository.GetAllBreedsAsync()).OrderBy(o => o.BreedName)];
    }

    public async Task<Breed> GetBreedByIdAsync(int id)
    {
        return await breedsRepository.GetBreedByIdAsync(id);
    }

    public async Task AddBreedAsync(Breed breed)
    {
        await breedsRepository.AddBreedAsync(breed);
    }
}
