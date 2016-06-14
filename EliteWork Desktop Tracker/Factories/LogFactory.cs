using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Model.Impl.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Factories
{
    class LogFactory
    {
        public static ILogData GetLogData(LogDataType type)
        {
            if (type == LogDataType.FILE)
                return new FileLogData();
            else if (type == LogDataType.IN_MEMORY)
                return new InMemoryLogData();
            else
                return null;
        }
    }
}
