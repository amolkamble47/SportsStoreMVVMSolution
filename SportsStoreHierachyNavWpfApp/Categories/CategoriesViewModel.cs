using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Concrete;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreHierachyNavWpfApp.Categories
{
    public class CategoriesViewModel: BindableBase
    {
        private IProductRepository _productRepository;
        private ObservableCollection<string> _categories;
        private string _displayMessage;
        public RelayCommand<string> CategoryCommand { get; private set; }
        public CategoriesViewModel()
        {
            CategoryCommand = new RelayCommand<string>(OnCategorySelected);
        }
        public string DisplayMessage
        {
            get => _displayMessage;
            set
            {
                if (value != _displayMessage)
                {
                    SetProperty(ref _displayMessage, value); //_displayMessage = value;  
                }
            }
        }

        public event Action<string> SelectedCategoryEvent = delegate { };
        private void OnCategorySelected(string selectedCategory)
        {
            DisplayMessage = string.Format($"Category Selected: {selectedCategory}"); 
            SelectedCategoryEvent(selectedCategory);
        }

        public async void LoadCategories()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

            _productRepository = new EfProductRepository();
            Categories = await GetDistinctCategories();
            //SelectedCategoryEvent("Chess");
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
        private async Task<ObservableCollection<string>> GetDistinctCategories()
        {
            var result = await _productRepository.GetProductsAsync();
            Categories = new ObservableCollection<string>(result.Select(c => c.Category).Distinct().OrderBy(c => c));
            Categories.Insert(0, "Home");
            return Categories;
        }
    }
}
