using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Context.Timer;
using EliteWork_Desktop_Tracker.Factories;
using EWActivityCatcher;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Common;
using EWWebProcessor;
using EWLocalCache;
using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EliteWork_Desktop_Tracker.Controllers.CacheStrategy;
using System.Threading;
using System.Diagnostics;

namespace EliteWork_Desktop_Tracker.Controllers
{
    class SessionController : ITimerHandler, IActivityHandler, IErrorTimerHandler
    {
        private static SessionController _Controller = null;
        private IServerApiProvider _ServerApiProvider = null;
        public IServerApiProvider ServerApiProvider { get { return _ServerApiProvider; } }
        private int _LastActivityTime = 0;
        private int _LastScreenshotTime = 0;
        private INotificationHandler _NotificationHandler;

        public static SessionController GetInstance()
        {
            if (_Controller == null)
                _Controller = new SessionController();

            return _Controller;
        }
        
        private SessionController()
        {
            _ServerApiProvider = ServerApiFactory.CreateServerApiProvider();
            ErrorTimer.GetInstance().InitTimer(this);
        }

        public void StartSession(bool subscribe)
        {
            CacheStrategyExecutor.GetInstance().CacheStrategy.SetServerApiProvider(_ServerApiProvider);
            CurrentContext.GetInstance().Session = SessionFactory.CreateSession();
            _LastActivityTime = TimingHelper.GetCurrentTimestamp();
            _LastScreenshotTime = _LastActivityTime;

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StartSession() -- mode 1: IsSessionDataLocked: " + 
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

                //while (CurrentContext.GetInstance().IsSessionDataLocked)
                //    Thread.Sleep(1000);

                CurrentContext.GetInstance().Session.StopTime = -1;

                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StartSession() -- mode 2: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

                //CurrentContext.GetInstance().IsSessionDataLocked = true;
                CacheStrategyExecutor.GetInstance().CacheStrategy.PostCurrentSession(CurrentContext
                    .GetInstance().Session.StartTimestamp.ToString());

                CacheStrategyExecutor.GetInstance().CacheStrategy.PostOldSessions(CurrentContext
                    .GetInstance().Session.StartTimestamp.ToString());
                //CurrentContext.GetInstance().IsSessionDataLocked = false;

                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StartSession() -- mode 3: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/
            }));

            ActivityProcessor.GetInstance().SetActivityHandler(this);
            if (subscribe)
                ActivityProcessor.GetInstance().SubscribeToMouseKeyEvents();
            Context.Timer.Timer.GetInstance().InitTimer(CommonConst.SESSION_INTERVAL, CommonConst.DELAY, this);
            Context.Timer.Timer.GetInstance().StartTimer();
        }

        public void OpenLogsUrlInBrowser()
        {
            Process.Start(_ServerApiProvider.CreateServerLogsUrl());
        }

        public void SetNotificationHandler(INotificationHandler handler)
        {
            _NotificationHandler = handler;
        }

        public void StopSession(bool unsubscribe)
        {
            Context.Timer.Timer.GetInstance().StopTimer();
            ErrorTimer.GetInstance().StopTimer();
            if (unsubscribe)
                ActivityProcessor.GetInstance().UnsubscribeFromMouseKeyEvents();

            int delta = TimingHelper.CalcTimestampDelta(CurrentContext
                .GetInstance().Session.StartTimestamp, TimingHelper.GetCurrentTimestamp());

            //if (delta < CommonConst.DELAY)
            //    return;

            CurrentContext.GetInstance().Session.PostTime = delta;
            CurrentContext.GetInstance().Session.StopTime = delta;

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StopSession() -- mode 1: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

                //while (CurrentContext.GetInstance().IsSessionDataLocked)
                //    Thread.Sleep(1000);

                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StopSession() -- mode 2: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

                //CurrentContext.GetInstance().IsSessionDataLocked = true;
                CacheStrategyExecutor.GetInstance().CacheStrategy.PostCurrentSession(CurrentContext
                .GetInstance().Session.StartTimestamp.ToString());

                CacheStrategyExecutor.GetInstance().CacheStrategy.PostOldSessions(CurrentContext
                    .GetInstance().Session.StartTimestamp.ToString());
               // CurrentContext.GetInstance().IsSessionDataLocked = false;

                /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("StopSession() -- mode 3: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

                _NotificationHandler.SessionStopped();
            }));
        }

        public string GetErrorTimerInterval()
        {
            return ErrorTimer.GetInstance().GetCurrentInterval().ToString();
        }

        public Point GetCurrentSessionTime()
        {
            int current = TimingHelper.GetCurrentTimestamp();
            TimeSpan timeSpan = 
                TimeSpan.FromSeconds(current - CurrentContext.GetInstance().Session.StartTimestamp);
            return new Point(timeSpan.Minutes, timeSpan.Hours);
        }

        public Point GetCurrentTime()
        {
            return new Point(DateTime.Now.TimeOfDay.Minutes, DateTime.Now.TimeOfDay.Hours);
        }

        public int SinceLastActivity()
        {
            int current = TimingHelper.GetCurrentTimestamp();
            return TimeSpan.FromSeconds(current - _LastActivityTime).Minutes;
        }

        public int SinceLastScreenshot()
        {
            int current = TimingHelper.GetCurrentTimestamp();
            return TimeSpan.FromSeconds(current - _LastScreenshotTime).Minutes;
        }

        public ClearCacheState ClearCache()
        {
            return CacheProcessor.ClearCache();
        }

        #region Timer Events

        public void NeedForScreenshotEventFired()
        {
            ActivityProcessor.GetInstance().MakeScreenshot();
        }

        #endregion

        #region Screenshot, Mouse, Keyboard Events


        public void MouseActionFired(int timestamp) 
        {
            _LastActivityTime = timestamp;

            if (CurrentContext.GetInstance().IsSessionSleep)
            {
                _NotificationHandler.ActivityFired();
            }
            else
            {
                int delta = TimingHelper.CalcTimestampDelta(CurrentContext
                    .GetInstance().Session.StartTimestamp, timestamp);
                if (!CurrentContext.GetInstance().Session.MouseTimes.Contains(delta))
                    CurrentContext.GetInstance().Session.MouseTimes.Add(delta);
            }
        }

        public void KeyboardActionFired(int timestamp)
        {
            _LastActivityTime = timestamp;

            if (CurrentContext.GetInstance().IsSessionSleep)
            {
                _NotificationHandler.ActivityFired();
            }
            else
            {
                int delta = TimingHelper.CalcTimestampDelta(CurrentContext
                .GetInstance().Session.StartTimestamp, timestamp);
                if (!CurrentContext.GetInstance().Session.KeyboardTimes.Contains(delta))
                    CurrentContext.GetInstance().Session.KeyboardTimes.Add(delta);
            }
        }

        public void ScreenshotActionFired(Image img, int timestamp)
        {
            _LastScreenshotTime = timestamp;
            _NotificationHandler.ScreenshotTaken();

            int delta = TimingHelper.CalcTimestampDelta(CurrentContext
                .GetInstance().Session.StartTimestamp, timestamp);

            CacheProcessor.SaveImage(img, delta.ToString(),
                CurrentContext.GetInstance().Session.StartTimestamp.ToString(), false);

            CurrentContext.GetInstance().Session.PostTime = delta;
            CurrentContext.GetInstance().Session.StopTime = -1;

            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("ScreenshotActionFired() -- mode 1: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

           // while (CurrentContext.GetInstance().IsSessionDataLocked)
           //     Thread.Sleep(1000);

            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("ScreenshotActionFired() -- mode 2: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

            //CurrentContext.GetInstance().IsSessionDataLocked = true;
            CacheStrategyExecutor.GetInstance().CacheStrategy.PostCurrentSession(CurrentContext
            .GetInstance().Session.StartTimestamp.ToString());

            CacheStrategyExecutor.GetInstance().CacheStrategy.PostOldSessions(CurrentContext
                .GetInstance().Session.StartTimestamp.ToString());
           // CurrentContext.GetInstance().IsSessionDataLocked = false;

            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("ScreenshotActionFired() -- mode 3: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/
        }

        public void CheckConnectionEventFired()
        {
            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("CheckConnectionEventFired() -- mode 1: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

            //while (CurrentContext.GetInstance().IsSessionDataLocked)
            //    Thread.Sleep(1000);

            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("CheckConnectionEventFired() -- mode 2: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/

           // CurrentContext.GetInstance().IsSessionDataLocked = true;
            CacheStrategyExecutor.GetInstance().CacheStrategy.PostOldSessions(string.Empty);
           // CurrentContext.GetInstance().IsSessionDataLocked = false;

            /*LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetSessionLine("CheckConnectionEventFired() -- mode 3: IsSessionDataLocked: " +
                            CurrentContext.GetInstance().IsSessionDataLocked.ToString()));*/
        }

        public void ConnectionStateChanged(bool connectionExist)
        {
            _NotificationHandler.ConnectionStateChanged(connectionExist);
        }

        #endregion
    }
}
