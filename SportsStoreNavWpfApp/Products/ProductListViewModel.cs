using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreNavWpfApp.Products
{
    class ProductListViewModel: BindableBase
    {
        private IProductRepository _productRepository;
        private ObservableCollection<Product> _products;
        private string _currentCategory;
        private string _displayMessage;
        public ProductListViewModel()
        {
            EditProductCommand = new RelayCommand<Product>(OnEditProduct);
            DeleteProductCommand = new RelayCommand<Product>(OnDeleteProduct);
        }

        public ObservableCollection<Product> Products
        { get => _products; set => SetProperty(ref _products, value); }
        public string CurrentCategory { get => _currentCategory; set => SetProperty(ref _currentCategory, value); }
        public async void LoadProducts()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            Products = new ObservableCollection<Product>(await _productRepository.GetProductsAsync());
        }
        public string DisplayMessage { get => _displayMessage; set => SetProperty(ref _displayMessage, value); }

        public RelayCommand<Product> EditProductCommand { get; private set; }
        public event Action<Product> EditProductRequested = delegate { };
        public RelayCommand<Product> DeleteProductCommand { get; private set; }
        public event Action<Product> DeleteProductRequested = delegate { };

        private void OnEditProduct(Product product)
        {
            EditProductRequested(product);
        }
        private void OnDeleteProduct(Product product)
        {
            DisplayMessage = string.Format($"Delete -> ProductId: {product.ProductId}, ProductName: {product.ProductName}, Selected for Deleting");
            DeleteProductRequested(product);
        }
    }
}
