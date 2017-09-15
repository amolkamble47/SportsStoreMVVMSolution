using SportsStoreValidationDIWpfApp.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreDomainLibrary.Entities;
using Microsoft.Practices.Unity;

namespace SportsStoreValidationDIWpfApp
{
    public class MainWindowViewModel: BindableBase
    {
        private ProductListViewModel _productListViewModel;
        private AddEditProductViewModel _addEditProductViewModel;
        private BindableBase _currentViewModel;

        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigate);

            //_productListViewModel = new ProductListViewModel();
            _productListViewModel = SportsStoreContainer.Container.Resolve<ProductListViewModel>();
            _productListViewModel.AddNewProductRequested += NavigateToAddNewProduct;
            _productListViewModel.EditProductRequested += NavigateToEditProduct;
            //_productListViewModel.DeleteProductRequested += NavigateToDeleteProduct;

            //_addEditProductViewModel = new AddEditProductViewModel();
            _addEditProductViewModel = SportsStoreContainer.Container.Resolve<AddEditProductViewModel>();
            _addEditProductViewModel.Done += NavigateToProduct;

            AddNewProductCommand = new RelayCommand(OnAddNewProduct);
        }

        private void NavigateToDeleteProduct(string deleteMessage)
        {
            //Display the Deleted message
            CurrentViewModel = _productListViewModel;
        }

        public BindableBase CurrentViewModel { get => _currentViewModel; set => SetProperty(ref _currentViewModel, value); }
        private void NavigateToProduct()
        {
            CurrentViewModel = _productListViewModel;
        }

        private void NavigateToEditProduct(Product product)
        {
            _addEditProductViewModel.EditFlag = true;
            _addEditProductViewModel.SetProduct(product);
            CurrentViewModel = _addEditProductViewModel;
        }

        private void NavigateToAddNewProduct(Product product)
        {
            _addEditProductViewModel.EditFlag = false;
            _addEditProductViewModel.SetProduct(product);
            CurrentViewModel = _addEditProductViewModel;
        }

        public RelayCommand<string> NavigationCommand { get; set; }
        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                default:
                    CurrentViewModel = _productListViewModel;
                    break;
            }
        }
        public RelayCommand AddNewProductCommand { get; set; }
        private void OnAddNewProduct()
        {
            NavigateToAddNewProduct(new Product());
        }
    }
}
