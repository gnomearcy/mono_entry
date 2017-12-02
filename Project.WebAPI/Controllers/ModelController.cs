using Project.DAL;
using Project.Models.Common;
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
    public class ModelController : ApiController
    {
        private IVehicleService Service;

        public ModelController(IVehicleService service)
        {
            this.Service = service;
        }

        #region Testing
        // Required default constructor for Swagger API testing
        public ModelController()
        {
            // Manually create a Service for usage in API methods
            var c = new VehicleDbContext();
            this.Service = new VehicleService(new VehicleMakeRepository(c), new VehicleModelRepository(c), new UnitOfWork(c));
        }
        #endregion

        [HttpPost]
        [Route("model/create")]
        public async Task<HttpResponseMessage> CreateModel([FromBody] IVehicleModel model)
        {
            var status_code = await Service.CreateModel(model);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.Created);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }


        [HttpPost]
        [Route("model/update")]
        public async Task<HttpResponseMessage> UpdateModel([FromBody] IVehicleModel model)
        {
            var status_code = await Service.UpdateModel(model);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        [HttpPost]
        [Route("model/delete")]
        public async Task<HttpResponseMessage> DeleteModel([FromBody] IVehicleModel model)
        {
            var status_code = await Service.DeleteModel(model.Id);
            switch (status_code)
            {
                case ServiceStatusCode.SUCCESS:
                    return Request.CreateResponse(HttpStatusCode.OK);
                case ServiceStatusCode.FAIL:
                    return Request.CreateResponse(HttpStatusCode.NotModified);
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }
    }
}
