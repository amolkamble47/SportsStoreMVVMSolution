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

namespace SportsStoreValidationDIWpfApp.Products
{
    public class ProductListViewModel: BindableBase
    {
        private IProductRepository _productRepository;
        private ObservableCollection<Product> _products;
        private List<Product> _allProducts;
        private ObservableCollection<string> _categories;
        private string _currentCategory;
        private string _displayMessage;
        private bool _messageFlag;
        private string _searchInput;
        public ProductListViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CategoryCommand = new RelayCommand<string>(OnCategorySelected);
            AddNewProductCommand = new RelayCommand(OnAddNewProduct);
            EditProductCommand = new RelayCommand<Product>(OnEdit);
            DeleteProductCommand = new RelayCommand<Product>(OnDelete);
            DisplayMessage = "Message: ";
            MessageFlag = false;
            DismissMessageCommand = new RelayCommand(OnDismissMessage);
            ClearSearchCommand = new RelayCommand(OnClearSearch);
        }

        public async void LoadProducts()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            //_productRepository = new EfProductRepository();//Not required after the DI
            _allProducts = await _productRepository.GetProductsAsync();
            await GetProducts(CurrentCategory);
            await GetCategories();
        }
        private async Task GetCategories()
        {
            var result = await _productRepository.GetProductsAsync();
            Categories = new ObservableCollection<string>(result.Select(c => c.Category).Distinct().OrderBy(c => c));
            Categories.Insert(0, "Home");
        }
        private async Task GetProducts(string currentCategory)
        {
            Products = new ObservableCollection<Product>(currentCategory == null || currentCategory == "Home" ? _allProducts : _allProducts.Where(c=>c.Category == currentCategory));
            //Products = new ObservableCollection<Product>(currentCategory == null || currentCategory == "Home" ? await _productRepository.GetProductsAsync() : await _productRepository.GetProductsByCategoryAsync(currentCategory) );
        }

        private void SearchProducts(string searchInput)
        {
            Products = new ObservableCollection<Product>(string.IsNullOrWhiteSpace(searchInput) ? _allProducts : _allProducts.Where(c => c.ProductName.ToLower().Contains(searchInput.ToLower())));
        }
        public string SearchInput
        {
            get => _searchInput;
            set
            {
                SetProperty(ref _searchInput, value);
                SearchProducts(_searchInput);
            }
        }
        public ObservableCollection<Product> Products { get => _products; set => SetProperty(ref _products, value); }
        public ObservableCollection<string> Categories { get => _categories; set => SetProperty(ref _categories, value); }
        public string CurrentCategory { get => _currentCategory; set => SetProperty(ref _currentCategory, value); }
        public string DisplayMessage { get => _displayMessage; set => SetProperty(ref _displayMessage, value); }
        public bool MessageFlag { get => _messageFlag; set => SetProperty(ref _messageFlag, value); }

        public RelayCommand ClearSearchCommand { get; private set; }
        private void OnClearSearch() { SearchInput = null; }

        public RelayCommand DismissMessageCommand { get; private set; }
        private void OnDismissMessage() { MessageFlag = false; }
        public RelayCommand<string> CategoryCommand { get; set; }
        public async void OnCategorySelected(string category) { await GetProducts(category); }
        public RelayCommand AddNewProductCommand { get; set; }
        public event Action<Product> AddNewProductRequested = delegate { };
        public void OnAddNewProduct() { AddNewProductRequested(new Product()); }
        public RelayCommand<Product> EditProductCommand { get; set; }
        public event Action<Product> EditProductRequested = delegate { };
        public void OnEdit(Product product) { EditProductRequested(product); }
        public RelayCommand<Product> DeleteProductCommand { get; set; }
        //public event Action<string> DeleteProductRequested = delegate { }; //If required to handle from the MainWindowViewModel
        public async void OnDelete(Product product)
        {
            await _productRepository.DeleteProductAsync(product.ProductId);
            MessageFlag = true;
            if (MessageFlag)
            {
                DisplayMessage = string.Format($"Product: {product.ProductName}, with the Id: {product.ProductId}, Deleted Successfully");
                LoadProducts();
            }
            //DeleteProductRequested(DisplayMessage);//If required to handle from the MainWindowViewModel
        }
    }
}
