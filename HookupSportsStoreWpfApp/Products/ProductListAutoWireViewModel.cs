using System.ComponentModel;
using System.Collections.ObjectModel;

using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;

namespace HookupSportsStoreWpfApp.Products
{
    public class ProductListAutoWireViewModel
    {
        private EfProductRepository _productRepository;
        public ObservableCollection<Product> Products { get; set; }

        public ProductListAutoWireViewModel()
        {
            //Guard Condition to check if in Designer
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            //await and async cannot be used in a ctor, hence Result property
            Products = new ObservableCollection<Product>(_productRepository.GetProductsAsync().Result);
        }
    }
}
