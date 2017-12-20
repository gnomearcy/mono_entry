using AutoMapper;
using Project.DAL;
using Project.Models.Common;
using Project.Repository;
using Project.Service;
using Project.Service.Common;
using Project.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Project.WebAPI.Controllers
{
    public class ModelController : ApiController
    {
        private IVehicleService Service;

        public ModelController(IVehicleService service)
        {
            this.Service = service;
        }

        [HttpPost]
        [Route("models")]
        public async Task<HttpResponseMessage> CreateModel([FromBody] VehicleModelDto model)
        {
            var mapped = Mapper.Map<IVehicleModel>(model);
            var serviceResult = await Service.CreateModel(mapped);
            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.Created);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }


        [HttpPut]
        [Route("models")]
        public async Task<HttpResponseMessage> UpdateModel([FromBody] VehicleModelDto model)
        {
            var mapped = Mapper.Map<IVehicleModel>(model);
            var serviceResult = await Service.UpdateModel(mapped);
            switch (serviceResult.StatusCode)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.NO_OP:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                case ServiceStatusCode.ERROR:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("models/{id}")]
        public async Task<HttpResponseMessage> DeleteModel([FromUri] Guid guid)
        {
            var serviceResult = await Service.DeleteModel(guid);

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
    }
}
