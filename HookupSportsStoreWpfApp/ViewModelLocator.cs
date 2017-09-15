using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HookupSportsStoreWpfApp
{
    public static class ViewModelLocator
    {
        public static bool GetAutoWireViewModel(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoWireViewModelProperty, value);
        }

        // Using a DependencyProperty as the backing store for AutoWireViewModel.  This enables animation, styling, binding, etc...

        //public static readonly DependencyProperty AutoWireViewModelProperty =
        //    DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false));

        //Making a behaviour out of this DependencyProperty
        public static readonly DependencyProperty AutoWireViewModelProperty =
            DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, AutoWireViewModelChanged));

        private static void AutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(d)) return;

            var viewType = d.GetType();
            var viewTypeName = viewType.FullName;

            #region With ViewModel Folder
            //var viewTypeName = viewType.FullName;
            //var strList = viewTypeName.Split(new char[] { '.' });
            //var viewModelTypeName = string.Format($"{strList[0]}.ViewModels.{strList[2]}Model");

            #endregion
            var viewModelTypeName = viewTypeName + "Model";
            var viewModelType = Type.GetType(viewModelTypeName);

            var viewModel = Activator.CreateInstance(viewModelType);
            ((FrameworkElement)d).DataContext = viewModel;
        }
    }
}
