using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookupSportsStoreWpfApp.Products
{
    public class ProductListViewModel
    {
        private EfProductRepository _productRepository;
        public ObservableCollection<Product> Products { get; set; }

        public ProductListViewModel()
        {
            //Guard Condition to check if in Designer
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            //await and async cannot be used in a ctor, hence Result property
            Products = new ObservableCollection<Product>(_productRepository.GetProductsAsync().Result);
        }
    }
}
