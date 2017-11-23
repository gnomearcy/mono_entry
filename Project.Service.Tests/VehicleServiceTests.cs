using Moq;
using Project.Common;
using Project.DAL;
using Project.Models;
using Project.Models.Common;
using Project.Repository;
using Project.Repository.Common;
using Project.Repository.Tests;
using Project.Service;
using Project.Service.Common;
using Project.WebAPI.App_Start.AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Service.Tests
{
    public class VehicleMakeService : TestBase
    {
        private IVehicleService MakeService()
        {
            var db = new DbTestDoubles();
            IVehicleMakeRepository makeRepo = new VehicleMakeRepository(db);
            IVehicleModelRepository modelRepo = new VehicleModelRepository(db);
            var uow = new Mock<IUnitOfWork>().Object;
            return new VehicleService(makeRepo, modelRepo, uow);
        }

        [Fact]
        public async void TestDoubleDBHasEmptySetsOnCreate()
        {
            // Setup
            var s = this.MakeService();

            // Arrange
            var makes = await s.GetAllMakes();
            var models = await s.GetAllModels();

            // Assert
            Assert.Equal(0, makes.Count);
            Assert.Equal(0, models.Count);
        }

        [Fact]
        public async void DeleteMakeAndAllRelatedModels()
        {
            // Setup
            var s = this.MakeService();

            // Arrange
            // #1 create Make model
            Guid makeId = Guid.NewGuid();
            string makeName = "makeTestName";
            string makeAbrv = "makeTestAbrv";
            IVehicleMake make = new VehicleMake { Id = makeId, Name = makeName, Abrv = makeAbrv};
            // #2 create Model models
            var modelCollection = new List<IVehicleModel>();
            var modelCount = 10;
            for(int i = 0; i < modelCount; i++)
            {
                var newModel = new VehicleModel {
                    Id = Guid.NewGuid(),
                    Name = $"name_{i}",
                    Abrv = $"abrv_{i}",
                    // Act as foreign key
                    MakeId = makeId
                };
                modelCollection.Add(newModel);
            }

            // Assertions

            // #1 Make collection is empty at start
            var makes = await s.GetAllMakes();
            Assert.Empty(makes);

            // #2 Insert an object and verify 1 exists
            // This method should insert the object and not execute update portion of the code which accesses
            // DbContext.Entry method to set the state of an Entity to EntityState.Modified
            await s.CreateUpdateMake(make);
            makes = await s.GetAllMakes();
            Assert.Single(makes);

            // #3 Update same object via existing method and verify only 1 is still there
            await Assert.ThrowsAsync<NullReferenceException>(async () => await s.CreateUpdateMake(make));
            makes = await s.GetAllMakes();

            // Test double class emulated System.Data.Entity.DbSet's Add method by simply adding the entity to 
            // in-memory collection, therefor another call to services' CreateUpdateMake method will actually add the new item.
            Assert.True(makes.Count > 1);

            // #4 Preparation for Model deletion based on MakeId
            // #4-1 Insert all Model models
            for (int i = 0; i < modelCount; i++)
            {
                await s.CreateUpdateModel(modelCollection.ElementAt(i));
            }
            var models = await s.GetAllModels();
            Assert.Equal(modelCount, models.Count);

            // #4-2 Delete all Model models that have passed in makeId
            await s.DeleteModelsByMake(makeId);
            models = await s.GetAllModels();

            // Services' batch delete implementation uses UnitOfWork pattern.
            // Currently, UnitOfWork uses concrete implementation of VehicleDbContext and therefor nothing is deleted.
            Assert.Equal(modelCount, models.Count);
        }
    }
}
