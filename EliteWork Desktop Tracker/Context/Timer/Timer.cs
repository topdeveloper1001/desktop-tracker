using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace EliteWork_Desktop_Tracker.Context.Timer
{
    class Timer
    {
        private static Timer _Timer = null;
        private System.Timers.Timer _InternalTimer = null;
        private System.Timers.Timer _RandomTimer = null;
        private Random _Random = null;
        private int _Interval = -1;
        private int _Delay = -1;
        private bool _IsStarted = false;
        private ITimerHandler _Handler = null;
        private int _InitialInterval = -1;
        private bool _FirstTime = true;

        private Timer() { }

        public static Timer GetInstance()
        {
            if (_Timer == null)
                _Timer = new Timer();

            return _Timer;
        }

        public void InitTimer(int interval, int delay, ITimerHandler handler)
        {
            if (_IsStarted)
                return;

            _FirstTime = true;
            _Handler = handler;
            _Interval = interval;
            _Delay = delay;
            _InternalTimer = new System.Timers.Timer();
            _InternalTimer.Elapsed += new ElapsedEventHandler(OnInternalTimedEvent);

            _Random = new Random();
            _RandomTimer = new System.Timers.Timer();
            _RandomTimer.Elapsed += new ElapsedEventHandler(OnRandomTimedEvent);
            _RandomTimer.AutoReset = false;
        }

        public void StartTimer()
        {
            if (_IsStarted)
                return;

            _InitialInterval = CalcInitialInterval();
            _InternalTimer.Interval = _InitialInterval; /*interval*/;
            _InternalTimer.Start();

            _RandomTimer.Interval = _Random.Next(_InitialInterval / 4, (_InitialInterval - (_InitialInterval / 4)));
            _RandomTimer.Start();
            _IsStarted = true;
        }

        public void StopTimer()
        {
            if (!_IsStarted)
                return;
            
            _InternalTimer.Stop();
            _RandomTimer.Stop();
            _IsStarted = false;
        }

        private void OnInternalTimedEvent(object source, ElapsedEventArgs e)
        {
            if (_FirstTime)
            {
                _FirstTime = false;
                _InternalTimer.Stop();
                _InternalTimer.Interval = _Interval;
                _InternalTimer.Start();
            }

            _RandomTimer.Stop();
            _RandomTimer.AutoReset = false;
            _RandomTimer.Interval = _Random.Next(_Delay, (_Interval - _Delay));
            _RandomTimer.Start();
        }

        private void OnRandomTimedEvent(object source, ElapsedEventArgs e)
        {
            _Handler.NeedForScreenshotEventFired();
        }

        private int CalcInitialInterval()
        {
            int secs = DateTime.Now.Second;
            int mins = DateTime.Now.Minute;
            int interval = mins;
            int addon = 0;
            while (interval % 5 != 0)
            {
                interval++;
                addon++;
            }
            if (addon == 0)
                addon = 5;
            return ((addon - 1) * 60 + (60 - secs)) * 1000;
        }
    }
}
