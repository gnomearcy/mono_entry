using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiContrib.IoC.Ninject;

namespace Project.WebAPI.App_Start.DI
{
    public static class DIConfig
    {
        public static void Configure()
        {
            var kernel = new StandardKernel(
                new Project.Service.DIModule(),
                new Project.Repository.DIModule()
            );

            System.Web.Http.Dependencies.IDependencyResolver ninjectResolver = new NinjectResolver(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}