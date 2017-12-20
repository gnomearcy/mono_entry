using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Common.Dto
{
    public interface IMakePageDto
    {
        ICollection<IVehicleMake> Data { get; set; }

        int PageNumber { get; set; }

        int PageCount { get; set; }

        int PageSize { get; set; }
    }
}
