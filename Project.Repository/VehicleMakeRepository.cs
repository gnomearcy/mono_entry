using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;
using System.Data.Entity;

namespace Project.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private VehicleDbContext Context;

        public VehicleMakeRepository(VehicleDbContext Context)
        {
            this.Context = Context;
        }

        public void Delete(VehicleMakeEntity model)
        {
            Context.Make.Remove(model);
        }
        public IEnumerable<VehicleMakeEntity> GetAll()
        {
            return Context.Make.ToList();
        }

        public VehicleMakeEntity GetById(Guid id)
        {
            return Context.Make.FirstOrDefault( x=> x.Id == id);
        }

        public void Insert(VehicleMakeEntity model)
        {
            var result = Context.Make.FirstOrDefault(t => t.Id == model.Id);
            if(result == null)
            {
                // Save the record only if it doesn't exist
                Context.Make.Add(model);
            }
        }

        public void Update(VehicleMakeEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Make.Attach(model);
            Context.Entry(model).State = EntityState.Modified;
        }
    }
}
