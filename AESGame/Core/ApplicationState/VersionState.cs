using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AESGame.Core.ApplicationState
{
    public class VersionState : NotifyChangedBase
    {
        public static VersionState Instance { get; } = new VersionState();

        private VersionState()
        {
            ProgramVersion = new Version(Application.ProductVersion);
        }

        public Version ProgramVersion { get; private set; }
        public string OnlineVersionStr { get; private set; } = null;

        private Version _onlineVersion = null;
        public Version OnlineVersion
        {
            get => _onlineVersion;
            internal set
            {
                _onlineVersion = value;
                OnPropertyChanged(nameof(OnlineVersion));
            }
        }
    }
}
