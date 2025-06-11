using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Interfaces;

public interface IBreedsRepository
{
    public Task<Breed[]> GetAllBreedsAsync();
    public Task<Breed> GetBreedByIdAsync(int id);
    public void AddBreed(Breed breed);
    public void DeleteBreed(Breed breed);
}