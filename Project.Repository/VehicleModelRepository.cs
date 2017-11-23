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
        public IDbContext Context;

        public VehicleModelRepository(IDbContext context)
        {
            this.Context = context;
        }

        public Task<int> Delete(VehicleModelEntity model)
        {
            Context.Model.Remove(model);
            return Task.FromResult(1);
        }

        public Task<ICollection<VehicleModelEntity>> GetAll()
        {
            var result = Context.Model.ToList();
            ICollection<VehicleModelEntity> r = result;
            return Task.FromResult(r);
        }

        public Task<VehicleModelEntity> GetById(Guid id)
        {
            return Task.FromResult(Context.Model.FirstOrDefault(t => t.Id == id));
        }

        public Task<int> Insert(VehicleModelEntity model)
        {
            var result = Context.Model.FirstOrDefault(t => t.Id == model.Id);
            if(result == null)
            {
                Context.Model.Add(model);
                Context.SaveChanges();
                return Task.FromResult(1);
            }
            return Task.FromResult(0);
        }

        public Task<int> Update(VehicleModelEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Model.Attach(model);
            (Context as DbContext).Entry(model).State = EntityState.Modified;
            //Context.Entry(model).State = EntityState.Modified;
            Context.SaveChanges();
            return Task.FromResult(1);
        }
    }
}
