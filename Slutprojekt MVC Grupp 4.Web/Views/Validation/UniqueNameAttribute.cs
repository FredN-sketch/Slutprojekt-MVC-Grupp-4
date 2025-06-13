using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slutprojekt.Domain.Entities;
using Slutprojekt.Infrastructure.Persistance;
using Slutprojekt.Web.Views.Breeds;

namespace Slutprojekt.Web.Views.Validation;

public class UniqueNameAttribute: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;

        if (validationContext.GetService(typeof(ApplicationContext)) is ApplicationContext context)
        {
            if (validationContext.ObjectInstance is InsertBreedVM insertBreed)
            {
                var exists = context!.Breeds.Any(b => b.BreedName.Equals(value!.ToString()));
                return exists ? new ValidationResult("That breed has already been added") : ValidationResult.Success;
            }

            if (validationContext.ObjectInstance is UpdateBreedVM updateBreed)
            {
                Breed? saved = context.Breeds.SingleOrDefault(b => b.Id == updateBreed.Id);
                if (saved != null && !value.Equals(saved.BreedName)) //changed
                {
                    var exists = context!.Breeds.Any(b => b.BreedName.Equals(value!.ToString()));
                    return exists ? new ValidationResult("That breed has already been added") : ValidationResult.Success;
                }
            }
        }
        return ValidationResult.Success;
    }
}
