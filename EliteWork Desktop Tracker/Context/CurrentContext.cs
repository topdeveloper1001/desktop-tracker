using EliteWork_Desktop_Tracker.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Context
{
    class CurrentContext
    {
        private static CurrentContext Context = null;

        private State _CurrentState = State.UNKNOWN;
        public State CurrentState { get { return _CurrentState; } set { _CurrentState = value; } }

        private ISession _Session = null;
        public ISession Session { get { return _Session; } set { _Session = value; } }

        private ILoginData _LoginData = null;
        public ILoginData LoginData { get { return _LoginData; } set { _LoginData = value; } }

        private IVersion _VersionData = null;
        public IVersion VersionData { get { return _VersionData; } set { _VersionData = value; } }

        private bool _IsSessionDataLocked = false;
        public bool IsSessionDataLocked
        {
            get { lock (this) { return _IsSessionDataLocked; } }
            set { lock (this) { _IsSessionDataLocked = value; } }
        }

        private bool _IsLogDataLocked = false;
        public bool IsLogDataLocked
        {
            get { lock (this) { return _IsLogDataLocked; } }
            set { lock (this) { _IsLogDataLocked = value; } }
        }

        private bool _IsSessionSleep = false;
        public bool IsSessionSleep {
            get { lock (this) { return _IsSessionSleep; } }
            set { lock (this) { _IsSessionSleep = value; } } }

        private CurrentContext() { }

        public static CurrentContext GetInstance()
        {
            if (Context == null)
                Context = new CurrentContext();

            return Context;
        }
    }
}
