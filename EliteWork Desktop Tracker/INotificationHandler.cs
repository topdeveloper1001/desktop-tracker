using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker
{
    interface INotificationHandler
    {
        void ScreenshotTaken();
        void SessionStopped();
        void ConnectionStateChanged(bool connectionExist);
        void ActivityFired();
    }
}
