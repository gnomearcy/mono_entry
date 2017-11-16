using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.MockData
{
    /// <summary>
    /// Dummy data for API testing purposes
    /// </summary>
    public class Vehicles : System.Data.Entity.CreateDatabaseIfNotExists<VehicleDbContext>
    {
        protected override void Seed(VehicleDbContext context)
        {
            var BMW_GUID = Guid.NewGuid();
            var Mercedes_Guid = Guid.NewGuid();
            var Volkswagen_Guid = Guid.NewGuid();

            var makes = new List<VehicleMakeEntity>
            {
                new VehicleMakeEntity{ Id = BMW_GUID, Name = "German BMW car", Abrv = "BMW"},
                new VehicleMakeEntity{ Id = Mercedes_Guid, Name = "Mercedes", Abrv = "Mer"},
                new VehicleMakeEntity{ Id = Volkswagen_Guid, Name = "Volkswagen", Abrv = "VVGN"}
            };

            makes.ForEach(m => context.Make.Add(m));
            context.SaveChanges();

            var models = new List<VehicleModelEntity>
            {
                // BMW 3 and BMW 5, to see the 1-N relationship
                new VehicleModelEntity{ Id = Guid.NewGuid(), MakeId = BMW_GUID, Name = "BMW Series 3", Abrv = "BMW_3"},
                new VehicleModelEntity{ Id = Guid.NewGuid(), MakeId = BMW_GUID, Name = "BMW Series 5", Abrv = "BMW_5"},

                // Brand new non-existing Volkswagen car
                new VehicleModelEntity{ Id = Guid.NewGuid(), MakeId = Volkswagen_Guid, Name = "Volkswagen TipTopSeries", Abrv = "VVGN_TTS"},

                new VehicleModelEntity{ Id = Guid.NewGuid(), MakeId = Mercedes_Guid, Name = "Mercedes_SLK", Abrv = "Mer_ssssilk!"}
            };

            models.ForEach(m => context.Model.Add(m));
            context.SaveChanges();
        }
    }
}

