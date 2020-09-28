using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AESGame.Common;
using AESGame.Core.ApplicationState;
using AESGame.Core.ApplicationStateManager;
using AESGame.Models;

namespace AESGame.ViewModels
{
    public class MainVM : BaseVM
    {
        VConfig config = new VConfig();

        public MainVM()
            : base("AESGAME")
        {
            DataUsage.safeReloadPlugins();
        }

        public async Task InitializeAESEngine(IStartupLoader sl)
        {
            DataUsage.safeReloadPlugins();
            SessionDetail.setSession();
        }


        public DevelopmentDetail DevelopmentDetail => DevelopmentDetail.Instance;
        public DataUsage DataUsage => DataUsage.Instance;
        public SessionDetail SessionDetail => SessionDetail.Instance;
        public string limitAESString => "Giới hạn " + config.limitAESString + " lần";
        public string limitAESFile => "Giới hạn " + config.limitAESFile + " lần";
        public string LocalVersion => VersionState.Instance.ProgramVersion.ToString();
        public string OnlineVersion => VersionState.Instance.OnlineVersion?.ToString() ?? "N/A";
    }
}
