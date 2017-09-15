using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace SportsStoreValidationDIWpfApp.Products
{
    public class AddEditProductViewModel: BindableBase
    {
        private IProductRepository _productRepository;
        private SimpleEditableProduct _product;
        private Product _editableProduct;
        private bool _editFlag;

        public AddEditProductViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CancelCommand = new RelayCommand(OnCancel);
            SaveCommand = new RelayCommand(OnSave, CanSave);
        }
        public void LoadProduct()
        {
            //Product = SetProduct();
        }
        public void SetProduct(Product product)
        {
            _editableProduct = product;
            if (Product != null) Product.ErrorsChanged -= RaiseCanExecuteChanged;
            Product = new SimpleEditableProduct();
            Product.ErrorsChanged += RaiseCanExecuteChanged;
            CopyProduct(product, Product);


            //_editableProduct = product;
            //Product = new SimpleEditableProduct();
            //CopyProduct(product, Product);
        }
        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void UpdateProduct(SimpleEditableProduct source, Product target)
        {
            target.ProductName = source.ProductName;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Category = source.Category;
        }

        private void CopyProduct(Product source, SimpleEditableProduct target)
        {
            target.ProductId = source.ProductId;
            if (EditFlag)
            {
                target.ProductName = source.ProductName;
                target.Description = source.Description;
                target.Price = source.Price;
                target.Category = source.Category;
            }
        }

        public bool EditFlag { get => _editFlag; set => SetProperty(ref _editFlag, value); }


        public SimpleEditableProduct Product { get => _product; set => SetProperty(ref _product, value); }

        public event Action Done = delegate { };
        public RelayCommand CancelCommand { get; private set; }
        private void OnCancel()
        {
            Done();
        }
        public RelayCommand SaveCommand { get; private set; }
        private async void OnSave()
        {
            UpdateProduct(Product, _editableProduct);
            if (EditFlag)
            {
                await _productRepository.UpdateProductAsync(_editableProduct);
            }
            else
            {
                await _productRepository.AddProductAsync(_editableProduct);
            }
            Done();
        }
        private bool CanSave()
        {
            return !Product.HasErrors;
            //return true;
        }
    }
}
