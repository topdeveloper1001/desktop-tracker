using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.ServerApi
{
    interface ILogFormatProvider
    {
        string GetLoginFailedLine(string reason);
        string GetLoginSuccessLine();
        string GetNavigationLine(string page);
        string GetSettingsLine(string action);
        string GetSessionLine(string condition);
        string GetNetworkLine(string condition);
        string GetCrashReportLine(string condition);
    }
}
