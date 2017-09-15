using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CommunicationSportsStoreWpfApp
{
    public static class MVVMBehavior
    {
        public static string GetLoadedMethodName(DependencyObject obj)
        {
            return (string)obj.GetValue(LoadedMethodNameProperty);
        }

        public static void SetLoadedMethodName(DependencyObject obj, string value)
        {
            obj.SetValue(LoadedMethodNameProperty, value);
        }

        // Using a DependencyProperty as the backing store for LoadedMethodName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LoadedMethodNameProperty =
            DependencyProperty.RegisterAttached("LoadedMethodName", typeof(string), typeof(MVVMBehavior), new PropertyMetadata(null, OnLoadedMethodNameChanged));

        private static void OnLoadedMethodNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = d as FrameworkElement;
            if (element != null)
            {
                element.Loaded += (s, e2) =>
                {
                    var viewModel = element.DataContext;
                    if (viewModel == null) return;
                    var methodName = viewModel.GetType().GetMethod(e.NewValue.ToString());
                    if (methodName != null) methodName.Invoke(viewModel, null);
                };
            }
        }
    }
}
