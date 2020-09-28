using System;

namespace AESGame.Core.Notifications
{
    public enum NotificationsGroup
    {
        Misc,
        ConnectionLost,
        NoEnabledDevice,
        [Obsolete]
        FailedVideoController,
        [Obsolete]
        WmiEnabled,
        [Obsolete]
        Net45,
        [Obsolete]
        BitOS64,
        VisUpdate,
        VisWasUpdated,
        LargePages,
    }
}
