﻿using Slutprojekt.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutprojekt.Application.Breeds.Interfaces;

namespace Slutprojekt.Infrastructure.Persistance.Repositories
{
    public class BreedsRepository(ApplicationContext context) : IBreedsRepository
    {
        public async Task<Breed[]> GetAllBreedsAsync()
        {
            return await context.Breeds.ToArrayAsync();
        }
        public async Task<Breed?> GetBreedByIdAsync(int id)
        {
            return await context.Breeds.SingleOrDefaultAsync(b => b.Id == id);
        }
        public void AddBreed(Breed breed)
        {
            context.Breeds.Add(breed);
        }
        public void DeleteBreed(Breed breed)
        {
            context.Breeds.Remove(breed);

        }
    }
}
