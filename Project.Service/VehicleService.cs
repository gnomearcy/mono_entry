using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Models.Common;
using Project.Repository.Common;
using Project.DAL;
using AutoMapper;
using Project.Common;
using Project.Models.Dto;

namespace Project.Service
{
    public class VehicleService : IVehicleService
    {
        private IVehicleMakeRepository vehicleMakeRepository;
        private IVehicleModelRepository vehicleModelRepository;
        private IUnitOfWork unitOfWork;
        
        public VehicleService(IVehicleMakeRepository makeRepository, IVehicleModelRepository modelRepository, IUnitOfWork unitOfWork)
        {
            this.vehicleMakeRepository = makeRepository;
            this.vehicleModelRepository = modelRepository;
            this.unitOfWork = unitOfWork;
        }

        #region Make
        public async Task<ServiceStatusCode> CreateMake(IVehicleMake make)
        {
            try
            {
                var mapped = Mapper.Map<VehicleMakeEntity>(make);
                await vehicleMakeRepository.Insert(mapped);
                return await Task.FromResult(ServiceStatusCode.SUCCESS);
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }
        }

        public async Task<ServiceStatusCode> UpdateMake(IVehicleMake make)
        {
            try
            {
                var mapped = Mapper.Map<VehicleMakeEntity>(make);
                await vehicleMakeRepository.Update(mapped);
                return await Task.FromResult(ServiceStatusCode.SUCCESS);
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }
        }

        public async Task<ServiceStatusCode> DeleteMake(Guid id)
        {
            VehicleMakeEntity result = await vehicleMakeRepository.GetById(id);
            if(result == null)
            {
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }

            await vehicleMakeRepository.Delete(result);
            return await Task.FromResult(ServiceStatusCode.SUCCESS);
        }

        public async Task<Tuple<MakePageDto, ServiceStatusCode>> GetMakePageFor(MakePagePayload payload)
        {
            var makes = await vehicleMakeRepository.GetQueryable();
            var amount = makes.Count();
            if(makes == null || amount == 0)
            {
                // Error page denoting there are no pages, with PageCount property
                var errorPage = new MakePageDto
                {
                    Data = new List<IVehicleMake>(),
                    PageNumber = payload.TargetPage,
                    PageCount = 0,
                    PageSize = payload.PageSize,
                };
                return await Task.FromResult(Tuple.Create(errorPage, ServiceStatusCode.SUCCESS));
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
                    var invalidPage = new MakePageDto
                    {
                        Data = new List<IVehicleMake>(),
                        PageNumber = payload.TargetPage,
                        PageCount = pageCount,
                        PageSize = payload.PageSize
                    };
                    return await Task.FromResult(Tuple.Create(invalidPage, ServiceStatusCode.SUCCESS));
                }

                int take = payload.PageSize;

                Func<VehicleMakeEntity, object> sortLambda = null;
                switch (payload.SortField)
                {
                    case MakePagePayload.SortType.NAME:
                        sortLambda = (s => s.Name);
                        break;
                    case MakePagePayload.SortType.ABREVIATION:
                        sortLambda = (s => s.Abrv);
                        break;
                    default:
                        // Unsupported SortType enum
                        return await Task.FromResult(Tuple.Create<MakePageDto, ServiceStatusCode>(null, ServiceStatusCode.FAIL));
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
                var validPage = new MakePageDto
                {
                    Data = mapped,
                    PageNumber = payload.TargetPage,
                    PageCount = pageCount,
                    PageSize = payload.PageSize,
                };

                return await Task.FromResult(Tuple.Create(validPage, ServiceStatusCode.SUCCESS));
            }
        }


        public async Task<Tuple<ICollection<IVehicleMake>, ServiceStatusCode>> FilterMakes(FilterPayload payload)
        {
            var makes = await vehicleMakeRepository.GetQueryable();

            try
            {
                var filtered =
                makes
                .Where(s => s.Name.Contains(payload.Name))
                .Union(makes.Where(s => s.Abrv.Contains(payload.Abrv)))
                .ToList();

                var mapped = Mapper.Map<ICollection<IVehicleMake>>(filtered);
                return await Task.FromResult(Tuple.Create(mapped, ServiceStatusCode.SUCCESS));
            }
            catch(Exception)
            {
                // Possible maping exception or something broke on the database part.
                // For the sake of this demo, exception is not logged or similar.
                return await Task.FromResult(Tuple.Create<ICollection<IVehicleMake>, ServiceStatusCode>(null, ServiceStatusCode.FAIL));
            }
            
        }
        #endregion

        #region Model
        public async Task<ServiceStatusCode> CreateModel(IVehicleModel model)
        {
            try
            {
                var mapped = Mapper.Map<VehicleModelEntity>(model);
                await vehicleModelRepository.Insert(mapped);
                return await Task.FromResult(ServiceStatusCode.SUCCESS);
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }
        }

        public async Task<ServiceStatusCode> UpdateModel(IVehicleModel model)
        {
            try
            {
                var mapped = Mapper.Map<VehicleModelEntity>(model);
                await vehicleModelRepository.Update(mapped);
                return await Task.FromResult(ServiceStatusCode.SUCCESS);
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }
        }

        public async Task<ServiceStatusCode> DeleteModel(Guid id)
        {
            VehicleModelEntity result = await vehicleModelRepository.GetById(id);
            if (result == null)
            {
                return await Task.FromResult(ServiceStatusCode.FAIL);
            }

            await vehicleModelRepository.Delete(result);
            return await Task.FromResult(ServiceStatusCode.SUCCESS);
        }

        /// <summary>
        /// Displaying example of UnitOfWork pattern.
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        public async Task<int> DeleteModelsByMake(Guid makeId)
        {
            // Delete original make object
            var make = await vehicleMakeRepository.GetById(makeId);
            if (make == null)
            {
                return await Task.FromResult((int)ServiceStatusCode.FAIL);
            }

            await unitOfWork.DeleteAsync(make);
            var models = await vehicleModelRepository.GetAll();

            if(models != null)
            {
                foreach (var model in models)
                {
                    if (model.MakeId == makeId)
                    {
                        await unitOfWork.DeleteAsync(model);
                    }
                }
            }
           

            await unitOfWork.CommitAsync();
            return await Task.FromResult((int)ServiceStatusCode.SUCCESS);
        }

        #endregion
    }
}
