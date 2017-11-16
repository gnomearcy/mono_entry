using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace Project.WebAPI.App_Start.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}