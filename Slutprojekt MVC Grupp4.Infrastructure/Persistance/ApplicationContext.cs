using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Infrastructure.Persistance;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<BreedType> BreedTypes { get; set; } = null!;
    public DbSet<Breed> Breeds { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Breed>().HasData(

    new Breed() { Id = 10, BreedType = 1, BreedName = "Collie, långhårig", Description = "Massa mjuk päls, elegant, lättlärd och gillar aktiviteter" },
    new Breed() { Id = 09, BreedType = 1, BreedName = "Vit herdehud", Description = "Livlig, lättlärd sällskapshund med behov av aktivitet" },
    new Breed() { Id = 08, BreedType = 1, BreedName = "Tysk schäferhund", Description = "Samarbetsvillig, livlig och uppmärksam jobbkompis" },
    new Breed() { Id = 23, BreedType = 2, BreedName = "Boxer", Description = "Alert, arbetsvillig och livsglad bästa vän" },
    new Breed() { Id = 24, BreedType = 2, BreedName = "Grand danois", Description = "Trofast, stor, stark och pampig" },
    new Breed() { Id = 25, BreedType = 2, BreedName = "Leonberger", Description = "Behaglig och följsam med behov av fast hand" },
    new Breed() { Id = 36, BreedType = 3, BreedName = "Bedlingtonterrier", Description = "Annorlunda utseende, charmig med stark vilja." },
    new Breed() { Id = 47, BreedType = 4, BreedName = "Tax", Description = "Vänlig, envis och uthållig trots sina korta ben" },
    new Breed() { Id = 05, BreedType = 5, BreedName = "Tysk spets/mittelspitz", Description = "Livlig, lättlärd pälsboll som hänger med" },
    new Breed() { Id = 66, BreedType = 6, BreedName = "Basset hound", Description = "Social, tillgiven kortbent spårexpert" },
    new Breed() { Id = 67, BreedType = 6, BreedName = "Beagle", Description = "Envis, arbetsvillig och glad" },
    new Breed() { Id = 71, BreedType = 7, BreedName = "Engelsk setter", Description = "Energisk och krävande med passion för fågeljakt" },
    new Breed() { Id = 33, BreedType = 8, BreedName = "Golden retriever", Description = "Vänlig och aktiv med stor passion för vatten" },
    new Breed() { Id = 32, BreedType = 8, BreedName = "Labrador retriever", Description = "Social och stark apportör som är duktig på det mesta" },
    new Breed() { Id = 11, BreedType = 9, BreedName = "Chihuahua, korthårig", Description = "Liten och sällskaplig hund som kan ta ton (vill inte bli uppäten!)" },
    new Breed() { Id = 14, BreedType = 10, BreedName = "Afghanhund", Description = "Självständig skönhet med böljande päls." },
    new Breed() { Id = 12, BreedType = 10, BreedName = "Greyhound", Description = "Vänlig, envis, stor och specialist på kapplöpning." }


        );
        modelBuilder.Entity<BreedType>().HasData(
            new BreedType() { Id = 1, BreedTypeName = "Grupp 1 - Vall-, boskaps- och herdehundar" },
            new BreedType() { Id = 2, BreedTypeName = "Grupp 2 - Schnauzer och pinscher, molosser och bergshundar samt sennenhundar" },
            new BreedType() { Id = 3, BreedTypeName = "Grupp 3 - Terrier" },
            new BreedType() { Id = 4, BreedTypeName = "Grupp 4 - Taxar" },
            new BreedType() { Id = 5, BreedTypeName = "Grupp 5 - Spetsar och raser av urhundstyp" },
            new BreedType() { Id = 6, BreedTypeName = "Grupp 6 - Drivande hundar samt sök- och spårhundar" },
            new BreedType() { Id = 7, BreedTypeName = "Grupp 7 - Stående fågelhundar" },
            new BreedType() { Id = 8, BreedTypeName = "Grupp 8 - Stötande hundar, apporterande hundar och vattenhundar" },
            new BreedType() { Id = 9, BreedTypeName = "Grupp 9 - Sällskapshundar" },
            new BreedType() { Id = 10, BreedTypeName = "Grupp 10 - Vinthundar" }
        );
    }
}
