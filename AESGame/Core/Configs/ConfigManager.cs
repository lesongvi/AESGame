using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using AESGame.Core.ApplicationState;
using AESGame.Core.ApplicationStateManager;
using AESGame.Core.Configs.Data;

namespace AESGame.Core.Configs
{
    public static class ConfigManager
    {
        private static GeneralConfig GeneralConfig { get; set; } = new GeneralConfig();
        public static void InitializeConfig()
        {
            GeneralConfig.PropertyChanged += DevelopmentDetail.Instance.GeneralConfig_PropertyChanged;
            GeneralConfig.PropertyChanged += DataUsage.Instance.GeneralConfig_PropertyChanged;
            GeneralConfig.PropertyChanged += SessionDetail.Instance.GeneralConfig_PropertyChanged;

            GeneralConfigFileCommit();
        }

        public static void GeneralConfigFileCommit()
        {
            ApplicationStateManager.ApplicationStateManager.App.Dispatcher.Invoke(() =>
            {
                //Silent is golden
            });
        }
    }
}
