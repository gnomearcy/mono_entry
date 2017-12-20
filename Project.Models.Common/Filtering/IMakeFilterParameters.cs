using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Common.Filtering
{
    public interface IMakeFilterParameters
    {
        [Required]
        [StringLength(maximumLength: Int32.MaxValue, MinimumLength = 1)]
        string Name { get; set; }

        [Required]
        [StringLength(maximumLength: Int32.MaxValue, MinimumLength = 1)]
        string Abrv { get; set; }
    }
}
