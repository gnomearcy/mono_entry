using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;
using System.Data.Entity;
using Project.Repository.Models;

namespace Project.Repository
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        public IDbContext Context;

        public VehicleModelRepository(IDbContext context)
        {
            this.Context = context;
        }

        async Task<IRepositoryResult<object>> IRepository<VehicleModelEntity, Guid>.Delete(VehicleModelEntity model)
        {
            Context.Model.Remove(model);
            Context.SaveChanges();
            return await new RepoSuccess().ToTask();
        }

        async Task<IRepositoryResult<ICollection<VehicleModelEntity>>> IRepository<VehicleModelEntity, Guid>.GetAll()
        {
            var result = Context.Model.ToList();
            ICollection<VehicleModelEntity> r = result;
            return await new RepoValue<ICollection<VehicleModelEntity>>
            {
                RepositoryResult = r
            }
            .ToTask();
        }

        async Task<IRepositoryResult<VehicleModelEntity>> IRepository<VehicleModelEntity, Guid>.GetById(Guid id)
        {
            return await new RepoValue<VehicleModelEntity>
            {
                RepositoryResult = Context.Model.FirstOrDefault(t => t.Id == id)
            }
            .ToTask();
        }

        async Task<IRepositoryResult<object>> IRepository<VehicleModelEntity, Guid>.Insert(VehicleModelEntity model)
        {
            var result = Context.Model.FirstOrDefault(t => t.Id == model.Id);
            if (result == null)
            {
                Context.Model.Add(model);
                Context.SaveChanges();
                return await new RepoSuccess().ToTask();
            }
            return await new RepoNoOp().ToTask();
        }

        async Task<IRepositoryResult<object>> IRepository<VehicleModelEntity, Guid>.Update(VehicleModelEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Model.Attach(model);
            (Context as DbContext).Entry(model).State = EntityState.Modified;
            //Context.Entry(model).State = EntityState.Modified;
            Context.SaveChanges();
            return await new RepoSuccess().ToTask();
        }
    }
}
