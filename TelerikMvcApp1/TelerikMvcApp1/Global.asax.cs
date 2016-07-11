using DAL.Interfaces;
using DAL.ModelsDAL;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TelerikMvcApp1.App_Start;

namespace TelerikMvcApp1
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
         { 
             var kernel = new StandardKernel(); 
             RegisterServices(kernel);
             //GlobalConfiguration.Configuration.DependencyResolver = new LocalNinjectDependencyResolver(kernel);              
             return kernel;
         }


        private static void RegisterServices(IKernel kernel)
        {
            //var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            //kernel.Bind(a => a.FromAssembliesInPath(path).SelectAllClasses().BindDefaultInterface());

            kernel.Bind<IContext>().To<Context>();
            kernel.Bind<IDAL>().To<DAL.DAL>();
            //kernel.Bind<ICategoriesDAL>().To<CategoriesDAL>();
            //kernel.Bind<IDocumentsDAL>().To<DocumentsDAL>();
        }

         protected override void OnApplicationStarted()
         {
             base.OnApplicationStarted();
             AreaRegistration.RegisterAllAreas();
             FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
             RouteConfig.RegisterRoutes(RouteTable.Routes);
             BundleConfig.RegisterBundles(BundleTable.Bundles);
         }


        //protected void Application_Start()
        //{
        //    AreaRegistration.RegisterAllAreas();
        //    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}
    }
}
