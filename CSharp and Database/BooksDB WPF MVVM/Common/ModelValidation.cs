using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksDB_WPF_MVVM.Common
{
    public class ModelValidation
    {
        public void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);
            bool isValid = Validator.TryValidateObject(model, context, results, true);
            if (isValid == false)
            {
                foreach (var item in results)
                {
                    errorMessage += "- " + item.ErrorMessage + "\r\n";
                }
                throw new Exception(errorMessage);
            }
        }
    }
}
