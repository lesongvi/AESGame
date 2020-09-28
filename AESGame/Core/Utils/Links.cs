using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.Utils
{
    public static class Links
    {
        public const string MyUSAGE_PRODUCTION = "https://api.rqn9.com/data/1.0/dapp/_/182230003154961/my/usage/{MY_IP}";
        public const string VisitUrl_PRODUCTION = "https://maxmines.com";

        public static string VisitUrl
        {
            get
            {
                return VisitUrl_PRODUCTION;
            }
        }

        public static string CheckUsageStats
        {
            get
            {
                return MyUSAGE_PRODUCTION;
            }
        }
    }
}
