using Moq;
using Project.Repository;
using Project.Repository.Common;
using Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Test.Common
{
    public class VehicleServiceFixture
    {
        public VehicleService Data { get; private set; }

        public VehicleServiceFixture()
        {
            var db = new DbTestDoubles();
            IVehicleMakeRepository makeRepo = new VehicleMakeRepository(db);
            IVehicleModelRepository modelRepo = new VehicleModelRepository(db);
            var uow = new Mock<IUnitOfWork>().Object;
            this.Data = new VehicleService(makeRepo, modelRepo, uow);
        }
    }
}
