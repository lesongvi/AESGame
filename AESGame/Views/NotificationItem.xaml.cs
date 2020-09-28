using AESGame.Core.Notifications;
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

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for NotificationItem.xaml
    /// </summary>
    public partial class NotificationItem : UserControl
    {

        private Notification _notification;
        public NotificationItem()
        {
            InitializeComponent();
            DataContextChanged += NotificationItemItem_DataContextChanged;
        }

        private void NotificationItemItem_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Notification notification)
            {
                _notification = notification;
                var baseAction = _notification.Actions.FirstOrDefault();
                if (baseAction is NotificationAction action)
                {
                    ActionButton.Content = action.Info;
                    ActionButton.Click += (s, be) => action.Action?.Invoke();
                    ActionButton.Visibility = Visibility.Visible;
                }
                return;
            }
            throw new Exception("unsupported datacontext type");
        }

        private void RemoveNotification(object sender, RoutedEventArgs e)
        {
            _notification.RemoveNotification();
        }

        private void ExecuteNotificationAction(object sender, RoutedEventArgs e)
        {

        }

        private void InfoToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (InfoToggleButton.IsChecked.Value)
            {
                Expand();
            }
            else
            {
                Collapse();
            }
            InfoToggleButtonText.Text = InfoToggleButtonText.Text;
        }

        private void Collapse()
        {
            notificationsDetailsGrid.Visibility = Visibility.Collapsed;
            InfoToggleButton.IsChecked = false;
            InfoToggleButtonText.Text = "Mở rộng";
        }

        private void Expand()
        {
            notificationsDetailsGrid.Visibility = Visibility.Visible;
            InfoToggleButton.IsChecked = true;
            InfoToggleButtonText.Text = "Thu hẹp";
        }
    }
}
