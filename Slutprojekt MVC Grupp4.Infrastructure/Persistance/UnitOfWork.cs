using Slutprojekt.Application;
using Slutprojekt.Application.Breeds.Interfaces;
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
}
