using AESGame.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AESGame.Core.ApplicationStateManager
{
    static partial class ApplicationStateManager
    {
        public static DispatcherObject App { get; set; }
        public static string pcIp = getMyIp();

        public static string getMyIp()
        {
            try
            {
                return new System.Net.WebClient().DownloadString("https://api.ipify.org");
            } catch(Exception e)
            {
                return "0.0.0.0";
            }
        } 
        public enum SetResult
        {
            INVALID = 0,
            NOTHING_TO_CHANGE,
            CHANGED
        }

        public static void VisitMiningStatsPage()
        {
            var urlLink = Links.CheckUsageStats.Replace("{MY_IP}", pcIp);
            Helpers.VisitUrlLink(urlLink);
        }
    }
}
