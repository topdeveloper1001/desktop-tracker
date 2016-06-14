using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace EliteWork_Desktop_Tracker.Helpers
{
    class NotifyTimer
    {
        private static NotifyTimer _Instance = null;
        private System.Timers.Timer _InternalTimer = null;
        private bool _IsStarted = false;
        private INotifyHandler _Handler = null;
        private const int NOTIFY_PERIOD = 1000;

        private NotifyTimer() { }

        public static NotifyTimer GetInstance()
        {
            if (_Instance == null)
                _Instance = new NotifyTimer();

            return _Instance;
        }
        
        public void InitTimer(INotifyHandler handler)
        {
            _Handler = handler;
            _InternalTimer = new System.Timers.Timer();
            _InternalTimer.Elapsed += new ElapsedEventHandler(OnInternalTimedEvent);
            _InternalTimer.AutoReset = true;
        }

        public void StartTimer()
        {
            if (_IsStarted)
                return;
            
            _InternalTimer.Interval = NOTIFY_PERIOD;
            _InternalTimer.Start();
            _IsStarted = true;

        }

        public void StopTimer()
        {
            if (!_IsStarted)
                return;
            
            _InternalTimer.Stop();
            _IsStarted = false;
        }

        private void OnInternalTimedEvent(object source, ElapsedEventArgs e)
        {
            _Handler.TimerTick(false);
        }
    }
}
