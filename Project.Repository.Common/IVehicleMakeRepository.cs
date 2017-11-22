using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;

namespace Project.Repository.Common
{
    public interface IVehicleMakeRepository : IRepository<VehicleMakeEntity, Guid>
    {

    }
}
