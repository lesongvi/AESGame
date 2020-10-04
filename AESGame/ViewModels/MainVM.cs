using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AESGame.Common;
using AESGame.Core.ApplicationState;
using AESGame.Core.ApplicationStateManager;
using AESGame.Core.Notifications;
using AESGame.Models;

namespace AESGame.ViewModels
{
    public class MainVM : BaseVM
    {
        VConfig config = new VConfig();
        private readonly object _lock = new object();

        public MainVM()
            : base("AESGAME")
        {
            DataUsage.safeReloadPlugins();
            NotificationsManager.Instance.PropertyChanged += RefreshNotifications_PropertyChanged;
        }

        public async Task InitializeAESEngine(IStartupLoader sl)
        {
            DataUsage.safeReloadPlugins();
            SessionDetail.setSession();
        }

        private ObservableCollection<Notification> _helpNotificationList;
        public ObservableCollection<Notification> HelpNotificationList
        {
            get => _helpNotificationList;
            set
            {
                _helpNotificationList = value;
                OnPropertyChanged();
            }
        }

        private void RefreshNotifications_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            lock (_lock)
            {
                HelpNotificationList = new ObservableCollection<Notification>(NotificationsManager.Instance.Notifications);
                OnPropertyChanged(nameof(HelpNotificationList));
            }
        }


        public DevelopmentDetail DevelopmentDetail => DevelopmentDetail.Instance;
        public DataUsage DataUsage => DataUsage.Instance;
        public SessionDetail SessionDetail => SessionDetail.Instance;
        public string limitAESString => "Giới hạn " + config.limitAESString + " lần";
        public string limitAESFile => "Giới hạn " + config.limitAESFile + " lần";
        public string LocalVersion => VersionState.Instance.ProgramVersion.ToString();
        public string OnlineVersion => VersionState.Instance.OnlineVersion?.ToString() ?? "1.0";
    }
}
