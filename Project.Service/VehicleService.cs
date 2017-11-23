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

        public async Task<IVehicleMake> CreateUpdateMake(IVehicleMake make)
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

            var result = await vehicleMakeRepository.GetById(mapped.Id);
            if(result == null)
            {
                await vehicleMakeRepository.Insert(mapped);
            }
            else
            {
                await vehicleMakeRepository.Update(mapped);
            }
            return await Task.FromResult(make);
        }

        public async Task<int> DeleteMake(Guid id)
        {
            if(id == null)
            {
                throw new ArgumentException("id == null");
            }

            VehicleMakeEntity result = await vehicleMakeRepository.GetById(id);
            if(result == null)
            {
                return await Task.FromResult(0);
            }

            return await vehicleMakeRepository.Delete(result);
        }

        public async Task<ICollection<IVehicleMake>> GetAllMakes()
        {
            var result = await vehicleMakeRepository.GetAll();
            if(result == null)
            {
                return await Task.FromResult<ICollection<IVehicleMake>>(null);
            }

            var mapped = new List<IVehicleMake>();
            foreach(VehicleMakeEntity make in result)
            {
                mapped.Add(Mapper.Map<IVehicleMake>(make));
            }
            ICollection<IVehicleMake> r = mapped;
            return await Task.FromResult(r);
        }

        #region Model CRUD
        public async Task<IVehicleModel> CreateUpdateModel(IVehicleModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Model model is null");
            }

            var mapped = Mapper.Map<VehicleModelEntity>(model);
            if (mapped == null)
            {
                throw new Exception("AutoMapper exception");
            }

            var result = await vehicleModelRepository.GetById(mapped.Id);
            if (result == null)
            {
                await vehicleModelRepository.Insert(mapped);
            }
            else
            {
                await vehicleModelRepository.Update(mapped);
            }
            return await Task.FromResult(model);
        }

        public async Task<ICollection<IVehicleModel>> GetAllModels()
        {
            var result = await vehicleModelRepository.GetAll();
            if (result == null)
            {
                return await Task.FromResult<ICollection<IVehicleModel>>(null);
            }

            var mapped = new List<IVehicleModel>();
            foreach (VehicleModelEntity make in result)
            {
                mapped.Add(Mapper.Map<IVehicleModel>(make));
            }
            ICollection<IVehicleModel> r = mapped;
            return await Task.FromResult(r);

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
            var models = await vehicleModelRepository.GetAll();
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
