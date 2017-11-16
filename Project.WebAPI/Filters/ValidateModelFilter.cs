using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Project.WebAPI.Filters
{
    /// <summary>
    /// Filter that returns <see cref="HttpStatusCode.BadRequest"/> if the action model state is not valid.
    /// </summary>
    public class ValidateModelFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        } 
    }
}