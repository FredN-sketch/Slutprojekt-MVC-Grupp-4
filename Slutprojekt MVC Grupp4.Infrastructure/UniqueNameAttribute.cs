using Slutprojekt.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt.Application.Breeds
{
    public class UniqueNameAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            bool exists = false;

            var context = validationContext.GetService(typeof(ApplicationContext)) as ApplicationContext;
            if(context != null)
                 exists = context!.Breeds.Any(b => b.BreedName.Equals(value!.ToString()));
            
                return exists ? new ValidationResult("That breed has already been added") : ValidationResult.Success;
        }
    }
}
