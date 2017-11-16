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
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class ArgNullCheckFilter : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validate;
        public ArgNullCheckFilter(Func<Dictionary<string, object>, bool> checkCondition)
        {
            _validate = checkCondition;
        }

        public ArgNullCheckFilter() : this(arguments => arguments.ContainsValue(null)) { }       

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (_validate(actionContext.ActionArguments))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, "The argument cannot be null");
            }
        }
    }
}