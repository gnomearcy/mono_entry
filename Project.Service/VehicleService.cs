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
using Project.Service.Models;
using Project.Models.Common.Paging;
using Project.Models.Common.Filtering;

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
        public async Task<IServiceResult<object>> CreateMake(IVehicleMake make)
        {
            try
            {
                var mapped = Mapper.Map<VehicleMakeEntity>(make);
                await vehicleMakeRepository.Insert(mapped);
                return await new ServiceSuccess().ToTask();
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await new ServiceError().ToTask();
            }
        }

        public async Task<IServiceResult<object>> UpdateMake(IVehicleMake make)
        {
            try
            {
                var mapped = Mapper.Map<VehicleMakeEntity>(make);
                await vehicleMakeRepository.Update(mapped);
                return await new ServiceSuccess().ToTask();
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await new ServiceError().ToTask();
            }
        }

        public async Task<IServiceResult<object>> DeleteMake(Guid id)
        {
            var result = await vehicleMakeRepository.GetById(id);
            if(result == null)
            {
                return await new ServiceNoOp().ToTask();
            }

            await vehicleMakeRepository.Delete(result.RepositoryResult);
            return await new ServiceSuccess().ToTask();
        }

        public async Task<IServiceResult<ICollection<IVehicleMake>>> GetMakePageFor(IMakePageParameters payload)
        {
            var result = await vehicleMakeRepository.GetPageFor(payload);
            return await new ServiceValue<ICollection<IVehicleMake>>
            {
                ServiceResult = result.RepositoryResult
            }
            .ToTask();
        }


        public async Task<IServiceResult<ICollection<IVehicleMake>>> FilterMakes(IMakeFilterParameters payload)
        {
            var result = await vehicleMakeRepository.FilterFor(payload);
            return await new ServiceValue<ICollection<IVehicleMake>>
            {
                ServiceResult = result.RepositoryResult
            }
            .ToTask();
        }
        #endregion

        #region Model
        public async Task<IServiceResult<object>> CreateModel(IVehicleModel model)
        {
            try
            {
                var mapped = Mapper.Map<VehicleModelEntity>(model);
                await vehicleModelRepository.Insert(mapped);
                return await new ServiceSuccess().ToTask();
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await new ServiceError().ToTask();
            }
        }

        public async Task<IServiceResult<object>> UpdateModel(IVehicleModel model)
        {
            try
            {
                var mapped = Mapper.Map<VehicleModelEntity>(model);
                await vehicleModelRepository.Update(mapped);
                return await new ServiceSuccess().ToTask();
            }
            catch (Exception)
            {
                // Perform logging or some other operation here
                // ...
                // Let the caller know about the failure
                return await new ServiceError().ToTask();
            }
        }

        public async Task<IServiceResult<object>> DeleteModel(Guid id)
        {
            var result = await vehicleModelRepository.GetById(id);
            if (result == null)
            {
                return await new ServiceNoOp().ToTask();
            }

            await vehicleModelRepository.Delete(result.RepositoryResult);
            return await new ServiceSuccess().ToTask();
        }

        /// <summary>
        /// Displaying example of UnitOfWork pattern.
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        public async Task<IServiceResult<object>> DeleteModelsByMake(Guid makeId)
        {
            // Delete original make object
            var make = await vehicleMakeRepository.GetById(makeId);
            if (make == null)
            {
                return await new ServiceNoOp().ToTask();
            }

            await unitOfWork.DeleteAsync(make);
            var repoResult = await vehicleModelRepository.GetAll();

            switch (repoResult.Code)
            {
                case RepositoryStatusCodes.SUCCESS:
                // Continue with execution
                    break;

                // For all other codes, terminate the method.
                case RepositoryStatusCodes.NO_OP:
                case RepositoryStatusCodes.ERROR:
                default:
                    return await new ServiceError().ToTask();
            }

            var models = repoResult.RepositoryResult;

            if (models != null)
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
            return await new ServiceSuccess().ToTask();
        }
        #endregion
    }
}
