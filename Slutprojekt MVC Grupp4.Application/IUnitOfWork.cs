using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application
{
    public interface IUnitOfWork
    {
        IBreedsRepository BreedsRepository { get; }
        IBreedTypeRepository BreedTypeRepository { get; }
        Task SaveChangesAsync();
        Task UpdateBreedAsync(Breed breed);
    }
}
