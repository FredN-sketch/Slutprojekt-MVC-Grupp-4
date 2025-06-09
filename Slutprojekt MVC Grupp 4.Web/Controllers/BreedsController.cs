using System.Reflection;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Web.Views.Breeds;
using Microsoft.AspNetCore.Mvc;

namespace Slutprojekt.Web.Controllers;

public class BreedsController(BreedService breedService, BreedTypeService breedType) : Controller
{
    //public static BreedService breedService = new BreedService();

    [Route("/")]
    public IActionResult Index()
    {
        var model = breedService.getAllBreeds();
        var view = new IndexVM()
        {
            Raser = model
            .Select(o => new IndexVM.HundrasVM
            {
                Id = o.Id,
                BreedName = o.BreedName,
            })
            .ToArray()
        };

        return View(view);
    }

    [HttpGet("display/{id}")]
    public IActionResult DisplayBreed(int id)
    {
        var model = breedService.getBreedById(id);
        var mode2 = breedType.GetBreedTypeById(model.BreedType);

        DisplayBreedVM view = new DisplayBreedVM
        {
            BreedTypeInfo = mode2.BreedTypeName,
            BreedName = model.BreedName,
            Description = model.Description,
        };
        return View(view);
    }


    [HttpGet("Create")]
    public IActionResult InsertBreed()
    {
        InsertBreedVM view = new InsertBreedVM()
        {
            Id = 0,
            BreedType = 0,
            BreedTypes = breedType.GetAllBreedTypes()
            .Select(o => new InsertBreedVM.TypesVM()
            {
                Id = o.Id,
                BreedTypeName = o.BreedTypeName,
            }).ToArray(),
            //BreedTypes = breedType.GetAllBreedTypes().ToArray(),
            BreedName = null,
            Description=null,
        };

        return View(view);
    }
    [HttpPost("Create")]
    public IActionResult InsertBreed(InsertBreedVM model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        Breed breed = new Breed
        {
            Id = model.Id,
            BreedType = model.BreedType,
            BreedName = model.BreedName,
            Description = model.Description,
        };

        breedService.addBreed(breed);
        return RedirectToAction("Index");
    }
}
