using EliteWork_Desktop_Tracker.Controllers;
using EWWebProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace EliteWork_Desktop_Tracker.Context.Timer
{
    class ErrorTimer
    {
        private static ErrorTimer _ErrorTimer = null;
        private System.Timers.Timer _InternalTimer = null;
        private int _Count = 0;
        private int[] _Times = { 1 * 60 * 1000, 2 * 60 * 1000, 4 * 60 * 1000, 8 * 60 * 1000 };
        private bool _IsStarted = false;
        private IErrorTimerHandler _Handler = null;

        private ErrorTimer() { }

        public static ErrorTimer GetInstance()
        {
            if (_ErrorTimer == null)
                _ErrorTimer = new ErrorTimer();

            return _ErrorTimer;
        }

        public void InitTimer(IErrorTimerHandler handler)
        {
            _Handler = handler;
            _InternalTimer = new System.Timers.Timer();
            _InternalTimer.Elapsed += new ElapsedEventHandler(OnInternalTimedEvent);
            _InternalTimer.AutoReset = false;
        }

        public void StartTimer()
        {
            if (_IsStarted)
                return;

            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("Exp. Timer Started"));

            if (_Count < 0 || _Count >= _Times.Length)
                _Count = 0;

            _InternalTimer.Interval = _Times[_Count];
            _InternalTimer.Start();
            _Handler.ConnectionStateChanged(false);
            _IsStarted = true;

        }

        public void StopTimer()
        {
            if (!_IsStarted)
                return;

            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("Exp. Timer Stopped"));

            _InternalTimer.Stop();
            _Handler.ConnectionStateChanged(true);
            _IsStarted = false;
            _Count = 0;
        }

        private void OnInternalTimedEvent(object source, ElapsedEventArgs e)
        {
            _Count++;

            if (WebProcessor.CheckInternetConnection())
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Exp. Internet Connection OK"));
                _InternalTimer.Stop();
                _Handler.ConnectionStateChanged(true);
                _IsStarted = false;
                _Handler.CheckConnectionEventFired();
            }
            else
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Exp. Internet Connection Fail"));
                _InternalTimer.Stop();
                _InternalTimer.Interval = _Count < _Times.Length ? _Times[_Count] : _Times[_Times.Length - 1];
                _InternalTimer.Start();
            }
        }

        public float GetCurrentInterval()
        {
            return ((float)(_Count < _Times.Length ? _Times[_Count] : _Times[_Times.Length - 1]) / (float)(60 * 1000));
        }

    }
}
