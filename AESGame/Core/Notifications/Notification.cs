﻿using AESGame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.Notifications
{
    public class Notification : NotifyChangedBase
    {
        public NotificationsType Type { get; } = NotificationsType.Info;
        public NotificationsGroup Group { get; } = NotificationsGroup.Misc;

        public Notification(string name, string content)
        {
            Name = name;
            NotificationContent = content;
        }

        public Notification(NotificationsType type, string name, string content)
        {
            Type = type;
            Name = name;
            NotificationContent = content;
        }

        public Notification(NotificationsType type, NotificationsGroup group, string name, string content)
        {
            Type = type;
            Group = group;
            Name = name;
            NotificationContent = content;
        }

        public List<INotificationBaseAction> Actions { get; } = new List<INotificationBaseAction>();

        private string _notificationName { get; set; }
        public string Name
        {
            get => _notificationName;
            set
            {
                _notificationName = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _notificationUUID { get; set; } = "";
        public string NotificationUUID
        {
            get => _notificationUUID;
            set
            {
                _notificationUUID = value;
                OnPropertyChanged(nameof(NotificationUUID));
            }
        }

        private bool _notificationNew { get; set; }
        public bool NotificationNew
        {
            get => _notificationNew;
            set
            {
                _notificationNew = value;
                OnPropertyChanged(nameof(NotificationNew));
            }
        }

        private string _notificationContent { get; set; }
        public string NotificationContent
        {
            get => _notificationContent;
            set
            {
                _notificationContent = value;
                OnPropertyChanged(nameof(NotificationContent));
            }
        }

        public void RemoveNotification()
        {
            NotificationsManager.Instance.RemoveNotificationFromList(this);
        }
    }
}
