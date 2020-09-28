using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AESGame.Core.Notifications
{
    public class NotificationAction : INotificationBaseAction
    {
        public string Info { get; internal set; }
        public Action Action { get; internal set; }
    }
}
