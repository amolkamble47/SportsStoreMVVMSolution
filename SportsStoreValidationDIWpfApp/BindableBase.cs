using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SportsStoreValidationDIWpfApp
{
    public class BindableBase : INotifyPropertyChanged
    {
        //CSharp feature CallerMemberName which will get to know the name of the propery called in
        protected virtual void SetProperty<T>(ref T member, T val, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
