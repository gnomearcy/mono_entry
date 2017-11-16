using Project.DAL;
using Project.Models.Common;
using Project.Models;
using Project.Service;
using Project.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Project.Repository;

namespace Project.WebAPI.Controllers
{
    public class VehicleController : ApiController
    {
        private IVehicleService Service;
        public VehicleController(IVehicleService Service)
        {
            this.Service = Service;
        }

        #region Testing
        // Required default constructor for Swagger API testing
        public VehicleController()
        {
            // Manually create a Service for usage in API methods
            this.Service = new VehicleService(new VehicleMakeRepository(new VehicleDbContext()), new VehicleModelRepository());
        }

        #endregion


        [HttpGet]
        [Route("models")]
        public IEnumerable<IVehicleModel> getModels()
        {
            return Service.GetAllModels();
        }

        [HttpGet]
        [Route("makes")]
        public HttpResponseMessage getMakes()
        {
            try
            {
                var result = Service.GetAllMakes();
                if (result == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error");
            }
        }

        #region VehicleMake CRUD
        [HttpPost]
        [Route("create/make")]
        public HttpResponseMessage CreateMake([FromBody] IVehicleMake model)
        {
            try
            {
                var result = Service.CreateUpdateMake(model);
                if(result == null)
                {
                    // Something bad happened down the service chain.
                    throw null;
                }

                // Return the newly created model back
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch(Exception unused)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error has occurred while creating a VehicleMake");
            }
        }

        [HttpPost]
        [Route("update/make")]
        public HttpResponseMessage UpdateMake([FromBody] IVehicleMake model)
        {
            try
            {
                var result = Service.CreateUpdateMake(model);
                if (result == null)
                {
                    throw null;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception unused)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error has occurred while creating a VehicleMake");
            }
        }

        [HttpPost]
        [Route("delete/make")]
        public HttpResponseMessage deleteMake(Guid id)
        {
            try
            {
                Service.DeleteMake(id);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception unused)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error has occurred while creating a VehicleMake");
            }
        }

        #endregion

        #region VehicleModel CRUD
        [HttpPost]
        [Route("create/model")]
        public HttpResponseMessage createModel([FromBody] IVehicleModel model)
        {
            try
            {
                var result = Service.CreateUpdateModel(model);
                if (result == null)
                {
                    throw null;
                }
                return Request.CreateResponse(HttpStatusCode.Created, result);
            }
            catch (Exception unused)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error has occurred while creating a VehicleModel");
            }
        }

        [HttpPost]
        [Route("update/model")]
        public HttpResponseMessage updateModel([FromBody] IVehicleModel model)
        {
            try
            {
                var result = Service.CreateUpdateModel(model);
                if (result == null)
                {
                    throw null;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception unused)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error has occurred while creating a VehicleModel");
            }
        }

        #endregion
    }
}
