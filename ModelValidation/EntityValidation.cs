using Mekatronik.CustomModels;
using System.ComponentModel.DataAnnotations;

namespace Mekatronik.ModelValidation
{
    public static class EntityValidation
    {
        
        public static string IsValidModel(ProductWithFile obj)
        {
            var context = new ValidationContext(obj);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(obj, context, results, true);

            if (!isValid)
            {
                string errors = "";
                foreach (var validationResult in results)
                {
                    errors += validationResult.ErrorMessage + ";";
                    //validation errors
                }
                return errors;
            }
            return "";

        }
    }
}
