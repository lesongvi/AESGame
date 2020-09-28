using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for TeamInfo.xaml
    /// </summary>
    public partial class TeamInfo : UserControl
    {
        public TeamInfo()
        {
            InitializeComponent();
        }

        private void btn_social_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var senderBtn = sender as Button;
                switch (senderBtn.Name)
                {
                    case "btn_facebook":
                        Process.Start("https://www.facebook.com/lesongvi/");
                        e.Handled = true;
                        break;
                    case "btn_instagram":
                        Process.Start("https://www.instagram.com/lesongvi/");
                        e.Handled = true;
                        break;
                    case "btn_twitter":
                        Process.Start("https://twitter.com/lesongvi/");
                        e.Handled = true;
                        break;
                    case "btn_youtube":
                        Process.Start("https://www.youtube.com/c/UCRaHiKkQVWIZxQKN00ID5iQ");
                        e.Handled = true;
                        break;
                    case "btn_github":
                        Process.Start("https://github.com/lesongvi");
                        e.Handled = true;
                        break;
                    case "btn_reddit":
                        Process.Start("https://www.reddit.com/r/lesongvi/");
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Social", $"Exception occured: {ex.Message}");
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
            e.Handled = true;
        }
    }
}
