using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model
{
    abstract class ISession
    {
        protected int _StartTimestamp;
        public int StartTimestamp { get { return _StartTimestamp; } set { _StartTimestamp = value; } }

        protected int _StopTime;
        public int StopTime { get { return _StopTime; } set { _StopTime = value; } }

        protected int _PostTime;
        public int PostTime { get { return _PostTime; } set { _PostTime = value; } }

        protected List<int> _ScreenshotFileNames;
        public List<int> ScreenshotFileNames
        {
            get { return _ScreenshotFileNames; }
            set { _ScreenshotFileNames = value; }
        }

        protected List<int> _KeyboardTimes;
        public List<int> KeyboardTimes { get { return _KeyboardTimes; } set { _KeyboardTimes = value; } }

        protected List<int> _MouseTimes;
        public List<int> MouseTimes { get { return _MouseTimes; } set { _MouseTimes = value; } }
    }
}
