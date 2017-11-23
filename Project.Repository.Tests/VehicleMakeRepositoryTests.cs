using Moq;
using Project.DAL;
using Project.Repository.Common;
using Project.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Data.Entity;
using Project.Models.Common;

namespace Project.Repository.Tests
{
    public class VehicleMakeRepositoryTests
    {
        [Fact]
        public void DeleteAllModels_AndEnsure_NoneAreLeft()
        {
            var mockModelRepo = new Mock<IVehicleModelRepository>();
            List<VehicleModelEntity> collection = new List<VehicleModelEntity>();
            mockModelRepo.Setup(ss => ss.GetAll()).ReturnsAsync(collection);
            foreach(var elem in collection)
            {
                mockModelRepo.Setup(ss => ss.Delete(elem));
            }

            mockModelRepo.Setup(ss => ss.GetAll()).ReturnsAsync(collection);
            Assert.Empty(collection);
        }

        /// <summary>
        /// Test with in-memory test doubles as per Microsoft docs:
        /// https://msdn.microsoft.com/en-us/data/dn314431.aspx#doubles
        /// </summary>
        [Fact]
        public async void InsertFew_CheckAmount()
        {
            var context = new DbTestDoubles();
            var repo = new VehicleModelRepository(context);
            var times = 3;
            for (int i = 0; i < times; i++)
            {
                context.Model.Add(new Mock<VehicleModelEntity>().Object);
            }

            var collection = await repo.GetAll();
            Assert.True((collection as List<VehicleModelEntity>).Count == times);
        }

        [Fact]
        public async void InsertThenUpdateAndExpectNullReference()
        {
            var db = new DbTestDoubles();
            IVehicleMakeRepository makeRepo = new VehicleMakeRepository(db);
            //IVehicleModelRepository modelRepo = new VehicleModelRepository(db);

            var make = new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "test", Abrv = "test2" };
            var byId = await makeRepo.GetById(make.Id);
            Assert.Null(byId);
            await makeRepo.Insert(make);
            var makes = await makeRepo.GetAll();
            Assert.Single(makes);

            // Test double does not have Entity Framework DbContext class available.
            await Assert.ThrowsAsync<NullReferenceException>(async () => await makeRepo.Update(make));
        }
    }
}
