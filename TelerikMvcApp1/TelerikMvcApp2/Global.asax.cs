using DAL.Interfaces;
using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DAL;
using System.Web.Http;
using TelerikMvcApp2.App_Start;

namespace TelerikMvcApp2
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = new LocalNinjectDependencyResolver(kernel);              
            return kernel;
        }


        private static void RegisterServices(IKernel kernel)
        {         
            kernel.Bind<IContext>().To<ModelDB>();
            kernel.Bind<IDAL>().To<DAL.DAL>();         
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
