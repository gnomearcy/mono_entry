using Moq;
using Project.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Repository.Tests
{
    public class RepositoryFixture
    {
        public Mock<VehicleMakeRepository> MakeRepo { private set; get; }
        public Mock<VehicleModelRepository> ModelRepo { private set; get; }

        public RepositoryFixture()
        {
            var db = new DbTestDoubles();
            MakeRepo = new Mock<VehicleMakeRepository>(db);
            ModelRepo = new Mock<VehicleModelRepository>(db);
        }
    }
}
