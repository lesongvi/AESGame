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

        private AESStringEngine aesStringInstance;
        private DataUsageCheck usageCheck;
        private UsageDetail usage;
        private VConfig config;
        private static Random random = new Random();
        public AESString()
        {
            InitializeComponent();
            usageCheck = new DataUsageCheck();
            config = new VConfig();

            usage = usageCheck.initData();

            DataContextChanged += AESString_DataContextChanged;
            SaltTitle.MouseLeftButtonDown += new MouseButtonEventHandler(SaltTitle_MouseLeftButtonDown);
            StringInputTitle.MouseLeftButtonDown += new MouseButtonEventHandler(StringInputTitle_MouseLeftButtonDown);
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void SaltTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int[] varMe = { 16, 24, 32 };
            for(int i = 0; i< varMe.Length; i++)
            {
                if(Salt.Text.Length < varMe[i])
                {
                    Salt.Text = RandomString(varMe[i]);
                    break;
                }
            }
        }

        private void StringInputTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            AESEncryptText.Text = RandomString(random.Next(1904));
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
