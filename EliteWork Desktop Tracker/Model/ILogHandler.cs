using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model
{
    interface ILogHandler
    {
        void AppendLine(string line);
    }
}
