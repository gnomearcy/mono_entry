using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;

namespace Project.Repository.Common
{
    public interface IVehicleMakeRepository : IRepository<VehicleMakeEntity>
    {
        //List<IVehicleMake> Get();

        //void Create(IVehicleMake vehicleMake);

        ////void Delete(IVehicleMake vehicleMake);

        ////void Update(IVehicleMake vehicleMake);
    }
}
