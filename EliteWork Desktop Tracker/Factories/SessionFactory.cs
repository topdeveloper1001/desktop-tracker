using Common;
using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Model.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Factories
{
    class SessionFactory
    {
        public static ISession CreateSession()
        {
            ISession session = new DefaultSession(); // JUST SIMPLEST FACTORY NOW
            session.StartTimestamp = TimingHelper.GetCurrentTimestamp();
            session.KeyboardTimes = new List<int>();
            session.MouseTimes = new List<int>();
            session.ScreenshotFileNames = new List<int>();
            return session;
        }
    }
}
