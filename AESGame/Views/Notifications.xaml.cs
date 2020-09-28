using AESGame.Common;
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
    /// Interaction logic for Notifications.xaml
    /// </summary>
    public partial class Notifications : UserControl
    {
        public Notifications()
        {
            InitializeComponent();
        }

        private void UserControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                foreach (var item in ic_NotificationsList.ItemsSource)
                {
                    if (item is Core.Notifications.Notification notification)
                    {
                        notification.NotificationNew = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Notifications", ex.Message);
            }
        }
    }
}
