using Project.Models.Common;
using Project.Models.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Dto
{
    public class MakePageDto : IMakePageDto
    {
        public ICollection<IVehicleMake> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageCount { get; set; }

        public int PageSize { get; set; }
    }
}
