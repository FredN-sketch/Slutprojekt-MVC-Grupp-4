using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Interfaces;

public interface IBreedService
{
    Task <Breed[]> GetAllBreedsAsync();
    Task<Breed> GetBreedByIdAsync(int id);
    Task AddBreedAsync(Breed breed);
}