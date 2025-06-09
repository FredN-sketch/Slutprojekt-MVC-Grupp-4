using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Interfaces;

public interface IBreedTypeService
{
    BreedType[] GetAllBreedTypes();
    BreedType GetBreedTypeById(int id);
}