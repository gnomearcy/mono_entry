using Project.DAL;
using Project.Models.Common;
using Project.Models.Dto;
using Project.Repository;
using Project.Service;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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


        #region Testing
        // Required default constructor for Swagger API testing
        public MakeController()
        {
            // Manually create a Service for usage in API methods
            var c = new VehicleDbContext();
            this.Service = new VehicleService(new VehicleMakeRepository(c), new VehicleModelRepository(c), new UnitOfWork(c));
        }
        #endregion

        [HttpPost]
        [Route("make/create")]
        public async Task<HttpResponseMessage> CreateMake([FromBody] IVehicleMake model)
        {
            var result = await Service.CreateMake(model);
            switch (result)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpPost]
        [Route("make/update")]
        public async Task<HttpResponseMessage> UpdateMake([FromBody] IVehicleMake model)
        {
            var status_code = await Service.UpdateMake(model);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error occurred.");
            }
        }

        [HttpPost]
        [Route("make/delete")]
        public async Task<HttpResponseMessage> DeleteMake(Guid id)
        {
            var status_code = await Service.DeleteMake(id);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occurred on server side.");
            }
        }

        [HttpPost]
        [Route("delete/make_and_models")]
        public async Task<HttpResponseMessage> DeleteMakeAndRelatedModels(Guid makeId)
        {
            var status_code = (ServiceStatusCode) await Service.DeleteModelsByMake(makeId);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Unknown error occurred on server side.");
            }
        }

        [HttpPost]
        [Route("make/page")]
        public async Task<HttpResponseMessage> GetPage([FromBody] MakePagePayload payload)
        {
            var page = await Service.GetMakePageFor(payload);
            switch (page.Item2)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK, page.Item1);
                case ServiceStatusCode.FAIL:
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "An internal server error occurred");
            }
            
        }

        //[HttpPost]
        //[Route("make/filter")]
        //public async Task<HttpResponseMessage> FilterStuff([FromBody] FilterPayload payload)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, null);
        //}
    }
}
