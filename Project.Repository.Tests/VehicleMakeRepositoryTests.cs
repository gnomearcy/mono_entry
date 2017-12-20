using Moq;
using Project.DAL;
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
using Project.Test.Common;
using Project.Repository.Common;

namespace Project.Repository.Tests
{
    public class VehicleMakeRepositoryTests : IClassFixture<RepositoryFixture>, IClassFixture<DatabaseFixture>
    {
        private DbTestDoubles Context;
        private readonly IVehicleModelRepository ModelRepo;
        private readonly IVehicleMakeRepository MakeRepo;

        public VehicleMakeRepositoryTests(RepositoryFixture repo, DatabaseFixture db)
        {
            this.MakeRepo = repo.MakeRepo.Object;
            this.ModelRepo = repo.ModelRepo.Object;
            this.Context = db.Db;
        }

        /// <summary>
        /// Test with in-memory test doubles as per Microsoft docs:
        /// https://msdn.microsoft.com/en-us/data/dn314431.aspx#doubles
        /// </summary>
        [Fact]
        public async void InsertFew_CheckAmount()
        {
            var times = 3;
            for (int i = 0; i < times; i++)
            {
                await ModelRepo.Insert(new VehicleModelEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "",
                    Abrv = "",
                    MakeEntity = null,
                    MakeId = Guid.NewGuid()
                });
            }

            var collection = await ModelRepo.GetAll();
            Assert.True(collection.RepositoryResult.Count == times);
        }

        [Fact]
        public async void InsertThenUpdateAndExpectNullReference()
        {
            var make = new VehicleMakeEntity() { Id = Guid.NewGuid(), Name = "test", Abrv = "test2" };
            var byId = await MakeRepo.GetById(make.Id);
            Assert.Null(byId);
            await MakeRepo.Insert(make);
            var makes = await MakeRepo.GetAll();
            Assert.Single(makes.RepositoryResult);

            // Test double does not have Entity Framework DbContext class available and by implementation,
            // adds the item to the store instead of updating it at place.
            await Assert.ThrowsAsync<NullReferenceException>(async () => await MakeRepo.Update(make));
        }
    }
}
