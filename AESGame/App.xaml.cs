using AESGame.Common;
using AESGame.Core.ApplicationStateManager;
using AESGame.Core.Configs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace AESGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
            ApplicationStateManager.App = this;
            ApplicationStateManager.ApplicationExit = () =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.Shutdown();
                });
            };
            var pathSet = false;
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (path != null)
            {
                var oneUpPath = new Uri(Path.Combine(path, @"..\")).LocalPath;
                Paths.SetRoot(oneUpPath);
                Paths.SetAppRoot(path);
                Environment.CurrentDirectory = oneUpPath;
                pathSet = true;
            }
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls |
                                                   SecurityProtocolType.Tls11 |
                                                   SecurityProtocolType.Tls12 |
                                                   SecurityProtocolType.Ssl3;
            ConfigManager.InitializeConfig();
            ThemeSetterManager.SetTheme("Light");

            var canRun = ApplicationStateManager.SystemRequirementsEnsured();
            if (!canRun)
            {
                Shutdown();
                return;
            }

            var main = new MainWindow();
            main.Show();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Exception ex_inner = ex.InnerException;
            string msg = ex.Message + "\n\n" + ex.StackTrace + "\n\n" +
                "Inner Exception:\n" + ex_inner.Message + "\n\n" + ex_inner.StackTrace;
            MessageBox.Show(msg, "Ứng dụng bị tạm dừng!", MessageBoxButton.OK);
            e.Handled = true;
            Application.Current.Shutdown();
        }
    }
}
