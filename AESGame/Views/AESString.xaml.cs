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
using Newtonsoft.Json;

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for AESString.xaml
    /// </summary>
    public partial class AESString : UserControl
    {
        AESStringEngine aesStringInstance;
        DataUsageCheck usageCheck;
        UsageDetail usage;
        VConfig config;
        public AESString()
        {
            InitializeComponent();
            usageCheck = new DataUsageCheck();
            config = new VConfig();

            Loaded += AESString_Loaded;
            //this.reloadData();
        }

        void AESString_Loaded(object sender, RoutedEventArgs e)
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

            TextDownNotice.Text = "Bạn đã mã hóa " + usage.string_usage + " chuỗi trong tổng số " + usage.total_usage + " lần";
            percent.Text = ((double) (usage.string_usage / config.limitAESString) * 100) + "%";
        }

        //vi, 111111111111111111111111, hZTbU6PNwgTcdwSqFaysgg==

        private void AESStringEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            var text = AESEncryptText.Text;
            if (Salt.Text.Length != 16 && Salt.Text.Length != 24 && Salt.Text.Length != 32)
            {
                MessageBox.Show("Salt phải là 16 bit, 24 bit hoặc 32 bit!");
                return;
            }
            aesStringInstance = new AESStringEngine(Salt.Text, config.IVKey);
            try
            {
                if(usage.string_usage < config.limitAESString)
                {
                    AESResults.Text = aesStringInstance.Encrypt(text);
                    usageCheck.AESStringDone();
                    this.reloadData();
                } else
                {
                    MessageBox.Show("Bạn đã đạt giới hạn!");
                }
                //usageCheck = 100;
                //usageCheck.SaveUserSettings();
            } catch (Exception err)
            {
                AESResults.Text = "";
                MessageBox.Show("Lỗi! Chi tiết lỗi: " + err);
            }
        }

        private void AESStringDecrypt_OnClick(object sender, RoutedEventArgs e)
        {
            var text = AESEncryptText.Text;
            if (Salt.Text.Length != 16 && Salt.Text.Length != 24 && Salt.Text.Length != 32)
            {
                MessageBox.Show("Salt phải là 16 bit, 24 bit hoặc 32 bit!");
                return;
            }
            aesStringInstance = new AESStringEngine(Salt.Text, config.IVKey);
            try
            {
                usageCheck.AESDeStringDone();
                this.reloadData();
                AESResults.Text = aesStringInstance.Decrypt(text);
            } catch(Exception err)
            {
                AESResults.Text = "";
                MessageBox.Show("Lỗi! Chi tiết lỗi: " + err);
            }
        }
    }
}
