using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slutprojekt.Application;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Application.Users;
using Slutprojekt.Infrastructure.Persistance;
using Slutprojekt.Infrastructure.Persistance.Repositories;
using Slutprojekt.Infrastructure.Services;

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
            builder.Services.AddScoped<IBreedTypeRepository, BreedTypeRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IIdentityUserService, IdentityUserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ApplicationContext>();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

            var app = builder.Build();

            app.UseAuthorization();

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
