using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HookupSportsStoreWpfApp.Products
{
    public class CatViewModel
    {
        public string CatDisplay { get; set; }
        public CatViewModel()
        {
            CatDisplay = "This is coming from the CatViewModel for the CatView";
        }
    }
}
