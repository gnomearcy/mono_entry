using Ninject.Modules;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class DIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVehicleService>().To<VehicleService>();
        }
    }
}
