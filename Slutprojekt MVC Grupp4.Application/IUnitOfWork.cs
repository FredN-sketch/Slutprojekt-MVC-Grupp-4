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
        IBreedTypeService BreedTypeService { get; }
        Task SaveChangesAsync();
    }
}
