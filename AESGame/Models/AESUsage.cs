using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Models
{
    public class AESUsage
    {
        public string title { get; private set; }
        public double percent { get; private set; }
        public AESUsage(UsageDetail u)
        {
            title = "ZZ";
            percent = CalculatePercentUsage(u);
        }

        private double CalculatePercentUsage(UsageDetail u)
        {
            return u.total_usage > .0 ? ((double)u.string_usage / u.total_usage * 100) : 0;
        }
    }
}
