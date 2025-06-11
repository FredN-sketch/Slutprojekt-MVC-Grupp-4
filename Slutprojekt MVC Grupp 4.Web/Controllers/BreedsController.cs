using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slutprojekt.Application.Breeds.Interfaces;
using Slutprojekt.Application.Breeds.Services;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Web.Views.Breeds;
using System.Data;
using System.Reflection;

namespace Slutprojekt.Web.Controllers;

[Authorize]
public class BreedsController(IBreedService breedService, IBreedTypeService breedType) : Controller
{
    [Route("members")]
    [Route("/")]
    public async Task<IActionResult> Index()
    {
        var model = await breedService.GetAllBreedsAsync();
        var view = new IndexVM()
        {
            Raser = [.. model
            .Select(o => new IndexVM.HundrasVM
            {
                Id = o.Id,
                BreedName = o.BreedName,
            })]
        };

        return View(view);
    }

    [HttpGet("display/{id}")]
    public async Task<IActionResult> DisplayBreed(int id)
    {
        var model = await breedService.GetBreedByIdAsync(id);
        var mode2 = await breedType.GetBreedTypeByIdAsync(model.BreedType);

        DisplayBreedVM view = new()
        {
            BreedTypeInfo = mode2.BreedTypeName,
            BreedName = model.BreedName,
            Description = model.Description!,
        };
        return View(view);
    }


    [HttpGet("Create")]
    public async Task<IActionResult> InsertBreed()
    {
        InsertBreedVM view = new()
        {
            Id = 0,
            BreedType = 0,
            BreedTypes = [.. (await breedType.GetAllBreedTypesAsync())
            .Select(o => new InsertBreedVM.TypesVM()
            {
                Id = o.Id,
                BreedTypeName = o.BreedTypeName,
            })],
            BreedName = null!,
            Description=null,
        };

        return View(view);
    }
    [HttpPost("Create")]
    public async Task<IActionResult> InsertBreed(InsertBreedVM model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        Breed breed = new()
        {
            Id = model.Id,
            BreedType = model.BreedType,
            BreedName = model.BreedName,
            Description = model.Description,
        };

        await breedService.AddBreedAsync(breed);
        return RedirectToAction("Index");
    }
}
