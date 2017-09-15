using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using SportsStoreHierachyNavWpfApp.Categories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreHierachyNavWpfApp.Products
{
    public class ProductListViewModel:BindableBase
    {
        private IProductRepository _productRepository;
        private ObservableCollection<Product> _products;
        private ObservableCollection<string> _categories;
        private string _displayMessage;
        private string _currentCategory;        
        public ProductListViewModel()
        {
            CategoryCommand = new RelayCommand<string>(OnCategorySelected);
            DeleteCommand = new RelayCommand<Product>(OnDelete);
            EditCommand = new RelayCommand<Product>(OnEdit);
            AddNewProductCommand = new RelayCommand(OnAddNewProduct);
        }
        public string CurrentCategory
        {
            get => _currentCategory;
            set
            {
                if (value != _currentCategory)
                {
                    SetProperty(ref _currentCategory, value);
                    //_selectedCategory = value; 
                }
            }
        }

        public async void LoadProducts()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            //Products = new ObservableCollection<Product>(await _productRepository.GetProductsAsync());
            //Products = await GetProducts(CurrentCategory);
            await GetProducts(CurrentCategory);
            await GetCategories();
        }

        public async Task GetProducts(string categoryName = null)
        {
            Products = new ObservableCollection<Product>(categoryName == null || categoryName == "Home" ? await _productRepository.GetProductsAsync() : await _productRepository.GetProductsByCategoryAsync(categoryName));
        }
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                SetProperty(ref _products, value);
                //_products = value;
            }
        }

        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                if (value != _categories)
                {
                    //_categories = value;
                    SetProperty(ref _categories, value);
                }

            }
        }
        private async Task GetCategories()
        {
            var result = await _productRepository.GetProductsAsync();
            Categories =  new ObservableCollection<string>(result.Select(c => c.Category).Distinct().OrderBy(c => c));
            Categories.Insert(0, "Home");
        }

        public string DisplayMessage { get => _displayMessage;
            set
            {
                if (value != _displayMessage)
                {
                    SetProperty(ref _displayMessage, value); //_displayMessage = value;  
                }
            }
        }

        public RelayCommand<string> CategoryCommand { get; private set; }
        private async void OnCategorySelected(string category)
        {
            await GetProducts(category);
        }

        public RelayCommand AddNewProductCommand { get; private set; }
        public event Action<Product> AddNewProductRequest = delegate { };
        private void OnAddNewProduct()
        {

            DisplayMessage = string.Format($"Adding a New Product");
            AddNewProductRequest(new Product());
        }

        public RelayCommand<Product> EditCommand { get; private set; }
        public event Action<Product> EditProductRequest = delegate { };
        private void OnEdit(Product product)
        {
            DisplayMessage = string.Format($"Edit: ProductId: {product.ProductId}, ProductName: {product.ProductName} selected for Editing");
            EditProductRequest(product);
            //EditProductEvent?.Invoke(product);
        }

        public RelayCommand<Product> DeleteCommand { get; private set; }
        private void OnDelete(Product product)
        {
            DisplayMessage = string.Format($"Edit: ProductId: {product.ProductId}, ProductName: {product.ProductName} selected for Deleting");
        }
    }
}


//private CategoriesViewModel _categoriesViewModel;
//_categoriesViewModel = new CategoriesViewModel();
//_categoriesViewModel.SelectedCategoryEvent += _categoriesViewModel_SelectedCategoryEvent;
//private async void _categoriesViewModel_SelectedCategoryEvent(string selectedCategory)
//{
//    DisplayMessage = string.Format($"The Category is: {selectedCategory}");
//    Products = await GetProducts(selectedCategory);
//}