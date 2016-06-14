using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.ServerApi.Impl
{
    class DefaultLogFormatProvider : ILogFormatProvider
    {
        public string GetLoginFailedLine(string reason)
        {
            return string.Format("<{0} - {1}>: LOGIN_FAILED, {2}", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), reason);
        }

        public string GetLoginSuccessLine()
        {
            return string.Format("<{0} - {1}>: LOGIN_SUCCESS", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime());
        }

        public string GetNavigationLine(string page)
        {
            return string.Format("<{0} - {1}>: NAVIGATION, {2}", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), page);
        }

        public string GetSettingsLine(string action)
        {
            return string.Format("<{0} - {1}>: SETTING, {2}", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), action);
        }

        public string GetSessionLine(string condition)
        {
            return string.Format("<{0} - {1}>: SESSION: {2}", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), condition);
        }

        public string GetNetworkLine(string condition)
        {
            return string.Format("<{0} - {1}>: NETWORK: {2}", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), condition);
        }

        public string GetCrashReportLine(string condition)
        {
            return string.Format("<{0} - {1}>: SYSTEM: Application Crashed{2}<{0} - {1}>##### CRASH LOG #####{2}<{0} - {1}>{3}{2}<{0} - {1}>##### CRASH LOG #####", TimingHelper.GetCurrentDate(), TimingHelper.GetCurrentTime(), Environment.NewLine, condition);
        }
    }
}
