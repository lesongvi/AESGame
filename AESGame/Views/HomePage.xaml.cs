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

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : UserControl
    {
        private AESUsage encryptUsage;
        DataUsageCheck usageCheck;
        UsageDetail usage;
        VConfig config;
        public HomePage()
        {
            InitializeComponent();

            usageCheck = new DataUsageCheck();
            config = new VConfig();
            limitAESString.Text = "Giới hạn " + config.limitAESString + " lần";
            limitAESFile.Text = "Giới hạn " + config.limitAESFile + " lần";

            Loaded += HomePage_Loaded;
        }

        void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                //Longer Process (//set the operation in another thread so that the UI thread is kept responding)
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.reloadData();
                }));
            });
        }

        private void reloadData()
        {
            usage = usageCheck.initData();

            AESStringUsage.Text = usage.string_usage.ToString();
            AESFileUsage.Text = usage.file_usage.ToString();
            totalUseDesc.Text = "Bạn đã mã hóa " + usage.string_usage + " chuỗi và " + usage.file_usage + " file";
            encryptUsage = new AESUsage(usage);
            DataContext = new encryptUsageViewModel(encryptUsage);
        }

        internal class encryptUsageViewModel
        {
            public List<AESUsage> dataUsage { get; private set; }

            public encryptUsageViewModel(AESUsage encryptUsage)
            {
                dataUsage = new List<AESUsage>();
                dataUsage.Add(encryptUsage);
            }
        }

        internal class AESUsage
        {
            public string title { get; private set; }
            public double percent { get; private set; }
            public AESUsage(UsageDetail u)
            {
                title = "Tần suất sử dụng";
                percent = CalculatePercentUsage(u);
            }

            private double CalculatePercentUsage(UsageDetail u)
            {
                return ((double)u.string_usage / u.total_usage * 100);
            }
        }
    }
}
