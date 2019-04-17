using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;

using ViVenty.Domain.Abstract;
using ViVenty.Domain.Entities;
using ViVenty.Domain.Concrete;
using System.Configuration;

namespace ViVenty.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver (IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }


        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IViventyRepository>().To<EFViventyRepository>();

            /*           EmailSettings emailSettings = new EmailSettings
                       {
                           WriteAsFile = bool.Parse(ConfigurationManager.
                           AppSettings["Email.WriteAsFile"] ?? "false")
                       }; */

            kernel.Bind<IEmailService>().To<EmailService>();

            kernel.Bind<IOrderProcessor>().To<OrderProcessor>();

        }
    }
}