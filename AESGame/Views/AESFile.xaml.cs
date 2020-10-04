using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace AESGame.Views
{
    /// <summary>
    /// Interaction logic for AESFile.xaml
    /// </summary>
    public partial class AESFile : UserControl
    {
        //E:\BMTT\Debug, 111111111111111111111111 - For debug

        private AESStringEngine aesStringInstance;
        private DataUsageCheck usageCheck;
        private UsageDetail usage;
        private VConfig config;
        public string txtFile;
        private static Random random = new Random();
        public AESFile()
        {
            InitializeComponent();
            usageCheck = new DataUsageCheck();
            config = new VConfig();

            usage = usageCheck.initData();

            DataContextChanged += AESFile_DataContextChanged;
            SaltTitle.MouseLeftButtonDown += new MouseButtonEventHandler(SaltTitle_MouseLeftButtonDown);
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
            for (int i = 0; i < varMe.Length; i++)
            {
                if (Salt.Text.Length < varMe[i])
                {
                    Salt.Text = RandomString(varMe[i]);
                    break;
                }
            }
        }

        private void AESFile_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //Silent is golden
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Chọn file cần mã hóa/giải mã";
            fdlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == true)
            {
                AESFilePath.Text = fdlg.FileName;
                txtFile = File.ReadAllText(fdlg.FileName);
            }
        }

        private void AESStringEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            var text = txtFile;
            Console.WriteLine(text);
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
                AESResults.Text = aesStringInstance.Encrypt(text);
                usageCheck.AESFileDone();
            }
            catch (Exception err)
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
            var text = txtFile;
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
                if (usage.file_usage < config.limitAESFile)
                {
                    usageCheck.AESDeFileDone();
                    AESResults.Text = aesStringInstance.Decrypt(text);
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
            }
            catch (Exception err)
            {
                AESResults.Text = "";
                var errorMessageShow = new CustomDialog()
                {
                    Title = "Lỗi!",
                    Description = "Lỗi! Chi tiết lỗi: " + err,
                    OkText = "Được",
                    AnimationVisible = Visibility.Collapsed
                };
                CustomDialogManager.ShowModalDialog(errorMessageShow);
            }
        }
    }
}
