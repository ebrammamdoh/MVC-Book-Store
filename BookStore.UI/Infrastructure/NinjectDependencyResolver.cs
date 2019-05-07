using BookStore.Domain.Abstract;
using BookStore.Domain.Context;
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
            //kernel.Bind<IBookRepository>().To<Book>();
            //Mock<IBookRepository> mock = new Mock<IBookRepository>();
            //mock.Setup(m => m.Books).Returns(
            //    new List<Book>
            //    {
            //        new Book {ISBN=1, Title="Book1", Description="Desc1", Category="comedy", Price=101.5m, Author="ahmed khaled" },
            //        new Book {ISBN=2, Title="Book2", Description="Desc2", Category="drama", Price=157.3m, Author="ahmed khaled" },
            //        new Book {ISBN=3, Title="Book3", Description="Desc3", Category="comedy", Price=45.1m, Author="ahmed khaled" },
            //    }
            //);

            //kernel.Bind<IBookRepository>().ToConstant(new EFBookRepository());
            EmailSetting emailSetting = new EmailSetting
            {
                writeAsFile = bool.Parse(ConfigurationManager.AppSettings["email.writeAsFile"] ?? "false")
            };

            kernel.Bind<IBookRepository>().To<EFBookRepository>();
            kernel.Bind<IOrderProcessor>().To<OrderProcessor>().WithConstructorArgument("_emailSetting", emailSetting);

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