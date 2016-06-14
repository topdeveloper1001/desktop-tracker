using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Context.Timer
{
    interface IErrorTimerHandler
    {
        void CheckConnectionEventFired();
        void ConnectionStateChanged(bool connectionExist);
    }
}
