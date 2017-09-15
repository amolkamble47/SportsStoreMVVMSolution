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
    public class ProductAttachedBehaviorListViewModel: INotifyPropertyChanged
    {
        private EfProductRepository _productRepository;
        private Product _selectedProduct;
        private string _disMessage;
        private ObservableCollection<Product> _products;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                if (_products != value)
                {
                    _products = value;
                    CallPropChange("Products");
                    //PropertyChanged(this, new PropertyChangedEventArgs("Products"));
                }
            }
        }
        public RelayCommand DeleteCommand { get; private set; }

        public ProductAttachedBehaviorListViewModel()
        {
            DisMessage = "Message -> ";
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }
        public async void LoadProducts()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            Products = new ObservableCollection<Product>(await _productRepository.GetProductsAsync());
        }
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                DeleteCommand.RaiseCanExecuteChanged();
                CallPropChange("SelectedProduct");
            }
        }

        public string DisMessage
        {
            get => _disMessage;
            set
            {
                _disMessage = value;
                CallPropChange("DisMessage");
                //PropertyChanged(this, new PropertyChangedEventArgs("DisMessage"));
            }
        }
        private void OnDelete()
        {
            DisMessage = string.Format($"Message -> Product: {_selectedProduct.ProductName} deleted");
            Products.Remove(_selectedProduct);
            
        }
        private bool CanDelete()
        {
            return _selectedProduct != null;
            //return true;//First
        }

        void CallPropChange(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
