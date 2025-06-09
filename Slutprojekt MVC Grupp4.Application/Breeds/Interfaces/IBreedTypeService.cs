using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Interfaces;

public interface IBreedTypeService
{
    Task<BreedType[]> GetAllBreedTypesAsync();
    Task<BreedType> GetBreedTypeByIdAsync(int id);
}