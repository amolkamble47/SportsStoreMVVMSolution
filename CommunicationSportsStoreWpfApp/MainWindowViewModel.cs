using CommunicationSportsStoreWpfApp.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;

namespace CommunicationSportsStoreWpfApp
{
    public class MainWindowViewModel: INotifyPropertyChanged
    {
        private Timer _timer = new Timer(5000);
        private string _dispMessage;
        public MainWindowViewModel()
        {
            
            CurrentViewModel = new ProductAttachedBehaviorListViewModel();
            DispMessage = "Welcome";
            //_timer.Elapsed += (s, e) => DispMessage = string.Format($"Tick Tock {DateTime.Now.ToLocalTime()}");
            //_timer.Start();
            //DispMessage = ((ProductAttachedBehaviorListViewModel)CurrentViewModel).DisMessage;
            //CurrentViewModel = new ProductCommandListViewModel();
        }
        public object CurrentViewModel { get; set; }
        public string DispMessage { get => _dispMessage;
            set
            {
                if (value != _dispMessage)
                {
                    _dispMessage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DispMessage")); 
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
