using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BasicSportsStoreWpfApp.Products
{
    public class ProductEditSimpleMVVMViewModel : INotifyPropertyChanged
    {
        private EfProductRepository _productRepository;
        private Product _product;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ICommand SaveCommand { get; set; }
        public ProductEditSimpleMVVMViewModel()
        {
            _productRepository = new EfProductRepository();
            SaveCommand = new RelayCommand(OnSave);
        }
        public int ProductId { get; set; }
        public Product Product
        {
            get { return _product; }
            set
            {
                if (value != _product)
                {
                    _product = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Product")); 
                }
            }
        }
        public async void LoadProduct()
        {
            Product = await _productRepository.GetProductAsync(ProductId);
        }

        public async void OnSave()
        {
            Product = await _productRepository.UpdateProductAsync(Product);
        }

    }
}
