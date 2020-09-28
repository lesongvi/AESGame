using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using AESGame.Views.Base;

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        private MainVM _vm;

        public HomePage()
        {
            InitializeComponent();

            DataContextChanged += HomePage_DataContextChanged;
        }

        private void HomePage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*if (e.NewValue is MainVM mainVM)
            {
                Console.WriteLine(e.NewValue);
                _vm = mainVM;
                return;
            }
            throw new Exception("HomePage_DataContextChanged " + e.NewValue  + " must be of type MainVM");*/
        }
    }
}
