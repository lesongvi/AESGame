using AESGame.Core.ApplicationStateManager;
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

namespace AESGame.Views.Common
{
    /// <summary>
    /// Interaction logic for UsageStats.xaml
    /// </summary>
    public partial class UsageStats : UserControl
    {
        public UsageStats()
        {
            InitializeComponent();
        }

        private void Click_VisitStatsOnline(object sender, RoutedEventArgs e)
        {
            ApplicationStateManager.VisitMiningStatsPage();
        }
    }
}
