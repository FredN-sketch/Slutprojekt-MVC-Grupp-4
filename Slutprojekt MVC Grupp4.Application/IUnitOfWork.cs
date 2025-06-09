using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutprojekt.Application.Breeds.Interfaces;

namespace Slutprojekt.Application
{
    public interface IUnitOfWork
    {
        IBreedsRepository BreedsRepository { get; }
        IBreedTypeRepository BreedTypeRepository { get; }
        Task SaveChangesAsync();
    }
}
