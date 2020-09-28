using AESGame.Common;
using AESGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.ApplicationState
{
    public class SessionDetail : NotifyChangedBase
    {
        DataUsageCheck usage = new DataUsageCheck();
        public static SessionDetail Instance { get; } = new SessionDetail();

        public void GeneralConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //setSession();
        }

        public void setSession()
        {
            var r = usage.hashMe();
            CurrentSession = r.hash;
            OnPropertyChanged(nameof(CurrentSession));
        }

        public string CurrentSession { get; private set; }
    }
}
