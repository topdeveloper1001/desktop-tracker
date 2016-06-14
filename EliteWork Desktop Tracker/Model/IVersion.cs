using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model
{
    abstract class IVersion
    {
        private double _CurrentVersion;
        public double CurrentVersion { get { return _CurrentVersion; } set { _CurrentVersion = value; } }

        private double _NewestVersion;
        public double NewestVersion { get { return _NewestVersion; } set { _NewestVersion = value; } }

        private int _MandatoryUpdate;
        public int MandatoryUpdate { get { return _MandatoryUpdate; } set { _MandatoryUpdate = value; } }

        private bool _VersionDetected;
        public bool VersionDetected { get { return _VersionDetected; } set { _VersionDetected = value; } }

        private VersionState _State;
        public VersionState State { get { return _State; } set { _State = value;  }  }

        private string _ButtonText;
        public string ButtonText { get { return _ButtonText; } set { _ButtonText = value; } }
    }
}
