using AutoMapper;
using Project.DAL;
using Project.Models.Common;
using Project.Models.Common.Filtering;
using Project.Models.Common.Paging;
using Project.Models.Filter;
using Project.Models.Paging;
using Project.Repository;
using Project.Service;
using Project.Service.Common;
using Project.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Project.WebAPI.Controllers
{
    public class MakeController : ApiController
    {
        private IVehicleService Service;

        public MakeController(IVehicleService service)
        {
            this.Service = service;
        }

        [HttpPost]
        [Route("makes")]
        public async Task<HttpResponseMessage> CreateMake([FromBody] VehicleMakeDto model)
        {
            var mapped = Mapper.Map<IVehicleMake>(model);
            var serviceResult = await Service.CreateMake(mapped);

            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.Created);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpPut]
        [Route("makes")]
        public async Task<HttpResponseMessage> UpdateMake([FromBody] VehicleMakeDto model)
        {
            var mapped = Mapper.Map<IVehicleMake>(model);
            var serviceResult = await Service.UpdateMake(mapped);

            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpDelete]
        [Route("makes")]
        public async Task<HttpResponseMessage> DeleteMake(Guid id)
        {
            var serviceResult = await Service.DeleteMake(id);

            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateErrorResponse(HttpStatusCode.NotModified, "No delete operations were performed");
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occurred on server side.");
            }
        }

        [HttpDelete]
        [Route("makes/{makeId}/models")]
        public async Task<HttpResponseMessage> DeleteMakeAndRelatedModels([FromUri] Guid makeId)
        {
            var serviceResult =  await Service.DeleteModelsByMake(makeId);
            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occurred on server side.");
            }
        }

        [HttpGet]
        [Route("makes/page")]
        public async Task<HttpResponseMessage> GetPage([FromUri] int pageNumber, [FromUri] int pageSize, [FromUri] bool sortAsc, [FromUri] SortType passedInSortType)
        {
            //var sortTypes = Enum.GetValues(typeof(SortType));
            //bool invalidSortType = true;
            //// Check if the passed in "sortType" query parameter is one of the valid values
            //foreach(SortType type in sortTypes)
            //{
            //    invalidSortType &= type == passedInSortType;
            //}
            //if (invalidSortType)
            //{
            //    var b = new StringBuilder();
            //    b.AppendLine(String.Format("Unsupported sort type: {0}", passedInSortType));
            //    b.AppendLine("Supported types:");
            //    foreach(SortType type in sortTypes)
            //    {
            //        b.AppendLine(String.Format("{0} - {1}", type.ToString(), (int)type));
            //    }
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, b.ToString());
            //}

            var payload = new PagingPayload
            {
                TargetPage = pageNumber,
                PageSize = pageSize,
                SortAsc = sortAsc,
                SortField = passedInSortType
            };

            var serviceResult = await Service.GetMakePageFor(payload);
            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK, serviceResult.ServiceResult);
                case ServiceStatusCode.NO_OP:
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal server error occurred");
            }
        }

        [HttpGet]
        [Route("makes/filter")]
        public async Task<HttpResponseMessage> FilterStuff([FromUri] string name, [FromUri] string abreviation)
        {
            var payload = new FilteringPayload
            {
                Name = name,
                Abrv = abreviation
            };
            var serviceResult = await Service.FilterMakes(payload);
            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK, serviceResult.ServiceResult);
                case ServiceStatusCode.ERROR:
                case ServiceStatusCode.NO_OP:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal server error occurred");
            }
        }
    }
}
