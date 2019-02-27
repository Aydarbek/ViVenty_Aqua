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
            Mock<IViventyRepository> mock = new Mock<IViventyRepository>();
            mock.Setup(m => m.Hsuits).Returns(new List<Hsuit>
            {
                new Hsuit { Name = "Ариэль", Price = 7000 },
                new Hsuit { Name = "Касатка", Price = 5500 },
                new Hsuit {Name = "Волна", Price = 6000 }
            });

            kernel.Bind<IViventyRepository>().To<EFViventyRepository>();

        }
    }
}