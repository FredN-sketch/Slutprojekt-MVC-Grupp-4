using Microsoft.EntityFrameworkCore;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Infrastructure.Persistance.Repositories
{
    public class BreedTypeRepository(ApplicationContext context) : IBreedTypeRepository
    {
        public async Task<BreedType[]> GetAllBreedTypesAsync()
        {
            return await context.BreedTypes.ToArrayAsync();
        }

        public async Task<BreedType?> GetBreedTypeByIdAsync(int id)
        {
            return await context.BreedTypes
                .SingleOrDefaultAsync(b => b.Id == id);
        }
    }
}
