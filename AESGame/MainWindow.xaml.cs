using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AESGame.Models;
using AESGame.ViewModels;

namespace AESGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private MainWindowViewModel pageSw;
        public MainWindow()
        {
            InitializeComponent();

            pageSw = new MainWindowViewModel();
            DataContext = pageSw;

        }

        private void GridNav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AESStringEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AESString_OnClick(object sender, RoutedEventArgs e)
        {
            pageSw.SwitchView = 1;
            DataContext = pageSw;
        }

        private void HomePage_OnClick(object sender, RoutedEventArgs e)
        {
            pageSw.SwitchView = 0;
        }

        private void AESFile_OnClick(object sender, RoutedEventArgs e)
        {
            pageSw.SwitchView = 2;
        }

        private void InfoTeam_OnClick(object sender, RoutedEventArgs e)
        {
            pageSw.SwitchView = 3;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
