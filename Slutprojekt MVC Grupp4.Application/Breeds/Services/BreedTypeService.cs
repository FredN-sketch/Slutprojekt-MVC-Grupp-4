using Slutprojekt.Domain.Entities;

namespace Slutprojekt.Application.Breeds.Services;

public class BreedTypeService
{
    List<BreedType> breedtypes = [
    new BreedType() { Id=01 , BreedTypeName="Grupp 1 - Vall-, boskaps- och herdehundar" },
    new BreedType() { Id=02 , BreedTypeName="Grupp 2 - Schnauzer och pinscher, molosser och bergshundar samt sennenhundar" },
    new BreedType() { Id=03 , BreedTypeName="Grupp 3 - Terrier" },
    new BreedType() { Id=04 , BreedTypeName="Grupp 4 - Taxar" },
    new BreedType() { Id=05 , BreedTypeName="Grupp 5 - Spetsar och raser av urhundstyp" },
    new BreedType() { Id=06 , BreedTypeName="Grupp 6 - Drivande hundar samt sök- och spårhundar" },
    new BreedType() { Id=07 , BreedTypeName="Grupp 7 - Stående fågelhundar" },
    new BreedType() { Id=08 , BreedTypeName="Grupp 8 - Stötande hundar, apporterande hundar och vattenhundar" },
    new BreedType() { Id=09 , BreedTypeName="Grupp 9 - Sällskapshundar" },
    new BreedType() { Id=10 , BreedTypeName="Grupp 10 - Vinthundar" },
    ];

    public BreedType[] GetAllBreedTypes() => breedtypes.OrderBy(b => b.Id).ToArray();


    public BreedType GetBreedTypeById(int id) => breedtypes.SingleOrDefault(b => b.Id == id);
}
