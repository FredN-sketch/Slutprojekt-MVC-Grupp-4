using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Services;

public class BreedTypeService(IUnitOfWork unitOfWork): IBreedTypeService
{
    public async Task<BreedType[]> GetAllBreedTypesAsync() => [.. (await unitOfWork.BreedTypeRepository.GetAllBreedTypesAsync()).OrderBy(b => b.Id)];

    public async Task<BreedType?> GetBreedTypeByIdAsync(int id) => await unitOfWork.BreedTypeRepository.GetBreedTypeByIdAsync(id);
}
