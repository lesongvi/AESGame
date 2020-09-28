using AESGame.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AESGame.Core.ApplicationState
{
    public class DevelopmentDetail : NotifyChangedBase
    {
        //public static event EventHandler OnVersionUpdate;

        public static DevelopmentDetail Instance { get; } = new DevelopmentDetail();

        DevelopmentDetail() { }

        public void GeneralConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //setDeVersion();
        }

        public void setDeVersion()
        {
            CurrentVersion = "1.0.0";
            OnPropertyChanged(nameof(CurrentVersion));
        }

        public string CurrentVersion { get; private set; } = "1.0.0";
    }
}
