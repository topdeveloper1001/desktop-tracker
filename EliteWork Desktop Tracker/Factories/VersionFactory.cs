using Common;
using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Model.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Factories
{
    class VersionFactory
    {
        public static IVersion CreateVersion()
        {
            IVersion versionData = new DefaultVersion(); // SIMPLEST VARIANT NOW
            versionData.CurrentVersion = CommonConst.CURRENT_VERSION;
            return versionData;
        }
    }
}
