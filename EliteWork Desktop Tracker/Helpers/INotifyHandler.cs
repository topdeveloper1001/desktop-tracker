using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Helpers
{
    interface INotifyHandler
    {
        void TimerTick(bool show);
    }
}
