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

        public Task<IVehicleMake> CreateUpdateMake(IVehicleMake make)
        {
            if(make == null)
            {
                throw new ArgumentNullException("Make model is null");
            }

            var mapped = Mapper.Map<VehicleMakeEntity>(make);
            if(mapped == null)
            {
                throw new Exception("AutoMapper exception");
            }

            var result = vehicleMakeRepository.GetById(mapped.Id);
            if(result == null)
            {
                vehicleMakeRepository.Insert(mapped);
            }
            else
            {
                vehicleMakeRepository.Update(mapped);
            }
            return Task.FromResult(make);
        }

       

        public void DeleteMake(Guid id)
        {
            if(id == null)
            {
                return;
            }

            var result = vehicleMakeRepository.GetById(id);
            if(result == null)
            {
                return;
            }

            vehicleMakeRepository.Delete(result);
        }

        public IEnumerable<IVehicleMake> GetAllMakes()
        {
            var result = vehicleMakeRepository.GetAll();
            if(result == null)
            {
                return null;
            }

            var mapped = new List<IVehicleMake>();
            foreach(VehicleMakeEntity make in result)
            {
                mapped.Add(Mapper.Map<IVehicleMake>(make));
            }
            return mapped;
        }

        #region Model CRUD
        public IVehicleModel CreateUpdateModel(IVehicleModel model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IVehicleModel> GetAllModels()
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// Displaying example of UnitOfWork pattern.
        /// </summary>
        /// <param name="makeId"></param>
        /// <returns></returns>
        public async Task<int> DeleteModelsByMake(Guid makeId)
        {
            // Delete original make object
            var result = await unitOfWork.DeleteAsync<VehicleMakeEntity>(makeId);
            if(result == 0) // 0 == failure, doesn't exist
            {
                // Nothing was deleted, return success to the caller
                return await Task.FromResult(0);
            }
            var models = vehicleModelRepository.GetAll();
            if(models == null)
            {
                // There are no models to delete for given MakeId
                return await Task.FromResult(0);
            }

            foreach(var model in models)
            {
                if(model.MakeId == makeId)
                {
                    await unitOfWork.DeleteAsync<VehicleModelEntity>(model);
                }
            }

            await unitOfWork.CommitAsync();
            return await Task.FromResult(0);
        }

        #endregion
    }
}
