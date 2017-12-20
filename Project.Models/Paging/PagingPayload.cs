using Project.Models.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Paging
{
    public class PagingPayload : IMakePageParameters
    {
        public int TargetPage { get; set; }
        public int PageSize { get; set; }
        public bool SortAsc { get; set; }
        public SortType SortField { get; set; }
    }
}
