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
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private VehicleDbContext Context;

        public VehicleModelRepository(VehicleDbContext context)
        {
            this.Context = context;
        }

        public void Delete(VehicleModelEntity model)
        {
            Context.Model.Remove(model);
        }

        public IEnumerable<VehicleModelEntity> GetAll()
        {
            return Context.Model.ToList();
        }

        public VehicleModelEntity GetById(Guid id)
        {
            return Context.Model.FirstOrDefault(t => t.Id == id);
        }

        public void Insert(VehicleModelEntity model)
        {
            var result = Context.Model.FirstOrDefault(t => t.Id == model.Id);
            if(result == null)
            {
                Context.Model.Add(model);
            }
        }

        public void Update(VehicleModelEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Model.Attach(model);
            Context.Entry(model).State = EntityState.Modified;
        }
    }
}
