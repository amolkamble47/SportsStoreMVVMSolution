using Microsoft.Practices.Unity;
using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreValidationDIWpfApp
{
    public static class SportsStoreContainer
    {
        private static UnityContainer _container;

        static SportsStoreContainer()
        {
            _container = new UnityContainer();
            AddBindinds();
        }

        private static void AddBindinds()
        {
            //ContainerControlledLifetimeManager is like singleton
            _container.RegisterType<IProductRepository, EfProductRepository>(new ContainerControlledLifetimeManager());
        }

        public static UnityContainer Container { get => _container; }
    }
}
