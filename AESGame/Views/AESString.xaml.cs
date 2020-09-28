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
using AESGame.Views.Common;
using Newtonsoft.Json;

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for AESString.xaml
    /// </summary>
    public partial class AESString : UserControl
    {
        //vi, 111111111111111111111111, hZTbU6PNwgTcdwSqFaysgg== - for Debug

        AESStringEngine aesStringInstance;
        DataUsageCheck usageCheck;
        UsageDetail usage;
        VConfig config;
        public AESString()
        {
            InitializeComponent();
            usageCheck = new DataUsageCheck();
            config = new VConfig();

            usage = usageCheck.initData();

            DataContextChanged += AESString_DataContextChanged;
        }

        private void AESString_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            /*if (e.NewValue is MainVM mainVM)
            {
                Console.WriteLine(e.NewValue);
                _vm = mainVM;
                return;
            }
            throw new Exception("AESString_DataContextChanged " + e.NewValue  + " must be of type MainVM");*/
        }

        private void AESStringEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            var text = AESEncryptText.Text;
            if (Salt.Text.Length != 16 && Salt.Text.Length != 24 && Salt.Text.Length != 32)
            {
                var errorMessageShow = new CustomDialog()
                {
                    Title = "Lỗi!",
                    Description = "Salt phải là 16 bit, 24 bit hoặc 32 bit!",
                    OkText = "Được",
                    AnimationVisible = Visibility.Collapsed
                };
                CustomDialogManager.ShowModalDialog(errorMessageShow);
                return;
            }
            aesStringInstance = new AESStringEngine(Salt.Text, config.IVKey);
            try
            {
                if(usage.string_usage < config.limitAESString)
                {
                    AESResults.Text = aesStringInstance.Encrypt(text);
                    usageCheck.AESStringDone();
                } else
                {
                    var errorMessageShow = new CustomDialog()
                    {
                        Title = "Lỗi!",
                        Description = "Bạn đã đạt giới hạn!",
                        OkText = "Được",
                        AnimationVisible = Visibility.Collapsed
                    };
                    CustomDialogManager.ShowModalDialog(errorMessageShow);
                }
            } catch (Exception err)
            {
                AESResults.Text = "";
                var errorMessageShow = new CustomDialog()
                {
                    Title = "Lỗi!",
                    Description = "Chi tiết lỗi: " + err,
                    OkText = "Được",
                    AnimationVisible = Visibility.Collapsed
                };
                CustomDialogManager.ShowModalDialog(errorMessageShow);
            }
        }

        private void AESStringDecrypt_OnClick(object sender, RoutedEventArgs e)
        {
            var text = AESEncryptText.Text;
            if (Salt.Text.Length != 16 && Salt.Text.Length != 24 && Salt.Text.Length != 32)
            {
                var errorMessageShow = new CustomDialog()
                {
                    Title = "Lỗi!",
                    Description = "Salt phải là 16 bit, 24 bit hoặc 32 bit!",
                    OkText = "Được",
                    AnimationVisible = Visibility.Collapsed
                };
                CustomDialogManager.ShowModalDialog(errorMessageShow);
                return;
            }
            aesStringInstance = new AESStringEngine(Salt.Text, config.IVKey);
            try
            {
                usageCheck.AESDeStringDone();
                AESResults.Text = aesStringInstance.Decrypt(text);
            } catch(Exception err)
            {
                AESResults.Text = "";
                var errorMessageShow = new CustomDialog()
                {
                    Title = "Lỗi!",
                    Description = "Chi tiết lỗi: " + err,
                    OkText = "Được",
                    AnimationVisible = Visibility.Collapsed
                };
                CustomDialogManager.ShowModalDialog(errorMessageShow);
            }
        }
    }
}
