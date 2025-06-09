using Slutprojekt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Application.Breeds.Interfaces
{
    public interface IBreedTypeRepository
    {
        Task<BreedType[]> GetAllBreedTypesAsync();
        Task<BreedType> GetBreedTypeByIdAsync(int id);
    }
}
