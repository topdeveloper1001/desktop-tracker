using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model
{
    abstract class ILogData
    {
        public abstract void WriteText(string text);
        public abstract void WriteLine(string line);
        public abstract string GetText();
    }
}
