using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AESGame.Models;
using AESGame.ViewModels;
using AESGame.Views.Base;
using AESGame.Views.Common;

namespace AESGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MainWin, INotifyPropertyChanged
    {
        private MainWindowViewModel pageSw;
        private readonly MainVM _vm;
        public MainWindow()
        {
            InitializeComponent();

            _vm = this.AssertViewModel<MainVM>();
            Title = "AESGameCore";
            CustomDialogManager.MainWindow = this;
            LoadingBar.Visibility = Visibility.Visible;

            pageSw = new MainWindowViewModel();
        }

        private void GridNav_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        protected override void OnTabSelected(ToggleButtonType tabType)
        {
            var tabName = tabType.ToString();
            foreach (TabItem tab in MainTabs.Items)
            {
                if (tabName.Contains(tab.Name))
                {
                    MainTabs.SelectedItem = tab;
                    break;
                }
            }
        }

        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            ThemeSetterManager.SetThemeSelectedThemes();
            await MainWindow_OnLoadedTask();
        }

        private void MainWindow_OnStateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
                Hide();
        }

        private async Task MainWindow_OnLoadedTask()
        {
            try
            {
                await _vm.InitializeAESEngine(LoadingBar.StartupLoader);
            }
            finally
            {
                LoadingBar.Visibility = Visibility.Collapsed;
                IsEnabled = true;
                SetTabButtonsEnabled();
            }
        }

        private void AESStringEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void TaskbarIcon_OnTrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void CloseMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseButton_onClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AESStringTB_Click(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }

        private void AESFileTB_Click(object sender, RoutedEventArgs e)
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();
        }
    }
}
