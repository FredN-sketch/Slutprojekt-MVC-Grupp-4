using Microsoft.EntityFrameworkCore;
using Slutprojekt.Application;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Infrastructure.Persistance;

public class UnitOfWork(ApplicationContext context, IBreedsRepository breedsRepository, IBreedTypeRepository breedTypeRepository):IUnitOfWork
{
    public IBreedsRepository BreedsRepository => breedsRepository;
    public IBreedTypeRepository BreedTypeRepository => breedTypeRepository;
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    public async Task UpdateBreedAsync(Breed breed)
    {
        await context.Breeds
            .Where(b => b.Id == breed.Id)
            .ExecuteUpdateAsync(b => b.SetProperty(b => b.BreedName, breed.BreedName)
                                        .SetProperty(b => b.Description, breed.Description)
                                        .SetProperty(b => b.BreedType, breed.BreedType));
    }
}
