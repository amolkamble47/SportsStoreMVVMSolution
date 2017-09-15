using SportsStoreDomainLibrary.Abstract;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStoreHierachyNavWpfApp.Products
{
    public class AddEditProductViewModel: BindableBase
    {
        //private IProductRepository _productRepository;
        private Product _product;
        private bool _editFlag;

        public AddEditProductViewModel()
        {
            CancelCommand = new RelayCommand(OnCancel);
        }
        public void LoadProduct()
        {
            //Product = SetProduct();
        }
        public void SetProduct(Product product)
        {
            Product = product;
        }
        public Product Product { get => _product; set => SetProperty(ref _product,value); }
        public bool EditFlag { get => _editFlag; set => SetProperty(ref _editFlag, value); }

        public RelayCommand CancelCommand { get; private set; }
        public event Action CancelRequested = delegate { };
        private void OnCancel()
        {
            CancelRequested();
            //throw new NotImplementedException();
        }


    }
}
