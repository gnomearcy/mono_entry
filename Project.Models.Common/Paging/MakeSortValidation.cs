using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Common.Paging
{
    public class MakeSortValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validValues = Enum.GetValues(typeof(SortType)).Cast<SortType>();
            ErrorMessage = ErrorMessageString;
            var currentValue = (SortType)value;

            foreach (var v in validValues)
            {
                if (currentValue == v)
                {
                    return ValidationResult.Success;
                }
            }

            StringBuilder b = new StringBuilder();
            b.Append("Supported sorting codes:");
            b.AppendLine("[ code ] -> [ sorting field ]");
            // Construct an error message
            foreach (var v in validValues)
            {
                b.AppendLine("[ " + (int)v + " ] -> " + v);
            }

            return new ValidationResult(b.ToString());
        }
    }
}
