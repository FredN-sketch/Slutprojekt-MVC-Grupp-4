using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Services;

public class BreedTypeService(IBreedTypeRepository breedTypeRepository): IBreedTypeService
{
    public async Task<BreedType[]> GetAllBreedTypesAsync() => (await breedTypeRepository.GetAllBreedTypesAsync()).OrderBy(b => b.Id).ToArray();

    public async Task<BreedType> GetBreedTypeByIdAsync(int id) => await breedTypeRepository.GetBreedTypeByIdAsync(id);
}
