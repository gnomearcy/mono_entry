using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    }
}