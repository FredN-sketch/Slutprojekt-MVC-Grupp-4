using System.ComponentModel.DataAnnotations;

namespace Slutprojekt.Web.Views.Breeds;

public class DisplayBreedVM
{
    [Display( Name = "BreedType info"  ) ]
    public required string BreedTypeInfo { get; set; }

    [Display( Name ="BreedName") ]
    public required string BreedName { get; set; }

    [Display( Name ="Description of breed") ]
    public required string Description { get; set; }
}
