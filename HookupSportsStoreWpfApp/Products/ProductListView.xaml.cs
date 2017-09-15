using System;
using System.Collections.Generic;
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

namespace HookupSportsStoreWpfApp.Products
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl
    {
        public ProductListView()
        {
            InitializeComponent();
            //second
            //Through Code when there are args to be passed to the ctor
            //this.DataContext = new ProductListViewModel();
        }
    }
}
