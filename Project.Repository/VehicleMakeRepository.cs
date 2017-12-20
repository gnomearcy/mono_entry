using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.DAL;
using System.Data.Entity;
using Project.Models.Common.Paging;
using Project.Repository.Models;
using AutoMapper;
using Project.Models.Common.Filtering;

namespace Project.Repository
{
    public class VehicleMakeRepository : IVehicleMakeRepository
    {
        private IDbContext Context;

        public VehicleMakeRepository(IDbContext Context)
        {
            this.Context = Context;
        }

        async Task<IRepositoryResult<ICollection<IVehicleMake>>> IFilterator<IMakeFilterParameters, ICollection<IVehicleMake>>.FilterFor(IMakeFilterParameters payload)
        {
            var makes = Context.Make;
            try
            {
                var filtered =
                makes
                .Where(s => s.Name.Contains(payload.Name))
                .Union(makes.Where(s => s.Abrv.Contains(payload.Abrv)))
                .ToList();

                var mapped = Mapper.Map<ICollection<IVehicleMake>>(filtered);
                return await new RepoValue<ICollection<IVehicleMake>>
                {
                    RepositoryResult = mapped
                }
                .ToTask();
            }
            catch (Exception)
            {
                // Possible maping exception or something broke on the database part.
                // For the sake of this demo, exception is not logged or similar.
                return await new RepoError<ICollection<IVehicleMake>>().ToTask();
            }
        }


        async Task<IRepositoryResult<ICollection<IVehicleMake>>> IPaginator<IMakePageParameters, ICollection<IVehicleMake>>.GetPageFor(IMakePageParameters payload)
        {
            var makes = Context.Make;
            var amount = Context.Make.Count();
            if (makes == null || amount == 0)
            {
                return await new RepoValue<ICollection<IVehicleMake>>
                {
                    RepositoryResult = new List<IVehicleMake>()
                }
                .ToTask();
            }
            else
            {
                // Check if the requested data is out of range
                int skip = Math.Abs(payload.TargetPage - 1) * payload.PageSize;
                int pageCount = (amount / payload.PageSize) + (amount % payload.PageSize == 0 ? 0 : 1);

                if (amount < skip)
                {
                    // There is more data to skip than it exists, therefor send back
                    // an invalid page denoting requested page was out of range
                    return await new RepoValue<ICollection<IVehicleMake>>
                    {
                        RepositoryResult = new List<IVehicleMake>()
                    }
                    .ToTask();
                }

                int take = payload.PageSize;

                Func<VehicleMakeEntity, object> sortLambda = null;
                switch (payload.SortField)
                {
                    case SortType.NAME:
                        sortLambda = (s => s.Name);
                        break;
                    case SortType.ABREVIATION:
                        sortLambda = (s => s.Abrv);
                        break;
                    default:
                        // Unsupported SortType enum
                       return await new RepoError<ICollection<IVehicleMake>>().ToTask();
                }


                var data =
                    (
                        payload.SortAsc ?
                        makes.OrderBy(sortLambda) :
                        makes.OrderByDescending(sortLambda)
                    )
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                var mapped = Mapper.Map<ICollection<IVehicleMake>>(data);
                return await new RepoValue<ICollection<IVehicleMake>>
                {
                    RepositoryResult = mapped
                }
                .ToTask();
            }
        }

        async Task<IRepositoryResult<ICollection<VehicleMakeEntity>>> IRepository<VehicleMakeEntity, Guid>.GetAll()
        {
            var result = Context.Make.ToList();
            ICollection<VehicleMakeEntity> r = result;
            return await new RepoValue<ICollection<VehicleMakeEntity>>
            {
                RepositoryResult = result
            }
            .ToTask();
        }

        async Task<IRepositoryResult<VehicleMakeEntity>> IRepository<VehicleMakeEntity, Guid>.GetById(Guid id)
        {
            return await new RepoValue<VehicleMakeEntity>
            {
                RepositoryResult = Context.Make.FirstOrDefault(x => x.Id == id)
            }
            .ToTask();
        }

        public Task<IRepositoryResult<object>> Insert(VehicleMakeEntity model)
        {
            var result = Context.Make.FirstOrDefault(t => t.Id == model.Id);
            if (result == null)
            {
                // Save the record only if it doesn't exist
                Context.Make.Add(model);
                Context.SaveChanges();
                return new RepoSuccess().ToTask();
            }
            return new RepoNoOp().ToTask();
        }

        async Task<IRepositoryResult<object>> IRepository<VehicleMakeEntity, Guid>.Update(VehicleMakeEntity model)
        {
            // Source:
            // https://stackoverflow.com/a/15339512/3744259
            Context.Make.Attach(model);
            (Context as DbContext).Entry(model).State = EntityState.Modified;
            Context.SaveChanges();
            return await new RepoSuccess().ToTask();
        }

        async Task<IRepositoryResult<object>> IRepository<VehicleMakeEntity, Guid>.Delete(VehicleMakeEntity model)
        {
            Context.Make.Remove(model);
            Context.SaveChanges();
            return await new RepoSuccess().ToTask();
        }
    }
}
