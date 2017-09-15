using SportsStoreDomainLibrary.Concrete;
using SportsStoreDomainLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicSportsStoreWpfApp.Products
{
    /// <summary>
    /// Interaction logic for ProductEditDirectView.xaml
    /// </summary>
    public partial class ProductEditDirectView : UserControl
    {
        private EfProductRepository _productRepository;
        private Product _product;
        public ProductEditDirectView()
        {
            _productRepository = new EfProductRepository();
            InitializeComponent();
        }

        public int ProductId
        {
            get { return (int)GetValue(ProductIdProperty); }
            set { SetValue(ProductIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProductId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductIdProperty =
            DependencyProperty.Register("ProductId", typeof(int), typeof(ProductEditDirectView), new PropertyMetadata(0));

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            _product = await _productRepository.GetProductAsync(ProductId);
            if(_product != null)
            {
                productNameTextBox.Text = _product.ProductName;
                descriptionTextBox.Text = _product.Description;
                priceTextBox.Text = _product.Price.ToString();
                categoryTextBox.Text = _product.Category;
            }
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            _product.ProductName = productNameTextBox.Text;
            _product.Description = descriptionTextBox.Text;
            _product.Price = Convert.ToDecimal(priceTextBox.Text);
            _product.Category = categoryTextBox.Text;
            var result = await _productRepository.UpdateProductAsync(_product);
            if (result != null)
            {
                MessageBox.Show("Product Updated", "Update Message", MessageBoxButton.OK);
            }
        }
    }
}
