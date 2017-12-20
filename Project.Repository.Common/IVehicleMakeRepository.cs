using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;
using Project.Models.Common.Paging;
using Project.Models.Common.Filtering;

namespace Project.Repository.Common
{
    public interface IVehicleMakeRepository : IRepository<VehicleMakeEntity, Guid>, 
        IPaginator<IMakePageParameters, ICollection<IVehicleMake>>,
        IFilterator<IMakeFilterParameters, ICollection<IVehicleMake>>
    {

    }
}
