using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Infrastructure.Persistance;
using Slutprojekt.Infrastructure.Persistance.Repositories;

namespace Slutprojekt.Terminal;

internal class Program
{
    static BreedService breedService = null!;
    static async Task Main(string[] args)
    {
        string? connectionString;

        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", false);
        var app = builder.Build();
        connectionString = app.GetConnectionString("DefaultConnection");

        var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlServer(connectionString)
            .Options;
        
        var context = new ApplicationContext(options);
        var breedsRepository = new BreedsRepository(context);
        var breedTypeRepository = new BreedTypeRepository(context);
        breedService = new BreedService(new UnitOfWork(context, breedsRepository, breedTypeRepository));

        var breeds = await breedService.GetAllBreedsAsync();
        foreach (var breed in breeds)
        {
            Console.WriteLine("{0, -30}{1, -20}", breed.BreedName, breed.Description);
        }
    }
}
