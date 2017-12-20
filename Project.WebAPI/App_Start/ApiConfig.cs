using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Project.Models;
using Project.Models.Common;
using Project.Models.Common.Filtering;
using Project.Models.Common.Paging;
using Project.Models.Filter;
using Project.Models.Paging;
using Project.WebAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Project.WebAPI.App_Start
{
    public static class ApiConfig
    {
        public static void Configure()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new VehicleMakeConverter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new VehicleModelConverter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new FilterParamsConverter());
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new PageParamsConverter());

            // Declare filters for ModelState validation and NullCheck check
            GlobalConfiguration.Configuration.Filters.Add(new ArgNullCheckFilter());
            GlobalConfiguration.Configuration.Filters.Add(new ValidateModelFilter());
        }

        internal class VehicleMakeConverter : CustomCreationConverter<IVehicleMake>
        {
            public override IVehicleMake Create(Type objectType)
            {
                return new VehicleMake();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                IVehicleMake obj = Create(objectType);
                serializer.Populate(reader, obj);
                return obj;
            }
        }

        internal class VehicleModelConverter: CustomCreationConverter<IVehicleModel>
        {
            public override IVehicleModel Create(Type objectType)
            {
                return new VehicleModel();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                IVehicleModel obj = Create(objectType);
                serializer.Populate(reader, obj);
                return obj;
            }
        }

        internal class FilterParamsConverter : CustomCreationConverter<IMakeFilterParameters>
        {
            public override IMakeFilterParameters Create(Type objectType)
            {
                return new FilteringPayload();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                IMakeFilterParameters obj = Create(objectType);
                serializer.Populate(reader, obj);
                return obj;
            }
        }
        internal class PageParamsConverter : CustomCreationConverter<IMakePageParameters>
        {
            public override IMakePageParameters Create(Type objectType)
            {
                return new PagingPayload();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null)
                {
                    return null;
                }

                IMakePageParameters obj = Create(objectType);
                serializer.Populate(reader, obj);
                return obj;
            }
        }
    }
}