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
        AESStringEngine aesStringInstance;
        DataUsageCheck usageCheck;
        UsageDetail usage;
        VConfig config;
        public string txtFile;
        public AESFile()
        {
            InitializeComponent();
            usageCheck = new DataUsageCheck();
            config = new VConfig();

            Loaded += AESFile_Loaded;
        }

        void AESFile_Loaded(object sender, RoutedEventArgs e)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.reloadData();
                }));
            });

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

        //E:\BMTT\Debug, 111111111111111111111111

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
                this.reloadData();
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
                    usageCheck.AESDeStringDone();
                    this.reloadData();
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

        private void reloadData()
        {
            usage = usageCheck.initData();

            TextDownNotice.Text = "Bạn đã mã hóa " + usage.file_usage + " file trong tổng số " + usage.total_usage + " lần";
            percent.Text = Math.Round(((double) usage.file_usage / usage.total_usage * 100), 2) + "%";
        }
    }
}
