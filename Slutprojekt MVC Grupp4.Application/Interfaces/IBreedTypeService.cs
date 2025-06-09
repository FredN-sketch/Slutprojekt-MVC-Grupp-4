using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Interfaces
{
    public interface IBreedTypeService
    {
        BreedType[] GetAllBreedTypes();
        BreedType GetBreedTypeById(int id);
    }
}