using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Interfaces;

public interface IBreedService
{
    Breed[] getAllBreeds();
    Breed getBreedById(int id);
    void addBreed(Breed breed);
}