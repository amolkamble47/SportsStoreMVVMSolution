using HookupSportsStoreWpfApp.ViewModelFirstProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookupSportsStoreWpfApp
{
    public class MainWindowViewModel
    {
        public object CurrentViewModel { get; set; }
        public MainWindowViewModel()
        {
            CurrentViewModel = new ProductsListViewModel(); 
        }
        //Can use ViewModelLocator to link up the View and ViewModel
    }
}
