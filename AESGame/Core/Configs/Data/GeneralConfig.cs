using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.Configs.Data
{
    [Serializable]
    class GeneralConfig : NotifyChangedBase
    {
        public Version ConfigFileVersion;
    }
}
