using Slutprojekt.Application.Breeds.Services;

namespace Slutprojekt_MVC_Grupp_4.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<BreedTypeService>();
            builder.Services.AddSingleton<BreedService>();
            //   builder.Services.AddScoped
            //   builder.Services.AddTransient

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
