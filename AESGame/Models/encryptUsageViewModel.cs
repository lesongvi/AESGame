using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Models
{
    public class encryptUsageViewModel
    {
        public List<AESUsage> dataUsage { get; private set; }

        public encryptUsageViewModel(AESUsage encryptUsage)
        {
            dataUsage = new List<AESUsage>();
            dataUsage.Add(encryptUsage);
        }
    }
}
