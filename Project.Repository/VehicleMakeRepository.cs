using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;

namespace Project.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private VehicleDbContext Context;

        public VehicleMakeRepository(VehicleDbContext Context)
        {
            this.Context = Context;
        }

        //public void Delete(object id)
        //{
        //    var result = Context.Make.Find(id);
        //    Context.Make.Remove(result);
        //    Context.SaveChanges();
        //}

        void IRepository<VehicleMakeEntity>.Delete(VehicleMakeEntity model)
        {
            Context.Make.Remove(model);
            Context.SaveChanges();
        }
        public IEnumerable<VehicleMakeEntity> GetAll()
        {
            return Context.Make;
        }

        public VehicleMakeEntity GetById(object id)
        {
            return Context.Make.Find(id);
        }

        public void Insert(VehicleMakeEntity model)
        {
            var result = Context.Make.FirstOrDefault(t => t.Id == model.Id);
            if(result == null)
            {
                // Save the record only if it doesn't exist
                Context.Make.Add(model);
                Context.SaveChanges();
            }
        }

        public void Update(VehicleMakeEntity model)
        {
            var result = Context.Make.SingleOrDefault(b => b.Id == model.Id);
            if(result != null)
            {
                result.Name = model.Name;
                result.Abrv = model.Abrv;
                Context.SaveChanges();
            }
        }

       
    }
}
