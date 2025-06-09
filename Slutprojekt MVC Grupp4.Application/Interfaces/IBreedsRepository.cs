using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Infrastructure.Persistance.Repositories
{
    public interface IBreedsRepository
    {
        public Task<Breed[]> GetAllBreedsAsync();
        public Task<Breed> GetBreedByIdAsync(int id);
        public Task AddBreedAsync(Breed breed);
        public Task RemoveBreedAsync(Breed breed);

    }
}