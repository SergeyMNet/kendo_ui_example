﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelerikMvcApp1.App_Start
{
    public class LocalNinjectDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel kernel;

        public LocalNinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }
    }
}