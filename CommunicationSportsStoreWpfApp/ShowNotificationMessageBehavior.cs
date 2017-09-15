using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CommunicationSportsStoreWpfApp
{
    public class ShowNotificationMessageBehavior: Behavior<ContentControl>
    {
        public string DisplayMessage
        {
            get { return (string)GetValue(DisplayMessageProperty); }
            set { SetValue(DisplayMessageProperty, value); }
        }
        // Using a DependencyProperty as the backing store for DisplayMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMessageProperty =
            DependencyProperty.Register("DisplayMessage", typeof(string), typeof(ShowNotificationMessageBehavior), new PropertyMetadata(null, OnMessageChanged));

        private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = ((ShowNotificationMessageBehavior)d);
            behavior.AssociatedObject.Content =  e.NewValue;// this behavior.AssociatedObject.Content is coming as null
            behavior.AssociatedObject.Visibility = Visibility.Visible;
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseLeftButtonDown += (s, e) => AssociatedObject.Visibility = Visibility.Collapsed;
            //base.OnAttached();
        }
    }
}
