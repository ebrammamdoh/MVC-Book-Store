using BookStore.Domain.Abstract;
using BookStore.Domain.Context;
using BookStore.UI.Infrastructure.Abstract;
using BookStore.UI.Infrastructure.Concret;
using Moq;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.UI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBinding();
        }

        private void AddBinding()
        {
            EmailSetting emailSetting = new EmailSetting
            {
                writeAsFile = bool.Parse(ConfigurationManager.AppSettings["email.writeAsFile"] ?? "false")
            };

            kernel.Bind<IBookRepository>().To<EFBookRepository>();
            kernel.Bind<IOrderProcessor>().To<OrderProcessor>().WithConstructorArgument("_emailSetting", emailSetting);

            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}