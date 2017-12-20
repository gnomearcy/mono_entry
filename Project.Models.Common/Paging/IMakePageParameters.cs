using Project.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Common.Paging
{
    public interface IMakePageParameters
    {
        [Required]
        [Range(1, Int32.MaxValue)]
        int TargetPage { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        int PageSize { get; set; }

        [Required]
        bool SortAsc { get; set; }

        [Required]
        [MakeSortValidation]
        SortType SortField { get; set; }
    }
}
