using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace Project.Models.Dto
{
    public class MakePagePayload
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        public int TargetPage { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]    
        public int PageSize { get; set; }

        [Required]
        public bool SortAsc { get; set; }

        [Required]
        [MakeSortValidation]
        public SortType SortField { get; set; }

        #region Supported sort options
        public enum SortType
        {
            NAME            = 1,
            ABREVIATION     = 2
        }
        #endregion

        #region Attribute implementation
        public class MakeSortValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var validValues = Enum.GetValues(typeof(SortType)).Cast<SortType>();
                ErrorMessage = ErrorMessageString;
                var currentValue = (SortType)value;


                foreach(var v in validValues)
                {
                    if(currentValue == v)
                    {
                        return ValidationResult.Success;
                    }
                }

                StringBuilder b = new StringBuilder();
                b.Append("Supported sorting codes:");
                b.AppendLine("[ code ] -> [ sorting field ]");
                // Construct an error message
                foreach(var v in validValues)
                {
                    b.AppendLine("[ " + (int)v + " ] -> " + v);
                }

                return new ValidationResult(b.ToString());
            }
        }
        #endregion
    }
}