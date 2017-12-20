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
            DynamicConfiguration();
        }

        private static void StaticConfiguration()
        {
            var kernel = new StandardKernel(
              new Project.Service.DIModule(),
              new Project.Repository.DIModule()
          );

            System.Web.Http.Dependencies.IDependencyResolver ninjectResolver = new NinjectResolver(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }


        private static void DynamicConfiguration()
        {
            var settings = new NinjectSettings
            {
                LoadExtensions = true
            };
            settings.ExtensionSearchPatterns = settings.ExtensionSearchPatterns.Union(new string[]{ "Project.*.dll" }).ToArray();
            var kernel = new StandardKernel(settings);
            System.Web.Http.Dependencies.IDependencyResolver ninjectResolver = new NinjectResolver(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = ninjectResolver;
        }
    }
}