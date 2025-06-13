
using System.ComponentModel.DataAnnotations;

namespace Slutprojekt.Web.Views.Breeds;

public class IndexVM
{
    public required HundrasVM[] Raser { get; set; }

    public class HundrasVM
    {
        public required int Id { get; set; }

        [Display(Name = "Breedname")]
        public required string BreedName { get; set; }

    }


}
