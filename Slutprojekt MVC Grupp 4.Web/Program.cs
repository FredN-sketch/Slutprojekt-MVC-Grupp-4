using Microsoft.EntityFrameworkCore;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Infrastructure.Persistance;
using Slutprojekt.Infrastructure.Persistance.Repositories;

namespace Slutprojekt_MVC_Grupp_4.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IBreedTypeService, BreedTypeService>();
            builder.Services.AddScoped<IBreedService, BreedService>();
            builder.Services.AddScoped<IBreedsRepository, BreedsRepository>();
            builder.Services.AddScoped<ApplicationContext>();
            //   builder.Services.AddScoped
            //   builder.Services.AddTransient

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(connectionString));

            var app = builder.Build();
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error/exception");
                app.UseStatusCodePagesWithRedirects("/error/http/{0}");
            }

            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
