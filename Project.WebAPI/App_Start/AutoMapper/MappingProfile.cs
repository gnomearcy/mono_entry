using AutoMapper;
using Project.DAL;
using Project.Models.Common;
using Project.Models.Common.Filtering;
using Project.Models.Common.Paging;
using Project.Models.Filter;
using Project.Models.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WebAPI.App_Start.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Currently only DAL objects are mapped to Domain objects
            CreateMap<VehicleMakeEntity, IVehicleMake>();
            CreateMap<VehicleModelEntity, IVehicleModel>();

            // For future reference
            CreateMap<IVehicleMake, VehicleMakeEntity>();
            CreateMap<IVehicleModel, VehicleModelEntity>();

            MapDto();
        }

        private void MapDto()
        {
            CreateMap<IMakeFilterParameters, FilteringPayload>();
            CreateMap<IMakePageParameters, PagingPayload>();
        }
    }
}