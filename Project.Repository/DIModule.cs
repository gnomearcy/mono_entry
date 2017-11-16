using Ninject.Modules;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleMakeRepository>().To<VehicleMakeRepository>();
            Bind<IVehicleModelRepository>().To<VehicleModelRepository>();
        }
    }
}
