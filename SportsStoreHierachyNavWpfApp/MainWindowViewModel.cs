using SportsStoreHierachyNavWpfApp.AboutUs;
using SportsStoreHierachyNavWpfApp.Categories;
using SportsStoreHierachyNavWpfApp.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStoreDomainLibrary.Entities;

namespace SportsStoreHierachyNavWpfApp
{
    public class MainWindowViewModel: BindableBase
    {
        private AboutUsViewModel _aboutUsViewModel;
        private ProductListViewModel _productListViewModel;
        private CategoriesViewModel _categoriesViewModel;
        private AddEditProductViewModel _addEditProductViewModel;
        private BindableBase _currentViewModel;
        //private bool _productsFlag;

        public RelayCommand<string> NavigationCommand { get; private set; }
        public MainWindowViewModel()
        {
            NavigationCommand = new RelayCommand<string>(OnNavigate);
            _aboutUsViewModel = new AboutUsViewModel();

            _productListViewModel = new ProductListViewModel();
            _productListViewModel.EditProductRequest += NavigateToEditProduct;
            _productListViewModel.AddNewProductRequest += NavigateToAddProduct;

            _categoriesViewModel = new CategoriesViewModel();
            _categoriesViewModel.SelectedCategoryEvent += NavigateToProducts;

            _addEditProductViewModel = new AddEditProductViewModel();
            _addEditProductViewModel.CancelRequested += NavigateToAddEditCancel;
        }

        private void NavigateToAddEditCancel()
        {
            CurrentViewModel = _productListViewModel;
        }

        private void NavigateToEditProduct(Product product)
        {
            _addEditProductViewModel.EditFlag = true;
            _addEditProductViewModel.SetProduct(product);
            CurrentViewModel = _addEditProductViewModel;
        }

        private void NavigateToAddProduct(Product product)
        {
            _addEditProductViewModel.EditFlag = false;
            _addEditProductViewModel.SetProduct(product);
            CurrentViewModel = _addEditProductViewModel;
        }

        private void NavigateToProducts(string selectedCategory)
        {
            _productListViewModel.CurrentCategory = selectedCategory;
            CurrentViewModel = _productListViewModel;
        }

        public BindableBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel,value);
        }
        private void OnNavigate(string destination)
        {
            switch (destination)
            {
                case "categories":
                    CurrentViewModel = _categoriesViewModel;
                    break;
                case "aboutus":
                    CurrentViewModel = _aboutUsViewModel;
                    break;
                case "products":
                    //if (_productsFlag) _productListViewModel.GetProducts();
                    CurrentViewModel = _productListViewModel;
                    //_productsFlag = true;
                    break;
                default:
                    CurrentViewModel = _productListViewModel;
                    break;
            }
        }


        //public object CurrentViewModel { get; set; } //use BindableBase
    }
}
