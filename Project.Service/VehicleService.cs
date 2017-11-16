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
        private IRepository<VehicleMakeEntity> vehicleMakeRepository;
        private IRepository<VehicleModelEntity> vehicleModelRepository;
        
        public VehicleService(IRepository<VehicleMakeEntity> makeRepository, IRepository<VehicleModelEntity> modelRepository)
        {
            this.vehicleMakeRepository = makeRepository;
            this.vehicleModelRepository = modelRepository;
        }

        public IVehicleMake CreateUpdateMake(IVehicleMake make)
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
                // Insert it
                vehicleMakeRepository.Insert(mapped);
            }
            else
            {
                vehicleMakeRepository.Update(mapped);
            }
            return make;
        }

        public IVehicleModel CreateUpdateModel(IVehicleModel model)
        {
            throw new NotImplementedException();
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

        public IEnumerable<IVehicleModel> GetAllModels()
        {
            throw new NotImplementedException();

        }
    }
}
