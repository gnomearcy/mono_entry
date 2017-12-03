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
        private IDbContext Context;

        public VehicleMakeRepository(IDbContext Context)
        {
            this.Context = Context;
        }

        public Task<int> Delete(VehicleMakeEntity model)
        {
            Context.Make.Remove(model);
            return Task.FromResult(1);
        }

        public Task<ICollection<VehicleMakeEntity>> GetAll()
        {
            var result = Context.Make.ToList();
            ICollection<VehicleMakeEntity> r = result;
            return Task.FromResult(r);
        }

        public Task<VehicleMakeEntity> GetById(Guid id)
        {
            return Task.FromResult(Context.Make.FirstOrDefault(x => x.Id == id));
        }

        public Task<IQueryable<VehicleMakeEntity>> GetQueryable()
        {
            return Task.FromResult<IQueryable<VehicleMakeEntity>>(Context.Make);
        }

        public Task<int> Insert(VehicleMakeEntity model)
        {
            var result = Context.Make.FirstOrDefault(t => t.Id == model.Id);
            if(result == null)
            {
                // Save the record only if it doesn't exist
                Context.Make.Add(model);
                Context.SaveChanges();
                return Task.FromResult(1);
            }
            return Task.FromResult(0);
        }

        public Task<int> Update(VehicleMakeEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Make.Attach(model);
            (Context as DbContext).Entry(model).State = EntityState.Modified;
            Context.SaveChanges();
            return Task.FromResult(1);
        }
    }
}
