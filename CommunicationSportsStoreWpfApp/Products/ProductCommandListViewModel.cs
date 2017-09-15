using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationSportsStoreWpfApp.Products
{
    public class ProductCommandListViewModel : INotifyPropertyChanged
    {
        private EfProductRepository _productRepository;
        private Product _selectedProduct;
        private string _displayMessage;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public RelayCommand DeleteCommand { get; set; }
        public ProductCommandListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            DisplayMessage = "Message -> ";
            Products = new ObservableCollection<Product>(_productRepository.GetProductsAsync().Result);
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        public ObservableCollection<Product> Products { get; set; }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public string DisplayMessage
        {
            get => _displayMessage;
            set
            {
                _displayMessage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("DisplayMessage"));
            }
        }
        private void OnDelete()
        {
            DisplayMessage = string.Format($"Message -> Product: {_selectedProduct.ProductName} deleted");
            Products.Remove(_selectedProduct);
        }
        private bool CanDelete()
        {
            return _selectedProduct != null;
            //return true;//First
        }



    }
}
